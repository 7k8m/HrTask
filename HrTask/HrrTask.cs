using System;
using System.Threading.Tasks;

namespace HrTask
{
    ///<summary>
    /// Task results to a heading one (H) and 
    /// remained task which result to heading one (R1) and 
    /// one more remained task (R2). 
    ///</summary>
    /// <typeparam name="H">Type of heading result</typeparam>
    /// <typeparam name="R1">Type of remaining result 1</typeparam>
    /// <typeparam name="R2">Type of remaining result 2</typeparam>
    public class HrrTask<H,R1,R2> : Task<(H headResult, HrTask<R1,R2> remainTask)>
    {

        public delegate (R1 resultHead, HrTask<R1,R2>.RemainFunc resultRemainTaskFunc) RemainTaskFunc();
        public delegate (H headResult, HrTask<R1,R2> remainTask) HRRTask();
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
                                    () => resultRemainFuncResult.resultRemainTaskFunc()
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