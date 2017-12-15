using System.Collections.Generic;
using System.Drawing;
using Autofac;
using CommandLine;
using TagsCloudContainer;
using TagsCloudContainer.Dto;
using TagsCloudContainer.Interfaces;

namespace TagCloudContainer.Console
{
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