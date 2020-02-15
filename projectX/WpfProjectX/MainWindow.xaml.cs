using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using WpfProjectX.ProgramModels;
using WpfProjectX.Services;
using WpfProjectX.Watcher;

namespace WpfProjectX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _path = $"{Environment.CurrentDirectory}\\programDataList.json";
        private ObservableCollection<ProgramModel> _programModelsList;
        private FileIOService _fileIOservice;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOservice = new FileIOService(_path);
            try
            {
                _programModelsList = _fileIOservice.LoadDate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
            ProgramWatcher _programWatcher = new ProgramWatcher();
            Thread thread = new Thread(_programWatcher.Wather);
            thread.Start();
            dgTodoList.DataContext = _programModelsList;
            _programWatcher.NotifyStart += AddNew;
            _programWatcher.NotifyStop += AddInSave;
            _programModelsList.CollectionChanged += _programModelsList_CollectionChanged;
        }

        public void AddNew(string id)
        {
            var proc = Process.GetProcessById(int.Parse(id));
            _programModelsList.Add(
             new ProgramModel()
             {
                 Id = id,
                 Name = proc.ProcessName,
                 TimeStart = proc.StartTime
             });
        }

        public void AddInSave(string id)
        {
            var element = _programModelsList.First(f => f.Id == id);
            var index = _programModelsList.IndexOf(element);
            DateTime time = DateTime.Now;
            _programModelsList[index].TimeStop = time.ToLocalTime();
            _programModelsList[index].LongTime =
            time.ToLocalTime().Subtract(_programModelsList[index].
            TimeStart).ToString("h':'m':'s");
        }

        private void _programModelsList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Replace)
            {
                try
                {
                    _fileIOservice.SaveDate(sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }
    }
}
