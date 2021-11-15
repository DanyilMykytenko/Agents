using System;
using System.Diagnostics;
using System.Threading;

namespace Agent
{
    class Program
    {
        static void Main(string[] args)
        {
            string mutex_id = @"Global\" + args[0];
            using (Mutex mutex = new Mutex(false, mutex_id))
            {
                try
                {
                    if (!mutex.WaitOne(0, false))
                        return; // i'm a duplicate process
                }
                catch (AbandonedMutexException)
                {
                }
                while(true)
                {
                    for(int i = 1; i <= 10; i++)
                    {
                        string agentId = "GrinchAgent" + i.ToString();
                        if (agentId == args[0])
                            continue;
                        using (Process myProcess = new Process())
                        {
                            myProcess.StartInfo.UseShellExecute = false;
                            myProcess.StartInfo.FileName = @"Agent.exe";
                            myProcess.StartInfo.CreateNoWindow = true;
                            myProcess.StartInfo.Arguments = agentId;
                            myProcess.Start();
                        }
                    }

                    using(Process myProcess = new Process())
                    {
                        myProcess.StartInfo.UseShellExecute = false;
                        myProcess.StartInfo.FileName = @"MusicalAgent.exe";
                        myProcess.StartInfo.CreateNoWindow = true;
                        myProcess.Start();
                    }

                    Thread.Sleep(5000);
                }
            }
        }
    }
}
