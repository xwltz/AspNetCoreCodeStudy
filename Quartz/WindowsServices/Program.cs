using Topshelf;

namespace WindowsServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<MyJob>(s =>
                {
                    s.ConstructUsing(name => new MyJob());
                    s.WhenStarted(tc => MyJob.Start());
                    s.WhenStopped(tc => MyJob.Stop());
                    s.WhenPaused(tc => MyJob.Pause());
                    s.WhenContinued(tc => MyJob.Continue());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Sample Topshelf Host");
                x.SetDisplayName("Job Services");
                x.SetServiceName("Xwltz Services");
            });
        }
    }
}
