using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
