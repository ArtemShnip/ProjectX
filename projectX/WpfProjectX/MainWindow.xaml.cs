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
        private readonly string _path = $"{Environment.CurrentDirectory}\\SaveData\\programDataList.json";
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
                Loger.WriteLog(ex);
                MessageBox.Show("Поврежден файл \"programDataList.json\".");
                _programModelsList = new ObservableCollection<ProgramModel>();
            }
            ProgramWatcher _programWatcher = new ProgramWatcher();
            Thread thread = new Thread(_programWatcher.Wather);
            thread.Start();
            dgTodoList.DataContext = _programModelsList;
            _programWatcher.NotifyStart += AddNew;
            _programWatcher.NotifyStop += AddInSave;
            try
            {
                _programModelsList.CollectionChanged += _programModelsList_CollectionChanged;
            }
            catch (Exception ex)
            {
                Loger.WriteLog(ex);
                MessageBox.Show("Поврежден файл \"programDataList.json\".");
            }
        }

        public void AddNew(string id)
        {
            var proc = Process.GetProcessById(int.Parse(id));
            DateTime date = DateTime.Now;
            var procStart = proc.StartTime;
            _programModelsList.Add(
             new ProgramModel()
             {

                 Id = id,
                 Date = date.ToShortDateString(),
                 Name = proc.ProcessName,
                 TimeStart = procStart,
                 ShortTimeStart = procStart.ToShortTimeString()
             });
        }

        public void AddInSave(string id)
        {
            var element = _programModelsList.First(f => f.Id == id);
            var index = _programModelsList.IndexOf(element);
            DateTime time = DateTime.Now;
            _programModelsList[index].TimeStop = time.ToShortTimeString();
            _programModelsList[index].LongTime =
            time.ToLocalTime().Subtract(_programModelsList[index].
            TimeStart).ToString("h':'m':'s");
        }

        private void _programModelsList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Reset)
            {
                try
                {
                    _fileIOservice.SaveDate(sender);
                }
                catch (Exception ex)
                {
                    Loger.WriteLog(ex);
                    MessageBox.Show("Поврежден файл \"programDataList.json\".");
                    Close();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _fileIOservice.SaveDate(_programModelsList);
                MessageBox.Show("Изменения сохранены");
            }
            catch (Exception ex)
            {
                Loger.WriteLog(ex);
                MessageBox.Show(ex.Message);
                Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddProgram addProgram = new AddProgram();

            if (addProgram.ShowDialog() == true)
            {
                if (addProgram.NameProgram != "")
                {
                    string name = addProgram.NameProgram;
                    _fileIOservice.SaveArrayProgram(name);
                }else
                    MessageBox.Show("you don't enter name");
                
            }
            
        }
    }
}
