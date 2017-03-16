using Autofac;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CpoDesign.Example.Autofac
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = CreateAutofacContainer();

            var logger = container.Resolve<ILogger>();
            logger.LogSystemMessage("Application has started.. enter run, exit");

            var jobTime = Stopwatch.StartNew();

            do
            {
                ShortPause();

                Console.WriteLine();
                logger.LogSystemMessage("enter a command and hit enter");

                var command = Console.ReadLine().ToLowerInvariant().Trim();

                if (command.StartsWith("run"))
                {
                    logger.LogSystemMessage("run command executed");
                }

                if (command.StartsWith("exit"))
                {
                    jobTime.Stop();

                    Console.WriteLine("Job complete in {0}ms ", jobTime.ElapsedMilliseconds);
                    logger.LogSystemMessage("Actor system shutdown. Press any key to exit...");
                    Console.ReadKey();

                    Environment.Exit(1);
                }

            } while (true);
        }

        private static IContainer CreateAutofacContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleLogger>().As<ILogger>();


            var container = builder.Build();
            return container;
        }

        public static void ShortPause()
        {
            System.Threading.Thread.Sleep(1000);
        }
    }

    public class ConsoleLogger : ILogger
    {
        public void LogSystemMessage(string message)
        {
            Console.WriteLine(message);
        }
    }

    public interface ILogger
    {
        void LogSystemMessage(string message);
    }
}

