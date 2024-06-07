using System;
using System.Threading;
using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            
            int start = int.Parse(StartTextBox.Text);
            int end = int.Parse(EndTextBox.Text);
            int threadCount = int.Parse(ThreadCountTextBox.Text);

            int range = (end - start + 1) / threadCount;
            int remainder = (end - start + 1) % threadCount;

            Thread[] threads = new Thread[threadCount];

            OutputTextBox.Clear();

            for (int i = 0; i < threadCount; i++)
            {
                int threadStart = start + i * range;
                int threadEnd = (i == threadCount - 1) ? end : (threadStart + range - 1);

                if (i == threadCount - 1 && remainder > 0)
                {
                    threadEnd += remainder;
                }

                threads[i] = new Thread(() => PrintNumbers(threadStart, threadEnd));
                threads[i].Start();
            }

            foreach (Thread t in threads)
            {
                t.Join();
            }

            OutputTextBox.AppendText("Всі числа виведено.\n");
        }

        private void PrintNumbers(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                Dispatcher.Invoke(() =>
                {
                    OutputTextBox.AppendText(i + "\n");
                });
                Thread.Sleep(100); 
            }
        }
    }
}
