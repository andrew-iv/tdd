using System.Drawing;
using System.IO;
using NUnit.Framework;

namespace TagsCloudVisualization.Tests.Infrastructure
{
    public static class ImageOutput
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Путь до файла с тестом</returns>
        public static string OutputForTest(this Bitmap image)
        {
            
            var testContext = TestContext.CurrentContext;
            var directory = Directory.CreateDirectory(Path.Combine(testContext.TestDirectory, "OutputForTest"));
            var filename = Path.Combine(directory.FullName,
                $"{testContext.Test.FullName}.bmp");
            image.Save(filename);
            return filename;
        }
    }
}