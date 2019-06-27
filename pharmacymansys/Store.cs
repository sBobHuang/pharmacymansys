using Oracle.ManagedDataAccess.Client;
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
    public partial class Store : Form
    {
        public Store()
        {
            InitializeComponent();
            MedStoreLoad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PurchaseForm pf = new PurchaseForm();
            pf.Show();
        }



        static string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.213.70)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=Orcl)));Persist Security Info=True;User ID=scott;Password=tiger;";
        OracleConnection conn = new OracleConnection(connectionString);
        DataTable dt;
        void MedStoreLoad()
        {
            try
            {
                conn.Open();
                string sqlquery = "SELECT * FROM MED_STORE";
                //string sqlquery = "SELECT * FROM MED_INFO";
                OracleCommand cmd = new OracleCommand(sqlquery, conn);
                OracleDataAdapter oda = new OracleDataAdapter();
                oda.SelectCommand = cmd;
                dt = new DataTable();
                //     dt.Columns["MED_NAME"].ColumnName = "Name";

                oda.Fill(dt);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.DataSource = bsource;
                oda.Update(dt);
                dt.Columns[0].ColumnName = "编号";
                dt.Columns[1].ColumnName = "名称";
                dt.Columns[2].ColumnName = "总量";
                dt.Columns[3].ColumnName = "库存量";
                dt.Columns[4].ColumnName = "销售量";
               
                dt.AcceptChanges();

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = string.Format("Name LIKE '%{0}%'", textBox1.Text);
            dataGridView1.DataSource = dv;
        }

        private void Store_Load(object sender, EventArgs e)
        {

        }

        private void Store_FormClosing(object sender, FormClosingEventArgs e)
        {
            adminControls adm = new adminControls();
            adm.Show();
        }








    }
}
