using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;

namespace tree1
{
    class Program
    {
        static int Main(string[] args)
        {
            RootCommand cmd = new RootCommand {
                new Option<int>(new string[] { "-d", "--depth" }, "Specify the depth to look into"),
                new Option(new string[] { "-s", "--size" }, "Show size"),
                new Option(new string[] { "-h", "--human-readable"}, "Show size in human readable format"),
                new Option<string> ("--sort", "Sort by size, creation date, last change date, name. Possible values: \"name\" (default), \"creation\", \"change\", \"size\""),
                new Option(new string[] { "-r", "--reverse"}, "Reverse order")
            };
            cmd.Handler = CommandHandler.Create<IConsole, bool, bool, bool, string, int>(Handle);
            return cmd.Invoke(args);
        }
        static void Handle(IConsole console, bool size, bool humanReadable, bool reverse, string sort = "name", int depth = int.MaxValue)
        {
            string folder = Directory.GetCurrentDirectory();
            try
            {
                Tree tree = new Tree(depth, size, humanReadable, reverse, sort);
                tree.PrintTree(folder);
            }
            catch (Exception)
            {
                Console.WriteLine("Exception");
            }
        }
    }
}
