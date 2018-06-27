namespace BankTrans 
{
    // a. If a transaction would cause a negative balance (including any fees) and a negative balance is not allowed
    public class NegativeBalanceException : AccountException {
        public NegativeBalanceException(Account account) : base(
              string.Format("The specified transaction would have resulted in a negative balance for account {0}", account),
              account
        ) { }
    }
}
