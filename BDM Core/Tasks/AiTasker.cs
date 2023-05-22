using System;
using System.Threading;
using System.Threading.Tasks;


namespace servr.ix.ui.Tasks
{
    class AiIntegrationTasker
    {
        readonly CancellationTokenSource cts = new CancellationTokenSource();
        readonly AutoResetEvent are = new AutoResetEvent(false);

        public async Task StartTask(int value, Func<int, Task> act)
        {
            while (true)
            {
                if (cts.IsCancellationRequested) throw new OperationCanceledException();
                await act(value);
                are.WaitOne();
                value++;
            }
            }
        public void NextStep()
        {
            are.Set();
        }

        public void CancelTask()
        {
            cts.Cancel();
        }      
       
    }
}
