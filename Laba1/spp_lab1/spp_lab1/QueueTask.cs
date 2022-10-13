using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace spp_lab1
{
    delegate void MathOperation();
    public class QueueTask
    {
        Queue<Thread> ThreadList = new Queue<Thread>();
        Queue<MathOperation> DelList = new Queue<MathOperation>(9);
        object locker = new object();
   
        bool shouldEndThread = false;
        private Logger logger = LogManager.GetCurrentClassLogger();
        public int counter;

        public QueueTask(int ThreadNum)
        {
            DelList = new Queue<MathOperation>(9);

            for (int i = 1; i <= ThreadNum; i++)
            {
                Thread myThread = new Thread(DequeueList);
                myThread.Start();
                ThreadList.Enqueue(myThread);
            }

        }

        public void EndThread(int ThreadNum)
        {
            while (counter != 0) { }

            if (counter == 0)
            {
                shouldEndThread = true;
                for (int i = 0; i < ThreadNum; i++)
                {
                    ThreadList.Dequeue();
                }
                logger.Info("Потоки завершили работу");
            }

        }

        public void DequeueList()
        {
            MathOperation tmpTask;
            while (!shouldEndThread)
            {
                if (DelList.Count != 0)
                {
                    Console.WriteLine("\nВыполнение внутри потока из пула {0}", Environment.CurrentManagedThreadId);
                    logger.Info($"Id процесса: {Environment.CurrentManagedThreadId}");
                }

                lock (locker)
                {
                    if (DelList.Count != 0 && counter != 0)
                    {
                        tmpTask = DelList.Dequeue();
                        tmpTask.Invoke();
                        counter--;
                    }
                }
            }
        }

        public void Delegates()
        {
            MathOperation Task1 = MultFunc;
            MathOperation Task2 = LogFunc;
            MathOperation Task3 = PowFunc;
            MathOperation Task4 = CosFunc;
            MathOperation Task5 = RootFunc;
            MathOperation Task6 = MaxFunc;
            MathOperation Task7 = RoundFunc;
            MathOperation Task8 = ExpFunc;
            MathOperation Task9 = SubFunc;

            DelList.Enqueue(Task1);
            DelList.Enqueue(Task2);
            DelList.Enqueue(Task3);
            DelList.Enqueue(Task4);
            DelList.Enqueue(Task5);
            DelList.Enqueue(Task6);
            DelList.Enqueue(Task7);
            DelList.Enqueue(Task8);
            DelList.Enqueue(Task9);

            counter = 9;
        }

        public void MultFunc()
        {
            int a = 150;
            double b = 368;
            double res = a * b;
            Console.WriteLine(res);
        }

        public void LogFunc()
        {
            double a = 4;
            double b = 128;
            double res = Math.Log(a, b);
            Console.WriteLine("{0: 0.000}", res);
        }

        public void PowFunc()
        {
            Console.WriteLine(Math.Pow(10, 10));
        }

        public void CosFunc()
        {
            double degrees = 30.0;
            double res = Math.Cos(degrees * Math.PI / 180.0);
            Console.WriteLine("{0: 0.000}", res);
        }

        public void RootFunc()
        {
            Console.WriteLine("{0: 0.000}", Math.Sqrt(10789));
        }

        public void MaxFunc()
        {
            Console.WriteLine(Math.Max(564, 333));
        }

        public void RoundFunc()
        {
            Console.WriteLine(Math.Round(6.78));
        }

        public void ExpFunc()
        {
            Console.WriteLine("{0: 0.000}", Math.Exp(6));
        }

        public void SubFunc()
        {
            Console.WriteLine("{0: 0.000}", 239.70 - 1);
        }
    }
}
