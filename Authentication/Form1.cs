using System.Data.SqlClient;

namespace Authentication
{
    public partial class Form1 : Form
    {

        private const string connectionString = "Data Source=COM-1214-3196;Initial Catalog=testDB;Integrated Security=True";
        private const string insertQuery = "INSERT INTO demoUser (username, password, confirmPassword) VALUES (@username, @password, @confirmPassword)";

        public Form1()
        {
            InitializeComponent();
        }

        public void ClearAllTextBox()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            //checkBox1.Checked = false;
        }
        public void SplitWindowSignin()
        {
            Singin window = new Singin();
            window.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = Convert.ToString(textBox1.Text);
            string password = Convert.ToString(textBox2.Text);
            string confirmPassword = Convert.ToString(textBox3.Text);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@confirmPassword", confirmPassword);

                    if(string.IsNullOrEmpty(username))
                    {
                        MessageBox.Show("You have must enter Username!");
                    }
                    else if(string.IsNullOrEmpty(password))
                    {
                        MessageBox.Show("You have must enter Password!");
                    }
                    else if(string.IsNullOrEmpty(confirmPassword))
                    {
                        MessageBox.Show("You have must enter Confirm Password!");
                    }
                    else if(password == confirmPassword)
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("Welcome " + username);
                        //ClearAllTextBox();
                        SplitWindowSignin();
                    }
                    else
                    {
                        MessageBox.Show("Password & Confirm Password must be same!");
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
                textBox3.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
                textBox3.UseSystemPasswordChar = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SplitWindowSignin();
        }
    }
}