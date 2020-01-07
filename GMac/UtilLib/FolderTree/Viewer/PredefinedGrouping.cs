using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace UtilLib.FolderTree.Viewer
{
    internal sealed class PredefinedGrouping
    {
        internal delegate IEnumerable<IGrouping<string, FolderTreeItem>> CreateGroupingDelegate(FolderItem rootFolder);


        internal readonly string DisplayName;

        internal readonly string Description;
        
        internal readonly CreateGroupingDelegate GroupingFunction;


        public PredefinedGrouping(string displayName, string description, CreateGroupingDelegate groupingFunction)
        {
            DisplayName = displayName;
            Description = description;
            GroupingFunction = groupingFunction;
        }


        internal static List<PredefinedGrouping> GroupingsList = new List<PredefinedGrouping>();

        private static SortedDictionary<string, string> _englishWordsList;


        static PredefinedGrouping()
        {
            GroupingsList.Add(new PredefinedGrouping("All Items", "View all files and folders in hierarchy", CreateGrouping_AllItems));

            GroupingsList.Add(new PredefinedGrouping("File Extensions", "Group files by extensions", CreateGrouping_FileExtensions));

            GroupingsList.Add(new PredefinedGrouping("Files Keywords", "Group files by keywords", CreateGrouping_Keywords));

            GroupingsList.Add(new PredefinedGrouping("Files English Keywords", "Group files by english keywords", CreateGrouping_EnglishKeywords));

            GroupingsList.Add(new PredefinedGrouping("Files With No English Keywords", "View files with names containing no english keywords", CreateGrouping_NoEnglishKeywords));

            GroupingsList.Add(new PredefinedGrouping("Files With All English Keywords", "View files with names having all english keywords", CreateGrouping_AllEnglishKeywords));

            GroupingsList.Add(new PredefinedGrouping("Files With Duplicate Size Plus Name", "View all files having the same size and name", CreateGrouping_DuplicateFileSizesPlusNames));

            GroupingsList.Add(new PredefinedGrouping("Duplicate Files", "View files with duplicate size and MD5 data hash. This grouping may take quite a while to perform", CreateGrouping_DuplicateFiles));
        }


        private static SortedDictionary<string, string> LoadEnglishWordsList()
        {
            if (ReferenceEquals(_englishWordsList, null) == false)
                return _englishWordsList;

            try
            {
                //TODO: Find a way to add this file as a resource inside the library itself
                var curDir = Path.GetDirectoryName(Application.ExecutablePath);

                var fileName = Path.Combine(curDir ?? String.Empty, "wordsEn.txt");

                var words = File.ReadAllLines(fileName);

                _englishWordsList = new SortedDictionary<string, string>();

                foreach (var word in words)
                    _englishWordsList.Add(word, word);

                return _englishWordsList;
            }
            catch
            {
                _englishWordsList = null;
            }

            return _englishWordsList;
        }

        private static IEnumerable<IGrouping<string, FolderTreeItem>> CreateGrouping_AllItems(FolderItem rootFolder)
        {
            return
                //From root folder ...
                rootFolder
                //Select all descendant files and folders ...
                .DescendantItems()
                //Then order the items by item path ...
                .OrderBy(item => item.ItemPath)
                //And put all of them in a single group
                .GroupBy(item => "All Items");
        }

        private static IEnumerable<IGrouping<string, FileItem>> CreateGrouping_DuplicateFileSizesPlusNames(FolderItem rootFolder)
        {
            return
                //From root folder ...
                rootFolder
                //Select all descendant files ..
                .DescendantFiles()
                //Then order the files by file path ...
                .OrderBy(item => item.ItemPath)
                //Then group the items by a string containing the (size + name) of files
                .GroupBy(item => item.FileSize().ToString("X8") + "-" + item.ItemName.ToLower())
                //Only select groups having more then one file
                .Where(item => item.Count() > 1)
                //Then order the groups by thier keys
                .OrderBy(item => item.Key);
        }

        private static IEnumerable<IGrouping<string, FileItem>> CreateGrouping_FileExtensions(FolderItem rootFolder)
        {
            return
                //From root folder ...
                rootFolder
                //Select all descendant files ...
                .DescendantFiles()
                //Then order the files by file path ...
                .OrderBy(item => item.ItemPath)
                //Then group files by extension
                .GroupBy(item => item.ItemExtension.ToLower())
                //Then order the groups by thier keys
                .OrderBy(item => item.Key);
        }

        private static IEnumerable<IGrouping<string, FolderTreeItem>> CreateGrouping_Keywords(FolderItem rootFolder)
        {
            return
                //From root folder ...
                rootFolder
                //Select all descendant files and folders ...
                .DescendantItems()
                //Then order the items by item path ...
                .OrderBy(item => item.ItemPath)
                //For each item, get the keywords in its name as a set of distinct
                //FolderTreeItemAttribute<string> objects and flatten the objects into a single list
                .SelectMany(item => item.ItemNameKeywords())
                //Then group all items by the keywords
                .GroupBy(itemAttr => itemAttr.Value, itemAttr => itemAttr.Item)
                //Then order the groups by thier keys
                .OrderBy(item => item.Key);
        }

        private static IEnumerable<IGrouping<string, FolderTreeItem>> CreateGrouping_EnglishKeywords(FolderItem rootFolder)
        {
            var englishWordsList = LoadEnglishWordsList();

            return
                //From root folder ...
                rootFolder
                //Select all descendant files and folders ...
                .DescendantItems()
                //Then order the items by item path ...
                .OrderBy(item => item.ItemPath)
                //For each item, get the english keywords in its name as a set of distinct
                //FolderTreeItemAttribute<string> objects and flatten the objects into a single list
                .SelectMany(item => item.ItemNameKeywords(englishWordsList))
                //Then group all items by the keywords
                .GroupBy(itemAttr => itemAttr.Value, itemAttr => itemAttr.Item)
                //Then order the groups by thier keys
                .OrderBy(item => item.Key);
        }

        private static IEnumerable<IGrouping<string, FileItem>> CreateGrouping_NoEnglishKeywords(FolderItem rootFolder)
        {
            var englishWordsList = LoadEnglishWordsList();

            return
                //From root folder ...
                rootFolder
                //Select all descendant files ...
                .DescendantFiles()
                //Only select files having no english keywords in their names
                .Where(item => item.ItemNameHasNoKeywordsFrom(englishWordsList))
                //Then order the files by file path ...
                .OrderBy(item => item.ItemPath)
                //And put all files in a single group
                .GroupBy(item => "All files with no english keywords");
        }

        private static IEnumerable<IGrouping<string, FileItem>> CreateGrouping_AllEnglishKeywords(FolderItem rootFolder)
        {
            var englishWordsList = LoadEnglishWordsList();

            return
                //From root folder ...
                rootFolder
                //Select all descendant files ...
                .DescendantFiles()
                //Only select files having all keywords as english keywords in their names
                .Where(item => item.ItemNameHasAllKeywordsFrom(englishWordsList))
                //Then order the files by file path ...
                .OrderBy(item => item.ItemPath)
                //And put all files in a single group
                .GroupBy(item => "All files with all english keywords");
        }

        private static IEnumerable<IGrouping<string, FileItem>> CreateGrouping_DuplicateFiles(FolderItem rootFolder)
        {
            return
                //From root folder ...
                rootFolder
                //Select all descendant files ...
                .DescendantFiles()
                //Then group them by size ...
                .GroupBy(item => item.FileSize())
                //Then select the groups having more than one file
                .Where(item => item.Count() > 1)
                //Then flatten the groups into a list of files
                .SelectMany(item => item)
                //Then order the files by path
                .OrderBy(item => item.ItemPath)
                //Then group the files by (size + data hash)
                .GroupBy(item => item.FileSize().ToString() + "-" + item.Md5DataHash())
                //Then select the groups having more than one file
                .Where(item => item.Count() > 1)
                //Then order groups by (size + data hash) key
                .OrderBy(item => item.Key);
        }
    }
}
