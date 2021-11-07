using System;

namespace _1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    abstract class A
    {
        public string Pror1 { get; set; } = "aaa";
        private int prop2;
        public int Prop2
        {
            get => prop2;
            set
            {
                if (value < 0)
                    prop2 = 0;
                else if (value > 100)
                    prop2 = 100;
                else
                    prop2 = value;
            }
        }

        public static int Foo() { return 1; }

        public abstract void Foo(DateTime date);
    }

    class B : A
    {
        private double PropB1 { get; set; }

        public override void Foo(DateTime date) { }
    }

    abstract class C: A
    {
        protected Guid PropC1 { get; set; }
    }

    class D: C
    {
        public E PropD1 { get; set; }

        public override void Foo(DateTime date) { }
    }

    class E
    {
        public E PropE1 { get; set; }
        
        public void Bar() { }

        private void Bar(int size) { }
    }

}
