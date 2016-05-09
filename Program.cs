using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Linq;

namespace FolderFlatter
{
    class Program
    {
        
        private static string rootPath { get; set; }
        private static string outputPath { get; set; }
        private static string[] extensions { get; set; }
        
        static void Main(string[] args)
        {

            Options opts = new Options();
            
            if(CommandLine.Parser.Default.ParseArguments(args, opts))
            {
                rootPath = opts.flattenPath;

                outputPath = opts.outputPath;

                if (!outputPath.EndsWith("\\"))
                {
                    outputPath = outputPath + "\\";
                }

                extensions = opts.extensions.Split(',');

                // Converts all the extensions to uppercase
                extensions = extensions.Select(
                    x => ((x.ElementAt(0) == '.') ? x.ToUpper() : "." + x.ToUpper())
                ).ToArray();

                // Walks the directory structure
                try
                {
                    Walk(new DirectoryInfo(rootPath));
                }
                catch (SecurityException)
                {
                    Console.WriteLine("Folder {0} can't be accesed.", rootPath);
                }
                catch (PathTooLongException)
                {
                    Console.WriteLine("Path is too long");
                }
                
            }   
            else
            {
                Console.WriteLine(opts.GetUsage());
            }                

        }

        private static void Walk(DirectoryInfo root)
        {

            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            #region Get all files under this directory
            try
            {
                files = root.GetFiles("*.*");
            }
            catch (SecurityException)
            {
                Console.WriteLine("Folder {0} can't be accesed.", root.ToString());
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Folder {0} does not exist.", root.ToString());
            }
            #endregion

            #region Process current files
            if (files != null)
            {
                foreach (FileInfo f in files)
                {
                    if(extensions.Contains(f.Extension.ToUpper()))
                    {
                        f.CopyTo(outputPath + f.Name);
                    }
                }
            }
            #endregion

            #region Get subdirectories and recursively walk them
            subDirs = root.GetDirectories();
            foreach (DirectoryInfo d in subDirs)
            {
                Walk(d);
            }
            #endregion

        }
    }
}
