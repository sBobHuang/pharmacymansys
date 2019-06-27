﻿using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pharmacymansys
{
    public partial class NewGrp : Form
    {
        public NewGrp()
        {
            InitializeComponent();
        }

        static string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.213.70)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=Orcl)));Persist Security Info=True;User ID=scott;Password=tiger;";
        OracleConnection conn = new OracleConnection(connectionString);

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.textBox1.Text))
                {
                    MessageBox.Show("Fill the Group name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    OracleCommand cmd = new OracleCommand("INSERT INTO MED_GROUP(GNAME,GDESCRIPTION,NOTES) VALUES('" + textBox1.Text + "','"+richTextBox1.Text+"','"+richTextBox2.Text+"')", conn);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Information Uploaded...");
                        this.Close();
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            conn.Close();
            
        }
    }
}
