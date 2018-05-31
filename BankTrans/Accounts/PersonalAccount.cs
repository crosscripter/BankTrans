using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ingentek.DeveloperExercises {
    /*
    * Our client has different types of accounts in their system.
    *   b. Personal accounts
    *       i. Personal accounts are never charged a fee.
    *       ii. Personal accounts are not allowed any overdrafts.     */
    public class PersonalAccount : Account {

        public PersonalAccount(int number, string owner, decimal balance)
            : base(number, owner, balance, AccountType.Personal) {
        }

        // Override the default Account.Withdraw implmementation to check
        // for possible overdraws before they happen.  Personal accounts are
        // not allowed to overdraft so a NegativeBalanceException is thrown
        // otherwise, the default implmentation is called and the balance is
        // returned.
        public override decimal Withdraw(decimal amount) {

            // ii.Personal accounts are not allowed any overdrafts. 
            if ((Balance - amount) < 0.00M) {
                throw new NegativeBalanceException(this);
            }

            return base.Withdraw(amount);
        }
    }
}
