using System;
using System.Collections.Generic;
using System.Text;

namespace NLevezinho.Console
{
    class HelloWorld : IWorld
    {
        public string Name { get; }
        public int Age { get; }

        public HelloWorld()
        {
            Name = "Nuno";
            Age = 25;
        }

        public void Print()
        {
            System.Console.WriteLine("Hello, World!");
        }
    }

    public interface IWorld
    {
        string Name { get; }
        int Age { get; }
        void Print();
    }

    class Car
    {
        private readonly IWorld _world;
        
        string _name = string.Empty;
        
        public Car(IWorld world)
        {
            _world = world;
        }

        public void Update(string name)
        {
            _name = name;
        }

        public void Hi()
        {
            _world.Print();
            System.Console.WriteLine($"Name: {_name}");
        }
    }
}
