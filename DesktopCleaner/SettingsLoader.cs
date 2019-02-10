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
        private string settingsFileName = "Settings.xml";

        public SettingsInfo GetOutputFolderForAnyOtherExtension()
        {
            XDocument doc = XDocument.Load(settingsFileName);
            SettingsInfo settingsInfo =new SettingsInfo();
            settingsInfo.NameOfOutputFolder = doc.Element("SettingsInfo")
                .Element("SettingsForanyAnyOtherExtension").Element("NameOfOutputFolder").Value;
            return settingsInfo;
        }

        public List<SettingsInfo> LoadSettingsForExtensions()
        {
            XDocument doc = XDocument.Load(settingsFileName);
            List<SettingsInfo> listSettingsInfo = new List<SettingsInfo>();

            foreach (XElement SIElement in doc.Element("SettingsInfo").Elements("SettingsForOneTypeOfFiles"))
            {
                SettingsInfo SI = new SettingsInfo();
                SI.NameOfOutputFolder = SIElement.Element("NameOfOutputFolder").Value;
                SI.FileTypes=new List<string>();

                foreach (var n in SIElement.Elements("FileType"))
                    SI.FileTypes.Add(n.Value.ToLower());

                listSettingsInfo.Add(SI);
            }
            return listSettingsInfo;
        }

        public List<SettingsInfo> LoadSettingsForKeywords()
        {
            XDocument doc = XDocument.Load(settingsFileName);
            List<SettingsInfo> listSettingsInfo = new List<SettingsInfo>();

            foreach (XElement SIElement in doc.Element("SettingsInfo").Elements("SettingsForKeywords"))
            {
                SettingsInfo SI = new SettingsInfo();
                SI.NameOfOutputFolder = SIElement.Element("NameOfOutputFolder").Value;
                SI.Keywords=new List<string>();

                foreach (var n in SIElement.Elements("KeyWord"))
                    SI.Keywords.Add(n.Value.ToLower());

                listSettingsInfo.Add(SI);
            }
            return listSettingsInfo;
        }

    }
}
