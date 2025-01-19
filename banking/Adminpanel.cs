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
    public partial class Adminpanel : Form
    {
        private readonly AccountRepository accountRepository = new AccountRepository();

        public Adminpanel()
        {
            InitializeComponent();

            InsertDataGrid(accountRepository.GetAllAccount());
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
    }
}
