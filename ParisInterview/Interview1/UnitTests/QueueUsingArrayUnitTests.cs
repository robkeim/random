using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interview1;

namespace UnitTests
{
    [TestClass]
    public class QueueUsingArrayUnitTests
    {
        [TestMethod]
        public void EnqueueAndDequeueBasic()
        {
            Interview1.QueueUsingArray<int> myQueue = new QueueUsingArray<int>();

            int testValue = 2;
            myQueue.Enqueue(testValue);
            int dequeuedValue = myQueue.Dequeue();

            Assert.AreEqual(testValue, dequeuedValue);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EnsureDequeueExceptionOnEmptyQueue()
        {
            Interview1.QueueUsingArray<int> myQueue = new QueueUsingArray<int>();
            myQueue.Dequeue();
        }
    }
}
