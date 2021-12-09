using System;
using System.Collections.Generic;
using System.Text;

namespace _2._2
{
    class Computer: Device 
    {
        public string Processor { get; set; }
        public int HardDiskSize { get; set; }

        public int RAMSize { get; set; }

        public Computer(string processor, int hardDiskSize, int rAMSize): base()
        {
            Processor = processor;
            HardDiskSize = hardDiskSize;
            RAMSize = rAMSize;
        }
    }
}
