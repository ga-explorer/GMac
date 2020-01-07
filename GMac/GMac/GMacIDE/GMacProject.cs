using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using CodeComposerLib.Irony.SourceCode;
using TextComposerLib.Settings;

namespace GMac.GMacIDE
{
    /// <summary>
    /// This class represents a GMac project
    /// </summary>
    [Serializable]
    public sealed class GMacProject : LanguageCodeProject
    {
        /// <summary>
        /// Create a new GMac project and save its relevant information to the given file
        /// </summary>
        /// <param name="projectFilePath">The file to which the project is saved</param>
        /// <returns>The new GMac project</returns>
        public static GMacProject CreateNew(string projectFilePath)
        {
            var project = new GMacProject(projectFilePath);

            project.SaveProjectToXmlFile();

            return project;
        }

        /// <summary>
        /// Create a previously saved project from the given file using serialization
        /// </summary>
        /// <param name="projectFilePath">The file to which the project is saved</param>
        /// <returns>The GMac project</returns>
        public static GMacProject CreateFromFile(string projectFilePath)
        {
            GMacProject project;

            using (Stream stream = File.Open(projectFilePath, FileMode.Open, FileAccess.Read))
            {
                var formatter = new BinaryFormatter();

                project = (GMacProject)formatter.Deserialize(stream);

                project.ProjectFilePath = projectFilePath;
            }

            return project;
        }

        public static GMacProject CreateFromXmlFile(string projectFilePath)
        {
            var project = new GMacProject(projectFilePath);

            var settingsComposer = new SettingsComposer
            {
                XmlConverter = {TextEncoding = Encoding.Unicode}
            };

            settingsComposer.UpdateFromFile(projectFilePath);

            const string prefix = "sourceFilePath";

            var fileItems = 
                settingsComposer
                .Where(item => item.Key.Substring(0, prefix.Length) == prefix);


            foreach (var item in fileItems)
            {
                var filePath = Path.GetFullPath(Path.Combine(project.ProjectFolderPath, item.Value));

                project.AddSourceFile(filePath, Encoding.Unicode);
            }

            return project;
        }


        private GMacProject(string projectFilePath)
            : base(projectFilePath)
        {
            LanguageName = "GMacDSL";
            AllowedSourceFilesExtensions = new[] { "gmac" };
        }


        public override void SaveProjectToFile()
        {
            //Reduce storage of project file by removing all source code lines information from DSL files objects
            ClearSourceCodeLines();

            using (Stream stream = File.Open(ProjectFilePath, FileMode.Create, FileAccess.Write))
            {
                var formatter = new BinaryFormatter();

                formatter.Serialize(stream, this);
            }
        }

        public string SaveProjectToXmlFile()
        {
            var settingsComposer = new SettingsComposer
            {
                XmlConverter = {TextEncoding = Encoding.Unicode}
            };

            var i = 0;
            foreach (var sf in SourceCodeFiles)
                settingsComposer["sourceFilePath" + (i++)] = sf.FileRelativePath;

            return settingsComposer.ToFile(ProjectFilePath);
        }
    }
}
