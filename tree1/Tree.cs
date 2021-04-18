using System;
using System.IO;
using System.Linq;

namespace tree1
{
    class Tree
    {
        public static void PrintTree(string folder, int maxDepth, string indent = "", int depth = 0)
        {
            if (maxDepth <= depth)
            {
                return;
            }
            DirectoryInfo directoryInfo = new DirectoryInfo(folder);
            IOrderedEnumerable<FileSystemInfo> entries = directoryInfo.GetFileSystemInfos()
                .OrderBy(x => x.Name);
            foreach (FileSystemInfo entry in entries)
            {
                if (entry.Attributes != FileAttributes.Directory)
                {
                    FileInfo fileInfo = new FileInfo(entry.FullName);
                    if (entry != entries.Last())
                    {
                        Console.WriteLine("{0}├───{1} ({2})", indent, entry.Name, fileInfo.Length);
                    }
                    else
                    {
                        Console.WriteLine("{0}└───{1} ({2})", indent, entry.Name, fileInfo.Length);
                    }
                }
                else
                {
                    if (entry != entries.Last())
                    {
                        Console.WriteLine("{0}├───{1}", indent, entry.Name);
                        PrintTree(entry.FullName, maxDepth, String.Concat(indent, "|   "), depth + 1);
                    }
                    else
                    {
                        Console.WriteLine("{0}└───{1}", indent, entry.Name);
                        PrintTree(entry.FullName, maxDepth, String.Concat(indent, "    "), depth + 1);
                    }
                }
            }
        }
    }
}