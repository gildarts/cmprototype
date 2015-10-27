using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Manager
{
    //comment.
    class XmlResultAsyncRunner : AsyncRunner<object, XmlElement>
    {
        public XmlResultAsyncRunner()
            : base()
        {
        }
    }

    /// <summary>
    /// 代表非同步執行某項工作。
    /// </summary>
    class AsyncRunner : AsyncRunner<object, object>
    {
        public AsyncRunner()
            : base()
        {
        }
    }

    /// <summary>
    /// 代表非同步執行某項工作。
    /// </summary>
    class AsyncRunner<TTaskArgs, TResult>
    {
        private Task Job { get; set; }

        private AsyncRunningForm RunningForm { get; set; }

        private int RunningTime = 0;

        /// <summary>
        /// 取得或設定傳遞進來的參數。
        /// </summary>
        public TTaskArgs Arguments { get; set; }

        /// <summary>
        /// 取得執行結果。
        /// </summary>
        public TResult Result { get; set; }

        /// <summary>
        /// 要對使用者顯示的訊息。
        /// </summary>
        public string Message
        {
            get
            {
                if (RunningForm == null)
                    return string.Empty;
                else
                    return RunningForm.Message;
            }
            set
            {
                if (RunningForm == null)
                    RunningForm = new AsyncRunningForm();

                RunningForm.Message = value;
            }
        }

        /// <summary>
        /// 取得或設定訊息的擁有視窗，訊息會以表單的方式顯示於該視窗中央。
        /// </summary>
        public Control MessageOwner { get; set; }

        /// <summary>
        /// 要執行工作。
        /// </summary>
        public Action<AsyncRunner<TTaskArgs, TResult>> Task { get; set; }

        /// <summary>
        /// 執行完成時的工作。
        /// </summary>
        public Action<AsyncRunner<TTaskArgs, TResult>> Complete { get; set; }

        /// <summary>
        /// 取得是否執行 Task 有發生錯誤。
        /// </summary>
        public bool IsTaskError { get; private set; }

        /// <summary>
        /// 取得執行 Task 發生的錯誤資訊。
        /// </summary>
        public Exception TaskError { get; private set; }

        /// <summary>
        /// 取得此非同步工作是否還在執行中。
        /// </summary>
        public bool IsRunning { get; private set; }

        public AsyncRunner()
        {
        }

        /// <summary>
        /// 等待工作執行完成(只等背景執行緒，不會等待 RunComplete，這意謂您的程式碼可能與  RunComplete 同時執行)。
        /// 強烈建議在背景執行緒才呼叫此方法。
        /// </summary>
        public void Wait()
        {
            Job.Wait();
        }

        /// <summary>
        /// 非同步執行工作。
        /// </summary>
        public void Run()
        {
            Action<AsyncRunner<TTaskArgs, TResult>> running = null;
            Action<AsyncRunner<TTaskArgs, TResult>> complete = x => { };

            if (Task != null)
                running = Task;
            if (Complete != null)
                complete = Complete;

            Run(running, complete);
        }

        /// <summary>
        /// 非同步執行工作。
        /// </summary>
        /// <param name="running"></param>
        public void Run(Action<AsyncRunner<TTaskArgs, TResult>> running)
        {
            Run(running, x => { });
        }

        /// <summary>
        /// 非同步執行工作。
        /// </summary>
        /// <param name="running">耗時的工作。</param>
        /// <param name="runComplete">工作完成時要執行的工作(會在呼叫端執行緒執行)。</param>
        public void Run(Action<AsyncRunner<TTaskArgs, TResult>> running,
            Action<AsyncRunner<TTaskArgs, TResult>> runComplete)
        {
            if (running == null)
                throw new ArgumentException("running 參數不可為 null。");

            Task = running;
            Complete = runComplete;

            //建立處於呼叫端執行緒的控制項。
            Control control = new Control();
            IntPtr point = control.Handle;

            Job = new Task(() =>
            {
                IsRunning = true;
                IsTaskError = false;
                TaskError = null;
                RunningTime = Environment.TickCount;

                try
                {
                    running(this);
                }
                catch (Exception ex)
                {
                    IsTaskError = true;
                    TaskError = ex;
                }
            });

            Job.ContinueWith(x =>
            {
                IsRunning = false;
                //Program.SetBarMessage("花費毫秒數：" + (Environment.TickCount - RunningTime).ToString());
                if (runComplete != null)
                {
                    try
                    {
                        if (control.InvokeRequired)
                            control.Invoke(new Action<AsyncRunner<TTaskArgs, TResult>>(arg => runComplete(arg)), this);
                        else
                            runComplete(this);
                    }
                    catch { }

                    if (control.InvokeRequired)
                        control.Invoke(new Action<AsyncRunner<TTaskArgs, TResult>>(arg => CloseRunningForm()), this);
                    else
                        CloseRunningForm();
                }
            });


            if (RunningForm == null)
                Job.Start();
            else
            {
                RunningForm.OnloadAction = () => Job.Start();

                if (MessageOwner != null)
                {
                    RunningForm.StartPosition = FormStartPosition.CenterParent;
                    RunningForm.ShowDialog(MessageOwner);
                }
                else
                {
                    RunningForm.StartPosition = FormStartPosition.CenterScreen;
                    RunningForm.ShowDialog();
                }
            }
        }

        private void CloseRunningForm()
        {
            if (RunningForm != null && RunningForm.Visible)
            {
                System.Threading.Thread.Sleep(350);
                RunningForm.Close();
            }
        }
    }
}
