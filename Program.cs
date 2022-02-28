using System;
using System.Resources;
using System.Collections;
using System.IO;


namespace ResourceReader
{
    class ReadResXResources
    {
        public static void Main()
        {
            var dir = new DirectoryInfo(@"C\:example\path"); // Path where RESX files are in
            StreamWriter sw = new StreamWriter(@"C:\strings.txt"); // Path for saving txt file

            foreach (FileInfo file in dir.GetFiles())
            {
                string filename = Path.GetFileName(file.FullName);
                Console.WriteLine("###" + filename + "###\n");
                sw.WriteLine("###" + filename + "###\n");
                sw.Flush();
                ResXResourceReader rsxr = new ResXResourceReader(dir + "\\" + filename); // Create a ResXResourceReader for the file
                foreach (DictionaryEntry d in rsxr) // Iterate through the resources and display the contents to the console.
                {
                    Console.WriteLine("\"" + d.Value.ToString() + "\"");
                    Console.WriteLine("\"" + d.Value.ToString() + "\"\n");
                    sw.WriteLine("\"" + d.Value.ToString() + "\"");
                    sw.Flush();
                    sw.WriteLine("\"" + d.Value.ToString() + "\"\n");
                    sw.Flush();
                }
                Console.WriteLine("###" + filename + "###\n");
                sw.WriteLine("###" + filename + "###\n");
                sw.Flush();
            }

            Console.ReadKey(true); //Close the reader.
        }
    }

}