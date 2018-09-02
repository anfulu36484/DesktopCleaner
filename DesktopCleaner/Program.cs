using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            SettingsLoader settingsLoader =new SettingsLoader();
            List<SettingsInfo> listSettingsInfoForExtensions = settingsLoader.LoadSettingsForExtensions();
            List<SettingsInfo> listSettingsInfoForKeywords = settingsLoader.LoadSettingsForKeywords();


            //var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            var desktopFolder = @"C:\Users\pk\Desktop\test\";
            DirectoryInfo directory = new DirectoryInfo(desktopFolder);
            FileInfo[] filesAndHidden = directory.GetFiles();
            var files = filesAndHidden.Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden));
            

            //ModeDirectories(directory, listSettingsInfoForExtensions);


            if (files.Count() > 0)
            {
                var fileToMoveByKeywords =MoveFilesByKeywords(listSettingsInfoForKeywords, files);
                files = files.Except(fileToMoveByKeywords);
                MoveFilesByExtension(listSettingsInfoForExtensions, files);
                MoveFilesByExtension(listSettingsInfoForExtensions, files);
                MoveFilesAnyOtherExtension(settingsLoader, files);
            }


            
        }

       /* private static void ModeDirectories(DirectoryInfo directory, List<SettingsInfo> listSettingsInfoForExtensions)
        {
            DirectoryInfo[] directories = directory.GetDirectories();

            string directoryOutput =
                listSettingsInfoForExtensions.First(n =>
                    n.NameOfOutputFolder ==new SettingsInfo("SettingsForOneTypeOfFiles",null).NameOfOutputFolder );
            foreach (var directoryInfo in directories)
            {
                Directory.Move(directoryInfo.FullName, directoryOutput + "/" + directoryInfo.Name);
            }
        }*/

        private static void MoveFilesByExtension(List<SettingsInfo> listSettingsInfo, IEnumerable<FileInfo> files)
        {
            foreach (var settingsInfo in listSettingsInfo)
            {
                if (!Directory.Exists(settingsInfo.NameOfOutputFolder))
                    Directory.CreateDirectory(settingsInfo.NameOfOutputFolder);


                foreach (string settingsInfoFileType in settingsInfo.FileTypes)
                {
                    var filesToMove = files.Where(n => Path.GetExtension(n.FullName).ToLower() == "." + settingsInfoFileType);
                    files = files.Except(filesToMove);
                    filesToMove.ToList().ForEach(n => File.Move(n.FullName, settingsInfo.NameOfOutputFolder + @"\" + n.Name));
                }
            }
        }

       
        private static List<FileInfo> MoveFilesByKeywords(List<SettingsInfo> listSettingsInfoForKeywords, IEnumerable<FileInfo> files)
        {
            List<FileInfo> fileToMoveOutput=new List<FileInfo>();
            foreach (var settingsInfo in listSettingsInfoForKeywords)
            {
                if (!Directory.Exists(settingsInfo.NameOfOutputFolder))
                    Directory.CreateDirectory(settingsInfo.NameOfOutputFolder);

                
                foreach (string settingsInfoFileType in settingsInfo.FileTypes)
                {
                    var filesToMove = files.Where(n => n.Name.ToLower().Contains(settingsInfoFileType.ToLower()));
                    files = files.Except(filesToMove);
                    filesToMove.ToList().ForEach(n => File.Move(n.FullName, settingsInfo.NameOfOutputFolder + @"\" + n.Name));

                    fileToMoveOutput.AddRange(filesToMove);
                }
            }

            return fileToMoveOutput;
        }


        private static void MoveFilesAnyOtherExtension(SettingsLoader settingsLoader, IEnumerable<FileInfo> files)
        {

            string outputFolder = settingsLoader.GetOutputFolderForAnyOtherExtension();

            if (!Directory.Exists(outputFolder));
            Directory.CreateDirectory(outputFolder);


            var anyfiles = files.Where(n => Path.GetExtension(n.FullName).ToLower() != ".lnk");

             anyfiles.ToList().ForEach(n => File.Move(n.FullName, outputFolder + @"\" + n.Name));

        }

    }
}
