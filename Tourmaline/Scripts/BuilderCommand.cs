using System.Text;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Tourmaline.Scripts
{
    public sealed class BuilderCommand : AsyncCommand
    {
        public override Task<int> ExecuteAsync(CommandContext context)
        {
            Table table = new();
            table.AddColumns("[Blue]Tourmaline[/]", "");
            table.AddRow("License", "GPL v3")
                .AddRow("Creator(s)", "Jewels")
                .AddRow("Mode", "Builder");
            table.Width(100);
            AnsiConsole.Write(table);

            AnsiConsole.MarkupLine("Welcome to the [Blue]Tourmaline[/] command builder!");
            AnsiConsole.MarkupLine("This is meant to help you create [Blue]Tourmaline[/] commands (CTRL + C to quit)");
            AnsiConsole.MarkupLine("We are going to ask you a couple of questions to help us generate the command.");

            string url = AnsiConsole.Prompt(new TextPrompt<string>("Target URL:"));

            string type = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("What type of directory enumeration do you need?")
                .AddChoices("Spider", "Brute"));

            StringBuilder strBuilder = new();
            if (type == "Spider")
            {
                strBuilder.Append("tourmaline spider ");
                strBuilder.Append(url + " ");

                int maxPaths = AnsiConsole.Prompt(new TextPrompt<int>("Max paths? (-1 if none)"));
                if (maxPaths != -1) strBuilder.Append($"-m {maxPaths}");

            } else
            {
                strBuilder.Append("tourmaline brute ");
                strBuilder.Append(url + " ");

                string wordlistPath = AnsiConsole.Prompt(new TextPrompt<string>("Wordlist Path?"));
                strBuilder.Append(wordlistPath + " ");
            }

            AnsiConsole.MarkupLine("Here's your command: \n");
            AnsiConsole.WriteLine("\t" + strBuilder.ToString() + "\n");

            return Task.FromResult(0);
        }
    }
}
