namespace BankTrans 
{
    //  b. If an account type is specified that isn't valid.
    public class InvalidAccountTypeException : AccountException {
        public InvalidAccountTypeException(string type) : base(
            string.Format("The account type of '{0}' is not a valid account type.", type),
            null
        ) { }
    }
}
