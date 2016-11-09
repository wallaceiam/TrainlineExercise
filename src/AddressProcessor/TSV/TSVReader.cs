using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressProcessing.TSV
{
    public class TSVReader
    {
        private TextReader _reader;
        private static readonly char[] separator = new char[] { '\t' };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reader">The TextReader</param>
        public TSVReader(TextReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            _reader = reader;
        }

        /// <summary>
        /// Reads a line and returns a collection of columns from the TSV
        /// </summary>
        /// <returns>A collection of columns, or null if there is no data</returns>
        public List<string> ReadLine()
        {
            var line = _reader.ReadLine();
            if (!string.IsNullOrEmpty(line))
            {
                return line.Split(separator, StringSplitOptions.None).ToList();
            }

            return null;
        }
        
    }
}
