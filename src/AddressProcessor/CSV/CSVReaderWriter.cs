using System;
using System.IO;

namespace AddressProcessing.CSV
{
    /*
        2) Refactor this class into clean, elegant, rock-solid & well performing code, without over-engineering.
            Assume this code is in production and backwards compatibility must be maintained.
    */

    /*
     * I have refactored out the CSVReaderWriter into two separate yet equally important classes: TSVReader and TSVWriter
     * I also took the chance to also rename CSV to TSV to be more descriptive of the actual expected file these classes should be used with.
     * I decided to depend on TextReader and TextWriter classes as they can be easily mocked for testing.  There was a few ways, I could have gone about this:
     *  I could have used the provided contacts.csv to read in the data for each test
     *  I could have used a MemoryStream and feed the data for each test
     *  I could have implemented an interface and a concreate wrapper around StreamReader/StreamWriter but this seemed like overkill
     *  Or simplily I could use TextReader/TextWriter and Moq
     * Tests have been added for both TSVReader and TSVWriter which don't depend up contacts.csv (included in the project).
     * 
     * Both classes use \t as the separator and could easily be refactor out further to allow for comma, tab, colon, pipe, space separated values
     * 
     * I feel the two refactored classes are clean, elegant, rock-solid, performant and most importantly testable.
     * 
     */ 

}
