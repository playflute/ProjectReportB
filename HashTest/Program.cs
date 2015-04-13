using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HashTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            Dictionary<string, Int32> dic = new Dictionary<string, int>();
            dic.Add("Lily", 1);
            dic.Add("Zhiagang", 1);
            dic.Add("SunYi",1);
            if (dic.ContainsKey("Luly"))
            {
                count = ++dic["Luly"];
                //dic.Add("Lily", count);
            }
            else
                dic.Add("Luly", 1);

            Console.Read();
        }
    }
}
