namespace CsvFile;

public class CustomerCsvFileWriter
{
    private readonly IFileSystem _fileSystem;

    public CustomerCsvFileWriter(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public void Write(string fileName, IEnumerable<Customer> customers)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException("File name cannot be null or whitespace.", nameof(fileName));
        }

        foreach (var customer in customers)
        {
            _fileSystem.WriteLine(fileName, customer.ToString());
        }
    }
}
