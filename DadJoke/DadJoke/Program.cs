using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DadJoke
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("For a list of dad jokes, enter a search term and press enter.\n" +
                "For a random joke, just press enter.");
            string searchString = Console.ReadLine();
            JokeController.RunAsync(searchString);
        }
    }
}
