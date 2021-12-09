using System;
using System.Collections.Generic;
using System.Text;

namespace _1._1
{
    delegate Class1 GetNewClass1();
    delegate Class1 TransformClass1(object obj);
    delegate string GetClass1Description(object obj, string comment);
    
    class Class1
    {
        public GetNewClass1 Generator;
        public TransformClass1 Transformer;

        public Class1(GetClass1Description d1, GetClass1Description d2)
        {
            OnDescribe += d1;
            OnDescribe += d2;
        }

        public event GetClass1Description OnDescribe;
    }
}
