namespace FLG.Cs.IO {
    public class File {
        /// <summary>
        /// Absolute path for the file
        /// </summary>
        public readonly string fullpath;

        /// <summary>
        /// Absolute path of the parent directory
        /// </summary>
        public readonly string directory;

        /// <summary>
        /// Name of the file without extension
        /// </summary>
        public readonly string file; // 

        /// <summary>
        /// Name of the file with extension
        /// </summary>
        public readonly string filename;

        /// <summary>
        /// Name of the extension
        /// </summary>
        public readonly string extension;

        public File(string filepath)
        {
            FileInfo info = new(filepath);
            fullpath = info.FullName;
            directory = info.Directory?.FullName ?? string.Empty;
            file = Path.GetFileNameWithoutExtension(info.Name);
            filename = info.Name;
            extension = info.Extension;
        }

        public override string ToString() => fullpath;
    }
}
