using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DadJoke
{
    class SearchJoke
    {
        private static readonly List<JokeData> _jokeDataList = new List<JokeData>();
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
        /// 
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
