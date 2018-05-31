using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ingentek.DeveloperExercises {
    // Specialized the CSVFile base class by allowing reading
    // of each line as a Transaction object instead of generic
    // T objects or strings
    class TransactionFile : CSVFile {

        public TransactionFile(string path) : base(path) { }

        // Parses each line of text or record into a Transaction
        private Transaction Parse(string line) {

            // Split line into fields 
            string[] fields = line.Split(',');

            // Parse out each field for an individual transaction.
            int source = int.Parse(fields[0].Trim());
            int destination = int.Parse(fields[1].Trim());
            decimal amount = decimal.Parse(fields[2].Trim());

            // Create a new transaction object
            return new Transaction(source, destination, amount);
        }

        // Overrides the CSVFile.Read() method to return 
        // an enumerable of Transaction objects instead.
        new public IEnumerable<Transaction> Read() {
            return Read(Parse);
        }
    }
}
