using System.IO;
using System.Linq;

namespace EVE
{
    public static class Paths
    {
        /// <summary>
        /// Converts a res path to a resources path for use with Resources.Load.
        /// </summary>
        /// <param name="path">The res path (must start with "res:").</param>
        /// <returns>A path valid for use with Resources.Load.</returns>
        public static string ResolveResPath(string path)
        {
            path = path.Replace("\\", "/");
            if (path.StartsWith("res:/"))
                path = path.Replace("res:/", "res/");
            return Path.ChangeExtension(path, null);
        }

        /// <summary>
        /// Converts a res path to an Asset path usable by general Unity methods.
        /// </summary>
        /// <param name="path">The res path (must start with "res:").</param>
        /// <returns>A path for general use in unity.</returns>
        public static string ResolveResPathGeneral(string path)
        {
            path = path.Replace("\\", "/");
            if (path.StartsWith("res:/"))
                return path.Replace("res:/", "Assets/SOF/Resources/res/");
            else
                return path;
        }
    }
}