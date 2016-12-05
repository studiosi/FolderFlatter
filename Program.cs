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
        private static bool overwrite { get; set; }
        
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

                try
                {
                    if (opts.empty)
                    {
                        Clean(new DirectoryInfo(outputPath));
                    }
                }
                catch(DirectoryNotFoundException)
                {
                    Console.WriteLine("The output directory does not exist. Cleaning not doing anything.");
                }
                catch(IOException)
                {
                    Console.WriteLine("The output directory can't be properly cleaned.");
                }
                catch(SecurityException)
                {
                    Console.WriteLine("You are not allowed to operate on the output directory. Cleaning failed.");
                }
                catch(UnauthorizedAccessException)
                {
                    Console.WriteLine("You are not allowed to access the output directory. Cleaning failed.");
                }
                

                extensions = opts.extensions.Split(',');
                overwrite = opts.overwrite;

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
                Console.Read();
            }                

        }

        private static void Clean(DirectoryInfo root)
        {
            foreach (System.IO.FileInfo file in root.GetFiles()) file.Delete();
            foreach (System.IO.DirectoryInfo subDirectory in root.GetDirectories()) subDirectory.Delete(true);
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
                        if(!overwrite)
                        {
                            if (File.Exists(outputPath + f.Name))
                            {
                                int i = 1;
                                string noExtensionName = Path.GetFileNameWithoutExtension(f.Name);
                                while(File.Exists(outputPath + noExtensionName + "-" + i + f.Extension))
                                {
                                    i += 1;
                                }
                                f.CopyTo(outputPath + noExtensionName + "-" + i + f.Extension);
                            }
                            else
                            {
                                f.CopyTo(outputPath + f.Name);
                            }
                        }
                        else
                        {
                            if(File.Exists(outputPath + f.Name))
                            {
                                File.Delete(outputPath + f.Name);
                            }
                            f.CopyTo(outputPath + f.Name);
                        }
                    }
                }
            }
            #endregion

            #region Get subdirectories and recursively walk them
            if(root.Exists)
            {
                subDirs = root.GetDirectories();
                foreach (DirectoryInfo d in subDirs)
                {
                    Walk(d);
                }
            }
            #endregion

        }
    }
}
