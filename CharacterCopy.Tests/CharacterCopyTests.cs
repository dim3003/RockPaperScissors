using NSubstitute;
using NUnit.Framework;

namespace CharacaterCopy.Tests;

[TestFixture]
public class CharacterCopyTests
{
    [TestFixture]
    public class Copy
    {
        private static ISource CreateSource(char firstChar, char[] nextChars)
        {
            var source = Substitute.For<ISource>();
            source.ReadChar().Returns(firstChar, nextChars);
            return source;
        }

        [TestCase('a', '\n')]
        [TestCase('!', '\n')]
        [TestCase('B', '\n')]
        public void GivenSingleCharacterBeforeNewLine_ShouldWriteThatCharacter(
            char firstChar,
            params char[] nextChars)
        {
            // Arrange
            ISource source = CreateSource(firstChar, nextChars);

            var destination = Substitute.For<IDestination>();

            var sut = new Copier(source, destination);

            // Act
            sut.Copy();

            // Assert
            destination.Received(1).WriteChar(firstChar);
            destination.Received(1).WriteChar(Arg.Any<char>());
        }


        [TestCase('a', 'x', '\n')]
        [TestCase('u', 'v', 'e', 'm', 'd', '\n')]
        [TestCase('!', ',', '2', 'd', '\n')]
        public void GivenManyCharactersBeforeNewLine_ShouldWriteThoseCharacters(
            char firstChar,
            params char[] nextChars)
        {
            // Arrange
            ISource source = CreateSource(firstChar, nextChars);

            var destination = Substitute.For<IDestination>();

            var sut = new Copier(source, destination);

            // Act
            sut.Copy();

            // Assert
            var allExpected = nextChars.Prepend(firstChar).Where(c => c != '\n');
            foreach (var group in allExpected.GroupBy(c => c))
            {
                destination.Received(group.Count()).WriteChar(group.Key);
            }
            destination.DidNotReceive().WriteChar('\n');
        }

        [TestCase('a', 'x', '\n')]
        [TestCase('u', 'v', 'e', 'm', 'd', '\n')]
        [TestCase('!', ',', '2', 'd', '\n')]
        public void GivenManyCharactersBeforeNewLine_ShouldWriteThoseCharactersInOrder(
            char firstChar,
            params char[] nextChars)
        {
            // Arrange
            ISource source = CreateSource(firstChar, nextChars);

            var destination = Substitute.For<IDestination>();

            var sut = new Copier(source, destination);

            // Act
            sut.Copy();

            // Assert
            var allExpected = nextChars.Prepend(firstChar).Where(c => c != '\n');
            Received.InOrder(() =>
            {
                foreach (var c in allExpected)
                {
                    destination.WriteChar(c);
                }
            });
            destination.DidNotReceive().WriteChar('\n');
        }

        [TestCase('a', 'a', '\n')]
        [TestCase('!', ',', '2', '2', 's', '\n')]
        public void GivenRepeatedCharactersBeforeNewLine_ShouldWriteThoseCharacters(
            char firstChar,
            params char[] nextChars)
        {
            // Arrange
            ISource source = CreateSource(firstChar, nextChars);

            var destination = Substitute.For<IDestination>();

            var sut = new Copier(source, destination);

            // Act
            sut.Copy();

            // Assert
            var allExpected = nextChars.Prepend(firstChar).TakeWhile(c => c != '\n');
            foreach (var group in allExpected.GroupBy(c => c))
            {
                destination.Received(group.Count()).WriteChar(group.Key);
            }
            destination.DidNotReceive().WriteChar('\n');
        }

        [TestCase('a', '\n', 'b')]
        [TestCase('!', ',', '2', 'f', 's', '\n', 'u', 'u')]
        public void GivenCharactersAfterNewLine_ShouldWriteOnlyCharactersBeforeNewLine(
            char firstChar,
            params char[] nextChars)
        {
            // Arrange
            ISource source = CreateSource(firstChar, nextChars);

            var destination = Substitute.For<IDestination>();

            var sut = new Copier(source, destination);

            // Act
            sut.Copy();

            // Assert
            var allExpected = nextChars.Prepend(firstChar).TakeWhile(c => c != '\n');
            foreach (var group in allExpected.GroupBy(c => c))
            {
                destination.Received(group.Count()).WriteChar(group.Key);
            }
            destination.DidNotReceive().WriteChar('\n');
        }



        [Test]
        public void GivenNoCharacterBeforeNewLine_ShouldWriteNothing()
        {
            // Arrange
            ISource source = CreateSource('\n', []);

            var destination = Substitute.For<IDestination>();

            var sut = new Copier(source, destination);

            // Act
            sut.Copy();

            // Assert
            destination.DidNotReceive().WriteChar('\n');
            destination.DidNotReceive().WriteChar(Arg.Any<char>());
        }
    }
}