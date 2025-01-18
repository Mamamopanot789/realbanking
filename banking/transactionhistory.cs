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
    public partial class transactionhistory : Form
    {
        private Account currentAccount;
        private readonly TransactionRepository transactionRepository = new TransactionRepository(); 

        public transactionhistory(Account currentAccount)
        {
            InitializeComponent();
            this.currentAccount = currentAccount;
            dataGridView1.DataSource = transactionRepository.GetAllTransactionsByAccountId(currentAccount.AccountId);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            transaction_home transaction = new transaction_home(currentAccount);
            transaction.Show();
            this.Hide();
        }
    }
}
