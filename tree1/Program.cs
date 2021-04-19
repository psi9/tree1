using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace tree1
{
    class Program
    {
        static int Main(string[] args)
        {
            var cmd = new RootCommand {
                new Option<int>(new[] { "-d", "--depth" }, "Specify the depth to look into"),
                new Option(new[] { "-s", "--size" }, "Show size"),
                new Option(new[] { "-h", "--human-readable"}, "Show size in human readable format"),
                new Option<string> ("--sort", "Sorting: size, creation date, last change date, name"),
                new Option("-r", "Reverse order")
            };
            cmd.Handler = CommandHandler.Create<IConsole, bool, bool, bool, string, int>(Handle);
            return cmd.Invoke(args);
        }
        static void Handle(IConsole console, bool size, bool humanReadable, bool reverse, string sort = "name", int depth = int.MaxValue)
        {
            string folder = "C:\\1111";
            Tree tree = new Tree(depth, size, humanReadable, reverse, sort);
            tree.PrintTree(folder);
        }
    }
}
