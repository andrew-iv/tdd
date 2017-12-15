using System.Reflection;
using Autofac;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Services;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    public static class DiConfiguration
    {
        private static readonly string[] BoringWords = new[]
            {"я", "ты", "что", "где", "в", "и", "на", "не", "он", "а", "е", "с", "меня", "мне"};

        public static ContainerBuilder Register(ContainerBuilder builder, int topWords)
        {
            builder.RegisterAssemblyTypes(typeof(IWordListTransformer).Assembly)
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(ICircularCloudLayouter).Assembly)
                .AsImplementedInterfaces();
            builder.Register(c => new ComposedWordListTransformer(
                new NormalizeWordListTransformer(),
                new ExcludeBoringWordListTransformer(BoringWords))
            ).As<IWordListTransformer>();
            builder.Register(c => TagCloodConverterFactory.ConstructDefault(topWords)).As<ITagCloodConverter>();
            return builder;
        }
    }
}