using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;
using AddressProcessing.TSV;
using FluentAssertions;
using Moq;

namespace AddressProcessing.Tests.TSV
{
    [TestFixture]
    class TSVReaderTests
    {
        [TestCase]
        public void TSVReader_should_throw_exception_if_no_stream()
        {
            new object().Invoking(x => new TSVReader(null)).ShouldThrow<ArgumentNullException>();
        }

        [TestCase]
        public void TSVReader_should_return_null_if_no_data()
        {
            //arrange
            var mockStream = new Mock<TextReader>();
            mockStream.Setup(x => x.ReadLine()).Returns(string.Empty).Verifiable();
            var tsvReader = new TSVReader(mockStream.Object);

            //act
            var result = tsvReader.ReadLine();

            //assert
            result.Should().BeNull("There is nothing in the stream");

            mockStream.Verify(x => x.ReadLine(), Times.Once);
        }

        [TestCase]
        public void TSVReader_should_return_a_column_if_no_tabs_present()
        {
            //arrange
            var mockStream = new Mock<TextReader>();
            mockStream.Setup(x => x.ReadLine()).Returns("This is a line with no tabs").Verifiable();
            var tsvReader = new TSVReader(mockStream.Object);

            //act
            var result = tsvReader.ReadLine();

            //assert
            result.Should().NotBeNull("There is something in the stream");
            result.Count.Should().Be(1, "There is text but no tab");
            result[0].Should().Be("This is a line with no tabs");

            mockStream.Verify(x => x.ReadLine(), Times.Once);
        }

        [TestCase]
        public void TSVReader_should_return_multiple_columns_if_tabs_are_present()
        {
            //arrange
            var mockStream = new Mock<TextReader>();
            mockStream.Setup(x => x.ReadLine())
                .Returns("Shelby Macias\t3027 Lorem St.| Kokomo | Hertfordshire | L9T 3D5 | England\t1 66 890 3865 - 9584\tet@eratvolutpat.ca").Verifiable();
            var tsvReader = new TSVReader(mockStream.Object);

            //act
            var result = tsvReader.ReadLine();

            //assert
            result.Should().NotBeNull("There is something in the stream");
            result.Count.Should().Be(4, "There are 4 \\\\t's");
            result[0].Should().Be("Shelby Macias");
            result[1].Should().Be("3027 Lorem St.| Kokomo | Hertfordshire | L9T 3D5 | England");
            result[2].Should().Be("1 66 890 3865 - 9584");
            result[3].Should().Be("et@eratvolutpat.ca");

            mockStream.Verify(x => x.ReadLine(), Times.Once);
        }

        [TestCase]
        public void TSVReader_should_not_strip_or_ignore_multiple_tabs()
        {
            //arrange
            var mockStream = new Mock<TextReader>();
            mockStream.Setup(x => x.ReadLine())
                .Returns("Porter Coffey\t\tAp #827-9064 Sapien. Rd.|Palo Alto|Fl.|HM0G 0YR|Scotland\t1 80 177 2329-1167\t\tCras@semperpretiumneque.ca").Verifiable();
            var tsvReader = new TSVReader(mockStream.Object);

            //act
            var result = tsvReader.ReadLine();

            //assert
            result.Should().NotBeNull("There is something in the stream");
            result.Count.Should().Be(6, "There are 6 \\\\t's");
            result[0].Should().Be("Porter Coffey");
            result[1].Should().Be("", "It is an empty tab");
            result[2].Should().Be("Ap #827-9064 Sapien. Rd.|Palo Alto|Fl.|HM0G 0YR|Scotland");
            result[3].Should().Be("1 80 177 2329-1167");
            result[4].Should().Be("");
            result[5].Should().Be("Cras@semperpretiumneque.ca");

            mockStream.Verify(x => x.ReadLine(), Times.Once);
        }
    }
}
