namespace BankTrans 
{
    // Simple POCO to represent account transactions
    public class Transaction 
    {
        public int Source { get; private set; }
        public int Destination { get; private set; }
        public decimal Amount { get; private set; }

        public Transaction(int source, int destination, decimal amount) 
        {
            Source = source;
            Destination = destination;
            Amount = amount;
        }
    }
}
