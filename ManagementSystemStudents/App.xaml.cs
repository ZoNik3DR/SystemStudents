using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using ManagementSystemStudents.ViewModels;
using System.Collections.ObjectModel;
using System.IO.Compression;
using IPluginSpace;
using System.Reflection;
using System.Threading;
using ManagementSystemStudents.HelpClasses;

namespace ManagementSystemStudents
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 



    public class Database
    {
        //Write Students
        string Path;
        string dbPath;
        string backupPath;
        DirectoryInfo Dir;
        FileInfo StudentsFile;
        FileInfo GroupsFile;
        FileInfo LecturesFile;

        private List<Group> groups;
        private List<Lecture> lectures;
        private List<Student> students;

        public List<Group> Groups => groups;
        public List<Lecture> Lectures => lectures;
        public List<Student> Students => students;


        public Database(string Path, string dbPath, string backupPath)
        {
            this.Path = Path;
            this.dbPath = dbPath;
            this.backupPath = backupPath;
        }

        public Database()
        {
            Path = Environment.CurrentDirectory;
            if (Path.Contains("Debug"))
                Path = Path.Replace("Debug", "");
            dbPath = Path + @"DB";
            backupPath = Path + @"backup";

        }

        public Database(string Path)
        {
            this.Path = Path;
            this.dbPath = Path + @"\DB";
            this.backupPath = Path + @"\backup";
        }

        private string WriteStudent(Student stud)
        {

            (string name, string surname, string midname, int receiptyear,
        string groupnum, bool iscaptain, string[] prev, MarkSubject[] Marks) = stud;

            return name + "\n" + surname + "\n" + midname + "\n" + receiptyear + "\n" + groupnum + "\n" + iscaptain + "\n"
                + prev.Length + "\n"
                + string.Join("\n", prev) + "\n" + Marks.Length + "\n"
                + string.Join("\n", Marks.Select(x => x.ToString()).ToArray());
        }
        private void WriteStudents(List<Student> students)
        {
            using (FileStream fs = StudentsFile.Open(FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (TextWriter writer = new StreamWriter(fs))
                {
                    writer.WriteLine(students.Count);
                    foreach (var s in students)
                        writer.WriteLine(WriteStudent(s));
                }
            }
        }

        //Read Students
        private Student ReadStudent(TextReader Reader)
        {
            string name = Reader.ReadLine();
            string surname = Reader.ReadLine();
            string midname = Reader.ReadLine();
            int receiptyear = int.Parse(Reader.ReadLine());
            string groupnum = Reader.ReadLine(); //temp solution
            bool iscaptain = bool.Parse(Reader.ReadLine());
            int n = int.Parse(Reader.ReadLine());
            List<string> prev = new List<string>(n);
            for (int i = 0; i < n; ++i)
                prev.Add(Reader.ReadLine());
            if (n == 0) Reader.ReadLine();
            n = int.Parse(Reader.ReadLine());
            List<MarkSubject> Marks = new List<MarkSubject>(n);
            for (int i = 0; i < n; ++i)
            {
                //        Marks[i].SubName = Reader.ReadLine();
                //          Marks[i].Exam = int.Parse(Reader.ReadLine());
                string subname = Reader.ReadLine();
                int exam = int.Parse(Reader.ReadLine());
                Marks.Add(new MarkSubject(subname, exam));
            }
            if (n == 0) Reader.ReadLine();
            return new Student(name, surname, midname, receiptyear, iscaptain, prev, Marks, groupnum); //temp solution
        }
        private List<Student> ReadStudents()
        {
            List<Student> studentslist = new List<Student>();
            if (StudentsFile.Exists)
            {
                using (FileStream fs = StudentsFile.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    using (TextReader reader = new StreamReader(fs))
                    {
                        int n = int.Parse(reader.ReadLine());
                        for (int i = 0; i < n; ++i)
                        {
                            studentslist.Add(ReadStudent(reader));
                        }
                        reader.ReadLine();
                    }
                }

            }
            studentslist.GroupBy(x => x.getGroupNumNotLinked);
            var StudentsbyGroups = studentslist.GroupBy(x => x.getGroupNumNotLinked).ToList();
            foreach (var s in StudentsbyGroups)
                foreach (var Student in s)
                    Student.FirstSetGroup(groups.FirstOrDefault(g => g.GroupNum == s.Key));
            return studentslist;

        }

        //Write Lectures
        private string WriteLecture(Lecture lecture)
        {
            return lecture.Name + "\n" + lecture.SubjectName;
        }
        private void WriteLectures(List<Lecture> lectures)
        {
            using (FileStream fs = LecturesFile.Open(FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (TextWriter writer = new StreamWriter(fs))
                {
                    writer.WriteLine(lectures.Count);
                    foreach (var l in lectures)
                        writer.WriteLine(WriteLecture(l));
                }
            }
        }

        //Read Lectures
        private Lecture ReadLecture(TextReader Reader)
        {
            string name = Reader.ReadLine();
            string subject = Reader.ReadLine();
            return new Lecture(name, subject);
        }
        private List<Lecture> ReadLectures()
        {
            List<Lecture> lectureslist = new List<Lecture>();
            if (LecturesFile.Exists)
            {
                using (FileStream fs = LecturesFile.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    using (TextReader reader = new StreamReader(fs))
                    {
                        int n = int.Parse(reader.ReadLine());
                        for (int i = 0; i < n; ++i)
                        {
                            lectureslist.Add(ReadLecture(reader));
                        }
                        reader.ReadLine();
                    }
                }

            }
            return lectureslist;
        }


        //Write Groups
        private void WriteGroup(Group group, BinaryWriter writer)
        {
            writer.Write(group.GroupNum);
            writer.Write(group.isdisbanded);
            writer.Write(group.Terms.Count);
            foreach (var t in group.Terms)
            {
                writer.Write(t.Lectures.Count);
                if (t.Lectures.Count != 0)
                    foreach (var l in t.Lectures)
                        writer.Write(l.ToString());
                // writer.Write(string.Join("", t.Lectures));
            }
        }

        private void WriteGroups(List<Group> groups)
        {
            using (FileStream fs = GroupsFile.Open(FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (var Deflate = new DeflateStream(fs, CompressionMode.Compress))
                {
                    using (BinaryWriter writer = new BinaryWriter(Deflate))
                    {
                        writer.Write(groups.Count);
                        foreach (var g in groups)
                            WriteGroup(g, writer);
                    }
                }
            }

        }

        private Group ReadGroup(BinaryReader Reader)
        {
            string groupnum = Reader.ReadString();
            bool isdisbanded = Reader.ReadBoolean();
            int n = Reader.ReadInt32(); //terms counter
            int lect = 0; //lectures counter
            List<Term> terms = new List<Term>(n);
            for (int i = 0, j = 1; i < n; ++i, ++j)
            {
                terms.Add(new Term(j));
                lect = Reader.ReadInt32();
                for (int z = 0; z < lect; ++z)
                {
                    string LectToString = Reader.ReadString();
                    terms[i].Add(lectures.First(x => x.ToString() == LectToString));
                }

            }
            return new Group(groupnum, isdisbanded, terms);
        }

        private List<Group> ReadGroups()
        {
            List<Group> groups = new List<Group>();

            try
            {
                using (FileStream fs = GroupsFile.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    using (var Deflate = new DeflateStream(fs, CompressionMode.Decompress))
                    {
                        using (BinaryReader reader = new BinaryReader(Deflate))
                        {
                            int n = reader.ReadInt32();
                            for (int i = 0; i < n; ++i)
                            {
                                groups.Add(ReadGroup(reader));
                            }
                        }
                    }
                }
            }
            catch
            { }
            return groups;
        }


        //CopyExistingFiles
        private void BackUp()
        {
            DirectoryInfo dirbackup = new DirectoryInfo(backupPath);
            if (!Directory.Exists(backupPath))
                Directory.CreateDirectory(backupPath);
            else
                foreach (FileInfo file in dirbackup.GetFiles())
                    file.Delete();
            foreach (var f in Dir.GetFiles())
                f.CopyTo(backupPath + @"\" + f.Name);
        }


        public void readFromDb(MainViewModel vm)
        {
            Dir = new DirectoryInfo(dbPath);
         //  Thread.Sleep(3000);
            if (!Dir.Exists)
            {
                students = new List<Student>();
                groups = new List<Group>();
                lectures = new List<Lecture>();
                return;
            }

            StudentsFile = new FileInfo(dbPath + @"\StudentsInfo.txt");
            GroupsFile = new FileInfo(dbPath + @"\GroupsInfo.txt");
            LecturesFile = new FileInfo(dbPath + @"\LecturesInfo.txt");

            lectures = ReadLectures();
            groups = ReadGroups();
            students = ReadStudents();
            vm.ApendCollections(students, groups, lectures);
        }

        public void SaveToDb(List<Student> students, List<Group> groups, List<Lecture> lectures)
        {
            Dir = new DirectoryInfo(dbPath);
            if (!Dir.Exists)
                Dir.Create();
            BackUp();

            StudentsFile = new FileInfo(dbPath + @"\StudentsInfo.txt");
            GroupsFile = new FileInfo(dbPath + @"\GroupsInfo.txt");
            LecturesFile = new FileInfo(dbPath + @"\LecturesInfo.txt");

            WriteStudents(students);
            WriteGroups(groups);
            WriteLectures(lectures);
        }
    }



    public class Plugins : List<IPlugin>
    {
        private static readonly string pluginsPath = Path.Combine(Directory.GetCurrentDirectory(), "plugins");
        MainViewModel mvm;


        public Plugins(MainViewModel obj)
        {
            mvm = obj;
        }

        public async void Init(string path)
        {
            await Task.Delay(2000);
            DirectoryInfo directPlugins;
            if (path != null)
                if (path == Path.GetFullPath(path))
                    directPlugins = new DirectoryInfo(path);
                else directPlugins = new DirectoryInfo(Environment.CurrentDirectory.ToString() + path);
            else return;
                //directPlugins = new DirectoryInfo(pluginsPath);
            if (directPlugins.Exists)
            {
                var pluginsDll = directPlugins.GetFiles();
                foreach (var dll in pluginsDll)
                {
                    if (dll.Extension == ".dll")
                    {
                        Assembly assembly = Assembly.LoadFrom(Path.Combine(pluginsPath, dll.Name));
                        foreach (var type in assembly.GetExportedTypes())
                        {
                            var typeRealPl = type.GetInterface("IPlugin");
                            if (typeRealPl != null)
                            {
                                IPlugin plug = (IPlugin)Activator.CreateInstance(type);
                                Add(plug);
                            }
                        }
                    }
                }
                mvm.LoadMenu(this.ToList());
            }
        }
    }  




    public partial class App : Application
    {
        
       private async void OnStartup(object sender, StartupEventArgs e)
       {

            Database db = new Database();
            MainViewModel viewModel = new MainViewModel();
            MainWindow view = new MainWindow();
            AsyncXmlReader reader = new AsyncXmlReader();
            Plugins loadplug = new Plugins(viewModel);
            string pluginspath = ConfigurationManager.AppSettings["PluginsFolder"];

            

  //          await Task.Run( () => loadplug.Init(pluginspath));
            view.DataContext = viewModel;
            view.Show();
            viewModel.SetWind(view);

            await Task.Run(() => reader.LoadPic());
            await Task.Run(() => db.readFromDb(viewModel));
            await Task.Run( () => reader.LoadHotKeys());

        }

    }
}
