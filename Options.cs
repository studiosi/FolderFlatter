using CommandLine;
using CommandLine.Text;
using System.Diagnostics;
using System.Reflection;

namespace FolderFlatter
{
    class Options
    {

        [Option ('r', "rootPath", Required = true, HelpText = "The root path you want to flatten.")]
        public string flattenPath { get; set; }

        [Option('o', "outputPath", Required = true, HelpText = "The path where you want to output the files.")]
        public string outputPath { get; set; }

        [Option('e', "extensions", Required = true, HelpText = "The extensions you want to extract, separated by comma.")]
        public string extensions { get; set; }

        [Option('v', "overwrite", HelpText = "Use it if you want to overwrite the files in case of two having the same name.")]
        public bool overwrite { get; set; }

        [Option('m', "empty", HelpText = "Use it if you want the destination folder to be emptied before starting copying. Use with caution.")]
        public bool empty { get; set; }

        public string GetUsage()
        {

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

            HelpText help = new HelpText
            {
                Heading = new HeadingInfo("FolderFlatter", version),
                Copyright = new CopyrightInfo("David Gil de Gómez Pérez", 2016),
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };

            help.AddPreOptionsLine("Under MIT License");
            help.AddPreOptionsLine("Usage: FolderFlatter -r RootPath -o OutputPath -e Extensions [[-v] -m]");
            help.AddOptions(this);

            return help;

        }

    }
}
