using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FindCharInString
{
    class DataManager
    {
       private string data { get; set; }
        private List<string> list;
        public DataManager(string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
           // var list = new List<string>();
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                data = null;
                list = new List<string>();
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line))
                        data += line;// list.Add(line);//dodawanie do listy;
                }
            }
            Regex.Replace(data, @"s", "");
        }

        public void divideData(int threadNumber, int charPerThread)
        {
            int strCounter = 0;
            for (int i = 0; i < threadNumber; i++)
            {
                string str = null;
                if (i == threadNumber - 1)
                {//load rest of data
                    while (strCounter < data.Length)
                    {
                        str += data[strCounter];
                        strCounter++;
                    }
                    list.Add(str);
                    strCounter = 0;
                    break;
                }
                for (int j = 0; j < charPerThread; j++)
                {
                    str += data[strCounter];
                    strCounter++;
                }
                list.Add(str);
            }
        }
        public string getData()
        {
            return this.data;
        }
        public List<string> getList()
        {
            return this.list;
        }
    }
   
}
