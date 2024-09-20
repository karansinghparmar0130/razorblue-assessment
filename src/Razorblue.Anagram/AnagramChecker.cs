namespace Razorblue.Anagram;

public static class AnagramChecker
{
    private const string Input1Identifier = "input1";
    private const string Input2Identifier = "input2";
    
    public static bool IsAnagram(string? input1, string? input2)
    {
        // Attaching source name to generalise logging
        var listOfCharacterFromInput1 = ConvertInputCharListForComparison(input: input1, source: Input1Identifier);
        var listOfCharacterFromInput2 = ConvertInputCharListForComparison(input: input2, source: Input2Identifier);

        // Checks for length and character match in sequence
        return listOfCharacterFromInput1.SequenceEqual(listOfCharacterFromInput2);
    }
    
    private static IEnumerable<char> ConvertInputCharListForComparison(string? input, string source)
    {
        // We can check and filter out whitespaces using string.IsNullOrWhiteSpace
        // but that would mean we are iterating twice as it internally loop through and checks each character
        // https://github.com/dotnet/runtime/blob/5535e31a712343a63f5d7d796cd874e563e5ac14/src/libraries/System.Private.CoreLib/src/System/String.cs#L504
        if (string.IsNullOrEmpty(input))
            throw new ArgumentException($"Value of {source} should be provided");
        
        var listOfChar = new List<char>();
        
        //  Changing case to facilitate case insensitive comparision
        foreach (var character in input.ToLower())
        {
            // Ignoring whitespaces to support anagram phrases
            if (char.IsWhiteSpace(character))
                continue;
            
            // Restricting inputs to letters only as per below definition
            // Anagram: A word or phrase made by using the letters of another word or phrase in a different order
            if (!char.IsLetter(character))
                throw new ArgumentException($"Character: {character} not supported, value of {source} should contain letters only");

            listOfChar.Add(character);
        }

        // If list generated after validation is empty, this means input contains only whitespaces
        if (listOfChar.Count is 0)
            throw new ArgumentException($"Value of {source} should be provided");
        
        // Sort for comparision
        listOfChar.Sort();
        
        return listOfChar;
    }
}
