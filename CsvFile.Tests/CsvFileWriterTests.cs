using NSubstitute;
using NUnit.Framework;
using Bogus;

namespace CsvFile.Tests;

[TestFixture]
public class CsvFileWriterTests
{
    [TestFixture]
    public class Write 
    {
        [Test]
        public void GivenOneCustomer_ShouldWriteCustomerDataAsCsvLineToProvidedFile()
        {
            // Arrange
            var customers = CustomerFaker.Generate(1);
            var fileSystem = CreateMockFileSystem();
            var sut = CreateCustomerCsvFileWriter(fileSystem);
            // Act
            sut.Write("customers.csv", customers);
            // Assert
            AssertCustomerWasWrittenToFile(fileSystem, "customers.csv", customers.First());
        }

        [Test]
        public void GivenTwoCustomers_ShouldWriteBothCustomersDataAsCsvLinesToProvidedFile()
        {
            // Arrange
            var customers = CustomerFaker.Generate(2);
            var fileSystem = CreateMockFileSystem();
            var sut = CreateCustomerCsvFileWriter(fileSystem);
            // Act
            sut.Write("cust.csv", customers);
            // Assert
            foreach(var customer in customers)
            {
                AssertCustomerWasWrittenToFile(fileSystem, "cust.csv", customer);
            }
        }

        [Test]
        public void GivenThreeCustomers_ShouldWriteAllCustomersDataAsCsvLinesToProvidedFile()
        {
            // Arrange
            var customers = CustomerFaker.Generate(3);
            var fileSystem = CreateMockFileSystem();
            var sut = CreateCustomerCsvFileWriter(fileSystem);
            // Act
            sut.Write("cust1.csv", customers);
            // Assert
            foreach(var customer in customers)
            {
                AssertCustomerWasWrittenToFile(fileSystem, "cust1.csv", customer);
            }
        }
        
        [Test]
        public void GivenThreeCustomers_ShouldWriteThreeLinesToFile()
        {
            // Arrange
            var customers = CustomerFaker.Generate(3);
            var fileSystem = CreateMockFileSystem();
            var sut = CreateCustomerCsvFileWriter(fileSystem);
            var writtenLines = new List<string>();
            fileSystem
                .When(fs => fs.WriteLine(Arg.Any<string>(), Arg.Any<string>()))
                .Do(call => writtenLines.Add(call.ArgAt<string>(1)));
            // Act
            sut.Write("cust2.csv", customers);
            // Assert
            Assert.That(writtenLines, Is.EqualTo(new[]
            {
                $"{customers[0].Name},{customers[0].ContactNumber}",
                $"{customers[1].Name},{customers[1].ContactNumber}",
                $"{customers[2].Name},{customers[2].ContactNumber}",
            }));
        }

        [Test]
        public void GivenNoCustomer_ShouldWriteNothing()
        {
            // Arrange
            var customers = CustomerFaker.Generate(0);
            var fileSystem = CreateMockFileSystem();
            var sut = CreateCustomerCsvFileWriter(fileSystem);
            var writtenLines = new List<string>();
            fileSystem
                .When(fs => fs.WriteLine(Arg.Any<string>(), Arg.Any<string>()))
                .Do(call => writtenLines.Add(call.ArgAt<string>(1)));
            // Act
            sut.Write("cust2.csv", customers);
            // Assert
            fileSystem.DidNotReceive().WriteLine(Arg.Any<string>(), Arg.Any<string>());
        }

        [TestCase("")]
        [TestCase(null)]
        public void GivenNoFileName_ShouldThrowAnException(string? fileName)
        {
            // Arrange
            var customers = CustomerFaker.Generate(1);
            var fileSystem = CreateMockFileSystem();
            var sut = CreateCustomerCsvFileWriter(fileSystem);
            var writtenLines = new List<string>();
            fileSystem
                .When(fs => fs.WriteLine(Arg.Any<string>(), Arg.Any<string>()))
                .Do(call => writtenLines.Add(call.ArgAt<string>(1)));
            // Act & Assert
            Assert.Throws<ArgumentException>(() => sut.Write(fileName, customers));
        }
    }

    private static void AssertCustomerWasWrittenToFile(IFileSystem fileSystem, string fileName, Customer customer)
    {
        fileSystem.Received(1).WriteLine(fileName, $"{customer.Name},{customer.ContactNumber}");
    }

    private static readonly Faker<Customer> CustomerFaker = new Faker<Customer>()
        .RuleFor(c => c.Name, f => f.Person.FullName)
        .RuleFor(c => c.ContactNumber, f => f.Phone.PhoneNumber("########"));

    private static IFileSystem CreateMockFileSystem()
    {
        return Substitute.For<IFileSystem>();
    }

    private static CustomerCsvFileWriter CreateCustomerCsvFileWriter(IFileSystem fileSystem)
    {
        return new CustomerCsvFileWriter(fileSystem);
    }
}
