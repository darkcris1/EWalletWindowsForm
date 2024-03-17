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
    public partial class SendMoney : Form
    {
        public SendMoney()
        {
            InitializeComponent();
        }

        private void Ml3S9fa_Click(object sender, EventArgs e)
        {

        }

        private void SendMoney_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.SendMoney2(textBox2, User.CurrentUser.Username, this.textBox1.Text, 1);
        }

        public void SendMoney2(TextBox amountTextBox, string username, string receiverUsername, int type)
        {
            using (MySqlConnection connection = DB.Connection())
            {
                // Start a transaction
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Parse amount from text field
                        double amount = double.Parse(amountTextBox.Text);
                        MessageBox.Show(User.CurrentUser.Username);
                        MessageBox.Show(username);

                        if (User.CurrentUser.Username == receiverUsername)
                        {
                            throw new Exception("Cannot send to yourself");
                        }

                        if (User.CurrentUser.money < amount)
                        {
                            throw new Exception("Invalid amount: " + amount.ToString());
                        }

                        
                        // Get sender ID based on username (assuming a separate method)
                        int senderId = GetUserIdByUsername(username, transaction);

                        // Get receiver ID based on username (assuming a separate method)
                        int receiverId = GetUserIdByUsername(receiverUsername, transaction);

                        // Generate a unique reference number (implement your generation logic)
                        string referenceNumber = GenerateReferenceNumber();

                        // Insert sender transaction
                        InsertTransaction(senderId, receiverId, amount, type, referenceNumber, transaction);

                        // Insert receiver transaction (with opposite type)
                        int oppositeType = GetOppositeTransactionType(type);
                        InsertTransaction(receiverId, senderId, amount, oppositeType, referenceNumber, transaction);
                        MessageBox.Show("Money sent successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        transaction.Commit();

                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show("Invalid amount format. Please enter a number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        transaction.Rollback();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private int GetUserIdByUsername(string username, MySqlTransaction transaction)
        {
            string commandText = "SELECT id FROM users WHERE username = @username";
            using (MySqlCommand command = new MySqlCommand(commandText, DB.conn))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Transaction = transaction;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        return reader.GetInt32(0); // Assuming "id" is the first column (index 0)
                    }
                    else
                    {
                        throw new Exception("User not found with username: " + username);
                    }
                }
            }
        }

        private string GenerateReferenceNumber()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var randoms = new Random();
            return new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[randoms.Next(s.Length)]).ToArray());
        }

        private int GetOppositeTransactionType(int type)
        {
            if (type == 1) // Send
            {
                return 2; // Receive
            }
            else if (type == 2) // Receive
            {
                return 1; // Send
            }
            else
            {
                throw new ArgumentException("Invalid transaction type");
            }
        }

        private void InsertTransaction(int userId, int counterpartyId, double amount, int type, string referenceNumber, MySqlTransaction transaction)
        {
            MySqlConnection connection = DB.conn;
            string commandText = "INSERT INTO Transaction (user, amount, type, reference_number, created_at, updated_at) VALUES (@userId, @amount, @type, @referenceNumber, NOW(), NOW())";
            using (MySqlCommand command = new MySqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@amount", amount);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@referenceNumber", referenceNumber);
                command.Transaction = transaction;
                command.ExecuteNonQuery();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show(this);
            this.Hide();
        }
    }
}
