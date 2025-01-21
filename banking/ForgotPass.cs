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
    public partial class ForgotPass : Form
    {

        
        private readonly AccountRepository accountRepository = new AccountRepository();
        private Account account;
        public ForgotPass()
        {
            InitializeComponent();

            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            button3.Hide();
            button4.Hide();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
           Account currentAccount = accountRepository.GetAccountByAccountNumber(maskedTextBox1.Text);
            
            if(currentAccount == null)
            {
                MessageBox.Show("There is no Account With this number");
                return;
            }

            maskedTextBox1.ReadOnly = true;
            this.account = currentAccount;
            button2.Hide();
            label3.Text = currentAccount.Q1;
            label4.Text = currentAccount.Q2;
            textBox1.Show();
            textBox2.Show();
            label3.Show();
            label4.Show();
            label7.Show();
            button3.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string a1, a2;
            a1= textBox1.Text;
            a2= textBox2.Text;

            if (!account.A2.Equals(a2) || !account.A1.Equals(a1)) {
                MessageBox.Show("The Answer Is Not Correct");
                return;
            }
            textBox1.ReadOnly= true;
            textBox2 .ReadOnly= true;

            button3.Hide();
            button4.Show();
            label5.Show();
            label6.Show();
            textBox3.Show();
            textBox4.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            login login = new login();
            login.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string pin, confirmpin;

            pin = textBox3.Text;
            confirmpin = textBox4.Text;

            if (pin != confirmpin)
            {
                MessageBox.Show("The Pin Code Are Not The Same");
                return;
            }

            MessageBox.Show("Account Pin Successfully Changed");

            accountRepository.UpdateAccountPinByAccountNumber(account.AccountNumber,pin);
            login login = new login();
            login.Show();
            this.Close();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("This Field Only Allow Numbers");
                return;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
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
