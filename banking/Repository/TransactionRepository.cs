using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace banking.model
{
    public class TransactionRepository
    {
        private readonly string connectionString;

        public TransactionRepository()
        {
            // Retrieve connection string from app.config
            connectionString = ConfigurationManager.ConnectionStrings["BankingDb"].ConnectionString;
        }

        // Deposit Method
        public void Deposit(string accountId, decimal amount)
        {
            string query = @"INSERT INTO Tbl_Transaction (transactionId, transactionType, accountId, receiverId, status, createdAt, updatedAt, amount)
                             VALUES (@transactionId, 'Deposit', @accountId, NULL, 'Success', @createdAt, @updatedAt, @amount)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@transactionId", Guid.NewGuid().ToString());
                command.Parameters.AddWithValue("@accountId", accountId);
                command.Parameters.AddWithValue("@createdAt", DateTime.Now);
                command.Parameters.AddWithValue("@updatedAt", DateTime.Now);
                command.Parameters.AddWithValue("@amount", amount);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Withdraw Method
        public void Withdraw(string accountId, decimal amount)
        {
            string query = @"INSERT INTO Tbl_Transaction (transactionId, transactionType, accountId, receiverId, status, createdAt, updatedAt, amount)
                             VALUES (@transactionId, 'Withdraw', @accountId, NULL, 'Active', @createdAt, @updatedAt, @amount)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@transactionId", Guid.NewGuid().ToString());
                command.Parameters.AddWithValue("@accountId", accountId);
                command.Parameters.AddWithValue("@createdAt", DateTime.Now);
                command.Parameters.AddWithValue("@updatedAt", DateTime.Now);
                command.Parameters.AddWithValue("@amount", amount);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // GetAllTransactions Method
        public List<Transaction> GetAllTransactionsByAccountId(string accountId)
        {
            List<Transaction> transactions = new List<Transaction>();
            string query = @"SELECT transactionId, transactionType, accountId, receiverId, status, createdAt, updatedAt, amount
                             FROM Tbl_Transaction WHERE accountId = @accountId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@accountId", accountId);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        transactions.Add(new Transaction(
                            reader["transactionId"].ToString(),
                            reader["transactionType"].ToString(),
                            reader["accountId"].ToString(),
                            reader["receiverId"] as string, // Handle nullable fields
                            reader["status"].ToString(),
                            Convert.ToDateTime(reader["createdAt"]),
                            Convert.ToDateTime(reader["updatedAt"]),
                            Convert.ToDecimal(reader["amount"])
                        ));
                    }
                }
            }

            return transactions;
        }

        // GetAllTransactionsBetweenStartDateAndEndDate Method
        public List<Transaction> GetAllTransactionsBetweenStartDateAndEndDate(DateTime startDate, DateTime endDate)
        {
            List<Transaction> transactions = new List<Transaction>();
            string query = @"SELECT transactionId, transactionType, accountId, receiverId, status, createdAt, updatedAt, amount
                             FROM Tbl_Transaction
                             WHERE createdAt BETWEEN @startDate AND @endDate";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@startDate", startDate);
                command.Parameters.AddWithValue("@endDate", endDate);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        transactions.Add(new Transaction(
                            reader["transactionId"].ToString(),
                            reader["transactionType"].ToString(),
                            reader["accountId"].ToString(),
                            reader["receiverId"] as string, // Handle nullable fields
                            reader["status"].ToString(),
                            Convert.ToDateTime(reader["createdAt"]),
                            Convert.ToDateTime(reader["updatedAt"]),
                            Convert.ToDecimal(reader["amount"])
                        ));
                    }
                }
            }

            return transactions;
        } 


    public decimal GetTotalMoneyByAccountId(string accountId)
        {
            // Retrieve all transactions for the account
            List<Transaction> transactions = GetAllTransactionsByAccountId(accountId);

            // Initialize the total balance
            decimal totalMoney = 0;

            // Calculate total using a loop
            foreach (var transaction in transactions)
            {
                if (transaction.TransactionType == "Deposit")
                {
                    totalMoney += transaction.Amount;
                }
                else if (transaction.TransactionType == "Withdraw")
                {
                    totalMoney -= transaction.Amount;
                }
            }

            return totalMoney;
        }
    }
}
