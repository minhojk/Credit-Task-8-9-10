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
    public partial class Form2 : Form
    {
        String Conn = ConfigurationManager.ConnectionStrings["WapDB"].ConnectionString;
        public Form2()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index < 0 || index >= dataGridView1.RowCount)
                return;
            try
            {
                DataGridViewRow row = dataGridView1.Rows[index];
                String ItemID = Convert.ToString(row.Cells[0].Value);
                String ItemName = Convert.ToString(row.Cells[1].Value);
                String ItemSize = Convert.ToString(row.Cells[2].Value);

                txtItemID.Text = ItemID;
                txtItemName.Text = ItemName;
                txtItemSize.Text = ItemSize;

            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM Item";

            DataTable dataTable1 = new DataTable();

            using (SqlConnection connection = new SqlConnection(Conn))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql1, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable1);
                    }
                }
            }

            dataGridView1.DataSource = dataTable1;

            string sql2 = "SELECT * FROM Agent";

            DataTable dataTable2 = new DataTable();

            using (SqlConnection connection = new SqlConnection(Conn))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql2, connection))
                {

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable2);
                    }
                }
            }
            dataGridView2.DataSource = dataTable2;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index < 0 || index >= dataGridView1.RowCount)
                return;
            try
            {
                DataGridViewRow row = dataGridView1.Rows[index];
                String AgentID = Convert.ToString(row.Cells[0].Value);
                String AgentName = Convert.ToString(row.Cells[1].Value);
                String Address = Convert.ToString(row.Cells[2].Value);

                txtAgentID.Text = AgentID;
                txtAgentName.Text = AgentName;
                txtAddress.Text = Address;

            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(Conn);
            conn.Open();
            String sSQL = "INSERT INTO Item (ItemID,ItemName,Size) VALUES(@IID,@IName,@ISize)";
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.Add(new SqlParameter("@IID", txtItemID.Text));
            cmd.Parameters.Add(new SqlParameter("@IName", txtItemName.Text));
            cmd.Parameters.Add(new SqlParameter("@ISize", txtItemSize.Text));
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
            MessageBox.Show("Added");
            dataGridView1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(Conn);
            conn.Open();
            String sSQL = "INSERT INTO Agent (AgentID,AgentName,Address) VALUES(@AID,@AName,@Place)";
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.Add(new SqlParameter("@AID", txtAgentID.Text));
            cmd.Parameters.Add(new SqlParameter("@AName", txtAgentName.Text));
            cmd.Parameters.Add(new SqlParameter("@Place", txtAddress.Text));
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
            MessageBox.Show("Added");
            dataGridView1.Refresh();
        }
    }

}
