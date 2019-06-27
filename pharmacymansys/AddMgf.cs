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
    public partial class AddMgf : Form
    {
        public AddMgf()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Utilities.ResetAllControls(this);
        }

        static string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.213.70)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=Orcl)));Persist Security Info=True;User ID=scott;Password=tiger;";
        OracleConnection conn = new OracleConnection(connectionString);

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox1.Text) || string.IsNullOrWhiteSpace(this.textBox2.Text) || string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                MessageBox.Show("请将信息填写正确", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    conn.Open();
                    string sqlquery = "INSERT INTO MFG_INFO(MGF_NAME,MFG_ADD,MFG_PHN) VALUES('" + textBox1.Text + "','" + richTextBox1.Text + "','" + textBox2.Text + "')";
                    MessageBox.Show(sqlquery);
                    OracleCommand cmd = new OracleCommand(sqlquery,conn);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        cmd.Dispose();
                        this.Hide();
                        adminControls adc = new adminControls();
                        adc.Show();
                    }
                }
                catch (Exception exe)
                {
                    MessageBox.Show(exe.Message);
                }
            }
        }

        private void AddMgf_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }

    }
}
