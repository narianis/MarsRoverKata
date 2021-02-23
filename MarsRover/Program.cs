using System;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            MarsRover rover = new MarsRover();
            char[] commands = new char[] {'b','b'};
            
            rover.SetInitialState(3,3,'S');
            rover.ReadAndProcessCommands(commands);
        }
    }
}