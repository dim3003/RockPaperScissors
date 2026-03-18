using NUnit.Framework;

namespace FizzBuzz.Tests;

[TestFixture]
public class FizzBuzzerTests
{
    [TestFixture]
    public class Go
    {
        [TestFixture]
        public class WhenNumberDivisibleBy3
        {
            [TestCase(3)]
            [TestCase(21)]
            [TestCase(99)]
            public void ShouldReturnFizz(int number)
            {
                // Arrange
                var sut = new FizzBuzzer();
                // Act
                var actual = sut.Go(number);
                // Assert
                Assert.That(actual, Is.EqualTo("Fizz"));
            }
        }
    }

}
