using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DadJoke
{
    public class JokeAttributes
    {
        public string current_page { get; set; }
        public string limit { get; set; }
        public string next_page { get; set; }
        public string previous_page { get; set; }
        public List<Joke> results { get; set; }
        public string search_term { get; set; }
        public string status { get; set; }
        public string total_jokes { get; set; }
        public string total_pages { get; set; }
    }

    public class Joke
    {
        public string id { get; set; }
        public string joke { get; set; }
    }

    public class JokeData
    {
        public string Id { get; set; }
        public string Joke { get; set; }
        public int WordCount { get; set; }
    }

    class JokeController
    {
        static async Task<Joke> GetRandomJoke()
        {
            Joke joke = null;

            HttpResponseMessage response = await client.GetAsync(client.BaseAddress.ToString());
            if (response.IsSuccessStatusCode)
            {
                joke = await response.Content.ReadAsAsync<Joke>();
            }
            return joke;
        }

        static async Task<List<Joke>> GetJokesBySearch(string searchString)
        {
            List<Joke> jokes = null;
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress.ToString() +
                "search?term=" +
                searchString +
                "&limit=30");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                JokeAttributes jsonJokes = JsonConvert.DeserializeObject<JokeAttributes>(jsonString);
                jokes = jsonJokes.results;
            }
            return jokes;
        }

        static HttpClient client = new HttpClient();
        public static async Task RunAsync(string searchString)
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("https://icanhazdadjoke.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Dictionary<int, HashSet<JokeData>> jokeDataSetByGroup = new Dictionary<int, HashSet<JokeData>>();
            HashSet<JokeData> jokeHash = new HashSet<JokeData>();
            try
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    //Call get list of jokes with search string.
                    List<Joke> jokeList = await GetJokesBySearch(searchString);
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

                        jokeHash.Add(jokeData);
                        if (wordCount <= 10)
                        {
                            jokeDataSetByGroup.Add(1, jokeHash);
                        }
                        else if (wordCount > 10 && wordCount <= 20)
                        {
                            jokeDataSetByGroup.Add(2, jokeHash);
                        }
                        else
                        {
                            jokeDataSetByGroup.Add(3, jokeHash);
                        }

                    }

                    Console.WriteLine("Group 1 -- <= 10");
                    jokeDataSetByGroup.TryGetValue(1, out HashSet<JokeData> group1);
                    //Console.WriteLine(jokeWithUpperSearch);
                    foreach (JokeData jokeData1 in group1)
                    {
                        Console.WriteLine(jokeData1.Joke);
                    }

                    Console.WriteLine("Group 2 -- > 10 and < 20");
                    jokeDataSetByGroup.TryGetValue(2, out HashSet<JokeData> group2);
                    foreach (JokeData jokeData2 in group2)
                    {
                        Console.WriteLine(jokeData2.Joke);
                    }

                    Console.WriteLine("Group 3 -- >= 20");
                    jokeDataSetByGroup.TryGetValue(3, out HashSet<JokeData> group3);
                    foreach (JokeData jokeData3 in group3)
                    {
                        Console.WriteLine(jokeData3.Joke);
                    }

                }
                else
                {
                    //Call random joke
                    Joke randomJoke = new Joke();
                    randomJoke = await GetRandomJoke();
                    Console.WriteLine(randomJoke.joke);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        private static int GetWordCountFromJoke(string joke)
        {
            int jokeLength = joke.Length;
            string buildWord = string.Empty;
            string buildJoke = string.Empty;
            int charIterator = 0;
            int wordCount = 0; //I need to store this somewhere.
            char[] jokeCharacters = joke.ToCharArray();
            while (charIterator < jokeLength)
            {
                char c = jokeCharacters[charIterator];
                if (c == ' ' && buildWord.Length > 0)
                {
                    wordCount++;
                    buildWord = string.Empty;
                }
                else
                {
                    buildWord += c;
                }

                if (charIterator == jokeLength - 1 && buildWord.Length > 0) //And not punctuation
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
    }
}
