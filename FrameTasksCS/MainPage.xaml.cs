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
        private double TASK_DELAY = 120;


        public Task DoTask(int _Counter, Action<string> _Setter)
        {
            IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync( (workItem) =>
            {
                int value = _Counter;
                while (value > 0)
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

        public TaskOnTimer()
        {
            //ThreadPoolTimer PeriodicTimer = ThreadPoolTimer.CreatePeriodicTimer((ThreadPoolTimer timer) =>
            //{
            //    m_Value += 1;
            //    Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            //    {
            //        _Setter(m_Value.ToString());
            //    });
            //}, TimeSpan.FromMilliseconds(_Millis));

        }

//        private int m_Value;
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


            IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync(async (workItem) =>
            {
                Task[] s2 = new Task[2];

                while(workItem.Status != AsyncStatus.Canceled)
                {
                    // step 1
                    var t1 = m_Task1.DoTask(8, (s) => { m_TasksVM.T1 = s; });
                    await t1;

                    // step 2 (2 parallel tasks)
                    var t2 = m_Task2.DoTask(3, (s) => { m_TasksVM.T2 = s; });
                    var t3 = m_Task3.DoTask(6, (s) => { m_TasksVM.T3 = s; });
                    s2[0] = t2;
                    s2[1] = t3;
                    await Task.WhenAll(s2);
                }
            });

        }




        // threads
        TaskOnTimer m_Task1;
        TaskOnTimer m_Task2;
        TaskOnTimer m_Task3;

        // VM
        TasksVM m_TasksVM;
    }
}
