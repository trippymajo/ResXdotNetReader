using System;
using System.Resources;
using System.Collections;
using System.IO;

namespace ResourceReader
{
    class ResXProc
    {
        private readonly string _dirPath;
        private readonly string _outputFullFileName;

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="dirPath">Directory path to process .resx files</param>
        /// <param name="outputFullFileName">Full file name of the output file</param>
        public ResXProc(string dirPath, string outputFullFileName)
        {
            _dirPath = dirPath;
            _outputFullFileName = outputFullFileName;
        }

        /// <summary>
        /// Starts the Process of the parsing
        /// </summary>
        public void Process()
        {
            if (!Directory.Exists(_dirPath))
            {
                Console.WriteLine($"Error: Directory '{_dirPath}' does not exist.");
                return;
            }

            DirectoryInfo dirInfo = new DirectoryInfo(_dirPath);
            FileInfo[] resxFiles = dirInfo.GetFiles("*.resx");

            using (var sw = new StreamWriter(_outputFullFileName))
            {
                foreach (FileInfo file in resxFiles)
                {
                    ProcessSingleFile(file, sw);
                }
            }

            Console.WriteLine("Processing complete. Press any key to exit.");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Processes single file from the directory
        /// </summary>
        /// <param name="file">File to process</param>
        /// <param name="sw">Stream writer for the output file</param>
        private void ProcessSingleFile(FileInfo file, StreamWriter sw)
        {
            string filename = file.Name;
            string filePath = file.FullName;

            Console.WriteLine($"### {filename} ###\n");
            sw.WriteLine($"### {filename} ###\n");

            try
            {
                ReadResxContents(filePath, sw);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file {filename}: {ex.Message}");
                sw.WriteLine($"Error reading file {filename}: {ex.Message}");
            }

            Console.WriteLine($"### {filename} ###\n");
            sw.WriteLine($"### {filename} ###\n");
        }

        /// <summary>
        /// ResX contents reading
        /// </summary>
        /// <param name="filePath">Path to resX file</param>
        /// <param name="sw">Stream writer for the output file</param>
        private void ReadResxContents(string filePath, StreamWriter sw)
        {
            using (var rsxr = new ResXResourceReader(filePath))
            {
                foreach (DictionaryEntry entry in rsxr)
                {
                    string value = entry.Value != null ? entry.Value.ToString() : "(null)";
                    Console.WriteLine($"\"{value}\"");
                    sw.WriteLine($"\"{value}\"");
                }
            }
        }
    }

    class Program
    {
        static void Main()
        {
            string directoryPath = @"C:\example\path";
            string outputFilePath = @"C:\strings.txt";

            ResXProc processor = new ResXProc(directoryPath, outputFilePath);
            processor.Process();
        }
    }
}