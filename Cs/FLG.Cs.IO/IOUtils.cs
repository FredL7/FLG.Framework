namespace FLG.Cs.IO {
    public static class IOUtils {
        public static List<string> GetFilePathsByExtension(string dir, string extension)
        {
            var files = Directory.GetFiles(dir);
            List<string> result = new();
            foreach (var file in files)
                if (Path.GetExtension(file) == extension)
                    result.Add(Path.GetFullPath(file));
            return result;
        }

        public static List<string> GetFilePathsByExtensions(string dir, string[] extensions)
        {
            var files = Directory.GetFiles(dir);
            List<string> result = new();
            foreach (var file in files)
                if (extensions.Contains(Path.GetExtension(file)))
                    result.Add(Path.GetFullPath(file));
            return result;
        }
    }
}
