using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        private int m_Value;

        public TaskOnTimer(double _Millis, Action<string> _Setter)
        {
//            m_TargetStringVM = _TargetStringVM;

            ThreadPoolTimer PeriodicTimer = ThreadPoolTimer.CreatePeriodicTimer((ThreadPoolTimer timer) =>
            {
                m_Value += 1;
                Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                {
                    _Setter(m_Value.ToString());
                });
            }, TimeSpan.FromMilliseconds(_Millis));

        }

//        private ref string m_TargetStringVM;
    }



    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            m_TasksVM = new TasksVM();

            this.DataContext = m_TasksVM;


            // set initial data (through binding)
            m_Task1 = new TaskOnTimer(450, (s) => { m_TasksVM.T1 = s; });
            m_Task2 = new TaskOnTimer(190, (s) => { m_TasksVM.T2 = s; });

        }


        // threads
        TaskOnTimer m_Task1;
        TaskOnTimer m_Task2;

        // VM
        TasksVM m_TasksVM;
    }
}
