using System;
using AddressProcessing.Address.v1;
using AddressProcessing.CSV;
using System.IO;

namespace AddressProcessing.Address
{
    public class AddressFileProcessor
    {
        private readonly IMailShot _mailShot;

        public AddressFileProcessor(IMailShot mailShot)
        {
            if (mailShot == null) throw new ArgumentNullException("mailShot");
            _mailShot = mailShot;
        }

        public void Process(string inputFile)
        {
            using (var stream = File.OpenText(inputFile))
            {
                var reader = new TSV.TSVReader(stream);

                var line = reader.ReadLine();
                while (line != null)
                {
                    if(line.Count > 1) //skip invalid lines...
                        _mailShot.SendMailShot(line[0], line[1]);

                    line = reader.ReadLine();
                }
            }
        }
    }
}
