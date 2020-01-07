using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace UtilLib.FolderTree
{
    public static class FolderTreeUtils
    {
        private static readonly MD5 Md5 = MD5.Create();
        
        private static readonly SHA512 Sha512 = SHA512.Create();

        private static readonly Regex KeywordsRegex = new Regex(@"[a-z0-9]+");


        public static long FileSize(this FileItem fileItem)
        {
            var fileInfo = new FileInfo(fileItem.ItemPath);

            return fileInfo.Length;
        }

        public static string Md5DataHash(this FileItem fileItem)
        {
            using (var stream = File.OpenRead(fileItem.ItemPath))
                return BitConverter.ToString(Md5.ComputeHash(stream));
        }

        public static string Sha512DataHash(this FileItem fileItem)
        {
            using (var stream = File.OpenRead(fileItem.ItemPath))
                return BitConverter.ToString(Sha512.ComputeHash(stream));
        }

        public static bool ItemNameHasNoKeywordsFrom(this FolderTreeItem item, SortedDictionary<string, string> allowedKeywords)
        {
            var matches = KeywordsRegex.Matches(item.ItemNameWithoutExtension.ToLower());

            for (var i = 0; i < matches.Count; i++)
            {
                var keyword = matches[i].Value;

                if (allowedKeywords.ContainsKey(keyword))
                    return false;
            }

            return true;
        }

        public static bool ItemNameHasAllKeywordsFrom(this FolderTreeItem item, SortedDictionary<string, string> allowedKeywords)
        {
            var matches = KeywordsRegex.Matches(item.ItemNameWithoutExtension.ToLower());

            for (var i = 0; i < matches.Count; i++)
            {
                var keyword = matches[i].Value;

                if (allowedKeywords.ContainsKey(keyword) == false)
                    return false;
            }

            return true;
        }

        public static IEnumerable<FolderTreeItemAttribute<string>> ItemNameKeywords(this FolderTreeItem item)
        {
            var keywords = new List<string>();

            var matches = KeywordsRegex.Matches(item.ItemNameWithoutExtension.ToLower());

            for (var i = 0; i < matches.Count; i++)
                keywords.Add(matches[i].Value);

            return 
                keywords
                .Distinct()
                .Select(value => new FolderTreeItemAttribute<string>(item, value));
        }

        public static IEnumerable<FolderTreeItemAttribute<string>> ItemNameKeywords(this FolderTreeItem item, SortedDictionary<string, string> allowedKeywords)
        {
            var keywords = new List<string>();

            var matches = KeywordsRegex.Matches(item.ItemNameWithoutExtension.ToLower());

            for (var i = 0; i < matches.Count; i++)
            {
                var keyword = matches[i].Value;

                if (allowedKeywords.ContainsKey(keyword))
                    keywords.Add(matches[i].Value);
            }

            return
                keywords
                .Distinct()
                .Select(value => new FolderTreeItemAttribute<string>(item, value));
        }
    }
}
