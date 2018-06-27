using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace BankTrans
{
    public class CSVFile
    {
        public string Path { get; private set; }

        public CSVFile(string path)
        {
            Path = path;
        }

        // Generic reader function, takes a transformer function
        // to know how to transform each line or record in the csv
        // into the generic T passed in:
        protected IEnumerable<T> Read<T>(Func<string, T> transform)
        {
            try
            {
                return File.ReadAllLines(Path)
                           .Where(l => l.Length > 0)
                           .Select(l => transform(l));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Default implementation, reads in a line of text
        // from the csv file and returns an enumerable of
        // strings representing each line.
        public IEnumerable<string> Read()
        {
            return Read(l => l);
        }
    }
}
