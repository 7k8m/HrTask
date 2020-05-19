using Microsoft.VisualStudio.TestTools.UnitTesting;
using HrTask;
using System.Threading.Tasks;

namespace TestHrTask
{
    [TestClass]
    public class TestMultipleRunTask
    {
        [TestMethod]
        public void TestRunTask()
        {
            var multiRunTask = new HrrTask<int,long,double>(() =>
            {
                return (
                    1,
                    new HrTask<long,double>(
                            () => (2, 
                                    () => 3)));
            });

            multiRunTask.Start();

            var result = multiRunTask.Result;
            Assert.AreEqual(result.headResult, 1);
            
            var remainResult = result.remainTask.Result;
            Assert.AreEqual(remainResult.headResult, 2);

            var remain2Result = remainResult.remainTask.Result;
            Assert.AreEqual(remain2Result, 3);
        }

        [TestMethod]
        public void TestRunFunc()
        {
            var multiRunTask = new HrrTask<int,long,double>(() =>
            {
                return (
                    1,
                    () => (2, 
                        () => 3));
            });

            multiRunTask.Start();

            var result = multiRunTask.Result;
            Assert.AreEqual(result.headResult, 1);

            var remainResult = result.remainTask.Result;
            Assert.AreEqual(remainResult.headResult, 2);

            var remain2Result = remainResult.remainTask.Result;
            Assert.AreEqual(remain2Result, 3);
        }
    }
}
