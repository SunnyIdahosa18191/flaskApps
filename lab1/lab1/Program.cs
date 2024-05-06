using MyLibrary;

namespace lab1
{
    internal class Program
    {    
        static void Main(string[] args)
        {
            MyQueue<string> myqueue = new MyQueue<string>();

            try
            {
                myqueue = new MyQueue<string>(-2);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("A size of a queue must be >= 0");
            }

            Console.WriteLine();

            myqueue.Notify += DisplayMessage;
            myqueue.Enqueue("Hello ");
            myqueue.Enqueue("world, ");
            myqueue.Enqueue("I am a custom ");
            myqueue.Enqueue("collection");

            Console.WriteLine();

            foreach (string i in myqueue)
                Console.Write(i);

            Console.WriteLine();

            string word = "collection";
            if (myqueue.Contains(word))
                Console.WriteLine($"The queue contains the word \"{word}\"");


            Console.WriteLine();
            try
            {
                myqueue.Dequeue();
                myqueue.Dequeue();
                myqueue.Dequeue();
                myqueue.Dequeue();
                myqueue.Dequeue();
                myqueue.Dequeue();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("An amount of dequeuing actions is more than a size of a queue");
            }

            Console.WriteLine();

            myqueue.Enqueue(":)");

            try { Console.WriteLine(myqueue.Peek()); }
            catch (InvalidOperationException) { Console.WriteLine("There are no values in the queue"); }

            Console.WriteLine();

            myqueue.Clear();

            Console.WriteLine();

            try { Console.WriteLine(myqueue.Peek()); }
            catch (InvalidOperationException) { Console.WriteLine("There are no values in the queue"); }

            Console.ReadLine();

            void DisplayMessage(string message)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
    }
}