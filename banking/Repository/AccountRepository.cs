using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Identity.Client;

namespace banking.model
{
    public class AccountRepository
    {
        private readonly string connectionString;

        public AccountRepository()
        {
            // Retrieve connection string from app.config
            connectionString = ConfigurationManager.ConnectionStrings["BankingDb"].ConnectionString;
        }

        // GetAllAccount
        public List<Account> GetAllAccount()
        {
            List<Account> accounts = new List<Account>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT accountId, firstName, middleName, lastName, accountNumber, status, pinNumber, q1, q2, a1, a2, createdAt, updatedAt FROM Tbl_Account";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        accounts.Add(new Account(
                            reader["accountId"].ToString(),
                            reader["firstName"].ToString(),
                            reader["middleName"].ToString(),
                            reader["lastName"].ToString(),
                            reader["accountNumber"].ToString(),
                            reader["status"].ToString(),
                            reader["pinNumber"].ToString(),
                            reader["q1"].ToString(),
                            reader["q2"].ToString(),
                            reader["a1"].ToString(),
                            reader["a2"].ToString(),
                            Convert.ToDateTime(reader["createdAt"]),
                            Convert.ToDateTime(reader["updatedAt"])
                        ));
                    }
                }
            }

            return accounts;
        }

        // AddAccount
        public void AddAccount(Account account)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    Guid guid = Guid.NewGuid();

                    string query = @"INSERT INTO Tbl_Account 
                                (accountId, firstName, middleName, lastName, accountNumber, status, pinNumber, q1, q2, a1, a2, createdAt, updatedAt)
                                VALUES (@accountId, @firstName, @middleName, @lastName, @accountNumber, @status, @pinNumber, @q1, @q2, @a1, @a2, @createdAt, @updatedAt)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@accountId", guid.ToString());
                    command.Parameters.AddWithValue("@firstName", account.FirstName);
                    command.Parameters.AddWithValue("@middleName", account.MiddleName);
                    command.Parameters.AddWithValue("@lastName", account.LastName);
                    command.Parameters.AddWithValue("@accountNumber", account.AccountNumber);
                    command.Parameters.AddWithValue("@status", "Active");
                    command.Parameters.AddWithValue("@pinNumber", account.PinNumber);
                    command.Parameters.AddWithValue("@q1", account.Q1);
                    command.Parameters.AddWithValue("@q2", account.Q2);
                    command.Parameters.AddWithValue("@a1", account.A1);
                    command.Parameters.AddWithValue("@a2", account.A2);
                    command.Parameters.AddWithValue("@createdAt", DateTime.Now);
                    command.Parameters.AddWithValue("@updatedAt", DateTime.Now);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public Account Login (string accountNumber, string pinNumber) {

            try
            {
                Account account = null;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT accountId, firstName, middleName, lastName, accountNumber, status, pinNumber, q1, q2, a1, a2, createdAt, updatedAt FROM Tbl_Account WHERE accountNumber = @accountNumber AND pinNumber = @pinNumber";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@accountNumber", accountNumber);
                    command.Parameters.AddWithValue("@pinNumber", pinNumber);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            account = new Account(
                                reader["accountId"].ToString(),
                                reader["firstName"].ToString(),
                                reader["middleName"].ToString(),
                                reader["lastName"].ToString(),
                                reader["accountNumber"].ToString(),
                                reader["status"].ToString(),
                                reader["pinNumber"].ToString(),
                                reader["q1"].ToString(),
                                reader["q2"].ToString(),
                                reader["a1"].ToString(),
                                reader["a2"].ToString(),
                                Convert.ToDateTime(reader["createdAt"]),
                                Convert.ToDateTime(reader["updatedAt"])
                            );
                        }
                    }
                }

                return account;
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
                return null;
            }
        
        }

        // GetAccountById
        public Account GetAccountByAccountNumber(string accountNumber)
        {

            try
            {
                Account account = null;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT accountId, firstName, middleName, lastName, accountNumber, status, pinNumber, q1, q2, a1, a2, createdAt, updatedAt FROM Tbl_Account WHERE accountNumber = @accountNumber";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@accountNumber", accountNumber);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            account = new Account(
                                reader["accountId"].ToString(),
                                reader["firstName"].ToString(),
                                reader["middleName"].ToString(),
                                reader["lastName"].ToString(),
                                reader["accountNumber"].ToString(),
                                reader["status"].ToString(),
                                reader["pinNumber"].ToString(),
                                reader["q1"].ToString(),
                                reader["q2"].ToString(),
                                reader["a1"].ToString(),
                                reader["a2"].ToString(),
                                Convert.ToDateTime(reader["createdAt"]),
                                Convert.ToDateTime(reader["updatedAt"])
                            );
                        }
                    }
                }

                return account;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            
        }

        public string GetAccountNumberByAccountId(string accountId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT accountNumber FROM Tbl_Account WHERE accountId = @accountId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@accountId", accountId);

                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            return result.ToString();
                        }
                        else
                        {
                            throw new Exception($"No account found for account ID: {accountId}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching accountNumber: {ex.Message}");
                return null;
            }
        }
        public string GetAccountIdByAccountNumber(string accountNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT accountId FROM Tbl_Account WHERE accountNumber = @accountNumber";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@accountNumber", accountNumber);

                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            return result.ToString();
                        }
                        else
                        {
                            throw new Exception($"No account found for account number: {accountNumber}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching accountId: {ex.Message}");
                throw;
            }
        }



    }
}
