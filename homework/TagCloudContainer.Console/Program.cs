using System.Drawing;
using Autofac;
using TagsCloudContainer;
using TagsCloudContainer.Dto;
using TagsCloudContainer.Interfaces;

namespace TagCloudContainer.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IContainer container = DiConfiguration.Register(new ContainerBuilder()).Build();
            container.Resolve<ITagsCloudContainer>().Draw("story.txt", "words.png", new WordRenderProperties
            {
                FontFamily = FontFamily.GenericSerif,
                ImageSize = new Size(1920, 1080)
            });
        }
    }
}