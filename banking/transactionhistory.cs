using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        private readonly AccountRepository accountRepository = new AccountRepository();

        public transactionhistory(Account currentAccount)
        {
            InitializeComponent();
            this.currentAccount = currentAccount;
            Insert_Data_DataSource(transactionRepository.GetAllTransactionsByAccountId(currentAccount.AccountId));
        }

        private void Insert_Data_DataSource (List<Transaction> transactions)
        {
            dataGridView1.Rows.Clear();

            if (transactions == null)
            {
                return;
            }

            transactions.ForEach(transaction => {
                dataGridView1.Rows.Add(
                    transaction.AccountId,
                    transaction.Amount.ToString("F2"),
                    accountRepository.GetAccountNumberByAccountId(transaction.ReceiverId),
                    transaction.TransactionType,
                    transaction.CreatedAt.ToString()
);
            });

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

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime startDate, endDate;

            startDate = dateTimePicker1.Value;
            endDate = dateTimePicker2.Value;

            if (startDate > endDate)
            {
                MessageBox.Show("The Start Date Can't Be Greater Than End Date");
                return;
            }

            List<Transaction> transactionList = transactionRepository.GetAllTransactionsBetweenStartDateAndEndDate(startDate, endDate, currentAccount.AccountId);

            Insert_Data_DataSource(transactionList);
        }
    }
}
