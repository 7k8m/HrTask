# HrTask
Task of Head Result and Task for Remain 

# Test Run

`````
        public void TestRun()
        {
            var task = new HrTask<int,int[]>( () => {
                return (
                  1, 
                  new Task<int[]>(() => {return new int[] { 2, 3 };})
                );
            });
            
            task.Start();
            var result = task.Result;
            
            Assert.AreEqual(result.headResult, 1);

            var remainResult = result.remainTask.Result;
            CollectionAssert.AreEqual(remainResult, new int[] { 2, 3 });

        }
`````
