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
    public partial class Form5 : Form
    {
        String Conn = ConfigurationManager.ConnectionStrings["WapDB"].ConnectionString;
        public Form5()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT TOP 10 ItemID, SUM(Quantity) AS TotalQuantity " +
                               "FROM OrderDetail " +
                               "INNER JOIN Orders ON OrderDetail.OrderID = Orders.OrderID " +
                               "GROUP BY ItemID " +
                               "ORDER BY TotalQuantity DESC";
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "SELECT od.ItemID, COUNT(*) AS Purchases " +
                           "FROM OrderDetail od " +
                           "INNER JOIN Orders o ON od.OrderID = o.OrderID " +
                           "GROUP BY od.ItemID";
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
                conn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "SELECT o.OrderID, o.OrderDate, od.ItemID, od.Quantity, od.UnitAmount " +
                       "FROM Orders o " +
                       "INNER JOIN OrderDetail od ON o.OrderID = od.OrderID " +
                       "WHERE o.AgentID = @AgentID";
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //Input Agent ID, Example: AGT001
                    cmd.Parameters.AddWithValue("@AgentID", txtCustomerID.Text);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
                conn.Close();
            }
        }
    }
}
