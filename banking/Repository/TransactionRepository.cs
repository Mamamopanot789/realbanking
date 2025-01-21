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
        private readonly AccountRepository accountRepository = new AccountRepository();

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
                 FROM Tbl_Transaction 
                 WHERE accountId = @accountId
                 ORDER BY createdAt DESC";


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
        public List<Transaction> GetAllTransactionsBetweenStartDateAndEndDate(DateTime startDate, DateTime endDate,string accountId)
        {
            List<Transaction> transactions = new List<Transaction>();
            string query = @"SELECT transactionId, transactionType, accountId, receiverId, status, createdAt, updatedAt, amount
                             FROM Tbl_Transaction
                             WHERE accountId=@accountId AND createdAt BETWEEN @startDate AND @endDate";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@accountId", accountId);
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
                if (transaction.TransactionType == "Deposit" || transaction.TransactionType == "Transfer-In")
                {
                    totalMoney += transaction.Amount;
                }
                else if (transaction.TransactionType == "Withdraw" || transaction.TransactionType == "Transfer-Out")
                {
                    totalMoney -= transaction.Amount;
                }
            }

            return totalMoney;
        }



        public void TransferMoney(string senderAccountId, string receiverAccountNumber, decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Transfer amount must be greater than zero.");
            }
            // Gets the value of the accountNumber
            string receiverIdNumber = accountRepository.GetAccountIdByAccountNumber(receiverAccountNumber);
            Console.WriteLine(receiverIdNumber);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Check sender's balance
                    decimal senderBalance = GetTotalMoneyByAccountId(senderAccountId);
                    if (senderBalance < amount)
                    {
                        throw new InvalidOperationException("Insufficient funds in the sender's account.");
                    }

                    // Deduct money from sender's account
                    string deductQuery = @"INSERT INTO Tbl_Transaction 
                                    (transactionId, transactionType, accountId, receiverId, status, createdAt, updatedAt, amount) 
                                    VALUES 
                                    (@transactionId, 'Transfer-Out', @accountId, @receiverId, 'Active', @createdAt, @updatedAt, @amount)";
                    using (SqlCommand deductCommand = new SqlCommand(deductQuery, connection, transaction))
                    {
                        deductCommand.Parameters.AddWithValue("@transactionId", Guid.NewGuid().ToString());
                        deductCommand.Parameters.AddWithValue("@accountId", senderAccountId);
                        deductCommand.Parameters.AddWithValue("@receiverId", receiverIdNumber);
                        deductCommand.Parameters.AddWithValue("@createdAt", DateTime.Now);
                        deductCommand.Parameters.AddWithValue("@updatedAt", DateTime.Now);
                        deductCommand.Parameters.AddWithValue("@amount", amount);
                        deductCommand.ExecuteNonQuery();
                    }

                    // Add money to receiver's account
                    string addQuery = @"INSERT INTO Tbl_Transaction 
                                 (transactionId, transactionType, accountId, receiverId, status, createdAt, updatedAt, amount) 
                                 VALUES 
                                 (@transactionId, 'Transfer-In', @accountId, @receiverId, 'Active', @createdAt, @updatedAt, @amount)";
                    using (SqlCommand addCommand = new SqlCommand(addQuery, connection, transaction))
                    {
                        addCommand.Parameters.AddWithValue("@transactionId", Guid.NewGuid().ToString());
                        addCommand.Parameters.AddWithValue("@accountId", receiverIdNumber);
                        addCommand.Parameters.AddWithValue("@receiverId", senderAccountId);
                        addCommand.Parameters.AddWithValue("@createdAt", DateTime.Now);
                        addCommand.Parameters.AddWithValue("@updatedAt", DateTime.Now);
                        addCommand.Parameters.AddWithValue("@amount", amount);
                        addCommand.ExecuteNonQuery();
                    }

                    // Commit the transaction
                    transaction.Commit();
                }
                catch (Exception)
                {
                    // Rollback transaction in case of any error
                    transaction.Rollback();
                    throw;
                }
            }
        }



    }
}
