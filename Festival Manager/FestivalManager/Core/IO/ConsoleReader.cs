namespace FestivalManager.Core.IO
{
    using System;
    using Contracts;

    public class ConsoleReader : IReader
	{
	    public string ReadLine()
	    {
	        var line = Console.ReadLine();

	        return line;
	    }
	}
}