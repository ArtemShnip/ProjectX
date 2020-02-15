using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WpfProjectX.ProgramModels
{
    class ProgramModel : INotifyPropertyChanged
    {
        private ObservableCollection<ProgramModel> _programModelsList;
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
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged("Client");
            }
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
        public ObservableCollection<ProgramModel> ProgramModels
        {
            get
            {
                return _programModelsList;
            }
            set
            {
                _programModelsList = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
