using Figgle;
using System.Text;
using VocabularyTrainer;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine($"*** Vocabulary Trainer ***{Environment.NewLine}");

var trainer = new Trainer(ReadTranslationsFromFile());
var option = 1;
do
{
    switch (option)
    {
        case 1: trainer.PerformTrainingCycle(); break;
        case 2: trainer.PrintStatistics(); break;
        // why don't we have to check for case 3 here?
        default: Console.WriteLine("Invalid option"); break;
    }
    option = ReadOption();
} while (option != 3);

PrintGoodbye();

static int ReadOption()
{
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine("(1) Another try?");
    Console.WriteLine("(2) Print Statistics");
    Console.WriteLine("(3) Quit");
    Console.Write("Please select an option: ");
    Console.ResetColor();

    int option;
    string? input;
    do
    {
        input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Invalid input, try again");
        }
    } while (!int.TryParse(input, out option) || option is < 1 or > 3);
    return option;
}

static void PrintGoodbye()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine(
        FiggleFonts.Standard.Render("Bye!"));
    Console.Write("Press any key to finish ... ");
    Console.ResetColor();
    Console.ReadKey();
}

static string[][] ReadTranslationsFromFile()
{
    // we assume the file exists and is valid - this time
    var lines = File.ReadAllLines("Data/translations.csv");
    var words = new string[lines.Length - 1][];
    for (var i = 0; i < words.Length; i++)
    {
        var line = lines[i + 1];
        var parts = line.Split(';');
        words[i] = parts;
    }

    return words;
}