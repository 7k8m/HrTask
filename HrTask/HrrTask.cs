using System;
using System.Threading.Tasks;

namespace HrTask
{
    public class HrrTask<H,R1,R2> : Task<(H headResult,HrTask<R1,R2> remainTask)>
    {

        public delegate (R1 resultHead, HrTask<R1,R2>.RemainFunc resultRemaintaskFunc) RemainTaskFunc();
        public delegate (H headResult,HrTask<R1,R2> remainTask) HRRTask();
        public delegate (H headResult, RemainTaskFunc remainFunc) HRRFunc();

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
                    var result = func();
                    return (
                        result.headResult, 
                        new HrTask<R1,R2>( 
                            ()=> { 
                                var resultRemainFuncResult = result.remainFunc();
                                return (
                                    resultRemainFuncResult.resultHead, 
                                    () => resultRemainFuncResult.resultRemaintaskFunc()
                                );
                            }
                        )
                    );
                }
            ) 
        {
        }
    }
}