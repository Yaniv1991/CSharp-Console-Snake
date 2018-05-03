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
        sbyte turnsToWait;

        PlayerPart head;
        PlayerPart tail;
        private bool CanGrow => (--turnsToWait == -1);


        public Direction PreviousDirection { get => previousDirection; set => previousDirection = value; }
        public int Size { get => size; }
        public PlayerPart Head { get => head; set => head = value; }
        public PlayerPart Tail { get => tail; set => tail = value; }

        public void StartGrowing(Point position)
        {
            isGrowing = true;
            positionToGrowTo = position;
            turnsToWait = (sbyte)size;
            Grow();
        }

        public void Move(Direction direction)
        {
            //Just to make sure the player won't try to reverse. Still needs work
            if (head.next != null)
            {
                if ((int)Math.Sqrt(((int)direction - (int)head.next.Direction) * ((int)direction - (int)head.next.Direction)) == 2)
                {
                    direction = previousDirection;
                }
            }
            
            PlayerPart playerPart = head;
            Direction directionForNextPart;
            for (int i = 0; i < size; i++)
            {
                directionForNextPart = playerPart.Direction;
                playerPart.Move(direction);
                direction = directionForNextPart;

                playerPart = playerPart.next;
            }

            Grow();
        }


        private void Grow()
        {
            if (!CanGrow|| !isGrowing)
            {
                return;
            }

            if (Tail != null)
            {
                Tail.next = new PlayerPart(tail.Direction, positionToGrowTo);
                tail = tail.next;
            }
            else
            {
                head.next = new PlayerPart(head.Direction, positionToGrowTo);
                Tail = head.next;
            }

            size++;
            isGrowing = false;
        }

        public Player()
        {
            size = 1;
            Head = new PlayerPart(Direction.Right, 10, 10);
            Head.Position[0] = 10; Head.Position[1] = 10;
        }
    }
}
