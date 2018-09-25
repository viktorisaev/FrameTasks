using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FrameTasksCS
{


    internal class TaskOnTimer
    {
        private double TASK_DELAY = 180;


        public Task DoTask(int _Counter, Action<string> _Setter)
        {
            IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync( (workItem) =>
            {
                int value = _Counter;
                while (value >= 0)
                {
                    Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                    {
                        _Setter(value.ToString());
                    });

                    Task.Delay(TimeSpan.FromMilliseconds(TASK_DELAY)).Wait();

                    value -= 1;
                }
            });

            return asyncAction.AsTask();
        }
    }






    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            m_TasksVM = new TasksVM();

            this.DataContext = m_TasksVM;


            // set initial data (through binding)
            m_Task1 = new TaskOnTimer();
            m_Task2 = new TaskOnTimer();
            m_Task3 = new TaskOnTimer();
            m_Task4 = new TaskOnTimer();
            m_Task5 = new TaskOnTimer();
            m_Task6 = new TaskOnTimer();


            IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync(async (workItem) =>
            {
                Task[] s2 = new Task[4];
                Task[] s3 = new Task[2];

                while (workItem.Status != AsyncStatus.Canceled)
                {
                    // step 1
                    var t1 = m_Task1.DoTask(13, (s) => { m_TasksVM.T1 = s; });
                    await t1;

                    // step 2 (2 parallel tasks)
                    var t2 = m_Task2.DoTask(4, (s) => { m_TasksVM.T2 = s; });
                    var t3 = m_Task3.DoTask(13, (s) => { m_TasksVM.T3 = s; });
                    var t4 = m_Task4.DoTask(2, (s) => { m_TasksVM.T4 = s; });
                    var t5 = m_Task5.DoTask(10, (s) => { m_TasksVM.T5 = s; });
                    var t6 = m_Task6.DoTask(8, (s) => { m_TasksVM.T6 = s; });
                    s2[0] = t2;
                    s2[1] = t3;
                    s2[2] = t4;
                    s2[3] = t5;
                    var tt2 = Task.WhenAll(s2);
                    await tt2;

                    s3[0] = tt2;
                    s3[1] = t6;
                    await Task.WhenAll(s3);
                }
            });

        }




        // threads
        TaskOnTimer m_Task1;
        TaskOnTimer m_Task2;
        TaskOnTimer m_Task3;
        TaskOnTimer m_Task4;
        TaskOnTimer m_Task5;
        TaskOnTimer m_Task6;

        // VM
        TasksVM m_TasksVM;
    }
}
