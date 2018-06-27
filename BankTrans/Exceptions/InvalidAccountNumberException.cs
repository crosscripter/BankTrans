namespace BankTrans
{
    // c. If an account number does not exist. 
    public class InvalidAccountNumberException : AccountException
    {
        public InvalidAccountNumberException(int account) : base(
          string.Format("The account number '{0}' does not exist or is an invalid account number", account),
          null
        ) { }
    }
}
