using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EWallet
{
    internal class User
    {

        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Id { get; set; }

        public static bool IsAuthenticated { get; set; }

        public double money { get; set; }

        public static User CurrentUser { get; set; } // Static property for current user

        public double CalculateUserMoney()
        {
            double totalAmount = 0.0;

            using (MySqlConnection connection = DB.Connection())
            {

                string commandText = "SELECT amount, type FROM Transaction WHERE user = @userId";
                using (MySqlCommand command = new MySqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@userId", CurrentUser.Id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            double amount = reader.GetDouble(0);
                            int type = reader.GetInt32(1);

                            if (type == 1) // Sent (negative)
                            {
                                totalAmount -= amount;
                            }
                            else if (type == 2) // Received (positive)
                            {
                                totalAmount += amount;
                            }
                            else if (type == 4) // Payout (negative)
                            {
                                totalAmount -= amount;
                            }
                            else if (type == 3) // Earned (positive)
                            {
                                totalAmount += amount;
                            }
                            // Handle other potential types if needed
                        }
                    }
                }
            }
            CurrentUser.money = totalAmount; 
            return totalAmount;
        }
    }
}
