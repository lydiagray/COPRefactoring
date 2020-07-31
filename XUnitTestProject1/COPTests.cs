using System.Linq;
using System.Runtime.InteropServices;
using COPRefactoring;
using Xunit;

namespace XUnitTestProject1
{
	public class COPTests
	{
		private readonly COPMethods methods = new COPMethods();

        [Theory]
        [InlineData("", 0)]
        [InlineData("This sentence has five words", 5)]
        [InlineData("Curabitur id consequat justo. Sed egestas viverra pharetra. Proin et tempus libero. Phasellus et eros turpis. Suspendisse quis auctor.", 19)]
        public void Analyse_ShouldReturnTotalNumberOfWords(string input, int expectedResult)
        {
            var result = methods.Analyse(input).TotalWords;

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("", 0)]
        [InlineData("A bee sits", 8)]
        [InlineData("I c@n't 5p3@k pr0p3r1y", 11)]
        public void Analyse_ShouldReturnTotalNumberOfLetters(string input, int expectedResult)
        {
            var result = methods.Analyse(input).TotalLetters;

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("Sentence with two words over five letters", 2)]
        [InlineData("I don't have any words with more than five", 0)]
        [InlineData("I'm flabbergasted!!! How very dare they?!", 1)]
        public void Analyse_ShouldReturnTotalNumberOfWordsWithMoreThanFiveLetters(string input, int expectedResult)
        {
            var result = methods.Analyse(input).TotalWordsWithMoreThanFiveLetters;

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("I AM SHOUTING THIS MESSAGE!", 22)]
        [InlineData("i like to whisper everything i speak", 0)]
        public void Analyse_ShouldReturnTotalNumberOfCapitalLetters(string input, int expectedResult)
        {
            var result = methods.Analyse(input).TotalCapitalLetters;

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("One two three four five", "five four three two One")]
        public void Analyse_ShouldReturnWordsInReverseOrder(string input, string expectedResult)
        {
            var result = methods.Analyse(input).WordsLastToFirst;

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("A bee", "E", 2)]
        [InlineData("A bee", "z", 0)]
        [InlineData("A bee named Barry who lives in Belfast and breeds bears", "b", 5)]
        public void Analyse_ShouldReturnCorrectFrequencyOfLetterOccurrences(string input, string letter, int expectedResult)
        {
            var result = methods.Analyse(input).LetterOccurrences.FirstOrDefault(_ => _.Letter.Equals(letter.ToLower()));

            Assert.Equal(expectedResult, result.Frequency);
        }

        [Theory]
        [InlineData("The rides at the fair were not very fair at all", "fair", 2)]
        [InlineData("There were rides at the fair, but queues were fair!", "fair", 2)]
        [InlineData("Fair! Fair. Fair, fair? 'fair', \"fair\" fair: fair; fair:fair", "fair", 8)]
        public void Analyse_ShouldReturnCorrectFrequencyOfRepeatedWords_WhereWordExists(string input, string word, int expectedResult)
        {
            var result = methods.Analyse(input).RepeatedWords.FirstOrDefault(_ => _.Word.Equals(word.ToLower()));

            Assert.Equal(expectedResult, result.Frequency);
        }

        [Theory]
        [InlineData("Codeword", "315452315184")]
        [InlineData("Stop! I have punctuation.", "19201516! 9 81225 1621143202112091514.")]
        public void Analyse_ShouldReturnCorrectCode(string input, string expectedResult)
        {
            var result = methods.Analyse(input).CodedWord;

            Assert.Equal(expectedResult, result);
        }
	}
}
