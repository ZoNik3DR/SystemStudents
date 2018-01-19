using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Input;

namespace ManagementSystemStudents.HelpClasses
{

    public class PicsSection : ConfigurationSection
    {
        [ConfigurationProperty("PicsCollection")]
        public PicsCollection PicsCollection
        {
            get { return ((PicsCollection)(base["PicsCollection"])); }
        }
    }




    [ConfigurationCollection(typeof(Pic), AddItemName = "Pic")]
    public class PicsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new Pic();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Pic)(element)).Target;
        }

        public Pic this[int idx]
        {
            get { return (Pic)BaseGet(idx); }
        }
    }

    public class Pic : ConfigurationElement
    {

        [ConfigurationProperty("target", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Target
        {
            get { return ((string)(base["target"])); }
            set { base["target"] = value; }
        }

        [ConfigurationProperty("path", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Path
        {
            get { return ((string)(base["path"])); }
            set { base["path"] = value; }
        }
    }






    public class AsyncXmlReader
    {
        List<string> keys;
        List<string> HotKeys;
        List<string> InstalledKeys;

        public AsyncXmlReader()
        {
            string[] arr ={
        "OpenM",
        "SaveM",
        "ExitM",
        "RightPlus",
        "LeftPlus",
        "MainIcon",
        "RightArrows",
        "RightArrow",
        "LeftArrows",
        "LeftArrow",
            };
            string[] hkeys =
            {
                "OpenM",
                "SaveM",
                "ExitM",
            };
            HotKeys = new List<string>(hkeys);
            keys = new List<string>(arr);
            InstalledKeys = new List<string>();
        }

        public async void LoadPic()
        {
            PicsSection section = (PicsSection)ConfigurationManager.GetSection("PicsSection");
            Application cur = Application.Current;
            if (section.PicsCollection.Count != 0)
            {
                string str = Environment.CurrentDirectory.ToString();
                str = Path.GetDirectoryName(str);

                if (!Directory.Exists(str + "\\Resources"))
                    return;
                foreach (Pic pic in section.PicsCollection)
                {
                    try
                    {
                        if (keys.Contains(pic.Target))
                        {
                            keys.Remove(pic.Target);
                            await cur.Dispatcher.BeginInvoke((Action)(() =>
                            {
                                try
                                {
                                    Uri ur;
                                    //               if (pic.Target == Path.GetFullPath(pic.Target))
                                    //                 ur = new Uri(pic.Target);
                                    //           else 
                                    ur = new Uri(str + pic.Path);
                                    BitmapImage imageBitmap = new BitmapImage(ur);
                                    cur.Resources.Add(pic.Target, imageBitmap);
                                    imageBitmap.Freeze();
                                }
                                catch
                                {

                                };
                            }));
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        public void LoadHotKeys()
        {

            foreach (var key in HotKeys)
            {
                Application.Current.Dispatcher.BeginInvoke((Action)(() => { 
                MainWindow wind = (MainWindow)Application.Current.Windows[0];
                KeyGestureConverter gesture = new KeyGestureConverter();
                string value = ConfigurationManager.AppSettings[key];
                if (InstalledKeys.Count != 0)
                {
                    if (InstalledKeys.Contains(value))
                        throw new ArgumentException();
                }
                InstalledKeys.Add(value);
                KeyGestureConverter converter = new KeyGestureConverter();
                try
                {
                    KeyGesture gest = (KeyGesture)converter.ConvertFromString(value);
                    switch (key)
                    {
                        case "OpenM":
                            wind.InputBindings.Add(new KeyBinding(wind.OpenM.Command, gest));
                            break;
                        case "SaveM":
                            wind.InputBindings.Add(new KeyBinding(wind.SaveM.Command, gest));
                            break;
                        case "ExitM":
                            wind.InputBindings.Add(new KeyBinding(wind.ExitM.Command, gest));
                            break;
                    }
                }
                catch
                { }
                }
                ));
            }
        }
    }
}
