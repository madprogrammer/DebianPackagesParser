using System.Collections.Generic;
using System.IO;
using System.Dynamic;

namespace MadProgrammer
{
    public class DebianPackageListParser
    {
        public static IEnumerable<dynamic> Parse(string filename)
        {
            return Parse(new StreamReader(new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), true));
        }

        public static IEnumerable<dynamic> Parse(TextReader reader)
        {
            string line;
            var item = new ExpandoObject() as IDictionary<string, object>;

            while ((line = reader.ReadLine()) != null) {
                if (line != "")
                {
                    // Sanity check
                    if (line.StartsWith(" ") || line.IndexOf(':') == -1)
                        continue;

                    var split = line.Split(new char[] { ':' }, 2);
                    item.Add(split[0], split[1].Trim());
                }
                else
                {
                    yield return item;
                    item = new ExpandoObject() as IDictionary<string, object>;
                }
            }
        }
    }
}
