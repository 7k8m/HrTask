using Microsoft.VisualStudio.TestTools.UnitTesting;
using HrTask;
using System.Threading.Tasks;

namespace TestHrTask
{
    [TestClass]
    public class TestMultipleRunTask
    {

        [TestMethod]
        public void TestRun()
        {
            var multiRunTask = new HrTask<int, HrTask<int,int>>(() =>
            {
                return (
                    1,
                    () => new HrTask<int,int>(
                            () => (2, 
                                    () => 3)));
            });

            var result = multiRunTask.Result;
            Assert.AreEqual(result.headResult, 1);
            var remainResultTask = result.remainTask.Result;

            var remainResult = remainResultTask.Result;
            Assert.AreEqual(remainResult.headResult, 2);

            var remain2Result = remainResult.remainTask.Result;
            Assert.AreEqual(remain2Result, 3);
        }
    }
}
