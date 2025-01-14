using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace FLG.Cs.IO {
    public static class IOUtils {
        public static List<FLGFile> GetFilePathsByExtension(string dir, string extensionFilter)
        {
            var files = Directory.GetFiles(dir);
            List<FLGFile> result = [];
            foreach (var file in files)
            {
                FLGFile f = new(file);
                if (f.extension == extensionFilter)
                    result.Add(f);
            }
            return result;
        }

        public static List<FLGFile> GetFilePathsByExtensions(string dir, string[] extensions)
        {
            var files = Directory.GetFiles(dir);
            List<FLGFile> result = [];
            foreach (var file in files)
            {
                FLGFile f = new(file);
                if (extensions.Contains(f.extension))
                    result.Add(f);
            }
            return result;
        }
    }
}
