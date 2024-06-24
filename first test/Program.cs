using System;
using System.IO;
using System.Linq;

class Program
{
    private static int first_number = 0;
    private static int second_number = 0;
    private static string symbol = "";
    private static string[] validSymbols = { "+", "-", "*", "/", "%", "^", "sqrt" };
    private static string directoryPath = @"C:\Users\begla\source\repos\first test";
    private static string filePath = Path.Combine(directoryPath, "answers.txt");

    static void Main()
    {
        EnsureFileExists();


        while (true)
        {
            Console.WriteLine("Enter an expression (enter '0' to stop): ");
            string input = Console.ReadLine();

            if (input == "0")
            {
                break;
            }

            string[] expression = input.Split();

            if (expression.Length < 2 || expression.Length > 3 || !validSymbols.Contains(expression[1]) || (expression.Length == 3 && expression[1] == "sqrt"))
            {
                Console.WriteLine("Invalid expression format. Please enter a valid expression.");
                AddResultToFile("Invalid expression format. Please enter a valid expression.");

                continue;
            }

            symbol = expression[1];

            if (expression.Length == 2 && symbol == "sqrt")
            {
                try
                {
                    first_number = Convert.ToInt32(expression[0]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid input for the number. Please enter a valid number.");
                    AddResultToFile(ex.ToString());

                    continue;
                }
            }
            else if (expression.Length == 2 && symbol != "sqrt")
            {
                Console.WriteLine("Enter the second number: ");
                string secondInput = Console.ReadLine();

                try
                {
                    first_number = Convert.ToInt32(expression[0]);
                    second_number = Convert.ToInt32(secondInput);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid input for one of the numbers. Please enter a valid number.");
                    AddResultToFile(ex.ToString());

                    continue;
                }
            }
            else if (expression.Length == 3 && symbol != "sqrt")
            {
                try
                {
                    first_number = Convert.ToInt32(expression[0]);
                    second_number = Convert.ToInt32(expression[2]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("One of the numbers is not correct. Please enter a valid number.");
                    AddResultToFile(ex.ToString());
                    continue;
                }
            }

            string result = CalculateResult(symbol, first_number, second_number);
            Console.WriteLine(result);
            AddResultToFile(result);
        }
    }

    static void EnsureFileExists()
    {
        if (!File.Exists(filePath))
        {
            try
            {
                using (FileStream fs = File.Create(filePath))
                {
                    Console.WriteLine($"File created successfully at {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating the file: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"File already exists at {filePath}");
        }
    }

    static string CalculateResult(string symbol, int first_number, int second_number)
    {
        switch (symbol)
        {
            case "+":
                return $"{first_number} + {second_number} = {first_number + second_number}";
            case "-":
                return $"{first_number} - {second_number} = {first_number - second_number}";
            case "*":
                return $"{first_number} * {second_number} = {first_number * second_number}";
            case "/":
                return second_number == 0 ? "Error: Division by zero." : $"{first_number} / {second_number} = {(first_number / (double)second_number)}";
            case "%":
                return $"{first_number} % {second_number} = {first_number % second_number}";
            case "^":
                return $"{first_number} ^ {second_number} = {Math.Pow(first_number, second_number)}";
            case "sqrt":
                return $"sqrt({first_number}) = {Math.Sqrt(first_number)}";
            default:
                return "Symbol is not recognized.";
        }
    }

    static void AddResultToFile(string result)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(result);
                writer.WriteLine(); 
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to file: {ex.Message}");
        }
    }
}
