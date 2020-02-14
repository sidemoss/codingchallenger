using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DadJoke
{
    class RandomJoke
    {
        /// <summary>
        /// Display random joke only.
        /// </summary>
        /// <param name="joke"></param>
        public static void DisplayRandomJoke(string joke)
        {
            Console.WriteLine("Random Joke");
            Console.WriteLine("===========");
            Console.WriteLine();
            Console.WriteLine(joke);
        }
    }
}
