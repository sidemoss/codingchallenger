using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DadJoke
{
    class RandomJoke : IDisplayJoke
    {
        private readonly string _joke;
		public RandomJoke(string joke)
		{
            _joke = joke;
		}

        public void WriteJokes()
		{
            Console.WriteLine("Random Joke");
            Console.WriteLine("===========");
            Console.WriteLine();
            Console.WriteLine(_joke);
        }
    }
}
