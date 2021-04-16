using System;
using System.IO;
using System.Linq;

namespace tree1
{
    class Tree
    {
        public static void PrintTree(string folder, int maxDepth, string nesting = "", int depth = 0) {
            if (depth >= maxDepth)
            {
                return;
            }
            DirectoryInfo directoryInfo = new DirectoryInfo(folder);
            IOrderedEnumerable<FileSystemInfo> entries = directoryInfo.GetFileSystemInfos()
                .OrderBy(x => x.Name);
            //отрисовка всех элементов, кроме последнего
            foreach (FileSystemInfo entry in entries.SkipLast(1))
            {
                if (entry.Attributes != FileAttributes.Directory)
                {
                    FileInfo fileInfo = new FileInfo(entry.FullName);
                    Console.WriteLine("{0}├───{1} ({2})", nesting, entry.Name, fileInfo.Length);
                }
                else
                {
                    Console.WriteLine("{0}├───{1}", nesting, entry.Name);
                    PrintTree(entry.FullName, maxDepth, String.Concat("|   ", nesting), depth++);
                }
            }
            //отрисовка последнего элемента
            FileSystemInfo lastEntry = entries.LastOrDefault();
            if (lastEntry.Attributes != FileAttributes.Directory)
            {
                FileInfo fileInfo = new FileInfo(lastEntry.FullName);
                Console.WriteLine("{0}└───{1} ({2})", nesting, lastEntry.Name, fileInfo.Length);
            }
            else
            {
                Console.WriteLine("{0}└───{1}", nesting, lastEntry.Name);
            }
        }
    }
}