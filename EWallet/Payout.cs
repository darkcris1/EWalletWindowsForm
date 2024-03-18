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
    public partial class Payout : Form
    {
        public Payout()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show(this);
            this.Hide();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double amount = double.Parse(textBox1.Text);
                if (amount <= 0)
                {
                    throw new Exception("Enter an amount");
                }

                if (amount > User.CurrentUser.money)
                {
                    throw new Exception("Invalid amount");
                }

                InsertTransaction(int.Parse(User.CurrentUser.Id), amount, 4);
                MessageBox.Show($"You successfully withdraw ₱ {amount.ToString("N2")}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = "";
                this.RefreshMoney();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void InsertTransaction(int userId, double amount, int type)
        {
            string commandText = "INSERT INTO Transaction (user, amount, type, reference_number, created_at, updated_at) VALUES (@userId, @amount, @type, @referenceNumber, NOW(), NOW())";
            using (MySqlCommand command = new MySqlCommand(commandText, DB.Connection()))
            {
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@amount", amount);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@referenceNumber", this.GenerateRandomText());
                command.ExecuteNonQuery();
            }
        }

        private string GenerateRandomText(int charLength = 7)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var randoms = new Random();
            string randomText = new string(Enumerable.Repeat(chars, charLength)
              .Select(s => s[randoms.Next(s.Length)]).ToArray());

            return randomText;
        }

        private void Payout_Load(object sender, EventArgs e)
        {
            this.RefreshMoney();
        }

        private void RefreshMoney()
        {
            User.CurrentUser.CalculateUserMoney();
            label9.Text = $"₱ {User.CurrentUser.money.ToString("N0")}";
        }
    }
}
