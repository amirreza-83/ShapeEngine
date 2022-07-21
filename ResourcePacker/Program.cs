namespace ResourcePacker
{
    internal class Program
    {

        private static string CompressEncode(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            byte[] comp = SevenZip.Compression.LZMA.SevenZipHelper.Compress(bytes);
            string file = Convert.ToBase64String(comp);
            return file;
        }

        private static void WriteLinesToFile(string[] lines, string path)
        {
            File.WriteAllLines(path, lines);
        }
        //private static string[] ReadLinesFromFile(string path)
        //{
        //    return File.ReadAllLines(path);
        //}

        public static void CompressEncodeFolder(string directoryPath, string folder, string outputPath)//run pre build
        {
            Directory.SetCurrentDirectory(directoryPath);
            string[] files = Directory.GetFiles(folder, "", SearchOption.AllDirectories);
            List<string> lines = new List<string>();
            foreach (var file in files)
            {
                string data = CompressEncode(file);
                lines.Add(data);
            }
            WriteLinesToFile(lines.ToArray(), outputPath + "resource.txt");
            WriteLinesToFile(files, outputPath + "resourceNames.txt");
        }





        static void Main(string[] args)
        {
            if (args.Length <= 3) return;
            if (args[3] == "false") return;
            CompressEncodeFolder(args[0], args[1], args[0] + args[2]);
        }
    }
}