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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EarnMoney home = new EarnMoney();
            home.Show(this);
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show(this);
            this.Hide();

            User.IsAuthenticated = false;
            User.CurrentUser = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendMoney home = new SendMoney();
            home.Show(this);
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TransactionHistory home = new TransactionHistory();
            home.Show(this);
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Settings home = new Settings();
            home.Show(this);
            this.Hide();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            this.RefreshMoney();
            label7.Text = " Hi " + User.CurrentUser.Fullname + "!";
        }

        private void RefreshMoney()
        {
            User.CurrentUser.CalculateUserMoney();
            label6.Text = "₱ " + User.CurrentUser.money.ToString("N2");
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.RefreshMoney();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Dashboard_Load_1(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Payout Payout = new Payout();
            Payout.Show();
        }
    }
}
