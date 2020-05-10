using System;
using System.Threading.Tasks;

namespace HrTask
{
    ///<summary>
    /// Task results to a heading one and remained tasks. 
    ///</summary>
    /// <typeparam name="H">Type of heading result</typeparam>
    /// <typeparam name="R">Type of remaining result</typeparam>
    public class HrTask<H,R> : Task<(H headResult,Task<R> remainTask)> 
    {
        ///<summary>
        /// Construct a HrTask 
        ///</summary>
        ///<param name="func">Function to be processed in HrTask</param>
        public HrTask(Func<(H headResult, Task<R> remainTask)> func) : base(func) {}

        ///<summary>
        ///Result of HrTask.
        ///Result.headResult is heading one and 
        /// result of Result.remainTask is remained one.
        ///</summary>
        public new (H headResult,Task<R> remainTask) Result{
            get{
                var result = base.Result;
                result.remainTask.Start();
                return result;
            }
        }
    }
}
