using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace MoveToFolder
{
    class Program
    {
        static void Main(string[] args)
        {
            //If dir specified, move all files in dir, else move the file specified
            bool isFile = false;
            string path;
            
            if (args.Length > 0)
            {
                if (Directory.Exists(args[0]))
                {
                    // Do nothing since we want to skip the file check, and drop out
                }
                else if (File.Exists(args[0]))
                {
                    isFile = true;
                }
                else
                {
                    Console.WriteLine("Path not found" + args[0]);
                    return;
                }
                path = args[0];
            }
            else
            {
                path = System.Environment.CurrentDirectory;
            }

            if (isFile)
            {
                moveToFolder(path);
                return;
            };

            List<string> files = new List<string>(Directory.EnumerateFiles(path));
            foreach (string file in files)
            {
                moveToFolder(file.ToString());
            }
        }

        //Accepts a path to a file and moves it to a directory of the same name sans extension
        static int moveToFolder(string path)
        {
            if (!File.Exists(path))
            {
                return 2; //File Not found
            }
            //Strip extension, make folder of that name and move the file into that folder
            string folderPath = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
            try
            {
                Directory.CreateDirectory(folderPath);
                File.Move(path, Path.Combine(folderPath, Path.GetFileName(path)));
                Console.WriteLine("Moving "+ path);
            }
            catch (SystemException e)
            {
                Console.WriteLine("Could not move " + path + "\n" + e.ToString());
                return -1;
            }
            return 0;
        }
    }
}
