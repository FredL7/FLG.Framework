namespace FLG.Cs.IO {
    public static class IOUtils {
        public static List<File> GetFilePathsByExtension(string dir, string extensionFilter)
        {
            var files = Directory.GetFiles(dir);
            List<File> result = new();
            foreach (var file in files)
            {
                File f = new(file);
                if (f.extension == extensionFilter)
                    result.Add(f);
            }
            return result;
        }

        public static List<File> GetFilePathsByExtensions(string dir, string[] extensions)
        {
            var files = Directory.GetFiles(dir);
            List<File> result = new();
            foreach (var file in files)
            {
                File f = new(file);
                if (extensions.Contains(f.extension))
                    result.Add(f);
            }
            return result;
        }
    }
}
