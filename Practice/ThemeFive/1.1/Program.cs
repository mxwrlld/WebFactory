using System;

namespace _1._1
{
    class Program
    {
        static void Main() {}
    }

    interface ISomeInterface
    {
        public int X { get; set; }
        public string Foo(string arg1);
        public void Bar(int number);
    }

    class Consumer
    {
        public void UseSomeInterface(ISomeInterface o) { }
    }

    class A : ISomeInterface
    {
        public int X { get; set; }
        public int Y { get; set; }

        public string Foo(string arg1) { return arg1; }

        public void Bar(int nubmer) { }

        public A Process(A a) { return a; }
    }

    class B : ISomeInterface
    {
        public int X { get; set; }

        public string Foo(string arg1){ return arg1; }

        public void Bar(int nubmer) { }

        private void DoSomething(double a) { }
    }
}
