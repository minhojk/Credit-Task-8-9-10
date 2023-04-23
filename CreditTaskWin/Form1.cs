using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace CreditTaskWin
{
    public partial class Form1 : Form
    {
        string strConn = ConfigurationManager.ConnectionStrings["User"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            String sql = "SELECT UserName, Password FROM tblUser WHERE " +
                "UserName='" + txtUsername.Text + "'and Password='" + txtPassword.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Login Successful");
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Login");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           Application.Exit();
        }
    }
}
