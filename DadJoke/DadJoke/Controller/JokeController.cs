using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DadJoke
{
    /// <summary>
    /// Keeping all api calls to icanhazdadjoke.com within the controller
    /// </summary>
    class JokeController
    {
        static readonly HttpClient _client = new HttpClient();
        /// <summary>
        /// Decide between random joke or list of jokes by search. Random joke is chosen by supplying empty string.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
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
                    //List<Joke> jokeList = await GetJokesBySearch(searchString);

                    var response = await CallBySearch(searchString);
                    if (response.IsSuccessStatusCode)
					{
                        var json = GetJsonFromResponse(response);
                        IEnumerable<Joke> jokeList = (IEnumerable<Joke>)GetJokes(json.Result);
                        SearchJoke.RunJokeSearch(jokeList, searchString);
                    }
                }
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
        /// <summary>
        /// The api call to get a random joke. Just a call to the base address.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// The api call to get a list of dad jokes based on a search string.
        /// Limit has been hardcoded as 30 jokes
        /// Deserialize has to deal with a nested object which contains the jokes json inside results key. 
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        //static async Task<IEnumerable<Joke>> GetJokesBySearch(string searchString)
        //{
        //    IEnumerable<Joke> jokes = null;
        //    var response = await _client.GetAsync(BuildSearchUrl(searchString));

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var jsonString = await response.Content.ReadAsStringAsync();
        //        var jsonJokes = JsonConvert.DeserializeObject<JokeAttributes>(jsonString);
        //        jokes = jsonJokes.results;
        //    }
        //    return jokes;
        //}


        static async Task<HttpResponseMessage> CallBySearch(string searchString)
        {
            return await _client.GetAsync(BuildSearchUrl(searchString));
        }

        static async Task<string> GetJsonFromResponse(HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }


        static IEnumerable<Joke> GetJokes(string jsonString)
        {
            return JsonConvert.DeserializeObject<JokeAttributes>(jsonString).results;
        }


        private static string BuildSearchUrl(string searchString)
		{
            return _client.BaseAddress.ToString() +
                "search?term=" +
                searchString +
                "&limit=30";
        }
    }
}
