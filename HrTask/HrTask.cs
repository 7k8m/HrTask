using System;
using System.Threading.Tasks;

namespace HrTask
{
    public class HrTask<H,R> : Task<(H,Task<R>)> 
    {
        public HrTask(Func<(H, Task<R>)> func) : base(func) {}
    }
}
