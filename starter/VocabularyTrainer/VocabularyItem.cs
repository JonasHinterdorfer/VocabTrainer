namespace VocabularyTrainer;

/// <summary>
///     Represents one word in the vocabulary
/// </summary>
public sealed class VocabularyItem
{
    private int _countCorrect;
    private int _countAsked;
    public string NativeWord { get; }
    public string Translation { get; }

    public VocabularyItem(string native, string translation)
    {
        this.NativeWord = native;
        this.Translation = translation;
        _countAsked = 0;
        _countCorrect = 0;
    }

    /// <summary>
    /// A translation attempt is checked for correctness.
    /// </summary>
    /// <param name="translationAttempt">The user provided translation</param>
    /// <returns>True if the translation was correct; false otherwise</returns>
    public bool TestTranslation(string translation)
    {
        _countAsked++;

        if (translation == this.Translation)
        {
            _countCorrect++;
            return true;
        }
        
        return false;
    }

    /// <summary>
    ///The vocabulary item is compared to another. First the number of correct answers is compared.
    ///If it is equal the native words are compared ordinal.
    /// </summary>
    /// <param name="vocabularyItem">The <see cref="VocabularyItem"/> to compare with</param>
    /// <returns>0 if equal; less than 0 if this item is smaller; greater than 0 otherwise</returns>
    public int CompareTo(VocabularyItem vocabularyItem)
    {
        if (this._countCorrect != vocabularyItem._countCorrect)
        {
            return vocabularyItem._countCorrect - _countCorrect;
        }

        return CompareStrings(this.NativeWord, vocabularyItem.NativeWord);
    }

    /// <summary>
    ///Overrides the default string representation to display the word and translation statistics.
    /// </summary>
    /// <returns>A string containing the word, its translation and the training statistics</returns>
    public override string ToString()
    {
        string spaces = new(' ',11- NativeWord.Length);
        string output = NativeWord + spaces;
        spaces = new(' ',11- Translation.Length);
        output += Translation + spaces;
        spaces = new(' ',6- _countAsked.ToString().Length);
        output += _countAsked+ spaces;
        output += _countCorrect;
        
        return output;
    }

    /// <summary>
    /// Compares two strings by ordinal value, ignoring case.
    /// </summary>
    /// <param name="a">First string</param>
    /// <param name="b">Second string</param>
    /// <returns>Less than 0 if a precedes b in the sorting order; greater than 0 if b precedes a; 0 otherwise</returns>
    public static int CompareStrings(string a, string b)
    {
        return String.CompareOrdinal(a.ToLower(), b.ToLower());
    }
}