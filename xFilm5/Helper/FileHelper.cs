using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xFilm5.Helper
{
    public class FileHelper
    {
        /// <summary>  
        /// This class provides basic facilities for manipulating files and file paths.
        /// 
        /// <h3>File-related methods</h3>
        /// There are methods to 
        /// <list type="bullet">
        ///     <item>copy a file to another file,</item>
        ///     <item>compare the content of 2 files,</item>
        ///     <item>delete files using the wildcard character,</item>
        ///     <item>etc</item>
        /// </list>
        /// </summary>
        ///     
        public sealed class FileUtils
        {
            private static bool hasWildCards(string file)
            {
                return file.IndexOf("*") > -1;
            }

            /// ---------------------------------------------------------------
            /// <summary>
            /// Get all the files that matches a wildcard pattern, eg. (*.tmp)
            /// </summary>
            /// <param name="pathPattern">Wildcard pattern to search, eg. (Profile*.doc)</param>
            /// <returns>an array of FileInfo objects that results from the wildcard pattern file search</returns>
            /// ---------------------------------------------------------------
            public static FileInfo[] GetFilesMatchWildCard(string pathPattern)
            {
                FileInfo[] files = new FileInfo[0];
                if (hasWildCards(pathPattern))
                {
                    string dir = Path.GetDirectoryName(pathPattern);
                    DirectoryInfo info = new DirectoryInfo(dir);
                    string pattern = Path.GetFileName(pathPattern);
                    if (info.Exists)
                        files = info.GetFiles(pattern);
                }
                else
                {
                    files = new FileInfo[] { new FileInfo(pathPattern.Trim()) };
                }
                return files;
            }

            public static FileInfo[] GetFilesMatchWildCard(string pathPattern, string loginUser, string loginPassword)
            {
                FileInfo[] files = new FileInfo[0];
                if (hasWildCards(pathPattern))
                {
                    var uri = new Uri(Path.GetDirectoryName(pathPattern));
                    System.Net.NetworkCredential readCredentials = new System.Net.NetworkCredential(loginUser, loginPassword);

                    using (new NetworkConnection(String.Format(@"\\{0}", uri.Host), readCredentials))
                    {

                        string dir = Path.GetDirectoryName(pathPattern);
                        DirectoryInfo info = new DirectoryInfo(dir);
                        string pattern = Path.GetFileName(pathPattern);
                        if (info.Exists)
                            files = info.GetFiles(pattern);
                    }
                }
                else
                {
                    files = new FileInfo[] { new FileInfo(pathPattern.Trim()) };
                }
                return files;
            }
        }
    }
}
