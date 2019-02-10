using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCleaner
{
    class FileNameLoader
    {
        private List<FileInfo> GetNameOfAllFiles()
        {
            string desktopFolder= @"C:\Users\pk\Desktop\test\";
            DirectoryInfo directory = new DirectoryInfo(desktopFolder);
            System.IO.FileInfo[] myFilesAndHidden = directory.GetFiles();
            var files = myFilesAndHidden
                .Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden)).ToList();
            return files;
        }

        private List<MyFileInfo> GetFileInfoForExtensions(List<SettingsInfo> listSettingsInfo, List<FileInfo> infoOfAllFiles)
        {
            List <MyFileInfo> output = new List<MyFileInfo>();
            foreach (var infoOfFile in infoOfAllFiles)
            {
                string fileExtension = Path.GetExtension(infoOfFile.FullName);
                foreach (var settingsInfo in listSettingsInfo)
                {
                    if (settingsInfo.FileTypes.Contains(fileExtension))
                    {
                        output.Add(new MyFileInfo()
                        {
                            FileName = infoOfFile.Name,
                            FullFileName = infoOfFile.FullName,
                            OutputFolder = settingsInfo.NameOfOutputFolder
                        });
                        break;
                    }
                }
            }
            return output;
        }

        private List<MyFileInfo> GetFileInfoForKeywords(List<SettingsInfo> listSettingsInfo, List<FileInfo> infoOfAllFiles)
        {
            List<MyFileInfo> output = new List<MyFileInfo>();
            foreach (var infoOfFile in infoOfAllFiles)
            {
                foreach (var settingsInfo in listSettingsInfo)
                {
                    if (settingsInfo.Keywords.Any(n=> infoOfFile.Name.Contains(n)))
                    {
                        output.Add(new MyFileInfo()
                        {
                            FileName = infoOfFile.Name,
                            FullFileName = infoOfFile.FullName,
                            OutputFolder = settingsInfo.NameOfOutputFolder

                        });
                        break;
                    }
                }
            }
            return output;
        }

        private List<MyFileInfo> GetAnyOtherFiles(SettingsInfo SettingsInfo, List<FileInfo> infoOfAllFiles)
        {
            var anyfiles = infoOfAllFiles.Where(n => Path.GetExtension(n.FullName) != ".lnk")
                .Select(n => new MyFileInfo()
                {
                    FileName = n.Name,
                    FullFileName = n.FullName,
                    OutputFolder = SettingsInfo.NameOfOutputFolder
                })
                .ToList();
            return anyfiles;
        }

        public List<MyFileInfo> Load()
        {
            SettingsLoader settingsLoader = new SettingsLoader();
            var infoOfAllFiles = GetNameOfAllFiles();
            var filesByKeywords = GetFileInfoForKeywords(settingsLoader.LoadSettingsForKeywords(), infoOfAllFiles);
            var filesByExtension = GetFileInfoForExtensions(settingsLoader.LoadSettingsForExtensions(), infoOfAllFiles)
                .Except(filesByKeywords,new MyFileInfoComparer()).ToList();
            var anyOtherFiles = GetAnyOtherFiles(settingsLoader.GetOutputFolderForAnyOtherExtension(), infoOfAllFiles)
                .Except(filesByKeywords, new MyFileInfoComparer())
                .Except(filesByExtension, new MyFileInfoComparer()).ToList();
            var x =filesByKeywords.Concat(filesByExtension).Concat(anyOtherFiles).ToList();
            //var y = x.Distinct().ToList();

            return x;
        }

       
    }

    class MyFileInfoComparer : IEqualityComparer<MyFileInfo>
    {
        public bool Equals(MyFileInfo x, MyFileInfo y)
        {
            return x.FileName == y.FileName;
        }

        public int GetHashCode(MyFileInfo obj)
        {
            return obj.GetHashCode();
        }
    }

}
