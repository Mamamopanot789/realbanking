using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace banking.model
{
    public class Transaction
    {
        private string transactionId;
        private string transactionType;
        private string accountId;
        private string receiverId;
        private string status;
        private DateTime createdAt;
        private DateTime updatedAt;
        private decimal amount;

        public string TransactionId
        {
            get { return transactionId; }
            set { transactionId = value; }
        }

        public string TransactionType
        {
            get { return transactionType; }
            set { transactionType = value; }

        }
        public string AccountId
        {
            get { return accountId; }
            set { accountId = value; }
        }
        public string ReceiverId
        {
            get { return receiverId; }
            set { receiverId = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public DateTime CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value; }
        }

        public DateTime UpdatedAt
        {
            get { return updatedAt; }
            set { updatedAt = value; }
        }

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        // Constructor to initialize all the fields

        public Transaction() { }

        public Transaction(string transactionId, string transactionType, string accountId, string receiverId,
                           string status, DateTime createdAt, DateTime updatedAt, decimal amount)
        {
            this.transactionId = transactionId;
            this.transactionType = transactionType;
            this.accountId = accountId;
            this.receiverId = receiverId;
            this.status = status;
            this.createdAt = createdAt;
            this.updatedAt = updatedAt;
            this.amount = amount;
        }

        // ToString method to return a string representation of the object
        public override string ToString()
        {
            return $"Transaction ID: {transactionId}, Type: {transactionType}, Account ID: {accountId}, Receiver ID: {receiverId}, " +
                   $"Status: {status}, Amount: {amount:C}, Created At: {createdAt}, Last Updated: {updatedAt}";
        }
    }
}
