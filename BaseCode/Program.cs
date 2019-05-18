using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BaseCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region 单例模式

            //{
            //    for (var i = 0; i < 5; i++)
            //    {
            //        Task.Run(() =>
            //        {
            //            SingletonPattern singleton = SingletonPattern.CreateInstance();
            //            Console.WriteLine("Singleton_Mode_Show");
            //        });
            //    }
            //}

            //{
            //    for (var i = 0; i < 5; i++)
            //    {
            //        Task.Run(() =>
            //        {
            //            SingletonPatternSecond singleton = SingletonPatternSecond.CreateInstance();
            //            Console.WriteLine("Singleton_Mode_Show");
            //        });
            //    }
            //}

            {
                List<Task> list = new List<Task>();
                Stopwatch st = new Stopwatch();
                st.Start();
                for (var i = 0; i < 5; i++)
                {
                    list.Add(Task.Run(() =>
                    {
                        SingletonPatternThird singleton = SingletonPatternThird.CreateInstance();
                        Console.WriteLine("Singleton_Mode_Show");
                    }));
                }
                st.Stop();
                Task.WaitAll(list.ToArray());
                Console.WriteLine("耗时：" + st.ElapsedMilliseconds);
            }

            #endregion

            Console.ReadLine();
        }
    }
}
