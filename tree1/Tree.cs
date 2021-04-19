using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace tree1
{

    class Tree
    {
        private int MaxDepth { get; set; }
        private bool Size { get; set; }
        private bool HumanReadable { get; set; }
        private bool Reverse { get; set; }
        private string Sorting { get; set; }
        public Tree(int maxDepth, bool size, bool humanReadable, bool reverse, string sorting)
        {
            MaxDepth = maxDepth;
            Size = size;
            HumanReadable = humanReadable;
            Reverse = reverse;
            Sorting = sorting;
        }
        public void PrintTree(string folder, string indent = "", int depth = 0)
        {
            if (this.MaxDepth <= depth)
            {
                return;
            }
            List<FileSystemInfo> entries = GetFolderContent(folder);
            foreach (FileSystemInfo entry in entries)
            {
                if (entry != entries.Last())
                    PrintEntry(entry, indent);
                else
                    PrintLastEntry(entry, indent);
                if (IsDirectory(entry))
                {
                    if (entry != entries.Last())
                        PrintTree(entry.FullName, String.Concat(indent, "|   "), depth + 1);
                    else
                        PrintTree(entry.FullName, String.Concat(indent, "    "), depth + 1);
                }
            }
        }
        private void PrintEntry(FileSystemInfo entry, string indent)
        {
            if ((this.HumanReadable) && (!IsDirectory(entry)))
            {
                FileInfo fileInfo = new FileInfo(entry.FullName);
                Console.WriteLine("{0}├───{1} ({2})", indent, entry.Name, ShowHumanReadable(fileInfo.Length));
            }
            else if ((this.Size) && (!IsDirectory(entry)))
            {
                FileInfo fileInfo = new FileInfo(entry.FullName);
                Console.WriteLine("{0}├───{1} ({2})", indent, entry.Name, fileInfo.Length);
            }
            else
            {
                Console.WriteLine("{0}├───{1}", indent, entry.Name);
            }
        }
        private void PrintLastEntry(FileSystemInfo entry, string indent)
        {
            if ((this.HumanReadable) && (!IsDirectory(entry)))
            {
                FileInfo fileInfo = new FileInfo(entry.FullName);
                Console.WriteLine("{0}└───{1} ({2})", indent, entry.Name, ShowHumanReadable(fileInfo.Length));
            }
            else if ((this.Size) && (!IsDirectory(entry)))
            {
                FileInfo fileInfo = new FileInfo(entry.FullName);
                Console.WriteLine("{0}└───{1} ({2})", indent, entry.Name, fileInfo.Length);
            }
            else
            {
                Console.WriteLine("{0}└───{1}", indent, entry.Name);
            }
        }
        private List<FileSystemInfo> GetFolderContent(string folder)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(folder);
            FileSystemInfo[] entries = directoryInfo.GetFileSystemInfos();
            if (!this.Reverse)
            {
                switch (this.Sorting)
                {
                    case "name":
                        return entries.OrderBy(entry => entry.Name).ToList();
                    case "creation":
                        return entries.OrderBy(entry => entry.CreationTime).ToList();
                    case "change":
                        return entries.OrderBy(entry => entry.LastWriteTime).ToList();
                    case "size":
                        List<FileSystemInfo> sortedFiles = directoryInfo.GetFiles().OrderBy(entry => entry.Length).Cast<FileSystemInfo>().ToList();
                        List<FileSystemInfo> directories = directoryInfo.GetDirectories().OrderBy(entry => entry.Name).Cast<FileSystemInfo>().ToList();
                        return sortedFiles.Concat(directories).ToList();
                }
            }
            else
            {
                switch (this.Sorting)
                {
                    case "name":
                        return entries.OrderByDescending(entry => entry.Name).ToList();
                    case "creation":
                        return entries.OrderByDescending(entry => entry.CreationTime).ToList();
                    case "change":
                        return entries.OrderByDescending(entry => entry.LastWriteTime).ToList();
                    case "size":
                        List<FileSystemInfo> sortedFiles = directoryInfo.GetFiles().OrderByDescending(entry => entry.Length).Cast<FileSystemInfo>().ToList();
                        List<FileSystemInfo> directories = directoryInfo.GetDirectories().OrderByDescending(entry => entry.Name).Cast<FileSystemInfo>().ToList();
                        return sortedFiles.Concat(directories).ToList();
                }
            }
            return null;
        }
        private string ShowHumanReadable(long fileSize)
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
        private bool IsDirectory(FileSystemInfo entry)
        {
            return entry.Attributes == FileAttributes.Directory;
        }
    }
}