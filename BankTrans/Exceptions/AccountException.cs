using System;

namespace BankTrans
{
    /***** ERRORS ***** 
    * a. If a transaction would cause a negative balance (including any fees) and a negative
    *    balance is not allowed or the maximum negative balance would be exceeded.
    * b. If an account type is specified that isn't valid.
    * c. If an account number does not exist. 
    */
    public class AccountException : ApplicationException
    {
        protected Account Account { get; private set; }

        public AccountException(string message, Account account) : base(message)
        {
            Account = account;
        }

        public override string ToString()
        {
            return string.Format("Error: {0} for record: {1}", Message, Account);
        }
    }
}
