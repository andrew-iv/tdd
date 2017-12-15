using CommandLine;

namespace TagCloudContainer.Console
{
    class Options
    {
        [ValueOption(0)]
        public string InputFile{ get; set; }

        [Option("o",DefaultValue = "words.png", HelpText = "Выходной файл")]
        public string OutputFile { get; set; }

        [Option("c", DefaultValue = 100, HelpText = "Количество слов")]
        public int Top { get; set; }

    }
}