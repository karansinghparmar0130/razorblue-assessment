Console.WriteLine("Task 4 - Fizz Buzz");

while (true)
{
    Console.WriteLine("------------------------\n");
    
    try
    {
        // Create an array of integers from 1 to 100
        int[] numbers = new int[100];
        for (int i = 0; i < 100; i++)
        {
            numbers[i] = i + 1;
        }

        for (int k = 0; k < numbers.Length; k++)
        {
            for (int j = 0; j < numbers.Length; j++)
            {
                if (k == j)
                {
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        if (i == j)
                        {
                            string fizz = "Fizz";
                            string buzz = "Buzz";
                            string fizzbuzz = "FizzBuzz";

                            if (numbers[i] % 3 is 0 && numbers[i] % 5 is 0)
                            {
                                string output = "";
                                foreach (char c in fizzbuzz)
                                {
                                    output += c;
                                }

                                Console.WriteLine(output);
                            }
                            else if (numbers[i] % 3 is 0)
                            {
                                string output = "";
                                foreach (char c in fizz)
                                {
                                    output += c;
                                }

                                Console.WriteLine(output);
                            }
                            else if (numbers[i] % 5 is 0)
                            {
                                string output = "";
                                foreach (char c in buzz)
                                {
                                    output += c;
                                }

                                Console.WriteLine(output);
                            }
                            else
                            {
                                string output = "";
                                foreach (char c in numbers[i].ToString())
                                {
                                    output += c;
                                }

                                Console.WriteLine(output);
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine();
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
