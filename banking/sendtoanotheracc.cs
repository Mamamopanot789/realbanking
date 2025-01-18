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
    }
}
