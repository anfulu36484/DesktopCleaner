using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCleaner
{
    class FileMover
    {

        void MoveFile(MyFileInfo myFileInfo)
        {
            if (!Directory.Exists(myFileInfo.OutputFolder))
                Directory.CreateDirectory(myFileInfo.OutputFolder);

            File.Move(myFileInfo.FullFileName, myFileInfo.OutputFolder + myFileInfo.FileName);
        }

        public void Move()
        {
            FileNameLoader fileNameLoader =new FileNameLoader();
            fileNameLoader.Load().ForEach(MoveFile);
        }
    }
}
