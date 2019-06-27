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
    public partial class managerControls : Form
    {
        public managerControls()
        {
            InitializeComponent();
            MedStoreLoad();
        }


        static string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=139.196.213.70)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=Orcl)));Persist Security Info=True;User ID=scott;Password=tiger;";
        OracleConnection conn = new OracleConnection(connectionString);
        DataTable dt;

        private void button3_Click(object sender, EventArgs e)
        {
            ViewMgf vm = new ViewMgf();
            vm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SellForm sf = new SellForm();
            sf.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MedView mv = new MedView();
            mv.Show();
        }


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
                dt.Columns[0].ColumnName = "编码";
                dt.Columns[1].ColumnName = "名字";
                dt.Columns[2].ColumnName = "数量";
                dt.Columns[3].ColumnName = "库存量";
                dt.Columns[4].ColumnName = "总量";

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

        private void button4_Click(object sender, EventArgs e)
        {
            NewGrp catl = new NewGrp();
            catl.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Store st = new Store();
            st.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            ViewStuff vs = new ViewStuff();
            vs.Show();
        }

        private void managerControls_Load(object sender, EventArgs e)
        {

        }
    }
}
