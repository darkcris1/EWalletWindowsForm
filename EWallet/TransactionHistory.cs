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
    public partial class TransactionHistory : Form
    {
        public TransactionHistory()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show(this);
            this.Hide();
        }

        private void TransactionHistory_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear(); // Clear existing columns (optional)
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Ref Num", "Ref Num");
            dataGridView1.Columns.Add("Amount", "Amount");

            dataGridView1.Columns.Add("Type", "Type");
            dataGridView1.Columns.Add("Created At", "Created At");
            this.LoadTransactions();
        }

        public void LoadTransactions()
        {
            using (MySqlConnection connection = DB.Connection())
            {

                string commandText = "SELECT id, reference_number, amount, type, created_at FROM Transaction WHERE user = @userId ORDER BY created_at DESC";
                using (MySqlCommand command = new MySqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@userId", User.CurrentUser.Id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        dataGridView1.Rows.Clear(); // Clear existing data

                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string refNum = reader.GetString(1);
                            double amount = reader.GetDouble(2);
                            int type = reader.GetInt32(3);
                            DateTime createdAt = reader.GetDateTime(4);

                            string typeString = GetTransactionTypeDisplay(type);

                            dataGridView1.Rows.Add(id, refNum, "₱ " + amount.ToString("N2"), typeString, createdAt.ToString("MMM dd yyyy h:mm tt"));
                        }
                    }
                }
            }
        }

        private string GetTransactionTypeDisplay(int type)
        {
            switch (type)
            {
                case 1:
                    return "Sent";
                case 2:
                    return "Received";
                case 3:
                    return "Earned";
                case 4:
                    return "Payout";
                default:
                    return "Unknown";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
