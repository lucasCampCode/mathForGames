using System;
using System.Collections.Generic;
using System.Text;
using MathLib;
using Raylib_cs;

namespace MathForGames
{
    class Entity
    {
        protected char _icon = '-';
        protected Vector2 _position;
        protected Vector2 _velocity;
        protected Vector2 _facing;
        protected ConsoleColor _color;
        protected Color _rayColor;


        public bool Started { get; private set; }
        public Vector2 Forward{ get { return _facing; } set { _facing = value; } }
        public Vector2 Position { get { return _position; } set { _position = value;} }

        public Vector2 Velocity
        {
            get
            {
                return _velocity;
            }
            set
            {
                _velocity = value;
            }
        }

        

        public Entity(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
        {
            _rayColor = Color.WHITE;
            _icon = icon;
            _position = new Vector2(x, y);
            _velocity = new Vector2();
            _color = color;
            Forward = new Vector2(1, 0);
        }
        public Entity(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : this(x, y, icon, color)
        {
            _rayColor = rayColor;
        }

        private void UpdateFacing()
        {
            if (_velocity.Magnitude <= 0)
                return;
            _facing = _velocity.Normalized;
        }

        public virtual void Start()
        {

            Started = true;
        }

        public virtual void Update(float deltaTime)
        {
            UpdateFacing();
            _position += _velocity * deltaTime;
        }

        public virtual void Draw()
        {
            
            int scale = 15;
            Raylib.DrawText(_icon.ToString(), (int)(_position.X * scale), (int)(_position.Y * scale), scale,_rayColor);
            Raylib.DrawLine(
                (int)(Position.X * scale),
                (int)(Position.Y * scale),
                (int)((Position.X + Forward.X) * scale),
                (int)((Position.Y + Forward.Y) * scale),
                Color.BLACK
                ) ;
            if(Position.X >= 0 && Position.X < Console.WindowWidth
                && Position.Y >= 0 && Position.Y < Console.WindowHeight)
            Console.ForegroundColor = _color;
            Console.SetCursorPosition((int)_position.X, (int)_position.Y);
            Console.Write(_icon);
            Console.ForegroundColor = ConsoleColor.White;


        }

        public void End()
        {

            Started = false;
        }

    }
}
