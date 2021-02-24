using Xunit;
using MarsRover;

namespace MarsRover
{
    public class FullTestSuite
    {

        private void TestWith(int x,int y, char dir, char[] commands)
        {
            MarsRover rover = new MarsRover();
            rover.SetInitialState(x,y,dir);
            rover.ReadAndProcessCommands(commands);
        }
        
        [Fact]
        public void ForwardWithNoObstacles()
        {
            char[] commands = new char[] {'f', 'f', 'f', 'f', 'f'};
            
            TestWith(1,1,'N',commands);
            
            Assert.Equal(1,rover.CoordinateX());
            Assert.Equal(1,rover.CoordinateY());
        }

        [Fact]
        public void LeftRotation()
        {
            char[] commands = new char[] {'f', 'l', 'f'};
            
            TestWith(2,2,'N',commands);
            
            Assert.Equal(1,rover.CoordinateX());
            Assert.Equal(3,rover.CoordinateY());
        }

        [Fact]
        public void RightRotation()
        {
            char[] commands = new char[] {'f', 'r', 'f'};
            
            TestWith(3,3,'N',commands);
            
            Assert.Equal(4,rover.CoordinateX());
            Assert.Equal(4,rover.CoordinateY());
        }

        [Fact]
        public void BackwardMovementVertical()
        {
            char[] commands = new char[] {'b','b'};
            
            TestWith(3,3,'S',commands);
            
            Assert.Equal(3,rover.CoordinateX());
            Assert.Equal(5,rover.CoordinateY());
        }
        [Fact]
        public void BackwardMovementHorizontal()
        {
            char[] commands = new char[] {'b','b'};
            
            TestWith(3,1,'W',commands);
            
            Assert.Equal(5,rover.CoordinateX());
            Assert.Equal(1,rover.CoordinateY());
        }

        [Fact]
        public void SetupWithInvalidValues()
        {
            char[] commands = new[] {'x', 'y', 'z'};
            
            Assert.Equal(-1,rover.SetInitialState(0,25,'H'));
        }

        [Fact]
        public void ObstacleAtFirstMove()
        {
            char[] commands = {'f'};
            rover.MakeObstacle(1,2);
            TestWith(1, 1, 'N', commands);
            
            Assert.Equal(1, rover.CoordinateX());
            Assert.Equal(1,rover.CoordinateY());
            Assert.True(rover.ObstacleEncountered());
        }
    }
}