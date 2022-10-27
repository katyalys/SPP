using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba3_2
{
    public class LogBuffer
    {
        private int bufCapacity;
        private string filePath;
        private ConcurrentQueue<string> buf;
        private int capacity;
        private StreamWriter _streamWriter;
        Mutex mutex;
        Mutex mutex1;
        public static Logger logger = LogManager.GetCurrentClassLogger();

        //private static Timer aTimer;
        public LogBuffer()
        {
            filePath = @"C:\Компьютер\Универ 3.0\СПП\Laba3\Laba3_1\Laba3_2\test.txt";
            if (!File.Exists(filePath))
            {
                logger.Error("File does not exist");
                throw new Exception("File does not exist");
            }
            capacity = 20;
            buf = new ConcurrentQueue<string>();
            _streamWriter = new StreamWriter(filePath, true);

            // Create a timer and set a two second interval.
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Interval = 100;
            aTimer.Elapsed += WriteToFileFromTimer;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            mutex = new Mutex();
        }

        public async void WriteToFileFromTimer(Object source, System.Timers.ElapsedEventArgs e)
        {
            await Task.Run(() => WriteToBuffer());
            logger.Info("Wrote to buffer using timer");
        }

        public async void WriteToFileFromCapacity()
        {
            if (capacity > buf.Count) { return; }
            await Task.Run(() => WriteToBuffer());
            logger.Info("Wrote tio buffer using capacity");
        }

        public async Task WriteToBuffer()
        {
            mutex.WaitOne();
            while (!buf.IsEmpty)
            {
                buf.TryDequeue(out string mes);
                if (mes != null)
                {
                    await _streamWriter.WriteLineAsync(mes);
                }
            }
            mutex.ReleaseMutex();
        }

        public void Add(string item)
        {
            buf.Enqueue(item);
            WriteToFileFromCapacity();
        }

        public void Close()
        {
            _streamWriter.Close();
        }
    }


}
