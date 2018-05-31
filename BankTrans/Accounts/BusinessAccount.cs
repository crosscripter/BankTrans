using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ingentek.DeveloperExercises {
    public class BusinessAccount : Account {

        // i.Business accounts are charged a $1.00 fee when they are the source of a transaction.
        private static readonly decimal SourceFee = 1.00M;

        // ii. They are allowed up to $1000 in overdrafts, so their account balances may end negative.
        private static readonly decimal OverdraftLimit = -1000.00M;

        // iii. If an overdraft occurs, a fee of $20 is assessed.
        private static readonly decimal OverdraftFee = 20.00M;

        public BusinessAccount(int number, string owner, decimal balance)
            : base(number, owner, balance, AccountType.Business) {
        }

        // Overrides the default implementation Account.Withdraw to check for
        // Business account rules and access any fees neccessary:
        public override decimal Withdraw(decimal amount) {
            // i.Business accounts are charged a $1.00 fee when they are the source of a transaction.
            amount += SourceFee;

            // iii. If an overdraft occurs, a fee of $20 is assessed.
            amount += ((Balance - amount) < 0.00M) ? OverdraftFee : 0.00M;

            // ii. They are allowed up to $1000 in overdrafts, so their account balances may end negative.
            // Throw an exception if account balance is less than the max negative balance or overdraft limit:
            // TODO: -1000 or 1000 ? > or <?
            if ((Balance - amount) < OverdraftLimit) {
                throw new MaxNegativeBalanceExceededException(this);
            }

            return base.Withdraw(amount);
        }        
    }
}
