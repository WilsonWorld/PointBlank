using System;
using System.Collections.Generic;
using System.Linq;

public class DeveloperConsole
{
    private readonly string prefix;
    private readonly IEnumerable<IConsoleCommand> commands;

    public DeveloperConsole(string aPrefix, IEnumerable<IConsoleCommand> aCommands)
    {
        this.prefix = aPrefix;
        this.commands = aCommands;
    }
    
    // If the input has a prefix, remove it and store the command word and any following args into process function
    public void ProcessCommandInput(string inputValue)
    {
        if (!inputValue.StartsWith(prefix)) { return; }

        inputValue = inputValue.Remove(0, prefix.Length);

        string[] inputSplit = inputValue.Split(' ');

        string commandInput = inputSplit[0];
        string[] args = inputSplit.Skip(1).ToArray();

        ProcessCommand(commandInput, args);
    }

    // If the command word is valid, process it and any arguments to fulfill the request
    public void ProcessCommand(string commandInput, string[] args)
    {
        foreach (var command in commands) {
            if (!commandInput.Equals(command.CommandWord, StringComparison.OrdinalIgnoreCase)) {
                continue;
            }

            if (command.Process(args)) {
                return;
            }
        }
    }
}
