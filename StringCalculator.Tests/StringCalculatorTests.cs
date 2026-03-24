using NUnit.Framework;

namespace StringCalculator.Tests;

[TestFixture]
public class StringCalculatorTests
{
    [TestFixture]
    public class Add
    {
        [TestCase("1", 1)]
        [TestCase("15", 15)]
        [TestCase("999", 999)]
        public void GivenNumber_ShouldReturnSameNumber(string input, int expected)
        {
            // Arrange
            var sut = new StringCalculator();
            // Act
            var result = sut.Add(input);
            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("1,2", 3)]
        [TestCase("5,15,20", 40)]
        [TestCase("999,1,11,2", 1013)]
        public void GivenManyNumbers_ShouldReturnSumOfNumbers(string input, int expected)
        {
            // Arrange
            var sut = new StringCalculator();
            // Act
            var result = sut.Add(input);
            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase(" ", 0)]
        [TestCase("    ", 0)]
        public void GivenNoNumbers_ShouldReturn0(string input, int expected)
        {
            // Arrange
            var sut = new StringCalculator();
            // Act
            var result = sut.Add(input);
            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("1,2,4", 7)]
        [TestCase("15,20,200,300", 535)]
        [TestCase("1,2,3,4,5,6,7,8,9,10,11,12,13,14", 105)]
        public void GivenUnknownNumberOfNumbers_ReturnSumOfNumbers(string input, int expected)
        {
            // Arrange
            var sut = new StringCalculator();
            // Act
            var result = sut.Add(input);
            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("1\n2", 3)]
        [TestCase("1\n2", 3)]
        [TestCase("15\n25\n2", 42)]
        public void GivenNewLineBetweenNumbers_ReturnSumOfNumbers(string input, int expected)
        {
            // Arrange
            var sut = new StringCalculator();
            // Act
            var result = sut.Add(input);
            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("//;\n1;2", 3)]
        [TestCase("///\n1/2", 3)]
        [TestCase("//;\n1;2", 3)]
        [TestCase("//;\n14;20;30", 64)]
        public void GivenDelimiterBeforeNumbers_ReturnSumOfNumbers(string input, int expected)
        {
            // Arrange
            var sut = new StringCalculator();
            // Act
            var result = sut.Add(input);
            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("-1", "Negatives not allowed: -1")]
        [TestCase("-63", "Negatives not allowed: -63")]
        [TestCase("-3, 1, -34, 45", "Negatives not allowed: -3, -34")]
        public void GivenNegatives_ReturnExceptionWithNegativeNumbers(string input, string expected)
        {
            // Arrange
            var sut = new StringCalculator();

            // Act & Assert
            Assert.That(
                () => sut.Add(input),
                Throws.Exception.With.Message.Contains(expected)
            );
        }

        [TestCase("29, 1001", 29)]
        [TestCase("10034, 1001, 3, 99, 32190", 102)]
        [TestCase("//;\n10034; 1001; 3; 99; 32190", 102)]
        public void GivenNumbersAbove1000_ReturnsSumOfAllNumbersBelow1000(string input, int expected)
        {
            // Arrange
            var sut = new StringCalculator();

            // Act
            var result = sut.Add(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
