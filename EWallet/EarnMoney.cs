using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace EWallet
{
    public partial class EarnMoney : Form
    {
        public EarnMoney()
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
            if (label7.Text == textBox1.Text)
            {
                InsertTransaction(int.Parse(User.CurrentUser.Id), 0.10, 3);
                MessageBox.Show("You earned ₱ 0.10", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string rando = GenerateRandomText();
                label7.Text = rando;

                textBox1.Text = "";
            }
            else
            {
                MessageBox.Show("Invalid text. Try Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string rando = GenerateRandomText();
                label7.Text = rando;
                textBox1.Text = "";
            }
        }

        private void InsertTransaction(int userId, double amount, int type)
        {
            MySqlConnection connection = DB.conn;
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

        private void EarnMoney_Load(object sender, EventArgs e)
        {
            string rando = this.GenerateRandomText();
            label7.Text = rando;

        }

        private string GenerateRandomText(int charLength = 7)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var randoms = new Random();
            string randomText = new string(Enumerable.Repeat(chars, charLength)
              .Select(s => s[randoms.Next(s.Length)]).ToArray());

            return randomText;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
