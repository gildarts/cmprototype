using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Threading.Tasks;
using System.Threading;

namespace Manager
{
    public partial class MultiTaskingRunner : Office2007Form
    {
        private Dictionary<string, Task> Tasks = new Dictionary<string, Task>();
        private Dictionary<Task, DataGridViewRow> TaskRows = new Dictionary<Task, DataGridViewRow>();
        private Dictionary<Task, CancellationTokenSource> TaskCancellations = new Dictionary<Task, CancellationTokenSource>();
        private int CompleteCount { get; set; }

        public MultiTaskingRunner()
        {
            InitializeComponent();
        }

        public void AddTask(string name, Action<object> action, object aState, CancellationTokenSource source)
        {
            CancellationToken token = source.Token;
            TaskState state = new TaskState(aState, token);

            Task task = new Task(x =>
            {
                TaskState t = (TaskState)x;
                action(t.State);
                if (t.Token.IsCancellationRequested)
                    t.Token.ThrowIfCancellationRequested();
            }, state, token);

            task.ContinueWith(x => CompleteTask(x));

            Tasks.Add(name, task);
            TaskCancellations.Add(task, source);
        }

        public void CompleteTask(Task task)
        {
            if (TaskRows.ContainsKey(task))
            {
                DataGridViewRow row = TaskRows[task];

                if (InvokeRequired)
                    Invoke(new Action(() => FinalMessage(task, row)));
                else
                    FinalMessage(task, row);
            }
            else
                throw new Exception("爆了!");
        }

        private void FinalMessage(Task task, DataGridViewRow row)
        {
            if (task.IsCanceled)
                row.Cells[0].Value = "Cancelled";
            else
            {
                if (task.IsFaulted)
                {
                    row.Cells[0].Value = "Error";
                    row.Cells[2].Value = task.Exception.InnerException.Message;
                }
                else
                    row.Cells[0].Value = "Complete";
            }

            DataGridViewButtonCell cell = row.Cells[3] as DataGridViewButtonCell;
            cell.Value = "完成";

            CompleteCount++;

            if (CompleteCount >= Tasks.Count)
            {
                MessageBox.Show("所有工作已完成。");
                foreach (Task each in Tasks.Values)
                {
                    if (each.Exception != null)
                        return;
                }

                Close();
            }
        }

        public void ExecuteTasks()
        {
            CompleteCount = 0;
            ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MultiTaskingRunner_Load(object sender, EventArgs e)
        {
            PrepareTasks();
        }

        private void PrepareTasks()
        {
            dgvTasks.Rows.Clear();

            foreach (KeyValuePair<string, Task> each in Tasks)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgvTasks, "Preapre", each.Key, "", "取消");
                row.Tag = each.Value;
                dgvTasks.Rows.Add(row);
                TaskRows.Add(each.Value, row);

                if (TaskCancellations.ContainsKey(each.Value))
                {
                    TaskCancellations[each.Value].Token.Register(state =>
                    {
                        DataGridViewRow r = state as DataGridViewRow;
                        if (r.Cells[0].Value.ToString() != "Running") return;

                        if (InvokeRequired)
                            Invoke(new Action(() => r.Cells[0].Value = "Cancelling"));
                        else
                            r.Cells[0].Value = "Cancelling";
                    }, row);
                }
            }

            foreach (DataGridViewRow row in dgvTasks.Rows)
            {
                Task t = row.Tag as Task;
                row.Cells[0].Value = "Running";
                t.Start();
            }
        }

        private void dgvTasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 3) return;

            DataGridViewRow row = dgvTasks.Rows[e.RowIndex];
            Task task = row.Tag as Task;

            if (TaskCancellations.ContainsKey(task))
                TaskCancellations[task].Cancel();
            else
                throw new Exception("爆了!");
        }

        private class TaskState
        {
            public TaskState(object aState, CancellationToken token)
            {
                State = aState;
                Token = token;
            }

            public object State { get; set; }

            public CancellationToken Token { get; set; }
        }

        private void dgvTasks_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 | e.ColumnIndex != 2) return;

            DataGridViewRow row = dgvTasks.Rows[e.RowIndex];

            if (row.Cells[0].Value.ToString() == "Error")
            {
                Task t = row.Tag as Task;
                Exception ex = t.Exception.InnerException;

                ErrorForm.Show(ex.Message, ex);
            }
        }
    }
}
