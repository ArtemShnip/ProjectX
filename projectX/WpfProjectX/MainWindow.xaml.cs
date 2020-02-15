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

            _programModelsList.Add(
             new ProgramModel()
             {
                 Id = "jjjj",
                 Name = "ferfr"
             });

            ProgramWatcher _programWatcher = new ProgramWatcher();
            Thread thread = new Thread(_programWatcher.Wather);
            thread.Start();
            dgTodoList.DataContext = _programModelsList; // если без этого то сохраняет в json и можно сортировать,но не отображает в таблице
            _programWatcher.NotifyStart += AddNew;
            _programWatcher.NotifyStop += AddInSave;
            _programModelsList.ListChanged += _programModelsList_ListChanged;
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
            _programModelsList.ListChanged += _programModelsList_ListChanged;
        }
        public void AddInSave(string id)
        {
            //DateTime time = DateTime.Now;
            //int index = 222; //_programModelsList.IndexOf(id);
            //_programModelsList[index].TimeStop = time.ToLocalTime();
            //_programModelsList[index].LongTime =
            //    time.ToLocalTime().Subtract(_programModelsList[index].
            //    TimeStart).ToString("h':'m':'s");
        }

        public void _programModelsList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
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
