using Figgle;

namespace VocabularyTrainer;

/// <summary>
///     Vocabulary trainer.
///     Based on a supplied vocabulary training cycles are performed.
/// </summary>
public sealed class Trainer
{
    private const int WORD_IDX = 0;
    private const int TRANSLATION_IDX = 1;
    private const int CYCLE_COUNT = 3;
    private readonly VocabularyItem[] _vocabularyItems;

    /// <summary>
    ///     Constructs a new <see cref="Trainer"/> instance based on the given vocabulary.
    /// </summary>
    /// <param name="wordsAndTranslations">Raw vocabulary read from a file</param>
    public Trainer(string[][] wordsAndTranslations)
    {
        this._vocabularyItems = CreateVocabularyItems(wordsAndTranslations);
    }

    /// <summary>
    ///     Performs a training cycle for <see cref="CYCLE_COUNT"/> words.
    /// </summary>
    public void PerformTrainingCycle()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine(FiggleFonts.Standard.Render("Vocabulary Trainer"));
        Console.WriteLine($"Starting a new training cycle with {CYCLE_COUNT} tries ...");
        var alreadyAsked = new bool[_vocabularyItems.Length];
        
        for (var i = 0; i < CYCLE_COUNT; i++)
        {
            int index = PickNextWord(alreadyAsked);
            VocabularyItem vocab = _vocabularyItems[index];
            
            PrintVocab(vocab);
            
            string answer = Console.ReadLine()!;
            bool isRight = vocab.TestTranslation(answer);
            
            UserAnwer(vocab, isRight);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        
        }
        Console.WriteLine();
    }


    private void UserAnwer(VocabularyItem vocab, bool isRight)
    {
        if (isRight)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("OK!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"No, {vocab.NativeWord} = {vocab.Translation}");
        }
    }

    private void PrintVocab(VocabularyItem vocab)
    {
        string spaces = new string(' ', 10 - vocab.NativeWord.Length);
        Console.Write(vocab.NativeWord + spaces + "= ");
        Console.ResetColor();
    }
    /// <summary>
    ///     Prints training statistics to the terminal.
    /// </summary>
    public void PrintStatistics()
    {
        Sort();

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine(FiggleFonts.Standard.Render("Training Statistics"));
        Console.WriteLine($"{"English",-10} {"German",-10} {"Asked",-5} {"Correct",-7}");
        Console.WriteLine(new string('-', 35));
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        foreach (var vocabularyItem in this._vocabularyItems)
        {
            Console.WriteLine(vocabularyItem);
        }
        Console.WriteLine();
        Console.ResetColor();
    }

    /// <summary>
    ///     Picks the next word by random.
    ///     If all words have already been used any one is chosen.
    /// </summary>
    /// <param name="alreadyAsked">An array of flags indicating which words have already been used</param>
    /// <returns>Index of next word to use</returns>
    private int PickNextWord(bool[] alreadyAsked)
    {
        Random random = new Random();
        for (int i = 0; i < alreadyAsked.Length + 1000; i++)
        {
            int voc = random.Next(0, alreadyAsked.Length);
            if (!alreadyAsked[voc])
            {
                alreadyAsked[voc] = true;
                return voc;
            }
                
        }
        return -1;
    }

    /// <summary>
    ///     Sorts the vocabulary items using the CompareTo method of the <see cref="VocabularyItem"/> class.
    /// </summary>
    private void Sort()
    {
        for(int i = 0; i < _vocabularyItems.Length; i++) 
        {
            for(int j = i + 1; j < _vocabularyItems.Length; j++)
            {
                if (_vocabularyItems[i].CompareTo(_vocabularyItems[j]) < 0)
                {
                    (_vocabularyItems[i], _vocabularyItems[j]) = (_vocabularyItems[j], _vocabularyItems[i]);
                }
            }
        }
    }

    /// <summary>
    ///     Creates a <see cref="VocabularyItem"/> array from the raw words.
    /// </summary>
    /// <param name="wordsAndTranslations">Raw vocabulary read from a file</param>
    /// <returns>A <see cref="VocabularyItem"/> for each (valid) word</returns>
    private static VocabularyItem[] CreateVocabularyItems(string[][] wordsAndTranslations)
    {
        VocabularyItem[] vocabItems = new VocabularyItem[wordsAndTranslations.GetLength(0)];

        for (int i = 0; i < vocabItems.GetLength(0); i++)
        {
            vocabItems[i] = new(wordsAndTranslations[i][0], wordsAndTranslations[i][1]);
        }
        
        return vocabItems;
    }
}