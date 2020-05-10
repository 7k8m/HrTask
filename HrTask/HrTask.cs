using System;
using System.Threading.Tasks;

namespace HrTask
{
    public class HrTask<H,R> : Task<(H headResult,Task<R> remainTask)> 
    {
        public HrTask(Func<(H headResult, Task<R> remainTask)> func) : base(func) {}

        public new (H headResult,Task<R> remainTask) Result{
            get{
                var result = base.Result;
                result.remainTask.Start();
                return result;
            }
        }
    }
}
