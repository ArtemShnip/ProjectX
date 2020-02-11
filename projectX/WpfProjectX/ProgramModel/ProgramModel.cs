using System;
using System.ComponentModel;

namespace WpfProjectX.ProgramModels
{
    class ProgramModel : INotifyPropertyChanged
    {
        private string _id;
        private string _name;
        private DateTime _date;
        private DateTime _timeStart;
        private DateTime _timeStop;
        private string _longTime;
        private string _client;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public DateTime TimeStart
        {
            get { return _timeStart; }
            set { _timeStart = value; }
        }


        public DateTime TimeStop
        {
            get { return _timeStop; }
            set { _timeStop = value; }
        }

        public string LongTime
        {
            get { return _longTime; }
            set { _longTime = value; }
        }

        public string Client
        {
            get { return _client; }
            set 
            {
                if (_client == value)
                    return;
                _client = value;
                OnPropertyChanged("Client");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
