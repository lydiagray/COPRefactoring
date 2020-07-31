using System.Collections.Generic;

namespace COPRefactoring
{
    public class ResultModel
    {
        // The number of times each letter of the alphabet occurs in the input (there should be a Letter Occurrence for each letter of the alphabet)
        public List<LetterOccurrence> LetterOccurrences { get; set; }

        // A list of every word in the input and how many times it occurs
        public List<RepeatedWord> RepeatedWords { get; set; }

        public int TotalWords { get; set; }

        // Alphabet only
        public int TotalLetters { get; set; }

        // Alphabet only
        public int TotalWordsWithMoreThanFiveLetters { get; set; }

        public int TotalCapitalLetters { get; set; }

        // Input words reversed eg. "One two three" would become "three two One"
        public string WordsLastToFirst { get; set; }

        // Replace each letter(only A-Za-z) with its number equivalent eg aA = 1, bB = 2 etc"
        public string CodedWord { get; set; }
    }

    public class LetterOccurrence
    {
        public string Letter { get; set; }

        public int Frequency { get; set; }
    }

    public class RepeatedWord
    {
        public string Word { get; set; }

        public int Frequency { get; set; }
    }
}
