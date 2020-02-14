using System;
using System.Collections.Generic;
using System.Linq;

namespace DadJoke
{
    class SearchJoke
    {
        private static readonly List<JokeData> _jokeDataList = new List<JokeData>();
        /// <summary>
        /// Loops through the joke list finding the keyword and uppercasing it on each joke.
        /// Gets the word count from the joke string.
        /// Separated the functionality here because each method is so different. I did not see enough of a crossover to
        /// warrant combining them into one method. Also, I needed to return a string in one and word count in the other.
        /// </summary>
        /// <param name="jokeList"></param>
        /// <param name="searchString"></param>
        public static void RunJokeSearch(List<Joke> jokeList, string searchString)
        {
            foreach (Joke dadJoke in jokeList)
            {
                string jokeWithUpperSearch = GetNewJokeStringWithUpperSearch(dadJoke.joke, searchString);
                int wordCount = GetWordCountFromJoke(jokeWithUpperSearch);
                JokeData jokeData = new JokeData
                {
                    Id = dadJoke.id,
                    Joke = jokeWithUpperSearch,
                    WordCount = wordCount
                };

                _jokeDataList.Add(jokeData);
                
            }

            Console.WriteLine("\nGroup 1 -- < 10");
            Console.WriteLine("===============");
            FilterGroup1();

            Console.WriteLine("\nGroup 2 -- >= 10 and < 20");
            Console.WriteLine("=========================");
            FilterGroup2();

            Console.WriteLine("\nGroup 3 -- >= 20");
            Console.WriteLine("================");
            FilterGroup3();

        }
        /// <summary>
        /// Takes the joke string and iterates through each character to get the word count.
        /// Decided to use letters and digits only to constitute words and spaces to indicate the end of a word.
        /// Used the string length - 1 to determine the ending of the string and to end the last word
        /// Used buildWord to make sure word had at least one character before counting it.
        /// </summary>
        /// <param name="joke"></param>
        /// <returns></returns>
        private static int GetWordCountFromJoke(string joke)
        {
            int jokeLength = joke.Length;
            string buildWord = string.Empty;
            int charIterator = 0;
            int wordCount = 0; 
            char[] jokeCharacters = joke.ToCharArray();
            while (charIterator < jokeLength)
            {
                char c = jokeCharacters[charIterator];
                if (Char.IsLetterOrDigit(c))
                {
                    buildWord += c;
                }
                else if (c == ' ' && buildWord.Length > 0)
                {
                    wordCount++;
                    buildWord = string.Empty;
                }

                if (charIterator == jokeLength - 1 && buildWord.Length > 0)
                {
                    wordCount++;
                    buildWord = string.Empty;
                }

                charIterator++;
            }

            return wordCount;
        }
        /// <summary>
        /// Start with converting joke and search strings to lower case. Then, to look specifically for the search string
        /// within the joke string. During testing, the icanhazdadjoke.com api was not case sensitive but the string.Replace() 
        /// call was case sensitive. Since the case was not important to the api, it seemed best to keep it consistent.
        /// 
        /// Each search string match was then uppercased within the lowercased joke string.
        /// 
        /// Then, I went through each character from this uppercased ( but mostly lowercased joke string) in a loop 
        /// to determine which ones were upper, so that they could be added to the new joke string being created. If the 
        /// upper case is not indicated, then it chooses the character from the original string.
        /// 
        /// After finishing the loop, it now has a new joke string with search terms uppercased.
        /// </summary>
        /// <param name="jokeString"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        private static string GetNewJokeStringWithUpperSearch(string jokeString, string searchString)
        {
            int jokeStringLength = jokeString.Length;
            char[] jokeStringChars = jokeString.ToCharArray();

            string jokeStringLower = jokeString.ToLower();
            string searchStringLower = searchString.ToLower();

            string jokeStringLowerWithSearchStringUppers = jokeStringLower.Replace(searchStringLower, searchString.ToUpper());

            char[] jokeStringLowerWithSearchStringUppersChars = jokeStringLowerWithSearchStringUppers.ToCharArray();
            string buildNewJokeString = string.Empty;
            int charIterator = 0;

            while (charIterator < jokeStringLength)
            {
                if (char.IsUpper(jokeStringLowerWithSearchStringUppersChars[charIterator]))
                {
                    buildNewJokeString += jokeStringLowerWithSearchStringUppersChars[charIterator];
                }
                else
                {
                    buildNewJokeString += jokeStringChars[charIterator];
                }

                charIterator++;
            }

            return buildNewJokeString;
        }

        /// <summary>
        /// Used linq to filter query for joke word count less than 10. And display it.
        /// Appended word count to make testing easier
        /// </summary>
        private static void FilterGroup1()
        {
            var group1 = from jokeData in _jokeDataList
                         where jokeData.WordCount < 10
                         select new { joke = jokeData.Joke, wordCount = jokeData.WordCount };
            
            foreach (var group in group1)
            {
                Console.WriteLine($"{ group.joke} ({group.wordCount})\n");
            }
        }
        /// <summary>
        /// Used linq to filter query for joke word count less than 20. And display it.
        /// Appended word count to make testing easier
        /// </summary>
        private static void FilterGroup2()
        {
            var group2 = from jokeData in _jokeDataList
                         where jokeData.WordCount >= 10 && jokeData.WordCount < 20
                         select new { joke = jokeData.Joke, wordCount = jokeData.WordCount };

            foreach (var group in group2)
            {
                Console.WriteLine($"{ group.joke} ({group.wordCount})\n");
            }
        }
        /// <summary>
        /// Used linq to filter query for joke word count more than 19. And display it.
        /// Appended word count to make testing easier
        /// </summary>
        private static void FilterGroup3()
        {
            var group3 = from jokeData in _jokeDataList
                         where jokeData.WordCount >= 20
                         select new { joke = jokeData.Joke, wordCount = jokeData.WordCount };

            foreach (var group in group3)
            {
                Console.WriteLine($"{ group.joke} ({group.wordCount})\n");
            }
        }
    }
}
