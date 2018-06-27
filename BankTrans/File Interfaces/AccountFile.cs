using System.Collections.Generic;

namespace BankTrans
{
    // Specializes the CSVFile class to provide the ability
    // to read a CSVFile and get Account objects back instead
    // of strings.
    class AccountFile : CSVFile
    {
        // Creates an Account object from a line of text
        // an individual record from the accounts csv file.
        private Account Parse(string line)
        {
            // Split line into individual fields
            string[] fields = line.Split(',');

            // Parse out each field
            // TODO: Eww, hardcoded magic indices...
            int number = int.Parse(fields[0].Trim());
            string owner = fields[1].Trim();
            decimal balance = decimal.Parse(fields[2].Trim());
            string type = fields[3].Trim();

            // Initialize a new Account object
            Account account;

            // Parse out the account type and create 
            // the equivalent Account class instance:
            switch (type.Trim().ToUpper())
            {
                case "PERSONAL":
                    account = new PersonalAccount(number, owner, balance);
                    break;
                case "BUSINESS":
                    account = new BusinessAccount(number, owner, balance);
                    break;
                default:
                    throw new InvalidAccountTypeException(type);
            }

            return account;
        }

        public AccountFile(string path) : base(path) { }

        // Provides the AccountFile a Read() method that returns an
        // IEnumerable of Account objects instead of generic T or strings.
        // Passes in the Parse implementation as the transform function
        // to the base classes' Read<T>(Func<string,T>) method.
        new public IEnumerable<Account> Read()
        {
            return Read(Parse);
        }
    }
}
