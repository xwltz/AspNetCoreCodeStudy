using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Quartz.Impl;
using Quartz.Web.Class;

namespace Quartz.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected async void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            await MyJob();
        }

        private static async Task MyJob()
        {
            //创建调度器工厂
            ISchedulerFactory factory = new StdSchedulerFactory();
            //创建调度器
            IScheduler scheduler = await factory.GetScheduler();
            //创建一个任务
            IJobDetail job = JobBuilder.Create<MyTimer>().WithIdentity("My_Job", "Group_One").Build();
            //创建一个触发器
            ITrigger trigger = TriggerBuilder.Create().WithIdentity("My_Trigger", "Group_One").StartNow().WithSimpleSchedule(a => a.WithIntervalInSeconds(10).RepeatForever()).Build();
            //将任务和触发器添加到调度器里
            await scheduler.ScheduleJob(job, trigger);
            //开始执行
            await scheduler.Start();
        }
    }
}
