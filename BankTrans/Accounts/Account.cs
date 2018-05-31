using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ingentek.DeveloperExercises {
    /*
    All bank accounts have an account number, account owner name, and a balance.The
    account number is a unique integer less than 1000000 and duplicates will not be given to
    you in the account file.
    */
    public class Account {
        // All Account types will have a number, owner and balance.
        // NOTE: We use decimal types for all numeric transactions for monetary accuracy.

        // Account number is a unique integer less than 1000000
        public int Number { get; protected set; }
        public string Owner { get; protected set; }
        public decimal Balance { get; protected set; }

        // Our client has different types of accounts in their system.
        public AccountType Type { get; private set; }

        public Account(int number, string owner, decimal balance, AccountType type) {
            Number = number;
            Owner = owner;
            Balance = balance;
            Type = type;
        }

        // Default implementations for withdraws and deposits.
        // Other subclasses will override these and add more specific implementations.
        // NOTE: Using lock statement for thread-safe atomic transactions:

        public virtual decimal Withdraw(decimal amount) {
            lock (this) {
                Balance -= amount;
            }

            return Balance;
        }

        public virtual decimal Deposit(decimal amount) {
            lock (this) {
                Balance += amount;
            }

            return Balance;
        }

        // Shared output formatting for the output file
        public override string ToString() {
            return string.Format("{0}, {1}, {2:F}, {3}", Number, Owner, Balance, Type);
        }
    }
}
