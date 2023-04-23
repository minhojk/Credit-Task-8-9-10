using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreditTaskWin
{
    public partial class Form3 : Form
    {
        String Conn = ConfigurationManager.ConnectionStrings["WapDB"].ConnectionString;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string query = "SELECT OrderID, AgentID FROM Orders";
            using (SqlConnection connection = new SqlConnection(Conn))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cbxOrder.Items.Add(new KeyValuePair<string, string>(reader["OrderID"].ToString(), reader["AgentID"].ToString()));
                }
            }
        }

        private void cbxOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            KeyValuePair<string, string> selectedAgent = (KeyValuePair<string, string>)cbxOrder.SelectedItem;
            string query = "SELECT ID, OrderID, ItemID, Quantity, UnitAmount FROM OrderDetail WHERE OrderID = @OrderID";
            using (SqlConnection connection = new SqlConnection(Conn))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@OrderID", selectedAgent.Key);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.Show();

            string query = "SELECT * FROM OrderDetail";
            using (SqlConnection connection = new SqlConnection(Conn))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                adapter.Fill(ds,"OrderDetail");
                CrystalReport cr = new CrystalReport();
                cr.SetDataSource(ds);
                f.crystalReportViewer1.ReportSource = cr;
                f.crystalReportViewer1.Refresh();
            }
        }
    }
}
