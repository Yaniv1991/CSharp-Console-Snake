using System;

namespace Snake
{
    class PlayerPart
    {
        int[] position;

        int y , x;
        
        Point _pointPosition;
        public PlayerPart next;
        Direction direction;

        public bool IsMoving { get; set; }

        public PlayerPart(Direction direction, int y, int x)
        {
            IsMoving = true;
            this.y = y;
            this.x = x;
            position = new int[] { y, x };
            this.direction = direction;

            _pointPosition = new Point(position[0], position[1]);
        }

        public PlayerPart(Direction direction, Point position,bool isMoving = true)
        {
            this.direction = direction;
            _pointPosition = position;
            this.position = new int[] { position.Y, position.X };
            IsMoving = isMoving;
        }


        private void MoveReverse()
        {
            if (direction == Direction.Up)
            {
                position[0]++;
                //_pointPosition.Y--;
            }
            else if (direction == Direction.Down)
            {
                position[0]--;
            }
            else if (direction == Direction.Left)
            {
                position[1]++;
            }
            else if (direction == Direction.Right)
            {
                position[1]--;
            }
        }

        public int[] Position { get => position; set => position = value; }
        public Direction Direction { get => direction; set => direction = value; }

        public void Move()
        {
            if (!IsMoving)
            {
                return;
            }

            if (direction == Direction.Up)
            {
                position[0]--;
            }
            else if (direction == Direction.Down)
            {
                position[0]++;
            }
            else if (direction == Direction.Left)
            {
                position[1]--;
            }
            else if (direction == Direction.Right)
            {
                position[1]++;
            }
        }

        public void Move(Direction direction)
        {
            if (!IsMoving)
            {
                return;
            }

            this.direction = direction;
            if (direction == Direction.Up)
            {
                position[0]--;
            }
            else if (direction == Direction.Down)
            {
                position[0]++;
            }
            else if (direction == Direction.Left)
            {
                position[1]--;
            }
            else if (direction == Direction.Right)
            {
                position[1]++;
            }
        }
    }
}
