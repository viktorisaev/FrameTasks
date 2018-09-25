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
            set { m_T1 = value; OnPropertyChanged("T1"); }
        }
        private string m_T1;

        public string T2
        {
            get { return m_T2; }
            set { m_T2 = value; OnPropertyChanged("T2"); }
        }
        private string m_T2;

        public string T3
        {
            get { return m_T3; }
            set { m_T3 = value; OnPropertyChanged("T3"); }
        }
        private string m_T3;

        public string T4
        {
            get { return m_T4; }
            set { m_T4 = value; OnPropertyChanged("T4"); }
        }
        private string m_T4;

        public string T5
        {
            get { return m_T5; }
            set { m_T5 = value; OnPropertyChanged("T5"); }
        }
        private string m_T5;

        public string T6
        {
            get { return m_T6; }
            set { m_T6 = value; OnPropertyChanged("T6"); }
        }
        private string m_T6;



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
