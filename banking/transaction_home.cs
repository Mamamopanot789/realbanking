using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using banking.model;

namespace banking
{
    public partial class transaction_home : Form
    {
        private Account currentAccount;
        private TransactionRepository transactionRepository = new TransactionRepository();

        public transaction_home(Account currentAccount)
        {
            InitializeComponent();
            this.currentAccount = currentAccount;
            //Console.WriteLine(
            //currentAccount.ToString());
            label3.Text = currentAccount != null ? currentAccount.FullName() : null ;
            label8.Text = transactionRepository.GetTotalMoneyByAccountId(currentAccount.AccountId).ToString("F2");
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
            transactionhistory transactionhistory = new transactionhistory(currentAccount);
            transactionhistory.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Deposit deposit = new Deposit(currentAccount);
            deposit.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            withdraw withdraw = new withdraw(currentAccount);
            withdraw.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sendtoanotheracc sendtoanotheracc = new sendtoanotheracc(currentAccount);
            sendtoanotheracc.Show();
            this.Close();   
        }
    }
}
