using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameTasksCS
{
    public class TasksVM : INotifyPropertyChanged
    {
        public string T1
        {
            get { return m_T1; }
            set { m_T1 = value; OnPropertyChanged("T1"); }  // the name is used in XAML
        }
        private string m_T1;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
