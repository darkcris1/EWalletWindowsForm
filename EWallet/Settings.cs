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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EWallet
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dashboard home = new Dashboard();
            home.Show(this);
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox3.Text;
            string fullName = textBox2.Text;
            string password = textBox1.Text;
            if (username == "" || fullName == "" || password == "")
            {
                MessageBox.Show("All inputs should be valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            this.UpdateUser(User.CurrentUser.Id.ToString(), username, fullName, password);
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            string username = textBox3.Text = User.CurrentUser.Username;
            string fullName = textBox2.Text = User.CurrentUser.Fullname;
        }
        public bool UpdateUser(string userId, string newUsername, string newFullName, string newPassword)
        {
            using (MySqlConnection connection = DB.Connection())
            {

                // Check for existing username excluding current user
                string checkUsernameCommandText = "SELECT COUNT(*) FROM users WHERE username = @newUsername AND id <> @userId";
                using (MySqlCommand checkUsernameCommand = new MySqlCommand(checkUsernameCommandText, connection))
                {
                    checkUsernameCommand.Parameters.AddWithValue("@newUsername", newUsername);
                    checkUsernameCommand.Parameters.AddWithValue("@userId", userId);

                    int usernameCount = Convert.ToInt32(checkUsernameCommand.ExecuteScalar());

                    if (usernameCount > 0)
                    {
                        MessageBox.Show("Username already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false; // Username already exists
                    }
                }

                // Username is available, proceed with update
                string updateCommandText = "UPDATE users SET username = @newUsername, full_name = @newFullName, password = @newPassword WHERE id = @userId";
                using (MySqlCommand updateCommand = new MySqlCommand(updateCommandText, DB.Connection()))
                {
                    updateCommand.Parameters.AddWithValue("@newUsername", newUsername);
                    updateCommand.Parameters.AddWithValue("@newFullName", newFullName);
                    updateCommand.Parameters.AddWithValue("@newPassword", newPassword); // Assuming password hashing before storing
                    updateCommand.Parameters.AddWithValue("@userId", userId);

                    updateCommand.ExecuteNonQuery();
                }
                MessageBox.Show("Update Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                User.CurrentUser.Fullname = newFullName;
                User.CurrentUser.Username = newUsername;

                return true; // Update successful
            }
        }
    }
}
