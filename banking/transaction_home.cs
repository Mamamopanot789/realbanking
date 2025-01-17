using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace banking
{
    public partial class transaction_home : Form
    {
        public transaction_home()
        {
            InitializeComponent();
        }

        private void transaction_home_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            transactionhistory transactionhistory = new transactionhistory();
            transactionhistory.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Deposit deposit = new Deposit();
            deposit.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            withdraw withdraw = new withdraw();
            withdraw.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sendtoanotheracc sendtoanotheracc = new sendtoanotheracc();
            sendtoanotheracc.Show();
            this.Close();   
        }
    }
}
