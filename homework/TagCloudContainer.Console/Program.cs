using System.Collections.Generic;
using System.Drawing;
using Autofac;
using CommandLine;
using TagsCloudContainer;
using TagsCloudContainer.Dto;
using TagsCloudContainer.Interfaces;

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


    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if (Parser.Default.ParseArguments(args, options))
            {
                IContainer container = DiConfiguration.Register(new ContainerBuilder(), options.Top).Build();
                container.Resolve<ITagsCloudContainer>().Draw(options.InputFile, options.OutputFile, new WordRenderProperties
                {
                    FontFamily = FontFamily.GenericSerif,
                    ImageSize = new Size(1920, 1080)
                });
            }

            
        }
    }
}