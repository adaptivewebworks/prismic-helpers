using System;
using System.IO;
using Newtonsoft.Json.Linq;
using prismic;

namespace AdaptiveWebworks.Prismic.Tests
{

    public class Fixtures
    {
        public static JToken Get(string file)
        {
            var directory = Directory.GetCurrentDirectory();
            var sep = Path.DirectorySeparatorChar;
            var path = $"{directory}{sep}Fixtures{sep}{file}";
            string text = File.ReadAllText(path);
            return JToken.Parse(text);
        }

        public static Document GetDocument(string file)
        {
            var json = Get(file);
            return Document.Parse(json);
        }
    }
}
