using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ingentek.DeveloperExercises {
    // c. If an account number does not exist. 
    public class InvalidAccountNumberException : AccountException {
        public InvalidAccountNumberException(int account)
            : base(
                  string.Format("The account number '{0}' does not exist or is an invalid account number", account),
                  null
            ) { }

    }
}
