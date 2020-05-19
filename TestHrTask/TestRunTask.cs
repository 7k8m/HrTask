using Microsoft.VisualStudio.TestTools.UnitTesting;
using HrTask;
using System.Threading.Tasks;

namespace TestHrTask
{
    [TestClass]
    public class TestRunTask
    {
        [TestMethod]
        public void TestRun()
        {
            var task = new HrTask<int,int[]>( () => {
                return (
                  1, 
                  new Task<int[]>(() => new int[] { 2, 3 })
                );
            });

            task.Start();

            var result = task.Result;            
            Assert.AreEqual(result.headResult, 1);

            var remainResult = result.remainTask.Result;
            CollectionAssert.AreEqual(remainResult, new int[] { 2, 3 });
        }

        [TestMethod]
        public void TestRunFunc()
        {
            var task = new HrTask<int, int[]>(() => {
                return (
                    1,
                    () => new int[] { 2, 3 }
                );
            });

            task.Start();

            var result = task.Result;
            Assert.AreEqual(result.headResult, 1);

            var remainResult = result.remainTask.Result;
            CollectionAssert.AreEqual(remainResult, new int[] { 2, 3 });
        }

        [TestMethod]
        public async Task TestAsync()
        {
            var task = new HrTask<int,int[]>( () => {
                return (
                  1, 
                  new Task<int[]>(() => new int[] { 2, 3 })
                );
            });

            task.Start();
            
            var result = await task;            
            Assert.AreEqual(result.headResult, 1);

            var remainResult = await result.remainTask;
            CollectionAssert.AreEqual(remainResult, new int[] { 2, 3 });
        }

        [TestMethod]
        public async Task TestAsyncFunc()
        {
            var task = new HrTask<int, int[]>(() => {
                return (
                    1,
                    () => new int[] { 2, 3 }
                );
            });

            task.Start();

            var result = await task;
            Assert.AreEqual(result.headResult, 1);

            var remainResult = await result.remainTask;
            CollectionAssert.AreEqual(remainResult, new int[] { 2, 3 });
        }
    }
}
