using CommandLine;

namespace PerformCalcStudents.CommandLineOptions
{
    public class Options
    {
        [Option('i', "input", Required = false, HelpText = "Input file name")]
        public string InputFile { get; set; }

        [Option('o', "output", Required = false, HelpText = "Output file name")]
        public string OutputFile { get; set; }

        [Option('f', "fileType", Required = false, HelpText = "Type of file")]
        public string FileType { get; set; }
    }
}
