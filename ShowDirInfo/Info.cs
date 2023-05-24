using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShowDirInfo
{
    class Info : IInfo
    {
        ExeptionDelegate exeptionViewer;
        public Info(ExeptionDelegate viewer)
        {
            exeptionViewer = viewer;
            StreamWriter writer = new StreamWriter("log.txt");
            writer.Close();
        }
        public long DirectoriesInfo(string path)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                DateTime currTime = DateTime.Now;
                long dirSize = 0;
                foreach (var dir in dirInfo.GetDirectories())
                    dirSize += DirectoriesInfo(dir.FullName); 
                foreach (var file in dirInfo.GetFiles())
                {
                    dirSize += file.Length;
                }
                using (StreamWriter writer = new StreamWriter("log.txt", true))
                {
                    var dirName = Path.GetFileNameWithoutExtension(dirInfo.FullName);
                    var dirTime = Directory.GetCreationTime(dirInfo.FullName);
                    var dirWTime = Directory.GetLastWriteTime(dirInfo.FullName);
                    var dirControl = Directory.GetAccessControl(dirInfo.FullName);
                    var dirAtributes = dirInfo.Attributes;
                    writer.WriteLine("Папка: " + dirName);
                    writer.WriteLine("\tРазмер: " + dirSize + " Б");
                    writer.WriteLine("\tДата создания: " + dirTime);
                    writer.WriteLine("\tДата измениения: " + dirWTime);
                    writer.WriteLine("\tПрава доступа: " + dirControl.AccessRightType.Attributes.ToString());
                    writer.WriteLine("\tАтрибуты: " + dirAtributes.ToString());
                }
                return dirSize;
            }
            catch (Exception e)
            {
                exeptionViewer?.Invoke(e.Message);
                using (StreamWriter writer = new StreamWriter("log.txt", true))
                    writer.WriteLine(e.Message);
                return 0;
            }
        }
    }
}
