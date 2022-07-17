﻿// See https://aka.ms/new-console-template for more information
namespace ConsoleAppFolderParsePractice;

public class Program
{
    const string namedFoldersPath = @"C:\Users\Nich\Documents\Projects\Work Practice\ConsoleAppFolderParsePractice\ResourceFiles\2014";
    //const string folderPath = @"C:\Users\Nich\Documents\Projects\Work Practice\ConsoleAppFolderParsePractice\ResourceFiles";
    const string filePath = @"C:\Users\Nich\Documents\Projects\Work Practice\ConsoleAppFolderParsePractice\ResourceFiles\SimpleText.txt";

    public static void Main(string[] args)
    {

        TraverseTree(namedFoldersPath);

        Console.WriteLine("Press any key");
        Console.ReadKey();
    }

    public static void TraverseTree(string root)
    {
        // Data structure to hold names of subfolders to be
        // examined for files.
        Stack<string> dirs = new Stack<string>(20);

        if (!System.IO.Directory.Exists(root))
        {
            throw new ArgumentException();
        }
        dirs.Push(root);

        while (dirs.Count > 0)
        {
            string currentDir = dirs.Pop();
            string[] subDirs;
            try
            {
                subDirs = System.IO.Directory.GetDirectories(currentDir);
            }
            // An UnauthorizedAccessException exception will be thrown if we do not have
            // discovery permission on a folder or file. It may or may not be acceptable
            // to ignore the exception and continue enumerating the remaining files and
            // folders. It is also possible (but unlikely) that a DirectoryNotFound exception
            // will be raised. This will happen if currentDir has been deleted by
            // another application or thread after our call to Directory.Exists. The
            // choice of which exceptions to catch depends entirely on the specific task
            // you are intending to perform and also on how much you know with certainty
            // about the systems on which this code will run.
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
                continue;
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
                continue;
            }

            string[] files = null;
            try
            {
                files = System.IO.Directory.GetDirectories(currentDir);
            }

            catch (UnauthorizedAccessException e)
            {

                Console.WriteLine(e.Message);
                continue;
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
                continue;
            }

            // Perform the required action on each file here.
            // Modify this block to perform your required task.

            foreach (string file in files)
            {
                try
                {
                    // Perform whatever action is required in your scenario.
                    System.IO.DirectoryInfo fi = new System.IO.DirectoryInfo(file);

                    //Construct name
                    Console.WriteLine("\nFullName " + fi.FullName);
                    Console.WriteLine("Name " + fi.Name);
                    Console.WriteLine("Parent " + fi.Parent);
                }
                catch (System.IO.FileNotFoundException e)
                {
                    // If file was deleted by a separate application
                    //  or thread since the call to TraverseTree()
                    // then just continue.
                    Console.WriteLine(e.Message);
                    continue;
                }
            }

            // Push the subdirectories onto the stack for traversal.
            // This could also be done before handing the files.
            foreach (string str in subDirs)
                dirs.Push(str);
        }
    }
}
