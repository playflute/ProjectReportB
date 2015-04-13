using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace Multi_Thread_Test
{
    class Program
    {
        public static void Main()
        {
            // Create an instance of the Example class, and start two
            // timers.
            Program ex = new Program();
            ex.StartTimer(2000);
            //ex.StartTimer(1000);

            Console.WriteLine("Press Enter to end the program.");
            Console.ReadLine();
        }

        public void StartTimer(int dueTime)
        {
            Timer t = new Timer(new TimerCallback(TimerProc));
            t.Change(0, 10000);//第一个参数是延迟，第二个参数是间隔时间
        }

        private void TimerProc(object state)
        {
            // The state object is the Timer object.
            //我的一系列主要操作
            //更改数据库，我认为应该先删除之前的表数据，从新读取EXCEL，重写数据库表
            //Timer t = (Timer)state;
            //t.Dispose();
            Console.WriteLine("The timer callback executes.");
        }
    }
}
