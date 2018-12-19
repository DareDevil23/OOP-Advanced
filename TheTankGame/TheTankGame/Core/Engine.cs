using System;
using System.Linq;
using TheTankGame.Utils;

namespace TheTankGame.Core
{
    using Contracts;
    using IO.Contracts;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICommandInterpreter commandInterpreter;

        public Engine( IReader reader, IWriter writer, ICommandInterpreter commandInterpreter)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandInterpreter = commandInterpreter;

        }

        public void Run()
        {
            string line = string.Empty;

            while (true)
            {
                line = this.reader.ReadLine();
                var inputParams = line.Trim().Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries).ToList();
                var result = this.commandInterpreter.ProcessInput(inputParams);
                this.writer.WriteLine(result);

                if (line == GlobalConstants.EndCommand)
                {
                    break;
                }

            }           
        }
    }
}