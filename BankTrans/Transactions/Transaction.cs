using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ingentek.DeveloperExercises {

    // Simple POCO to represent account transactions
    public class Transaction {
        public int Source { get; private set; }
        public int Destination { get; private set; }
        public decimal Amount { get; private set; }

        public Transaction(int source, int destination, decimal amount) {
            Source = source;
            Destination = destination;
            Amount = amount;
        }
    }
}
