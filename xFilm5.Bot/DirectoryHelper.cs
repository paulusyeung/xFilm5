using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Origional: http://www.techmikael.com/2010/02/directory-search-with-multiple-filters.html
/// One version takes a regex pattern \.mp3|\.mp4, and the other a string list and runs in parallel.
/// </summary>
namespace xFilm5.Bot
{
    public class DirectoryHelper
    {
        // Works in .Net 3.5 - you might want to create several overloads
        //public static string[] GetFiles(string path, string searchPatternExpression, SearchOption searchOption)
        //{
        //    if (searchPatternExpression == null) searchPatternExpression = string.Empty;
        //    Regex reSearchPattern = new Regex(searchPatternExpression);
        //    return Directory.GetFiles(path, "*", searchOption).Where(file => reSearchPattern.IsMatch(Path.GetFileName(file))).ToArray();
        //}

        // Works in .Net 4.0 - inferred overloads with default values
        public static IEnumerable<string> GetFiles(string path, string searchPatternExpression = "", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            Regex reSearchPattern = new Regex(searchPatternExpression);
            return Directory.EnumerateFiles(path, "*", searchOption).Where(file => reSearchPattern.IsMatch(Path.GetFileName(file)));
        }

        // Works in .Net 4.0 - takes same patterns as old method, and executes in parallel
        public static IEnumerable<string> GetFiles(string path, string[] searchPatterns, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return searchPatterns.AsParallel().SelectMany(searchPattern => Directory.EnumerateFiles(path, searchPattern, searchOption));
        }
    }
}