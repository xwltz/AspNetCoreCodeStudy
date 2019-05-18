using Quartz;
using Quartz.Impl;

namespace WindowsServices
{
    public class MyJob
    {
        //调度器工厂
        public static ISchedulerFactory Factory;

        //调度器
        public static IScheduler Scheduler;

        /// <summary>
        ///     创建调度任务
        /// </summary>
        /// <returns></returns>
        public MyJob()
        {
            Factory = new StdSchedulerFactory();
            Scheduler = Factory.GetScheduler().GetAwaiter().GetResult();

            //创建一个任务
            var job = JobBuilder.Create<ExecuteJob>().WithIdentity("My_Job", "Group_One").Build();
            //创建一个触发器
            //var trigger = TriggerBuilder.Create().WithIdentity("My_Trigger", "Group_One").StartNow().WithSimpleSchedule(a => a.WithIntervalInHours(1).RepeatForever()).Build();

            var trigger = TriggerBuilder.Create().WithIdentity("My_Trigger", "Group_One").StartNow()
                .WithCronSchedule("0 0,28 8,10,12,14,16,17,18 * * ? *").Build();
            //将任务和触发器添加到调度器里
            Scheduler.ScheduleJob(job, trigger).GetAwaiter().GetResult();
        }

        /// <summary>
        ///     启动任务
        /// </summary>
        public static void Start()
        {
            Scheduler.Start();
        }

        /// <summary>
        ///     关闭任务
        /// </summary>
        public static void Stop()
        {
            Scheduler.Shutdown(true);
        }

        /// <summary>
        ///     重置任务
        /// </summary>
        public static void Continue()
        {
            Scheduler.ResumeAll();
        }

        /// <summary>
        ///     暂停任务
        /// </summary>
        public static void Pause()
        {
            Scheduler.PauseAll();
        }
    }
}