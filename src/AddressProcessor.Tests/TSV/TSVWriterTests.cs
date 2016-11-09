using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using AddressProcessing.TSV;
using System.IO;
using Moq;

namespace AddressProcessing.Tests.TSV
{
    [TestFixture]
    class TSVWriterTests
    {
        [TestCase]
        public void TSVWriter_should_throw_exception_if_no_stream()
        {
            new object().Invoking(x => new TSVWriter(null)).ShouldThrow<ArgumentNullException>();
        }

        [TestCase]
        public void TSVWriter_should_throw_exception_if_no_data()
        {
            //arrange
            var mockStream = new Mock<TextWriter>();
            var tsvWriter = new TSVWriter(mockStream.Object);

            //act & assert
            tsvWriter.Invoking(x => x.WriteLine(null)).ShouldThrow<ArgumentNullException>();
        }

        [TestCase]
        public void TSVWriter_should_write_to_the_stream()
        {
            //arrange
            var mockStream = new Mock<TextWriter>();
            mockStream.Setup(x => x.WriteLine("This is a line with no tabs")).Verifiable();
            var tsvWriter = new TSVWriter(mockStream.Object);

            //act
            tsvWriter.WriteLine(new List<string>() { "This is a line with no tabs" });

            //assert
            mockStream.Verify(x => x.WriteLine("This is a line with no tabs"), Times.Once);

        }

        [TestCase]
        public void TSVWriter_should_join_data_with_a_tab()
        {
            //arrange
            var mockStream = new Mock<TextWriter>();
            mockStream.Setup(x => x.WriteLine("This is a line\twith a tab")).Verifiable();
            var tsvWriter = new TSVWriter(mockStream.Object);

            //act
            tsvWriter.WriteLine(new List<string>() { "This is a line", "with a tab" });

            //assert
            mockStream.Verify(x => x.WriteLine("This is a line\twith a tab"), Times.Once);

        }

        [TestCase]
        public void TSVWriter_should_handle_some_real_data()
        {
            //arrange
            var mockStream = new Mock<TextWriter>();
            mockStream.Setup(x => x.WriteLine(It.IsAny<string>())).Verifiable();
            var tsvWriter = new TSVWriter(mockStream.Object);

            //act
            tsvWriter.WriteLine(new List<string>() { "Shelby Macias", "3027 Lorem St.| Kokomo | Hertfordshire | L9T 3D5 | England", "1 66 890 3865 - 9584", "et @eratvolutpat.ca" });
            tsvWriter.WriteLine(new List<string>() { "Porter Coffey", "Ap #827-9064 Sapien. Rd.|Palo Alto|Fl.|HM0G 0YR|Scotland", "1 80 177 2329-1167", "Cras@semperpretiumneque.ca" });
            tsvWriter.WriteLine(new List<string>() { "Noelani Ward", "637 - 911 Mi Rd.| Monrovia | MB | M5M 6SC | Scotland", "1 15 373 1666 - 1277", "adipiscing @neque.edu" });

            //assert
            mockStream.Verify(x => x.WriteLine("Shelby Macias\t3027 Lorem St.| Kokomo | Hertfordshire | L9T 3D5 | England\t1 66 890 3865 - 9584\tet @eratvolutpat.ca"), Times.Once);
            mockStream.Verify(x => x.WriteLine("Porter Coffey\tAp #827-9064 Sapien. Rd.|Palo Alto|Fl.|HM0G 0YR|Scotland\t1 80 177 2329-1167\tCras@semperpretiumneque.ca"), Times.Once);
            mockStream.Verify(x => x.WriteLine("Noelani Ward\t637 - 911 Mi Rd.| Monrovia | MB | M5M 6SC | Scotland\t1 15 373 1666 - 1277\tadipiscing @neque.edu"), Times.Once);
        }
    }
}
