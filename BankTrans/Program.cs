using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace BankTrans
{
    /// <summary>
    /// Banking Transaction Processing Application
    /// Create/Develop a tool that will take a file (containing: current customer accounts & balances) and
    /// combine it with a second file(containing: transaction records) and in turn produce a third file that will
    /// contain the current state of those accounts.
    /// </summary>
    class BankingTransactionApplication
    {
        // Setup window size
        // Clear screen of output
        // and print a fancy header
        static void Initialize()
        {
            Console.Title = "BankTrans - Banking Transaction Processing Application";
            Console.Clear();
            Console.WindowWidth = 85;
            Console.WindowHeight = 30;
            Console.WriteLine(@"

 /$$$$$$$                      /$$    /$$$$$$$$                                    
| $$__  $$                    | $$   |__  $$__/                                    
| $$  \ $$  /$$$$$$  /$$$$$$$ | $$   /$$| $$  /$$$$$$  /$$$$$$  /$$$$$$$   /$$$$$$$
| $$$$$$$  |____  $$| $$__  $$| $$  /$$/| $$ /$$__  $$|____  $$| $$__  $$ /$$_____/
| $$__  $$  /$$$$$$$| $$  \ $$| $$$$$$/ | $$| $$  \__/ /$$$$$$$| $$  \ $$|  $$$$$$ 
| $$  \ $$ /$$__  $$| $$  | $$| $$_  $$ | $$| $$      /$$__  $$| $$  | $$ \____  $$
| $$$$$$$/|  $$$$$$$| $$  | $$| $$ \  $$| $$| $$     |  $$$$$$$| $$  | $$ /$$$$$$$/
|_______/  \_______/|__/  |__/|__/  \__/|__/|__/      \_______/|__/  |__/|_______/ 


                    Banking Transaction Processing Application 
                            Created by Michael Schutt

                ");
        }

        static void Main(string[] args)
        {
            // Check for the correct number of command line arguments 
            // If not found then show the usage on the console:
            if (args.Length != 4)
            {
                Console.WriteLine(@"
    
    Usage: banktrans.exe <transactions file> <accounts file> <output file> <error file>
    Example: banktrans transactions.dat accounts.dat output.dat error.log


                ");
                return;
            }

            /****************************** PROCESS COMMAND LINE ***************************
             * Command-line Arguments:
             * Your program should take command line arguments or offer a GUI to specify the input and
             * output files. It should have the following options:
             */

            // Parse command-line arguments, set file paths, Create CSVFile objects 
            // and read the records and deserialize objects:

            // c. Output file - the path of a file to be created to store the final result of the transaction execution
            string outputFile = args[2];

            /***************************** SETUP ERROR LOGGING ******************************/
            // d. Error file - the path of a file to be created to store any errors encountered during execution
            // Add a new TraceListener for the Console and for the error log file
            // NOTE: Set Trace.AutoFlush to true for log files to work correctly!
            Trace.AutoFlush = true;

            Trace.Listeners.AddRange(new TraceListener[] {
                new ConsoleTraceListener(),             // Write to Console
                new TextWriterTraceListener(args[3])    // Write to specified error file
            });

            // a. Transaction file - the path of the transaction file to execute
            // Create a new TransactionFile interface, which will load each line
            // a csv transaction file as a new Transaction object.
            TransactionFile transactionFile = new TransactionFile(args[0]);
            IEnumerable<Transaction> transactions = transactionFile.Read();

            // b. Accounts file - the path of the accounts file to supply the initial state of accounts
            // Create a new AccountFile interface, which will load each line
            // of a csv account file as a new Account object.
            AccountFile accountFile = new AccountFile(args[1]);
            IEnumerable<Account> accounts = accountFile.Read();

            // Create an account map by zipping up accounts with their numbers and mapping 
            // from account numbers to the actual account objects.
            // NOTE: We are using the SortedDictionary type to make sure all account numbers
            // stay sorted from least to greatest while doing inserts.
            var accountMap = new SortedDictionary<int, Account>(
                accounts.Zip(accounts.Select(a => a.Number), (v, k) => new { v, k })
                        .ToDictionary(item => item.k, item => item.v)
            );

            /************************** START PROCESSING TRANSACTIONS *****************************
             * For each transaction in the TransactionFile find source and destination accounts and
             * the amount to withdraw from source and deposit into destination.  Process the transaction
             * and log any errors encountered during processing
             */
            int transactionCounter = 1;

            foreach (var transaction in transactions)
            {
                int source = transaction.Source;
                int destination = transaction.Destination;

                // Check for invalid account numbers.
                if (!accountMap.ContainsKey(source))
                {
                    Trace.TraceError(new InvalidAccountNumberException(source).Message);
                }
                else if (!accountMap.ContainsKey(destination))
                {
                    Trace.TraceError(new InvalidAccountNumberException(destination).Message);
                }

                // Get Accounts from account numbers
                Account sourceAccount = accountMap[source];
                Account destinationAccount = accountMap[destination];

                // Try to process the current transaction
                decimal amount = transaction.Amount;
                Console.WriteLine("Processing transaction {0} of {1}: ", transactionCounter++, transactions.Count());

                // Log out processing and catch any AccountExceptions:
                try
                {
                    Console.WriteLine("Withdrawing {0:C2} from {1} account #{2}...", amount, sourceAccount.Type, sourceAccount.Number);
                    sourceAccount.Withdraw(amount);

                    Console.WriteLine("Depositing {0:C2} into {1} account #{2}", amount, destinationAccount.Type, destinationAccount.Number);
                    destinationAccount.Deposit(amount);
                }
                catch (AccountException e)
                {
                    Trace.TraceError(e.Message);
                }
            }

            /*
            * The program should output a file containing the final state of all the accounts.
            * It should be in the same format as "accounts.dat" described below.
            * However, the account numbers should be in numeric order from least to greatest
            */
            string output = string.Join(Environment.NewLine, accountMap.Values);

            try
            {
                File.WriteAllText(outputFile, output);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
            }
        }
    }
}
