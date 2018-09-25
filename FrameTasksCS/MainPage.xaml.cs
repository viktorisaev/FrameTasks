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
        public Task DoTask(double _Millis, Action<string> _Setter)
        {
            IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync( (workItem) =>
            {
                m_Value += 1;
                Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                {
                    _Setter(m_Value.ToString());
                });

                Task.Delay(TimeSpan.FromMilliseconds(_Millis)).Wait();
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

        private int m_Value;
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
                    var t1 = m_Task1.DoTask(450, (s) => { m_TasksVM.T1 = s; });
                    await t1;

                    // step 2 (2 parallel tasks)
                    var t2 = m_Task2.DoTask(190, (s) => { m_TasksVM.T2 = s; });
                    var t3 = m_Task3.DoTask(490, (s) => { m_TasksVM.T3 = s; });
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
