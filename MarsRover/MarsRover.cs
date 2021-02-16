using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MarsRover
{
    public class MarsRover
    {
        public static int MINIMAL_COORDINATE_VALUE = 1;
        public static int MAXIMAL_COORDINATE_VALUE = 5;
        private int coordinateX;
        private int coordinateY;
        private char[] directions = {'N', 'E', 'S', 'W'};
        private int directionIndex;
        
        
        public void setInitialState(int xCoordinate, int yCoordinate, char direction)
        {
            if (IsWithinAllowedCoordinates(xCoordinate, yCoordinate))
            {
                coordinateX = xCoordinate;
                coordinateY = yCoordinate;
                SetDirection(direction);
            }
            else ReturnOutOfBounds();
        }

        private bool IsWithinAllowedCoordinates(int x, int y)
        {
            return (x >= MINIMAL_COORDINATE_VALUE && x <= MAXIMAL_COORDINATE_VALUE) && (y >= MINIMAL_COORDINATE_VALUE && y <= MAXIMAL_COORDINATE_VALUE);
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

        public void Move(char command)
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
        }

        private void MoveForward()
        {
            switch (directionIndex)
            {
                case 0:
                    coordinateY += 1;
                    break;
                case 1:
                    coordinateX -= 1;
                    break;
                case 2:
                    coordinateY -= 1;
                    break;
                case 3:
                    coordinateX += 1;
                    break;
            }
        }

        private void MoveBackward()
        {
            switch (directionIndex)
            {
                case 0:
                    coordinateY -= 1;
                    break;
                case 1:
                    coordinateX += 1;
                    break;
                case 2:
                    coordinateY += 1;
                    break;
                case 3:
                    coordinateX -= 1;
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

        public void CheckForObstacle()
        {
            
        }
    }
}