using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharmacymansys
{
    public partial class AdminSetting : Form
    {
        static string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.213.70)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=Orcl)));Persist Security Info=True;User ID=scott;Password=tiger;";
        OracleConnection conn = new OracleConnection(connectionString);

        

        public AdminSetting()
        {
            InitializeComponent();

            label4.Text = LoginName.name;
            
            
        }

        string name = LoginName.name;
        

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox3.Text) || string.IsNullOrWhiteSpace(this.textBox4.Text) || string.IsNullOrWhiteSpace(this.textBox5.Text))
            {
                if (string.IsNullOrWhiteSpace(this.textBox3.Text))
                {
                    MessageBox.Show("Enter your current password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (string.IsNullOrWhiteSpace(this.textBox4.Text))
                {
                    MessageBox.Show("Enter new Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Enter Confirm Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else if(textBox4.Text!=textBox5.Text)
            {
                MessageBox.Show("confirm password is not matching with new passsword");
                textBox5.Focus();
                return;
            }
            else
            {
                try
                {
                    conn.Open();
                    // String admin_username = textBox1.Text;
                    // String pass_word = textBox2.Text;

                    OracleCommand cmd = new OracleCommand("SELECT * FROM ADMIN WHERE ADMIN_USERNAME='" + name + "'", conn);
                    OracleDataReader r = cmd.ExecuteReader();
                    if (r.HasRows)
                    {

                        // MessageBox.Show();
                        cmd = new OracleCommand("SELECT PASSWORD FROM ADMIN WHERE ADMIN_USERNAME='" + name + "'", conn);
                        r = cmd.ExecuteReader();
                        r.Read();
                        // MessageBox.Show(r.GetString(0));
                        if (r.GetValue(0).ToString() == textBox3.Text)
                        {

                            //  MessageBox.Show("wELCOM");
                            try
                            {
                                // conn.Open();
                                /* OracleCommand*/
                                cmd = new OracleCommand("UPDATE ADMIN SET PASSWORD='" + textBox4.Text + "' WHERE ADMIN_USERNAME='" + name + "'", conn);
                                cmd.ExecuteNonQuery();
                                conn.Close();

                                r.Close();
                                MessageBox.Show("Your password has been changed.", "Success", MessageBoxButtons.OK);
                             //   LoginName.name = textBox1.Text;
                                this.Refresh();
                                // Application.Doevents();
                                this.Close();
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show(exc.Message);
                            }

                        }

                    }
                    else
                    {
                        MessageBox.Show("Invalid User Name Or Password", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    r.Close();
                    conn.Close();

                }

                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox1.Text) || string.IsNullOrWhiteSpace(this.textBox2.Text))
            {
                if (string.IsNullOrWhiteSpace(this.textBox1.Text))
                {
                    MessageBox.Show("Enter User Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Enter Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand("SELECT * FROM ADMIN WHERE ADMIN_USERNAME='" + name + "' AND PASSWORD='"+textBox2.Text+"'", conn);
                OracleDataReader r = cmd.ExecuteReader();
                
                if (r.HasRows)
                {
                    try
                    {
                       // conn.Open();
                       /* OracleCommand*/ cmd = new OracleCommand("UPDATE ADMIN SET ADMIN_USERNAME='" + textBox1.Text + "' WHERE ADMIN_USERNAME='" + name + "'", conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        
                        r.Close();
                        MessageBox.Show("Your user name has been changed to "+textBox1.Text+"", "Success", MessageBoxButtons.OK);
                        LoginName.name = textBox1.Text;
                        this.Refresh();
                       // Application.Doevents();
                        this.Close();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }
                }
            }
            
        }

        private void AdminSetting_Load(object sender, EventArgs e)
        {

        }
    }
}
