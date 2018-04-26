using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{

    class Player
    {
        Direction previousDirection;
        int size;
        bool isGrowing;
        Point positionToGrowTo;

        PlayerPart head;
        PlayerPart tail;
        private bool CanGrow()
        {
            PlayerPart part = head;
            while (part != null)
            {
                if (part.Position[0] == positionToGrowTo.Y && part.Position[1] == positionToGrowTo.X)
                {
                    return false;
                }
                part = part.next;
            }
            return true;
        }

        //PlayerPart tail;

        public Direction PreviousDirection { get => previousDirection; set => previousDirection = value; }
        public int Size { get => size; }
        public PlayerPart Head { get => head; set => head = value; }
        public PlayerPart Tail { get => tail; set => tail = value; }

        public void StartGrowing(Point position)
        {
            isGrowing = true;
            positionToGrowTo = position;
            Grow();
            size++;


        }

        public void Move(Direction direction)
        {
            //Just to make sure the player won't try to reverse
            if (head.next != null)
            {
                if ((int)Math.Sqrt(((int)direction - (int)previousDirection) * ((int)direction - (int)previousDirection)) == 2)
                {
                    direction = previousDirection;
                }
            }
            ChangeDirections(direction);
            previousDirection = direction;

            PlayerPart playerPart = head;

            int[] previousPosition = new int[2];
            while (true)
            {
                previousPosition = new int[] { playerPart.Position[0], playerPart.Position[1] };

                if (playerPart == head)
                {
                    playerPart.Move();
                }

                if (playerPart.next != null)
                {
                    playerPart = playerPart.next;
                    playerPart.Position = previousPosition;
                }
                else
                {
                    if (isGrowing && CanGrow())
                    {
                        Grow();
                        StartToMoveTail();
                    }
                    break;
                }
            }
        }

        private void StartToMoveTail()
        {
            if (isGrowing)
            {
                tail.IsMoving = true;
            }
        }

        private void Grow()
        {
            if (!CanGrow())
            {
                return;
            }

            if (Tail != null)
            {
                Tail.next = new PlayerPart(tail.Direction, positionToGrowTo, false);
            }
            else
            {
                head.next = new PlayerPart(head.Direction, positionToGrowTo);
                Tail = head.next;
            }
        }


        /// <summary>
        /// Alright. i need to update directions for each and every part of the snake
        /// for that i need the head to change direction. and then - the part after that should take the direction of the previous part's direction
        /// 
        /// </summary>
        /// <param name="direction"></param>
        private void ChangeDirections(Direction direction)
        {
            PlayerPart playerPart = head;
            Direction nextPartDirection = head.Direction;
            do
            {
                nextPartDirection = playerPart.Direction;
                playerPart.Direction = direction;
                direction = nextPartDirection;

                if (playerPart.next != null)
                {
                    playerPart = playerPart.next;
                }
            }
            while (playerPart.next != null);
        }

        public Player()
        {
            size = 1;
            Head = new PlayerPart(Direction.Right, 10, 10);
            Head.Position[0] = 10; Head.Position[1] = 10;
        }
    }
}
