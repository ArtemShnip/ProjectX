using System;
using System.ComponentModel;
using System.Diagnostics;
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
        private BindingList<ProgramModel> _programModelsList;

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
            dgTodoList.ItemsSource = _programModelsList;
            ProgramWatcher _programWatcher = new ProgramWatcher();
            Thread thread = new Thread(_programWatcher.Wather);
            thread.Start();
            _programWatcher.NotifyStart += Save;
            _programWatcher.NotifyStop += AddInSave;
            //_programModelsList.ListChanged += _programModelsList_ListChanged;          
        }

        public void Save(string id)                                // не добавляет в Bindinglist, проблема в потоках или в Loaded="Window_Loaded"(MainWindow.xaml)? 
        {
            int e = 6;
            var proc = Process.GetProcessById(int.Parse(id));                            // получаю процесс по Id
            _programModelsList = new BindingList<ProgramModel>()                         // добавляю в _programModelsList
            {
                new ProgramModel()
                {
                    Id = id,                                                             // полученный Id
                    Name = proc.ProcessName,                                             // короткое имя
                    TimeStart = proc.StartTime                                           // время запуска процесса
                }
            };
            dgTodoList.ItemsSource = _programModelsList;
            _programModelsList.ListChanged += _programModelsList_ListChanged;
        }
        int index;
        public void AddInSave(string id)
        {
            DateTime time = DateTime.Now;
            
            //for (int i = 0; i < _programModelsList.Count; i++)
            //{
            //    string q =_programModelsList[i].Id;
            //    if (q.Equals(id))
            //    {
            //        index = int.Parse(q);
            //    }
            //}
            //int index = _programModelsList.IndexOf(id);                                // ищу индекс в _programModelsList где Id равен полученному Id завершенного процесса
            _programModelsList[index].TimeStop = time.ToLocalTime();                     // добавляю время завершения процесса
            _programModelsList[index].LongTime =
                time.ToLocalTime().Subtract(_programModelsList[index].
                TimeStart).ToString("h':'m':'s");
        }

        private void _programModelsList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType== ListChangedType.ItemAdded|| e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
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

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
