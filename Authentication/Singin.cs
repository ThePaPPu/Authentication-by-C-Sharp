using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Authentication
{
    public partial class Singin : Form
    {
        const string connectionString = "Data Source=COM-1214-3196;Initial Catalog=testDB;Integrated Security=True";
        public Singin()
        {
            InitializeComponent();
        }

        public void SplitWindowSingup()
        {
            Form1 window = new Form1();
            window.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SplitWindowSingup();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = Convert.ToString(textBox1.Text);
            string password = Convert.ToString(textBox2.Text);

            
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string sqlQuery = "SELECT COUNT(*) FROM demoUser WHERE username=@username AND password=@password";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@username", username);
            sqlCommand.Parameters.AddWithValue("@password", password);

            int count = (int)sqlCommand.ExecuteScalar();
            sqlConnection.Close();

            if (count > 0)
            {
                this.Hide();
                MessageBox.Show("Welcome " + username + ", You are login successfully.");   
            }
            else
            {
                MessageBox.Show("Username or Password are incorrect");
            }

        }
    }
}
