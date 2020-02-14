using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DadJoke
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("\nFor a list of dad jokes, enter a search term and press enter.\n" +
                "For a random joke, just press enter.");
            string searchString = Console.ReadLine();
            await JokeController.RunAsync(searchString);
        }
    }
}
