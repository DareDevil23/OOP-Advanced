namespace FestivalManager.Core
{
	using System.Reflection;
	using Contracts;
	using Controllers.Contracts;
	using IO.Contracts;
	using System;
	using System.Linq;

    public class Engine : IEngine
	{
	    private readonly IReader reader;
	    private readonly IWriter writer;
	    private readonly IFestivalController festivalCоntroller;
	    private readonly ISetController setCоntroller;

	    public Engine(IReader reader, IWriter writer, IFestivalController festivalController, ISetController setController)
	    {
	        this.reader = reader;
	        this.writer = writer;
	        this.festivalCоntroller = festivalController;
	        this.setCоntroller = setController;
	    }

	    public void Run()
	    {
	        string input = string.Empty;

	        while ((input = reader.ReadLine()) != "END")
	        {
	            try
	            {
	                var result = this.ProcessCommand(input);

	                this.writer.WriteLine(result);
	            }
	            catch (TargetInvocationException ex)
	            {
                    this.writer.WriteLine("ERROR: " + ex.InnerException.Message);
	            }
	            catch (Exception ex)
	            {
	                this.writer.WriteLine("ERROR: " + ex.Message);
	            }
	        }

	        var end = this.festivalCоntroller.ProduceReport();

	        this.writer.WriteLine("Results:");
	        this.writer.WriteLine(end);
        }

	    public string ProcessCommand(string input)
	    {
	        string[] tokens = input.Trim().Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

	        string commandName = tokens[0];
	        var args = tokens.Skip(1).ToArray();

	        if (commandName == "LetsRock")
	        {
	            string setsResult = this.setCоntroller.PerformSets();

	            return setsResult;
	        }

	        MethodInfo festivalControllerMethod = this.festivalCоntroller
	            .GetType()
	            .GetMethods()
	            .FirstOrDefault(x => x.Name == commandName);

	        string methodResult;

	        try
	        {
	            methodResult = (string)festivalControllerMethod?.Invoke(this.festivalCоntroller, new object[] { args });
	        }
	        catch (TargetInvocationException ex)
	        {
	            throw ex;
	        }

	        return methodResult;
        }
	}
}