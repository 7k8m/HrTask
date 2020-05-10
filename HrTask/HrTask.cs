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
        public HrTask(Func<(H headResult, Task<R> remainTask)> func) 
            : base(() => {
                    var result = func();
                    result.remainTask.Start();
                    return result;
                }
            ) 
        {             
        }
    }
}
