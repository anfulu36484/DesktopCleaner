using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCleaner
{
    class SettingsInfo
    {
        public string NameOfOutputFolder { get; set; }
        public List<string> FileTypes { get; set; }

        public SettingsInfo() { }

        public SettingsInfo(string NameOfOutputFolder, List<string> fileTypes)
        {
            this.NameOfOutputFolder = NameOfOutputFolder;
            this.FileTypes = fileTypes;
        }
    }


}
