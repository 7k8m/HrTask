using System;
using System.Threading.Tasks;

namespace HrTask
{
    public class HrrTask<H,R1,R2> : Task<(H headResult,HrTask<R1,R2> remainTask)>
    {
        public delegate (R1 headResult,Task<R2> remainTask) RemainFunc();
        public delegate (H headResult,HrTask<R1,R2> remainTask) HRRTask();

        public delegate (H headResult, RemainFunc remainFunc) HRRFunc();

        ///<summary>
        /// Construct a HrrTask 
        ///</summary>
        ///<param name="func">Function to be processed in HrTask</param>
        public HrrTask(HRRTask func) 
            : base(() => {
                    var result = func();
                    return result;
                }
            ) 
        {
            Start();
        }

        ///<summary>
        /// Construct a HrrTask 
        ///</summary>
        ///<param name="func">Function to be processed in HrTask</param>
        public HrrTask(HRRFunc func) 
            : this(() => {
                    var r = func();

                    var head = r.headResult;
                    var remainTaskResult = r.remainFunc();

                    var remainTask = 
                    new HrTask<R1, R2>( 
                        () => (
                            remainTaskResult.headResult, 
                            remainTaskResult.remainTask));

                    return (head,remainTask);
                }
            ) 
        {
        }
    }
}