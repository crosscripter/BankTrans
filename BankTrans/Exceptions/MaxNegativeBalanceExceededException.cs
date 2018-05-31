using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ingentek.DeveloperExercises {
    // ...or the maximum negative balance would be exceeded.
    public class MaxNegativeBalanceExceededException : AccountException {
        public MaxNegativeBalanceExceededException(Account account)
            : base(
                  string.Format("The maximum negative balance would have been exceeded for account {0}", account),
                  account
            ) { }
    }
}
