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
    public partial class sendtoanotheracc : Form
    {
        private Account currentAccount;
        private readonly AccountRepository accountRepository = new AccountRepository();
        private readonly TransactionRepository transactionRepository = new TransactionRepository();


        public sendtoanotheracc(Account currentAccount)
        {
            InitializeComponent();
            this.currentAccount = currentAccount;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void sendtoanotheracc_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            decimal amount = 0;

            if (maskedTextBox1.Text.Trim().Length != 13)
            {
                MessageBox.Show("Pls Fill In The Account Number");
                return;
            }

            amount = decimal.Parse(textBox2.Text);

            if (amount < 0)
            {
                MessageBox.Show("The Amount Should Be Greater Than 0");
                return;

            }

            if (accountRepository.GetAccountByAccountNumber(currentAccount.AccountNumber) == null)
            {
                MessageBox.Show("There Is No Account Number");
                return;

            }
            decimal currentMoney = transactionRepository.GetTotalMoneyByAccountId(currentAccount.AccountId);

            if (currentMoney < amount)
            {
                MessageBox.Show("You Don't Have Enough Money");
                return;
            }

            transactionRepository.TransferMoney(currentAccount.AccountId, maskedTextBox1.Text, amount);
            MessageBox.Show("Transfer Have Been Successfull");
            transaction_home transaction_Home = new transaction_home(currentAccount);
            transaction_Home.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            transaction_home transaction_Home = new transaction_home(currentAccount);
            transaction_Home.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

           



        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("This Field Only Allow Numbers");
                return;
            }
        }
    }
}
