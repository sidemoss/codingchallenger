using System.Collections.Generic;

namespace DadJoke
{
    public class JokeData
    {
        public string Id { get; set; }
        public string Joke { get; set; }
        public int WordCount { get; set; }
    }

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

}
