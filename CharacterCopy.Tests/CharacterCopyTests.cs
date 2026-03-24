using NSubstitute;
using NUnit.Framework;

namespace CharacaterCopy.Tests;

[TestFixture]
public class CharacterCopyTests
{
    [TestCase('a', '\n')]
    [TestCase('!', '\n')]
    [TestCase('B', '\n')]
    public void Copy_GivenSingleCharacterBeforeNewLine_ShouldWriteThatCharacter(
        char firstChar,
        params char[] nextChars)
    {
        // Arrange
        var source = Substitute.For<ISource>();

        var destination = Substitute.For<IDestination>();

        var sut = new Copier(source, destination);

        // Act
        sut.Copy();

        // Assert
        destination.Received(1).WriteChar(firstChar);
        destination.Received(1).WriteChar(Arg.Any<char>());
    }
}
