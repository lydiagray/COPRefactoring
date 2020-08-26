﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace COPRefactoring
{
    public class COPMethods
    {
        // The challenge (turn Resharper off if you have it installed)
        // You can work individually, or in pairs if you prefer
        // This is terrible code. Your task is to make it better
        // The Analyse method takes a string input and outputs a ResultModel object.
        // The Analyse method does various things with the input to create the ResultModel object.
        // The Analyse method must remain and return the same object, but you can abstract the logic into private methods/classes if you want to.
        // There are unit tests that already pass, you don't need to change these, just make sure they still pass when you're done.
        // When you're happy with your refactor, push your changes to a branch with your name and we'll review the changes in another session.

        public ResultModel Analyse(string input) => new ResultModel
        {
            TotalWords = GetTotalWords(input),
            TotalLetters = GetTotalLetters(input),
            TotalWordsWithMoreThanFiveLetters = GetTotalWordsWithMoreThanFiveLetters(input),
            TotalCapitalLetters = GetTotalCapitalLetters(input),
            WordsLastToFirst = GetWordsFirstToLast(input),
            LetterOccurrences = GetLetterOccurences(input),
            RepeatedWords = GetRepeatedWords(input),
            CodedWord = GetCodedWord(input)
        };

        // Get total words
        public int GetTotalWords(string input)
        {
            var inputAsWordArray = input.Split();
            var totalWords = 0;
            foreach (var word in inputAsWordArray)
            {
                if (word.Length < 1) break;
                totalWords = totalWords + 1;
            }

            return totalWords;
        }

        // Get total letters
        public int GetTotalLetters(string input)
        {
            var regex = new Regex("[a-zA-Z]");
            var inputArray = input.ToCharArray();
            var totalLetters = 0;

            for (var i = 0; i < inputArray.Length; i++)
            {
                if (regex.IsMatch(inputArray[i].ToString()))
                {
                    totalLetters += 1;
                }
            }

            return totalLetters;
        }

        // TotalWordsWithMoreThanFiveLetters
        public int GetTotalWordsWithMoreThanFiveLetters(string input)
        {
            var totalWordsWithMoreThanFiveLetters = 0;
            for (var i = 0; i < input.Split().Length; i++)
            {
                var notLetterRegex = new Regex("[^a-zA-Z]");
                var strippedWord = notLetterRegex.Replace(input.Split()[i], "");

                var word = strippedWord.ToCharArray();

                if (word.Length > 5)
                {
                    totalWordsWithMoreThanFiveLetters++;
                }
            }

            return totalWordsWithMoreThanFiveLetters;
        }

        // TotalCapitalLetters
        public int GetTotalCapitalLetters(string input)
        {
            var upperCaseRegex = new Regex("[A-Z]");
            var totalUpperCaseLetters = 0;
            foreach (var letter in input.ToCharArray())
            {
                if (upperCaseRegex.IsMatch(letter.ToString()))
                {
                    totalUpperCaseLetters += 1;
                }
            }

            return totalUpperCaseLetters;
        }

        // WordsFirstToLast
        public string GetWordsFirstToLast(string input)
        {
            var reversedWordArray = new string[input.Split().Length];
            for (var i = 1; i <= input.Split().Length; i++)
            {
                reversedWordArray[input.Split().Length - i] = input.Split()[i - 1];
            }

            var reversedWordString = "";

            for (var i = 0; i < reversedWordArray.Length - 1; i++)
            {
                reversedWordString += reversedWordArray[i] + " ";
            }

            reversedWordString += reversedWordArray[reversedWordArray.Length - 1];

            return reversedWordString;
        }

        // LetterOccurrences
        public List<LetterOccurrence> GetLetterOccurences(string input)
        {
            var letterOccurrences = new List<LetterOccurrence>();
            var alphabet = "abcdefghijklmnopqrstuvwxyz";
            for (var i = 0; i < alphabet.Length; i++)
            {
                letterOccurrences.Add(new LetterOccurrence
                {
                    Letter = alphabet[i].ToString(),
                    Frequency = 0
                });
            }

            var regex = new Regex("[a-zA-Z]");

            foreach (var letter in input.ToCharArray())
            {
                if (regex.IsMatch(letter.ToString()))
                {
                    var currentLetter = letterOccurrences.FirstOrDefault(_ => _.Letter.Equals(letter.ToString().ToLower()));
                    if (currentLetter == null)
                    {
                        letterOccurrences.Add(new LetterOccurrence
                        {
                            Letter = letter.ToString().ToLower(),
                            Frequency = 1
                        });

                    }
                    else
                    {
                        letterOccurrences.Remove(currentLetter);
                        letterOccurrences.Add(new LetterOccurrence
                        {
                            Letter = currentLetter.Letter,
                            Frequency = currentLetter.Frequency + 1
                        });
                    }
                }
            }

            return letterOccurrences;
        }

        // RepeatedWords
        public List<RepeatedWord> GetRepeatedWords(string input)
        {
            var repeatedWords = new List<RepeatedWord>();
            var punctuationRegex = new Regex("[.,!?:;\"']");
            foreach (var word in input.Split())
            {
                var strippedWord = punctuationRegex.Replace(word, "");
                var currentWord = repeatedWords.FirstOrDefault(_ => _.Word.Equals(strippedWord.ToLower()));
                if (currentWord == null)
                {
                    repeatedWords.Add(new RepeatedWord
                    {
                        Word = strippedWord.ToLower(),
                        Frequency = 1
                    });
                }
                else
                {
                    repeatedWords.Remove(currentWord);
                    repeatedWords.Add(new RepeatedWord
                    {
                        Word = currentWord.Word,
                        Frequency = currentWord.Frequency + 1
                    });
                }
            }

            return repeatedWords;
        }

        // CodedWord
        public string GetCodedWord(string input)
        {
            var numbers = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26";
            var alphabetString = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            var numbersAsArray = numbers.Split(",");
            var alphabetAsArray = alphabetString.Split(",");
            var code = new Dictionary<string, int>();
            foreach (var number in numbersAsArray)
            {
                var numberAsInt = int.Parse(number);
                code.Add(alphabetAsArray[numberAsInt - 1], numberAsInt);
            }

            var inputAsCharArray = input.ToLower().ToCharArray();
            var codeResult = "";
            var alphabetRegex = new Regex("[a-zA-Z]");
            foreach (var character in inputAsCharArray)
            {
                if (alphabetRegex.IsMatch(character.ToString()))
                {
                    foreach (var pair in code)
                    {
                        if (pair.Key.Equals(character.ToString()))
                        {
                            codeResult = codeResult + pair.Value.ToString();
                        }
                    }
                }
                else
                {
                    codeResult = codeResult + character.ToString();
                }
            }

            return codeResult;
        }
    }
}
