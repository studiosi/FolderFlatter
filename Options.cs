using CommandLine;
using CommandLine.Text;

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

        public string GetUsage()
        {

            HelpText help = new HelpText
            {
                Heading = new HeadingInfo("FolderFlatter", "0.1α"),
                Copyright = new CopyrightInfo("David Gil de Gómez Pérez", 2016),
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };

            help.AddPreOptionsLine("Under Creative Commons 3.0 BY-SA");
            help.AddPreOptionsLine("Usage: FolderFlatter -r RootPath -o OutputPath -e Extensions");
            help.AddOptions(this);

            return help;

        }

    }
}
