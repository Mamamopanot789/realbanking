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
    public partial class Deposit : Form
    {
        private Account currentAccount;
        private readonly TransactionRepository transactionRepository = new TransactionRepository();

        public Deposit(Account currentAccount)
        {
            InitializeComponent();
            this.currentAccount = currentAccount;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            transaction_home transaction_Home = new transaction_home(currentAccount);
            transaction_Home.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            decimal amount = 0;
            
           amount =  decimal.Parse(textBox1.Text);

            if (amount < 0)
            {
                MessageBox.Show("The money should not be 0");
                return;
            }


            MessageBox.Show("Deposited Money");
            transactionRepository.Deposit(currentAccount.AccountId, amount);



            transaction_home transaction_Home = new transaction_home(currentAccount);
            transaction_Home.Show();
            this.Close();

            return;
        }
    }
}
