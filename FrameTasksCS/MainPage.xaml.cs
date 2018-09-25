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





    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            m_TasksVM = new TasksVM();

            this.DataContext = m_TasksVM;

            // set initial data (through binding)
            m_T1Value = 0;
            m_TasksVM.T1 = "my task!!";

            ThreadPoolTimer PeriodicTimer = ThreadPoolTimer.CreatePeriodicTimer((ThreadPoolTimer timer) => 
            {
                m_T1Value += 1;
                Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                {
                    m_TasksVM.T1 = m_T1Value.ToString();
                });
            }, TimeSpan.FromMilliseconds(450));
        }

        
        // threads

        // values
        int m_T1Value;

        // VM
        TasksVM m_TasksVM;
    }
}
