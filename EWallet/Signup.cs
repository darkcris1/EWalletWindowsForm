using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EWallet
{
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = DB.Connection())
            {
                try
                {

                    string fullname = textBox3.Text;
                    string password = textBox2.Text;
                    string username = textBox1.Text;

                    if (fullname == "")
                    {
                        throw new Exception("Enter a valid full name");
                    }
                    if (username == "")
                    {
                        throw new Exception("Enter a valid password");
                    }
                    if (password == "" || password.Length < 5) 
                    {
                        throw new Exception("Enter a valid password and must be atleast 5 characters");
                    }
                    

                    // **Hash the password for security**
                    // Use a secure hashing algorithm like bcrypt or Argon2
                    string hashedPassword = password; // Example using a BCrypt library

                    string query = "INSERT INTO users (username, password, full_name) VALUES (@username, @password, @fullname)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", hashedPassword);
                        command.Parameters.AddWithValue("@fullname", fullname);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Signup successful!. Proceed to login");
                            new Login().Show();
                            this.Hide();
                            // Redirect to login or other page
                        }
                        else
                        {
                            throw new Exception("Signup failed. Please try again.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show(this);
            this.Hide();
        }
    }
}
