using System;
using Xunit;
using MarsRover;

namespace MarsRover.Tests
{
    public class FullTestSuite
    {
        [Fact]
        public void ForwardWithNoObstacles()
        {
            char[] commands = new char[] {'f', 'f', 'f', 'f', 'f'};
            Point[,] grid = new Point[Point.MAXIMAL_COORDINATE_VALUE+1,Point.MAXIMAL_COORDINATE_VALUE+1];
            for (var i = 1; i <= Point.MAXIMAL_COORDINATE_VALUE; i++)
            {
                for (int j = 1; j <= Point.MAXIMAL_COORDINATE_VALUE; j++)
                {
                    grid[i, j] = new Point();
                }
            }
            MarsRover rover = new MarsRover(1, 1, Direction.N,grid);
            
            
            Assert.False(rover.ReadAndProcessCommands(commands));
            Assert.Equal(1,rover.X());
            Assert.Equal(1,rover.Y());
        }

        [Fact]
        public void LeftRotation()
        {
            char[] commands = new char[] {'f', 'l', 'f'};
            Point[,] grid = new Point[Point.MAXIMAL_COORDINATE_VALUE+1,Point.MAXIMAL_COORDINATE_VALUE+1];
            for (var i = 1; i <= Point.MAXIMAL_COORDINATE_VALUE; i++)
            {
                for (int j = 1; j <= Point.MAXIMAL_COORDINATE_VALUE; j++)
                {
                    grid[i, j] = new Point();
                }
            }
            MarsRover rover = new MarsRover(1, 1, Direction.N,grid);
            
            Assert.False(rover.ReadAndProcessCommands(commands));
            Assert.Equal(5,rover.X());
            Assert.Equal(2,rover.Y());
            
        }
        
        [Fact]
        public void RightRotation()
        {
            char[] commands = new char[] {'f', 'r', 'f'};

            Point[,] grid = new Point[Point.MAXIMAL_COORDINATE_VALUE+1,Point.MAXIMAL_COORDINATE_VALUE+1];
            for (var i = 1; i <= Point.MAXIMAL_COORDINATE_VALUE; i++)
            {
                for (int j = 1; j <= Point.MAXIMAL_COORDINATE_VALUE; j++)
                {
                    grid[i, j] = new Point();
                }
            }
            MarsRover rover = new MarsRover(3, 3, Direction.S,grid);
            
            Assert.False(rover.ReadAndProcessCommands(commands));
            Assert.Equal(2,rover.X());
            Assert.Equal(2,rover.Y());
        }
        
        [Fact]
        public void BackwardMovementVertical()
        {
            char[] commands = new char[] {'b','b'};
            Point[,] grid = new Point[Point.MAXIMAL_COORDINATE_VALUE+1,Point.MAXIMAL_COORDINATE_VALUE+1];
            for (var i = 1; i <= Point.MAXIMAL_COORDINATE_VALUE; i++)
            {
                for (int j = 1; j <= Point.MAXIMAL_COORDINATE_VALUE; j++)
                {
                    grid[i, j] = new Point();
                }
            }
            MarsRover rover = new MarsRover(3,3,Direction.S,grid);
            
            Assert.False(rover.ReadAndProcessCommands(commands));
            Assert.Equal(3,rover.X());
            Assert.Equal(5,rover.Y());
        }
        [Fact]
        public void BackwardMovementHorizontal()
        {
            char[] commands = new char[] {'b','b'};
            
            Point[,] grid = new Point[Point.MAXIMAL_COORDINATE_VALUE+1,Point.MAXIMAL_COORDINATE_VALUE+1];
            for (var i = 1; i <= Point.MAXIMAL_COORDINATE_VALUE; i++)
            {
                for (int j = 1; j <= Point.MAXIMAL_COORDINATE_VALUE; j++)
                {
                    grid[i, j] = new Point();
                }
            }
            MarsRover rover = new MarsRover(3,1,Direction.W,grid);
            
            Assert.False(rover.ReadAndProcessCommands(commands));
            Assert.Equal(5,rover.X());
            Assert.Equal(1,rover.Y());
        }
        
        [Fact]
        public void SetupWithInvalidValues()
         {
             char[] commands = {'f'};
             Point[,] grid = new Point[Point.MAXIMAL_COORDINATE_VALUE+1,Point.MAXIMAL_COORDINATE_VALUE+1];
             for (var i = 1; i <= Point.MAXIMAL_COORDINATE_VALUE; i++)
             {
                 for (int j = 1; j <= Point.MAXIMAL_COORDINATE_VALUE; j++)
                 {
                     grid[i, j] = new Point();
                 }
             }
             
             Assert.Throws<ArgumentOutOfRangeException>(()=>new MarsRover(7, 10, Direction.N,grid));
         }
        
        [Fact]
        public void ObstacleAtFirstMove()
        {
            char[] commands = {'f'};
            Point[,] grid = new Point[Point.MAXIMAL_COORDINATE_VALUE+1,Point.MAXIMAL_COORDINATE_VALUE+1];
            for (var i = 1; i <= Point.MAXIMAL_COORDINATE_VALUE; i++)
            {
                for (int j = 1; j <= Point.MAXIMAL_COORDINATE_VALUE; j++)
                {
                    grid[i, j] = new Point();
                }
            }
            MarsRover rover = new MarsRover(1, 1, Direction.N,grid);
            rover.grid[1, 2] = new Obstacle();
            
            Assert.True(rover.ReadAndProcessCommands(commands));
            Assert.Equal(1, rover.X());
            Assert.Equal(1,rover.Y());
        }
        
        [Fact]
        public void ObstacleInitialisedButNotMet()
        {
            char[] commands = {'f','b','r','f'};
            Point[,] grid = new Point[Point.MAXIMAL_COORDINATE_VALUE+1,Point.MAXIMAL_COORDINATE_VALUE+1];
            for (var i = 1; i <= Point.MAXIMAL_COORDINATE_VALUE; i++)
            {
                for (int j = 1; j <= Point.MAXIMAL_COORDINATE_VALUE; j++)
                {
                    grid[i, j] = new Point();
                }
            }
            MarsRover rover = new MarsRover(1, 1, Direction.N,grid);
            rover.grid[5, 5] = new Obstacle();
            
            Assert.False(rover.ReadAndProcessCommands(commands));
            Assert.Equal(2, rover.X());
            Assert.Equal(1,rover.Y());
        }
    }
}