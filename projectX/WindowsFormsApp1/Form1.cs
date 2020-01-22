using System;

using System.Collections.Generic;

using System.ComponentModel;

using System.Data;

using System.Diagnostics;

using System.Drawing;

using System.IO;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public List<Processes> procList = new List<Processes>();

        public int count = 0;
        public Form1()
        {
            InitializeComponent();
            List<Processes> procList = new List<Processes>();

            dataGridView1.AutoGenerateColumns = true;

            bindingSource1.DataSource = this.procList;

            Timer tmr = new Timer();

            tmr.Interval = 500;

            tmr.Tick += new EventHandler(tmr_Tick);

            tmr.Start();
        }
        private void tmr_Tick(object sender, EventArgs e)
        {

            try
            {

                this.procList.Clear();

                foreach (var winProc in Process.GetProcesses())
                {

                    string min_threads = "";

                    string priority = "";

                    try
                    {

                        min_threads = winProc.Threads.OfType<ProcessThread>().Where(t => t.ThreadState == ThreadState.Running).Count().ToString();

                        priority = winProc.BasePriority.ToString();

                    }
                    catch (Exception er)
                    {

                    }

                    this.procList.Add(new Processes(winProc.Id.ToString(), winProc.ProcessName, winProc.Threads.Count.ToString(), min_threads, priority));

                }

                dataGridView1.DataSource = this.procList;

                dataGridView1.Refresh();

            }
            catch (DataException er)
            {

                MessageBox.Show(er.Message, "Ошибка обновления данных.");

            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string data = "";

            int row = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex);

            int pid = Convert.ToInt32(dataGridView1[0, row].Value);

            Process process = Process.GetProcessById(pid);

            string process_name = process.ProcessName;

            foreach (ProcessThread thread in process.Threads)
            {

                data += "ID: " + thread.Id + ", Состояние: " + thread.ThreadState + "\n";

            }

            MessageBox.Show(data, "Информация о процессе " + process_name);
        }
        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }



        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;
                StreamWriter sw = new StreamWriter(filename);
                string start_time = "Нет доступа к информации о процессе";
                foreach (var winProc in Process.GetProcesses())
                {

                    try
                    {

                        start_time = winProc.StartTime.ToString();

                    }
                    catch (Exception er)
                    {

                    }

                    sw.WriteLine("Имя: " + winProc.ProcessName + ", Время старта: " + start_time);

                }

                sw.Close();

            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

    }
}
[TSQL] [/TSQL] 