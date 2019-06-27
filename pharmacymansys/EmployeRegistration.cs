
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Oracle.ManagedDataAccess.Client;

namespace pharmacymansys
{
    public partial class EmployeRegistration : Form
    {
        public EmployeRegistration()
        {
            InitializeComponent();
        }

        static string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.213.70)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=Orcl)));Persist Security Info=True;User ID=scott;Password=tiger;";
        OracleConnection conn = new OracleConnection(connectionString);
       
        string picpath=null;
        byte[] imageBt = null;
        string nm;
        string dob ;
        string bdg ;
        string eml;
        string mbn;
        string nid;
        string jod;
        string madd;
        string padd;
        string radio_value;
        string position;
        string salary;
        string unm;
        string pass;
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime t1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            //获取dateTimePicker1修改后的日期
            DateTime t2 = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day);
            //计算两个日期相差天数
            TimeSpan ts = t2.Subtract(t1);
            //当天数大于等于0的时候，即你选择的日期是系统日期之后，将dateTimePicker1置为不可用
            if (ts.Days > 0)
            {
                MessageBox.Show("入职日期在今日之后", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int radio = 0;
            if (radioButton1.Checked == true)
            {
                radio = 1;
            }
            else if (radioButton2.Checked == true)
            {

                radio = 2;
            }
            else
            {
                radio = 0;
            }

            if (string.IsNullOrWhiteSpace(this.textBox1.Text) || string.IsNullOrWhiteSpace(this.textBox5.Text) || string.IsNullOrWhiteSpace(this.textBox6.Text) || string.IsNullOrWhiteSpace(this.richTextBox2.Text)|| (radio==0))
            {
                MessageBox.Show("请填入必填信息", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                
                if (radioButton1.Checked == true)
                {
                    radio_value =radioButton1.Text;
                   
                }
                else 
                {

                    radio_value = radioButton2.Text;
                }

                nm = textBox1.Text;
                dob = dateTimePicker1.Value.ToString("yyyyMMdd");
                bdg = comboBox1.GetItemText(this.comboBox1.SelectedItem);
                eml = textBox4.Text;
                mbn = textBox5.Text;
                nid = textBox6.Text;
                jod = dateTimePicker2.Value.ToString("yyyyMMdd");
                madd = richTextBox1.Text;
                padd = richTextBox2.Text;


                /////////////上传照片///////////////////

                 if (this.textBox7.Text == "")
                 {
                     MessageBox.Show("You didn't select any image", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 }
                 else 
                 {
                     FileStream fstream = new FileStream(this.textBox7.Text, FileMode.Open, FileAccess.Read);
                     BinaryReader br = new BinaryReader(fstream);
                     imageBt = br.ReadBytes((int)fstream.Length);
                     tabControl1.SelectTab("tabPage3");
                 }
          
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            
        }

        private void EmployeRegistration_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 0;
            DateTime t1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            this.dateTimePicker1.Text=t1.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif|All files (*.*)|*.*";
            if(dlg.ShowDialog()==DialogResult.OK)
            {
                picpath = dlg.FileName.ToString();
                textBox7.Text = picpath;
                pictureBox1.ImageLocation = picpath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.SelectedIndex == -1 || string.IsNullOrWhiteSpace(this.textBox8.Text) || string.IsNullOrWhiteSpace(this.textBox9.Text) || string.IsNullOrWhiteSpace(this.textBox10.Text) || string.IsNullOrWhiteSpace(this.textBox11.Text))
                {
                    MessageBox.Show("请填完所有信息", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else 
                {
                    if (textBox10.Text != textBox11.Text)
                    {
                        MessageBox.Show("两次输入密码不一致", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        position = comboBox2.GetItemText(this.comboBox2.SelectedItem);
                        salary = textBox8.Text;
                        unm = textBox9.Text;
                        pass = textBox10.Text;
                        conn.Open();
                        OracleParameter parm = new OracleParameter();
                        parm.OracleDbType = OracleDbType.Blob;
                        parm.Value = imageBt;
                        OracleCommand cmd = new OracleCommand("INSERT INTO EMPLOYE_INFO(EMP_NAME,DOB,BLOOD,EMAIL,MOBILE,GENDER,NID,MAIL_ADD,PAR_ADD,JOIN_DATE,IMAGE,POSITION,SALARY) VALUES('" + nm+"',TO_DATE('" + dob + "', 'YYYYMMDD'), '" + bdg + "','" + eml + "','" + mbn + "','" + radio_value + "','" + nid + "','" + madd + "','" + padd + "',TO_DATE('" + jod + "', 'YYYYMMDD'),:1,'" + position + "','" + salary + "')", conn);

                        cmd.Parameters.Add(parm);


                         cmd.ExecuteNonQuery();
                     //   if (ni > 0)
                      //  {
                     //       MessageBox.Show("Information Uploaded...");
                     //   }
                        // cmd.Dispose();


                         if (position == "Manager")
                         {
                             cmd = new OracleCommand("INSERT INTO MANAGER_LOGIN(E_ID,U_NAME,PASSWORD) VALUES((SELECT MAX(EMP_ID) FROM EMPLOYE_INFO),'" + unm + "','" + pass + "')", conn);
                             int i = cmd.ExecuteNonQuery();
                             if (i > 0)
                             {
                                 MessageBox.Show("信息已更新");
                             }
                         }
                         else 
                         {
                             cmd = new OracleCommand("INSERT INTO STUFF_LOGIN(E_ID,U_NAME,PASSWORD) VALUES((SELECT MAX(EMP_ID) FROM EMPLOYE_INFO),'" + unm + "','" + pass + "')", conn);
                             int i = cmd.ExecuteNonQuery();
                             if (i > 0)
                             {
                                 MessageBox.Show("信息已更新");
                                 adminControls ad = new adminControls();
                                 this.Hide();
                                 ad.Show();
                             }
                         }
                    }
                    
                }
              
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            conn.Close();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '+') && (e.KeyChar != '-'))
            {
                e.Handled = true;
               // Console.Beep(5000, 1000);
                MessageBox.Show("请勿输入无效字符");
            }
        }

        private void EmployeRegistration_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                // Console.Beep(5000, 1000);
                MessageBox.Show("请勿输入无效字符");
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                // Console.Beep(5000, 1000);
                MessageBox.Show("请勿输入无效字符");
            }
        }
       
    }
}
