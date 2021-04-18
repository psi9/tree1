using System;
using System.IO;
using System.Linq;

namespace tree1
{
    class Tree
    {
        public static void PrintTree(string folder, int maxDepth, bool size, bool humanReadable, bool reverse, string sort, string indent = "", int depth = 0)
        {
            if (maxDepth <= depth)
            {
                return;
            }
            IOrderedEnumerable<FileSystemInfo> entries = getFolderContent(folder, sort, reverse);
            foreach (FileSystemInfo entry in entries)
            {
                if (entry.Attributes != FileAttributes.Directory)
                {
                    if (entry != entries.Last())
                    {
                        if (size)
                        {
                            FileInfo fileInfo = new FileInfo(entry.FullName);
                            Console.WriteLine("{0}├───{1} ({2})", indent, entry.Name, fileInfo.Length);
                        }
                        else if (humanReadable)
                        {
                            FileInfo fileInfo = new FileInfo(entry.FullName);
                            Console.WriteLine("{0}├───{1} ({2})", indent, entry.Name, HumanReadable(fileInfo.Length));
                        }
                        else
                        {
                            Console.WriteLine("{0}├───{1}", indent, entry.Name);
                        }
                    }

                    else
                    {
                        if (size)
                        {
                            FileInfo fileInfo = new FileInfo(entry.FullName);
                            Console.WriteLine("{0}└───{1} ({2} B)", indent, entry.Name, fileInfo.Length);
                        }
                        else if (humanReadable)
                        {
                            FileInfo fileInfo = new FileInfo(entry.FullName);
                            Console.WriteLine("{0}└───{1} ({2})", indent, entry.Name, HumanReadable(fileInfo.Length));
                        }
                        else
                        {
                            Console.WriteLine("{0}└───{1}", indent, entry.Name);
                        }
                    }
                }
                else
                {
                    if (entry != entries.Last())
                    {
                        Console.WriteLine("{0}├───{1}", indent, entry.Name);
                        PrintTree(entry.FullName, maxDepth, size, humanReadable, reverse, sort, String.Concat(indent, "|   "), depth + 1);
                    }
                    else
                    {
                        Console.WriteLine("{0}└───{1}", indent, entry.Name);
                        PrintTree(entry.FullName, maxDepth, size, humanReadable, reverse, sort, String.Concat(indent, "    "), depth + 1);
                    }
                }
            }
        }
        public static IOrderedEnumerable<FileSystemInfo> getFolderContent(string folder, string sort, bool reverse)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(folder);
            FileSystemInfo[] entries = directoryInfo.GetFileSystemInfos();
            if (reverse)
            {
                switch (sort)
                {
                    case "name":
                        return entries.OrderBy(x => x.Name);
                    case "creation":
                        return entries.OrderBy(x => x.CreationTime);
                    case "change":
                        return entries.OrderBy(x => x.LastWriteTime);
                }
            }
            else
            {
                switch (sort)
                {
                    case "name":
                        return entries.OrderByDescending(x => x.Name);
                    case "creation":
                        return entries.OrderByDescending(x => x.CreationTime);
                    case "change":
                        return entries.OrderByDescending(x => x.LastWriteTime);
                }
            }
            return null;
        }
        public static string HumanReadable (long fileSize)
        {
            if (fileSize == 0)
            {
                return "empty";
            }
            string[] sizes = { "B", "KB", "MB", "GB", "TB", "PB" };
            int order = 0;
            while (fileSize >= 1024 && order < sizes.Length - 1)
            {
                order++;
                fileSize = fileSize / 1024;
            }
            return String.Format("{0:0.##} {1}", fileSize, sizes[order]);
        }
    }
}