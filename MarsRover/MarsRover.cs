using System;

namespace MarsRover
{
    public class MarsRover : Point
    {
        private Direction _direction;
        public Point[,] grid;
        private bool _isObstacleEncountered;
        
        public MarsRover(int coordinateX, int coordinateY, Direction direction, Point[,] grid)
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
            this.grid = grid;
        }


        public bool ReadAndProcessCommands(char[] commands)
        {
            foreach (var command in commands)
            {
                if (IsNextMoveObstacle(command))
                {
                    _isObstacleEncountered = true;
                    break;
                }
                else
                {
                    Move(command);
                }
            }

            return _isObstacleEncountered;
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
            if ((_direction == Direction.N && command == 'f') || (_direction == Direction.S && command == 'b'))
            {
                if (y == MAXIMAL_COORDINATE_VALUE) return grid[x, MINIMAL_COORDINATE_VALUE] is Obstacle;
                return grid[x, y + 1] is Obstacle;
            }
            if ((_direction == Direction.E && command == 'f') || (_direction == Direction.W && command == 'b'))
            {
                if (x == MAXIMAL_COORDINATE_VALUE) return grid[MINIMAL_COORDINATE_VALUE, y] is Obstacle;
                return grid[x + 1, y] is Obstacle;
            }
            if ((_direction == Direction.S && command == 'f') || (_direction == Direction.N && command == 'b'))
            {
                if (y == MINIMAL_COORDINATE_VALUE) return grid[x, MAXIMAL_COORDINATE_VALUE] is Obstacle;
                return grid[x, y - 1] is Obstacle;
            }
            if ((_direction == Direction.W && command == 'f') || (_direction == Direction.E && command == 'b'))
            {
                if (x == MINIMAL_COORDINATE_VALUE) return grid[MAXIMAL_COORDINATE_VALUE, y] is Obstacle;
                return grid[x - 1, y] is Obstacle;
            }
            return false;
        }
    }
}