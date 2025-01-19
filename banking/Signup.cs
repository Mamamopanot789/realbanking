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
    public partial class Signup : Form
    {

        private readonly AccountRepository accountRepository = new AccountRepository(); 
        
        public Signup()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Signup_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            login back = new login();

           
            back.Show();
           
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            login login = new login();


            if (String.IsNullOrEmpty(textBox1.Text) ||
                String.IsNullOrEmpty(textBox2.Text) ||
                String.IsNullOrEmpty(textBox3.Text) ||
                String.IsNullOrEmpty(maskedTextBox1.Text) ||
                String.IsNullOrEmpty(textBox4.Text) ||
                String.IsNullOrEmpty(textBox5.Text) ||
                String.IsNullOrEmpty(textBox6.Text) ||
                String.IsNullOrEmpty(textBox7.Text) ||
                String.IsNullOrEmpty(comboBox1.Text) ||
                String.IsNullOrEmpty(comboBox2.Text)
                ) {
                MessageBox.Show("All fields are required. Please fill them in.");
                return;
            }

            if(maskedTextBox1.Text.Trim().Length != 13 )
            {
                MessageBox.Show("The Number Should Be 12 Digit");
                return ;
            }

            if (textBox4.Text.Trim().Length != 6)
            {
                MessageBox.Show("The Pin code should be 6 digit");
                return;
            }

            Account accountChecker = accountRepository.GetAccountByAccountNumber(maskedTextBox1.Text);

            if (accountChecker != null) {
                MessageBox.Show("The Account Number Is Already Taken");
                return;
            }
            //return;

            if (textBox4.Text != textBox5.Text)
            {
                MessageBox.Show("The Pin Number Are Not Same");
                return;
            }

            Account account = new Account();
            account.FirstName = textBox1.Text;
            account.MiddleName = textBox2.Text;
            account.LastName = textBox3.Text;
            account.AccountNumber = maskedTextBox1.Text; // Ensure valid integer
            account.Status = comboBox1.Text; // Assuming this is for status (e.g., Active/Inactive)
            account.PinNumber = textBox4.Text;
            account.Q1 = comboBox1.Text; // Assuming this is for security question 1
            account.A1 = textBox6.Text; // Answer for security question 1
            account.Q2 = comboBox2.Text; // Security question 2 text
            account.A2 = textBox7.Text; // Answer for security question 

            accountRepository.AddAccount(account);

            MessageBox.Show("Account Has Been Created");

            login.Show();
            
            this.Close();


        }
    }
}
