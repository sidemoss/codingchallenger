using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DadJoke
{
	public class DisplayJoke : IDisplayJoke
	{
		private readonly bool _isRandom;

		public DisplayJoke(bool isRandom)
		{
			_isRandom = isRandom;
		}

		public void WriteJokes()
		{
			
		}
	}
}
