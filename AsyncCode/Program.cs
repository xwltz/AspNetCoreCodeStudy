using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            {
                // .net1 最原始版本的多线程Thread
                // 功能强大，但不够稳定

                //ThreadStart threadStart = new ThreadStart(() =>
                //{
                //    Thread.Sleep(2000);
                //    const string msg = "This is Thread : ";
                //    Console.WriteLine(msg + Thread.CurrentThread.ManagedThreadId.ToString("00"));
                //});

                //Thread thread = new Thread(threadStart);
                //thread.Start();
            }

            {
                // .net2 线程池
                // 内置线程池，使用和释放都由线程池自行管理
                // 1线程重用，避免重复申请和销毁； 2线程限制数量
                // 使用方便，但不够灵活，缺少一些控制

                //var wait = new WaitCallback(o =>
                //{
                //    Thread.Sleep(2000);
                //    const string msg = "This is ThreadPool : ";
                //    Console.WriteLine(msg + Thread.CurrentThread.ManagedThreadId.ToString("00"));
                //});

                //ThreadPool.QueueUserWorkItem(wait);
                //ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxThreads);
                //ThreadPool.SetMinThreads(5, 5);
                //ThreadPool.SetMaxThreads(20, 20);
            }

            {
                // .net3 Task
                // 基于ThreadPool 丰富的API 应用多个场景
                // 最佳实践

                //Action action = new Action(() =>
                //{
                //    Thread.Sleep(2000);
                //    const string msg = "This is Action : ";
                //    Console.WriteLine(msg + Thread.CurrentThread.ManagedThreadId.ToString("00"));
                //});
                //Task.Run(action);
            }

            {
                Console.WriteLine(@"接项目！");
                Console.WriteLine(@"沟通需求，确认价格！");
                Console.WriteLine(@"签合同，收取50%的费用！");
                Console.WriteLine(@"组件团队！");
                Console.WriteLine(@"需求分析，系统设计！");
                Console.WriteLine(@"开发编码！");
                //以上为主线程

                //以下是多线程
                //多人共同开始编码
                var taskFactory = new TaskFactory();
                var taksList = new List<Task>();
                taksList.Add(taskFactory.StartNew(() => Coding("xwltz1", "WeiXinClient-Module-1")));
                taksList.Add(taskFactory.StartNew(() => Coding("xwltz2", "WeiXinClient-Module-2")));
                taksList.Add(taskFactory.StartNew(() => Coding("xwltz3", "WeiXinClient-Module-3")));
                taksList.Add(taskFactory.StartNew(() => Coding("xwltz4", "WeiXinClient-Module-4")));
                taksList.Add(taskFactory.StartNew(() => Coding("xwltz5", "WeiXinClient-Module-5")));
                taksList.Add(taskFactory.StartNew(() => Coding("xwltz6", "WeiXinClient-Web-1")));
                taksList.Add(taskFactory.StartNew(() => Coding("xwltz7", "WeiXinClient-Database-2")));
                taksList.Add(taskFactory.StartNew(() => Coding("xwltz8", "WeiXinClient-BackService-3")));

                //一定不会使用主线程，既可能是全新线程，也有可能是是刚刚用完的线程，无法预测

                //Task.WaitAny(taksList.ToArray());
                taskFactory.ContinueWhenAny(taksList.ToArray(), tlist => { Console.WriteLine(@"里程碑编码结束，收取10%的费用！"); });

                //Task.WaitAll(taksList.ToArray());
                taskFactory.ContinueWhenAll(taksList.ToArray(),
                    tlist => { Console.WriteLine(@"全部编码结束，收取20%的费用，费用收取后，进入项目测试联调阶段！"); });

                Console.WriteLine(@"项目验收，收取剩余费用！");
            }

            {
                // .net4 await async 
                // 基于Task
            }

            {
                // .net4.5 Parallel 并发运算
                // 基于Task
                Parallel.For(0, 5, i =>
                {
                    Thread.Sleep(2000);
                    const string msg = "This is Parallel : ";
                    Console.WriteLine(msg + i);
                });
            }
        }

        /// <summary>
        ///     编码过程
        /// </summary>
        /// <param name="name"></param>
        /// <param name="project"></param>
        private static void Coding(string name, string project)
        {
            Console.WriteLine(@"name:" + name + @"-project:" + project + @"-BeginTime:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:"));
            var number = 0;
            for (var i = 0; i < 1000000; i++) number += i;
            Console.WriteLine(@"name:" + name + @"-project:" + project + @"-Result:" + number + @"-EndTime:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:"));
        }

        private void button_Click(object sender, EventArgs e)
        {
            //const string preUrl = "http://www.";
            //var list = DapperHelper.GetList1();

            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //Parallel.ForEach(list, (item, index) =>
            //{
            //    var domain = string.Empty;
            //    if (item.SeoWebDomain.Contains(preUrl))
            //    {
            //        var s1 = item.SeoWebDomain.Replace("http://www.", "");
            //        var flag = s1.LastIndexOf("/", StringComparison.Ordinal) >= 0;
            //        if (flag)
            //        {
            //            domain = s1.Substring(0, s1.Length - 1);
            //        }

            //        DapperHelper.UpdateModel2(item.ID, domain);
            //    }
            //});
            //sw.Stop();

            //label1.Text = @"任务已完成，耗时：" + sw.ElapsedMilliseconds;
            //Console.ReadLine();
        }
    }
}