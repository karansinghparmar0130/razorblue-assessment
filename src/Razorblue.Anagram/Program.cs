using Razorblue.Anagram;

Console.WriteLine("Task 1 - Anagram started");

while (true)
{
    Console.WriteLine("------------------------\n");
    Console.Write("Please provide input1: ");
    var input1 = Console.ReadLine();
    Console.Write("Please provide input2: ");
    var input2 = Console.ReadLine();

    try
    {
        var isAnagram = AnagramChecker.IsAnagram(input1, input2);
        Console.WriteLine($"Are both inputs Anagram? {isAnagram}\n");
    }
    catch (Exception ex)
    {
        // This should ideally be abstracted to prevent internal info leakage
        // Done only in this context
        Console.WriteLine($"Error: {ex.Message}\n");
    }
    finally
    {
        Console.WriteLine("Press Enter to continue or Ctrl-C to exit\n");
        Console.ReadKey(intercept: true);
    }
}
