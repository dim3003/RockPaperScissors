namespace StringCalculator;

public class StringCalculator
{
    private const char DefaultDelimiter = ',';
    private const string CustomDelimiterPrefix = "//";

    public int Add(string numbers)
    {
        if (string.IsNullOrWhiteSpace(numbers))
            return 0;

        var (delimitedNumbers, delimiter) = GetDelimiter(numbers);
        var parsedNumbers = ParseNumbers(delimitedNumbers, delimiter);
        CheckForNegativeNumbers(parsedNumbers);

        return RemoveNumbersAbove1000(parsedNumbers).Sum();
    }

    private static IEnumerable<int> RemoveNumbersAbove1000(IEnumerable<int> parsedNumbers)
    {
        parsedNumbers = parsedNumbers.Where(n => n <= 1000);
        return parsedNumbers;
    }

    private static void CheckForNegativeNumbers(IEnumerable<int> parsedNumbers)
    {
        var negativeNumbers = parsedNumbers.Where(n => n < 0).ToArray();
        if (negativeNumbers.Any())
            throw new ArgumentException("Negatives not allowed: " + string.Join(", ", negativeNumbers));
    }

    private static IEnumerable<int> ParseNumbers(string numbers, char delimiter)
    {
        return numbers.Split(delimiter, '\n').Select(int.Parse);
    }

    private static (string numbers, char delimiter) GetDelimiter(string input)
    {
        if (!input.StartsWith(CustomDelimiterPrefix))
            return (input, DefaultDelimiter);

        var delimiter = input[CustomDelimiterPrefix.Length];
        var numbers = input.Substring(CustomDelimiterPrefix.Length + 2);

        return (numbers, delimiter);
    }
}
