namespace TheTankGame.Core
{
    using System.Collections.Generic;
    using Contracts;
    using System.Linq;

    public class CommandInterpreter : ICommandInterpreter
    {
        private readonly IManager tankManager;

        public CommandInterpreter(IManager tankManager)
        {
            this.tankManager = tankManager;
        }

        public string ProcessInput(IList<string> inputParameters)
        {
            string command = inputParameters[0];

            var args = inputParameters.Skip(1).ToList();

            var result = (string)this.tankManager.GetType().GetMethods().FirstOrDefault(m => m.Name.Contains(command))?
                .Invoke(this.tankManager, new object[] {args});

            return result;
        }
    }
}