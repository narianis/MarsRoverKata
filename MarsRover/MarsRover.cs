using System;
using System.Collections.Generic;

namespace MarsRover
{
    public class MarsRover
    {
        private Direction _direction;
        public Point[,] grid;
        private bool _isObstacleEncountered;
        public static int MINIMAL_COORDINATE_VALUE = 1;
        public static int MAXIMAL_COORDINATE_VALUE = 5;
        private int x;
        private int y;


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

        // private void MapCommandToAction(char command)
        // {
        //     var mapping = new Dictionary<char, Action>
        //     {
        //         {'f', MoveForward},
        //         {'b', MoveBackward},
        //         {'l', RotateLeft},
        //         {'r', RotateRight}
        //     };
        // }
        
        private void Move(char command)
        {
            Dictionary<char,Action> moveDictionary = new Dictionary<char, Action>();
            moveDictionary.Add('f', MoveForward);
            moveDictionary.Add('b', MoveBackward);
            moveDictionary.Add('l', RotateLeft);
            moveDictionary.Add('r', RotateRight);
            moveDictionary[command]();
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
            HandleEdgeWrapping();
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
            HandleEdgeWrapping();
        }

        private void RotateLeft()
        {
            int directionIndex = (int) _direction;
            directionIndex-= 1;
            if (directionIndex < 0) directionIndex = 3;
            _direction =  (Direction) directionIndex;
        }

        private void RotateRight()
        {
            int directionIndex = (int) _direction;
            directionIndex += 1;
            if (directionIndex > 3) directionIndex = 0;
            _direction =  (Direction) directionIndex;
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
        
        public int X() => x;
        public int Y() => y;
    }
}