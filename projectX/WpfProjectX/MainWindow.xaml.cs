using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfProjectX.ProgramModels;
using WpfProjectX.Services;
using WpfProjectX.Watcher;
using ProjectX_V._2;

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
            //Thread thread = new Thread(_programWatcher.Wather);
            //thread.Start();
            Parallel.Invoke(_programWatcher.Wather);
            _programModelsList.ListChanged += _programModelsList_ListChanged;
        }

        public void Save(string id)
        {
            var proc = Process.GetProcessById(int.Parse(id));
            _programModelsList.Add(new ProgramModel()
            {
                Id = id,
                Name = proc.ProcessName,
                TimeStart = proc.StartTime
            });
        }

        public void AddInSave(string time1, string id)
        {
            DateTime time = DateTime.Now;
            //int index = _programModelsList.IndexOf(x => x.Id == id);
            //_programModelsList[index].TimeStop = time.ToLocalTime();
            //_programModelsList[index].LongTime = time.ToLocalTime().Subtract(_programModelsList[index].TimeStart).ToString("h':'m':'s");
            //Console.WriteLine(_programModelsList[index].ToString());
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
    }
}
