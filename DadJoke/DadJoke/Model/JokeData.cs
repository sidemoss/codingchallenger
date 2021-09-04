using System.Collections.Generic;

namespace DadJoke
{
	public class JokeData
    {
        public string Id { get; set; }
        public string Joke { get; set; }
        public int WordCount { get; set; }
        public int WordArrayCount { get; internal set; }
    }
}
