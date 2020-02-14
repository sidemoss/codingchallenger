using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DadJoke
{
    class JokeController
    {
        static readonly HttpClient _client = new HttpClient();
        public static async Task RunAsync(string searchString)
        {
            // Update port # in the following line.
            _client.BaseAddress = new Uri("https://icanhazdadjoke.com/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    //Call get list of jokes with search string.
                    List<Joke> jokeList = await GetJokesBySearch(searchString);
                    SearchJoke.RunJokeSearch(jokeList, searchString);                }
                else
                {
                    //Call random joke
                    Joke randomJoke = await GetRandomJoke();
                    RandomJoke.DisplayRandomJoke(randomJoke.joke);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
        static async Task<Joke> GetRandomJoke()
        {
            Joke joke = null;

            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress.ToString());
            if (response.IsSuccessStatusCode)
            {
                joke = await response.Content.ReadAsAsync<Joke>();
            }
            return joke;
        }

        static async Task<List<Joke>> GetJokesBySearch(string searchString)
        {
            List<Joke> jokes = null;
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress.ToString() +
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
    }
}
