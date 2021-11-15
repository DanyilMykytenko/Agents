using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MusicalAgent
{
    class Program
    {
        static void Main(string[] args)
        {
            string mutex_id = @"Global\MusicalAgent";
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
                while (true)
                {
                    SoundPlayer player = new SoundPlayer(@"joel.wav");
                    player.Play();
                    Thread.Sleep(321600);
                }
            }
        }
    }
}
