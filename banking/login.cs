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
    public partial class login : Form
    {
        private readonly AccountRepository accountRepository = new AccountRepository();
        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Signup sign = new Signup();


            sign.Show();


            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Account account = accountRepository.Login(maskedTextBox1.Text,textBox1.Text);


            if (account == null)
            {
                MessageBox.Show("Wrong Account Number or Pin Number");
                return;
            }

            if (account.AccountNumber == "0000-000-0000")
            {
                Adminpanel adminpanel = new Adminpanel();
                adminpanel.ShowDialog();
                return;
            }

         transaction_home transaction = new transaction_home(account); 
            transaction.Show();
            this.Hide();            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgotPass forgotPass = new ForgotPass();
            forgotPass.ShowDialog();
            this.Hide();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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
