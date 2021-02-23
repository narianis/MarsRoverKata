using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MarsRover
{
    public class MarsRover
    {
        private static int MINIMAL_COORDINATE_VALUE = 1;
        private static int MAXIMAL_COORDINATE_VALUE = 5;
        private int coordinateX;
        private int coordinateY;
        private char[] directions = {'N', 'E', 'S', 'W'};
        private int directionIndex;
        private bool[,] obstacles = new bool[MAXIMAL_COORDINATE_VALUE+1, MAXIMAL_COORDINATE_VALUE+1];
        private int obstacleSensorCoordinateX;
        private int obstacleSensorCoordinateY;
        private bool obstacleEncountered;


        public int SetInitialState(int xCoordinate, int yCoordinate, char direction)
        {
            if (IsWithinAllowedCoordinates(xCoordinate, yCoordinate))
            {
                coordinateX = xCoordinate;
                coordinateY = yCoordinate;
                SetDirection(direction);
                return 1;
            }
            
            return ReturnOutOfBounds();
        }

        private bool IsWithinAllowedCoordinates(int x, int y)
        {
            return (x >= MINIMAL_COORDINATE_VALUE && x <= MAXIMAL_COORDINATE_VALUE) 
                   && (y >= MINIMAL_COORDINATE_VALUE && y <= MAXIMAL_COORDINATE_VALUE);
        }

        private int ReturnOutOfBounds()
        {
            return -1;
        }

        private void SetDirection(char direction)
        {
            directionIndex = direction switch
            {
                'N' => 0,
                'E' => 1,
                'S' => 2,
                'W' => 3,
                _ => directionIndex
            };
        }

        public void ReadAndProcessCommands(char[] commands)
        {
            foreach (var command in commands)
            {
                Console.WriteLine($"X: {coordinateX}, Y: {coordinateY}");
                CheckForObstacleOnNextMoveAndReport(command);
                if (obstacleEncountered)
                {
                    break;
                }
                Move(command,coordinateX,coordinateY);
            }
        }
        
        private void Move(char command, int x, int y)
        {
            switch (command)
            {
                case 'f':
                    MoveForward(x, y);
                    break;
                case 'b':
                    MoveBackward(x, y);
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

        private void MoveForward(int x, int y)
        {
            switch (directionIndex)
            {
                case 0:
                    y += 1;
                    break;
                case 1:
                    x += 1;
                    break;
                case 2:
                    y -= 1;
                    break;
                case 3:
                    x -= 1;
                    break;
            }
        }

        private void MoveBackward(int x, int y)
        {
            switch (directionIndex)
            {
                case 0:
                    y -= 1;
                    break;
                case 1:
                    x -= 1;
                    break;
                case 2:
                    y += 1;
                    break;
                case 3:
                    x += 1;
                    break;
            }
        }

        private void RotateLeft()
        {
            if (directionIndex == 0)
                directionIndex = 3;
            else
                directionIndex -= 1;
        }

        private void RotateRight()
        {
            if (directionIndex == 3)
                directionIndex = 0;
            else
                directionIndex += 1;
        }

        private void HandleEdgeWrapping()
        {
            if (obstacleSensorCoordinateX > MAXIMAL_COORDINATE_VALUE)
                obstacleSensorCoordinateX = MINIMAL_COORDINATE_VALUE;
            else if (obstacleSensorCoordinateX < MINIMAL_COORDINATE_VALUE)
                obstacleSensorCoordinateX = MAXIMAL_COORDINATE_VALUE;
            else if (obstacleSensorCoordinateY > MAXIMAL_COORDINATE_VALUE)
                obstacleSensorCoordinateY = MINIMAL_COORDINATE_VALUE;
            else if (obstacleSensorCoordinateY < MINIMAL_COORDINATE_VALUE)
                obstacleSensorCoordinateY = MAXIMAL_COORDINATE_VALUE;
            else if (coordinateX > MAXIMAL_COORDINATE_VALUE)
                coordinateX = MINIMAL_COORDINATE_VALUE;
            else if (coordinateX < MINIMAL_COORDINATE_VALUE)
                coordinateX = MAXIMAL_COORDINATE_VALUE;
            else if (coordinateY > MAXIMAL_COORDINATE_VALUE)
                coordinateY = MINIMAL_COORDINATE_VALUE;
            else if (coordinateY < MINIMAL_COORDINATE_VALUE)
                coordinateY = MAXIMAL_COORDINATE_VALUE;
        }

        private void CheckForObstacleOnNextMoveAndReport(char command)
        {
            if (command == 'l' || command == 'r') return;

            obstacleSensorCoordinateX = coordinateX;
            obstacleSensorCoordinateY = coordinateY;
            
            Move(command, obstacleSensorCoordinateX, obstacleSensorCoordinateY);
            obstacleEncountered = obstacles[obstacleSensorCoordinateX, obstacleSensorCoordinateY];
        }

        public int CoordinateX()
        {
            return coordinateX;
        }
        
        public int CoordinateY()
        {
            return coordinateY;
        }

        public void MakeObstacle(int x, int y) => obstacles[x, y] = true;

        public bool ObstacleEncountered() => obstacleEncountered;
    }
}