namespace Razorblue.Anagram.Tests;

public class AnagramCheckerTests
{
    [Theory]
    [InlineData("rescue", "Secure", true)] // Single word
    [InlineData("rail Safety", "fairy tales", true)]
    [InlineData("My name is hero", "mayhem senior", true)] // Phrase
    [InlineData("Test", "rest", false)]
    [InlineData("happy", "happy", true)]
    public void Should_properly_identify_anagram_When_correct_inputs_are_provided(string? input1, string? input2,
        bool isAnagram)
    {
        // Act
        var actual = AnagramChecker.IsAnagram(input1: input1, input2: input2);

        // Assert
        Assert.Equal(expected: isAnagram, actual);
    }

    [Theory]
    [InlineData("  ", "Secure", "Value of input1 should be provided")] // Whitespace
    [InlineData("", "Secure", "Value of input1 should be provided")] // Empty
    [InlineData(null, "Secure", "Value of input1 should be provided")] // Null
    [InlineData(".", "test", "Character: . not supported, value of input1 should contain letters only")] // Not a letter
    [InlineData("rescue", "  ", "Value of input2 should be provided")] // Whitespace
    [InlineData("rescue", "", "Value of input2 should be provided")] // Empty
    [InlineData("rescue", null, "Value of input2 should be provided")] // Null
    [InlineData("rescue", ".", "Character: . not supported, value of input2 should contain letters only")] // Not a letter
    public void Should_throw_argument_exception_When_incorrect_inputs_are_provided(string? input1, string? input2,
        string exceptionMessage)
    {
        // Act & Assert
        var actual = Assert.Throws<ArgumentException>(() => AnagramChecker.IsAnagram(input1: input1, input2: input2));
        Assert.Equal(expected: exceptionMessage, actual.Message);
    }
}
