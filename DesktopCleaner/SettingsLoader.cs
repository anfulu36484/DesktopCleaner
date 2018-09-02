using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DesktopCleaner
{
    class SettingsLoader
    {
        public string GetOutputFolderForAnyOtherExtension()
        {
            XDocument doc = XDocument.Load("Settings.xml");
            List<SettingsInfo> listSettingsInfo = new List<SettingsInfo>();
            string folderName="";

            foreach (XElement SIElement in doc.Element("SettingsInfo").Elements("SettingsForanyAnyOtherExtension"))
            {
                folderName= SIElement.Element("NameOfOutputFolder").Value;               
            }
            return folderName;
        }

        public List<SettingsInfo> LoadSettingsForExtensions()
        {
            XDocument doc = XDocument.Load("Settings.xml");
            List<SettingsInfo> listSettingsInfo = new List<SettingsInfo>();

            foreach (XElement SIElement in doc.Element("SettingsInfo").Elements("SettingsForOneTypeOfFiles"))
            {
                SettingsInfo SI = new SettingsInfo();

                SI.NameOfOutputFolder = SIElement.Element("NameOfOutputFolder").Value;

                List<string> fileTypes= new List<string>();

                foreach (var n in SIElement.Elements("FileType"))
                {
                    fileTypes.Add(n.Value);
                }

                SI.FileTypes = fileTypes;

                listSettingsInfo.Add(SI);
            }

            return listSettingsInfo;
        }

        public List<SettingsInfo> LoadSettingsForKeywords()
        {
            XDocument doc = XDocument.Load("Settings.xml");
            List<SettingsInfo> listSettingsInfo = new List<SettingsInfo>();

            foreach (XElement SIElement in doc.Element("SettingsInfo").Elements("SettingsForKeywords"))
            {
                SettingsInfo SI = new SettingsInfo();

                SI.NameOfOutputFolder = SIElement.Element("NameOfOutputFolder").Value;

                List<string> fileTypes = new List<string>();

                foreach (var n in SIElement.Elements("KeyWord"))
                {
                    fileTypes.Add(n.Value);
                }

                SI.FileTypes = fileTypes;

                listSettingsInfo.Add(SI);
            }

            return listSettingsInfo;
        }

    }
}
