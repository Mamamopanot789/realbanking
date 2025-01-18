using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace banking.model
{
    public class Account
    {
        private string accountId;
        private string firstName;
        private string middleName;
        private string lastName;
        private int accountNumber;
        private string status;
        private string pinNumber;
        private string q1;
        private string q2;
        private string a1;
        private string a2;
        private DateTime createdBy;
        private DateTime updatedAt;
        public string AccountId
        {
            get { return accountId; }
            set { accountId = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string MiddleName
        {
            get { return middleName; }
            set { middleName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public int AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; }

        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string PinNumber
        {
            get { return pinNumber; }
            set { pinNumber = value; }
        }

        public string Q1
        {
            get { return q1; }
            set { q1 = value; }
        }

        public string Q2
        {
            get { return q2; }
            set { q2 = value; }

        }

        public string A1
        {
            get { return a1; }
            set { a1 = value; }
        }

        public string A2
        {
            get { return a2; }
            set { a2 = value; }
        }

        public DateTime CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        public DateTime UpdatedAt
        {
            get { return updatedAt; }
            set { updatedAt = value; }
        }

        // Constructor to initialize all the fields
        public Account(string accountId, string firstName, string middleName, string lastName, int accountNumber, string status,
                       string pinNumber, string q1, string q2, string a1, string a2, DateTime createdBy, DateTime updatedAt)
        {
            this.accountId = accountId;
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
            this.accountNumber = accountNumber;
            this.status = status;
            this.pinNumber = pinNumber;
            this.q1 = q1;
            this.q2 = q2;
            this.a1 = a1;
            this.a2 = a2;
            this.createdBy = createdBy;
            this.updatedAt = updatedAt;
        }

        // ToString method to return a string representation of the object
        public override string ToString()
        {
            return $"Account ID: {accountId}, Full Name: {firstName} {middleName} {lastName}, Account Number: {accountNumber}, Status: {status}, " +
                   $"PIN: {pinNumber}, Security Q1: {q1}, Security Q2: {q2}, A1: {a1}, A2: {a2}, Created On: {createdBy}, Last Updated: {updatedAt}";
        }
    }
}
