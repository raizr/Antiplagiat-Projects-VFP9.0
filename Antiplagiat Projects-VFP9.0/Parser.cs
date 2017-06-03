using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Antiplagiat_Projects_VFP9._0 {
    public class Parser {
        public List<string> keywords;
        public List<int> InsHashWord;
        public Parser() {
            keywords = new List<string>();
            InsHashWord = new List<int>();
            keywords.AddRange(File.ReadAllLines("keywords"));
            /*Console.WriteLine(keywords[0]);
            Console.WriteLine(keywords.Count);*/
        }

        public string Find(string find) {
            int index = keywords.FindIndex(x => x == find);
            if( index >= 0) {
                InsHashWord.Add(index);
                //Console.Write("; H: " + index+" ("+find+")");
                return index.ToString();
            }else {
                return "";
            }
        }
    }
}
