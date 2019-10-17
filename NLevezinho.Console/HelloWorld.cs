using System;
using System.Collections.Generic;
using System.Text;

namespace NLevezinho.Console
{
    class HelloWorld
    {
        public string Name { get; }
        public int Age { get; }

        public HelloWorld(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
