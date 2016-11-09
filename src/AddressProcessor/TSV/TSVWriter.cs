using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressProcessing.TSV
{
    public class TSVWriter
    {
        private TextWriter _writer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="writer">The TextWrtier</param>
        public TSVWriter(TextWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            _writer = writer;
        }

        /// <summary>
        /// Writes a single line, joins together the values with a tab character
        /// </summary>
        /// <param name="values"></param>
        public void WriteLine(List<string> values)
        {
            if (values == null)
                throw new ArgumentNullException("values");

            _writer.WriteLine(string.Join("\t", values));
        }
    }
}
