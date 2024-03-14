
namespace MyLibrary.Tests
{
    [TestClass]
    public class MyCollectionTests
    {
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void CreateMyQueue_ExceptionExpected()
        {
            MyQueue<string> myqueue = new MyQueue<string>(-2);
        }

        [TestMethod]
        public void EnqueueTestEmpty()
        {
            MyQueue<string> myqueue = new MyQueue<string>(2);

            Assert.AreEqual(0, myqueue.Size());
        }

        [TestMethod]
        public void EnqueueTest()
        {
            MyQueue<string> myqueue = new MyQueue<string>();

            myqueue.Enqueue("Hello ");
            myqueue.Enqueue("world");

            Assert.AreEqual(2, myqueue.Size());
        }

        [TestMethod]
        public void DequeueTest()
        {
            MyQueue<string> myqueue = new MyQueue<string>();

            myqueue.Enqueue("Hello ");
            myqueue.Enqueue("world");

            myqueue.Dequeue();
            myqueue.Dequeue();

            Assert.AreEqual(0, myqueue.Size());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Dequeue_ExceptionExpected()
        {
            MyQueue<string> myqueue = new MyQueue<string>();

            myqueue.Enqueue("Hello ");
            myqueue.Dequeue();
            myqueue.Dequeue();
        }

        [TestMethod]
        public void ContainsTest_Fail_IfDoesntContain()
        {
            MyQueue<int> myqueue = new MyQueue<int>();

            myqueue.Enqueue(1);
            myqueue.Enqueue(2);
            myqueue.Enqueue(3);
            myqueue.Enqueue(4);
            myqueue.Enqueue(5);

            bool result = !myqueue.Contains(3);

            if (result)
                Assert.Fail();
        }

        [TestMethod]
        public void PeekTest()
        {
            int expected = 1;
            MyQueue<int> myqueue = new MyQueue<int>();

            myqueue.Enqueue(1);
            myqueue.Enqueue(2);
            myqueue.Enqueue(3);

            int result = myqueue.Peek();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Peek_ExceptionExpected()
        {
            MyQueue<string> myqueue = new MyQueue<string>();

            myqueue.Peek();
        }

        [TestMethod]
        public void ClearTest()
        {
            int expected = 0;
            MyQueue<int> myqueue = new MyQueue<int>();

            myqueue.Enqueue(1);
            myqueue.Enqueue(2);
            myqueue.Enqueue(3);
            myqueue.Enqueue(4);
            myqueue.Enqueue(5);

            myqueue.Clear();

            Assert.AreEqual(expected, myqueue.Size());
        }

        [TestMethod]
        public void NumerableTest()
        {
            int expected = 5;
            MyQueue<int> myqueue = new MyQueue<int>();

            myqueue.Enqueue(1);
            myqueue.Enqueue(2);
            myqueue.Enqueue(3);
            myqueue.Enqueue(4);
            myqueue.Enqueue(5);

            MyQueue<int> myqueue2 = new MyQueue<int>();

            foreach (var item in myqueue)
                myqueue2.Enqueue(item);

            Assert.AreEqual(expected, myqueue2.Size());           
        }

    }
}