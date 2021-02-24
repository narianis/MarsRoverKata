using System;

namespace MarsRover
{
    public class MarsRover : Point
    {
        private Direction _direction;
        public Point[,] grid;
        
        public MarsRover(int coordinateX, int coordinateY, Direction direction)
        {
            if(coordinateX < MINIMAL_COORDINATE_VALUE || coordinateX > MAXIMAL_COORDINATE_VALUE ||
               coordinateY < MINIMAL_COORDINATE_VALUE || coordinateY > MAXIMAL_COORDINATE_VALUE)
            {
                throw new ArgumentOutOfRangeException(
                    $"Coordinate X must be between {MINIMAL_COORDINATE_VALUE} and {MAXIMAL_COORDINATE_VALUE}");
            }

            x = coordinateX;
            y = coordinateY;
            _direction = direction;
        }
        

        public void ReadAndProcessCommands(char[] commands)
        {
            foreach (var command in commands)
            {
                IsNextMoveObstacle(command);
                Move(command);
            }
        }
        
        private void Move(char command)
        {
            switch (command)
            {
                case 'f':
                    MoveForward();
                    break;
                case 'b':
                    MoveBackward();
                    break;
                case 'l':
                    RotateLeft();
                    break;
                case 'r':
                    RotateRight();
                    break;
            }
            HandleEdgeWrapping(); 
        }

        private void MoveForward()
        {
            switch (_direction)
            {
                case Direction.N:
                    y += 1;
                    break;
                case Direction.E:
                    x += 1;
                    break;
                case Direction.S:
                    y -= 1;
                    break;
                case Direction.W:
                    x -= 1;
                    break;
            }
        }

        private void MoveBackward()
        {
            switch (_direction)
            {
                case Direction.N:
                    y -= 1;
                    break;
                case Direction.E:
                    x -= 1;
                    break;
                case Direction.S:
                    y += 1;
                    break;
                case Direction.W:
                    x += 1;
                    break;
            }
        }

        private void RotateLeft()
        {
            switch (_direction)
            {
                case Direction.N:
                    _direction = Direction.W;
                    break;
                case Direction.E:
                    _direction = Direction.N;
                    break;
                case Direction.S:
                    _direction = Direction.E;
                    break;
                case Direction.W:
                    _direction = Direction.S;
                    break;
            }
        }

        private void RotateRight()
        {
            switch (_direction)
            {
                case Direction.N:
                    _direction = Direction.E;
                    break;
                case Direction.E:
                    _direction = Direction.S;
                    break;
                case Direction.S:
                    _direction = Direction.W;
                    break;
                case Direction.W:
                    _direction = Direction.N;
                    break;
            }
        }

        private void HandleEdgeWrapping()
        {
            if (x > MAXIMAL_COORDINATE_VALUE)
                x = MINIMAL_COORDINATE_VALUE;
            else if (x < MINIMAL_COORDINATE_VALUE)
                x = MAXIMAL_COORDINATE_VALUE;
            else if (y > MAXIMAL_COORDINATE_VALUE)
                y = MINIMAL_COORDINATE_VALUE;
            else if (y < MINIMAL_COORDINATE_VALUE)
                y = MAXIMAL_COORDINATE_VALUE;
        }

        private bool IsNextMoveObstacle(char command)
        {
            //TODO add checking depending on direction and command
            if ((_direction == Direction.N && command == 'f') || (_direction == Direction.S && command == 'b'))
            {
                return grid[x, y + 1].Equals(typeof(Obstacle));
            }
            if (_direction == Direction.E)
            {
                return grid[x + 1, y].Equals(typeof(Obstacle));
            }
            if (_direction == Direction.S)
            {
                return grid[x, y - 1].Equals(typeof(Obstacle));
            }
            if (_direction == Direction.W)
            {
                return grid[x, y + 1].Equals(typeof(Obstacle));
            }
            return false;
        }
    }
}