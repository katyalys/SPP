using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace spp_lab2
{
    public class CopyFile
    {
        public static string destDirectory;
        public static string targetDirectory;

        public static Queue<string> destList;
        public static Queue<string> fileList;

        public static CountdownEvent EventCounter;//
        public static int n = 0;

        static CopyFile()
        {

            fileList = new Queue<string>();
            destList = new Queue<string>();
        }

        // Process all files in the directory passed in, recurse on any directories
        // that are found, and process the files they contain.
        public static void ProcessDirectory(string targetDirectory, string destDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(Path.GetFullPath(targetDirectory));
            if (fileEntries.Length != 0)
            {
                EventCounter.AddCount(fileEntries.Length); //
            }
            // foreach (string fileName in fileEntries)

            ProcessFile(fileEntries, destDirectory);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                string newDestinationDir = Path.Combine(destDirectory, Path.GetFileName(subdirectory));
                //  CopyDirectory(subdirectory, newDestinationDir, true);
                Directory.CreateDirectory(newDestinationDir);
                ProcessDirectory(subdirectory, newDestinationDir);
            }
        }

        // Insert logic for processing found files here.
        public static void ProcessFile(string[] fileEntries, string newDestinationDir)
        {
            foreach (string s in fileEntries)
            {
                fileList.Enqueue(s);

                // Use static Path methods to extract only the file name from the path.
                string fileName = System.IO.Path.GetFileName(s);
                string destFile = System.IO.Path.Combine(newDestinationDir, fileName);
                destList.Enqueue(newDestinationDir);
                // System.IO.File.Copy(s, destFile, true);
                //CopyFile.CopyFiles();
            }
        }

        public static void CopyFiles()
        {
            string w = fileList.Dequeue();
            string q = destList.Dequeue();
            System.IO.File.Copy(w, q + "\\" + Path.GetFileName(w), true);
            n++;
            EventCounter.Signal();
        }
    }
}
