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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace banking
{
    public partial class Adminpanel : Form
    {
        private readonly AccountRepository accountRepository = new AccountRepository();
        private List<Account> accountList;

        public Adminpanel()
        {
            InitializeComponent();
            accountList = accountRepository.GetAllAccount();
            InsertDataGrid(accountList);
        }

        private void InsertDataGrid (List<Account> accounts)
        {
            dataGridView1.Rows.Clear();

            accounts.ForEach(account =>
            {
                dataGridView1.Rows.Add(
                    account.AccountId,
                    account.AccountNumber,
                    account.FullName()
                    );
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            login login = new login();
            login.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string search;

            search = maskedTextBox1.Text;
            var updatedList = accountList
               .Where(account => account.AccountNumber.ToString().Contains(search))
               .ToList();

            if (updatedList == null)
            {
                InsertDataGrid(accountList);
                return;
            }

            InsertDataGrid(updatedList);


        }
    }
}
