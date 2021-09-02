using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace MyATMAndroid
{
    public class AtmUsers
    {
        private List<AtmUser> users;
        public AtmUsers()
        {
            var internalPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var usersFilePath = Path.Combine(internalPath, "users.json");
            if (File.Exists(usersFilePath))
            {
                var usersJson = File.ReadAllText(usersFilePath);
                users = JsonConvert.DeserializeObject<List<AtmUser>>(usersJson);
            }
            else
            {
                InitUsers();
            }
        }

        private void InitUsers()
        {
            users = new List<AtmUser>
                {
                    new AtmUser
                    {
                        AccountNumber = "HR230000000000000001",
                        Pin = "1234",
                        Name = "Karlo",
                        LastName = "Čulek",
                        Id = 0,
                        Transactions = new List<AtmUserTransaction>
                        {
                            new AtmUserTransaction
                            {
                                AccountBalance = 1000,
                                Amount = 0,
                                Date = DateTime.Now,
                                Description = "Initial deposit"
                            }
                        }
                    },
                    new AtmUser
                    {
                        AccountNumber = "HR240000000000000001",
                        Pin = "5678",
                        Name = "Karlo",
                        LastName = "Čulek",
                        Id = 1,
                        Transactions = new List<AtmUserTransaction>
                        {
                            new AtmUserTransaction
                            {
                                AccountBalance = 2000,
                                Amount = 0,
                                Date = DateTime.Now,
                                Description = "Initial deposit"
                            }
                        }
                    },
                    new AtmUser
                    {
                        AccountNumber = "HR240000000000000002",
                        Pin = "9999",
                        Name = "Loan PBZ",
                        LastName = "",
                        Id = 2,
                        Transactions = new List<AtmUserTransaction>
                        {
                            new AtmUserTransaction
                            {
                                AccountBalance = 200000,
                                Amount = 0,
                                Date = DateTime.Now,
                                Description = "Initial deposit"
                            }
                        }
                    },
                    new AtmUser
                    {
                        AccountNumber = "HR240000000000000003",
                        Pin = "9999",
                        Name = "Loan Zaba",
                        LastName = "",
                        Id = 3,
                        Transactions = new List<AtmUserTransaction>
                        {
                            new AtmUserTransaction
                            {
                                AccountBalance = 200000,
                                Amount = 0,
                                Date = DateTime.Now,
                                Description = "Initial deposit"
                            }
                        }
                    }
                };
            Save();
        }

        public AtmUser GetUser(string accountNumber)
        {
            return users.FirstOrDefault(u => u.AccountNumber == accountNumber);
        }

        public void RemoveAmount(int id, decimal amount, string comment)
        {
            AddAmount(id, (-1 * amount), comment);
        }

        public void AddAmount(int id, decimal amount, string comment)
        {
            var user = users.First(i => i.Id == id);
            var lastTransaction = user.Transactions.OrderByDescending(i => i.Date).First();
            var currentBalance = lastTransaction.AccountBalance + amount;
            user.Transactions.Add(new AtmUserTransaction
            {
                Amount = amount,
                Date = DateTime.Now,
                Description = comment,
                AccountBalance = currentBalance
            });
            Save();
        }

        public void TransferAmount(string sourceAccount, string targetAccount, decimal amount)
        {
            var sourceUser = GetUser(sourceAccount);
            var targetUser = GetUser(targetAccount);
            if (sourceUser.GetCurrentBalance() < amount)
            {
                throw new TransactionException("The balance exceeds the transfer amount.");
            }
            else if (targetUser == null)
            {
                throw new TransactionException($"No user found for account {targetAccount}.");
            }
            else if (amount <= 0)
            {
                throw new TransactionException("Amount needs to be a positive value.");
            }
            RemoveAmount(sourceUser.Id, amount, targetAccount);
            AddAmount(targetUser.Id, amount, sourceAccount);
            Save();
        }
        public void Save()
        {
            var internalPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var usersFilePath = Path.Combine(internalPath, "users.json");
            var usersJson = JsonConvert.SerializeObject(users);
            File.WriteAllText(usersFilePath, usersJson);
        }
    }
}