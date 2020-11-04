﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLib;
using Raylib_cs;

namespace MathForGames
{
    class Actor
    {
        protected char _icon = ' ';
        protected Vector2 _velocity;
        protected Matrix3 _transform;
        private Matrix3 _translation = new Matrix3();
        private Matrix3 _rotation = new Matrix3();
        private Matrix3 _scale = new Matrix3();
        protected ConsoleColor _color;
        protected Color _rayColor;
        protected Sprite _sprite;
        public bool Started { get; private set; }

        public Vector2 Forward
        {
            get
            {
                return new Vector2(_transform.m11, _transform.m21);
            }

        }
        public Vector2 Position
        {
            get
            {
                return new Vector2(_transform.m13, _transform.m23);
            }
            set
            {
                _translation.m13 = value.X;
                _translation.m23 = value.Y;
            }
        }
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


        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Actor(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
        {
            _rayColor = Color.WHITE;
            _icon = icon;
            _transform = new Matrix3();
            Position = new Vector2(x, y);
            _velocity = new Vector2();
            _color = color;
            _sprite = new Sprite("Images/enemy.png");
        }


        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn to the console</param>
        public Actor(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : this(x, y, icon, color)
        {
            _rayColor = rayColor;
        }

        public void SetTranslation(Vector2 pos)
        {
            _translation.m13 = pos.X;
            _translation.m23 = pos.Y;
        }

        public void SetRotation(float radians)
        {
            _rotation.m11 = (float)Math.Cos(radians);
            _rotation.m12 = (float)Math.Sin(radians);
            _rotation.m21 = -(float)Math.Sin(radians);
            _rotation.m22 = (float)Math.Cos(radians);
        }
        public void SetScale(float x,float y)
        {
            _scale.m11 = x;
            _scale.m22 = y;
        }

        public void UpdateTransform()
        {
            //SetTranslation(new Vector2(1,1));
            SetRotation(-(float)Math.Atan2(Velocity.Y, Velocity.X));
            SetScale(1,1);

            _transform = _translation * _rotation * _scale;
        }
        public virtual void Start()
        {
            Started = true;
        }


        public virtual void Update(float deltaTime)
        {
            UpdateTransform();
            //Increase position by the current velocity
            Position += _velocity * deltaTime;
        }

        public virtual void Draw()
        {
            //Draws the actor and a line indicating it facing to the raylib window.
            //Scaled to match console movement
            _sprite.Draw(_transform);
            Raylib.DrawText(_icon.ToString(), (int)(Position.X * 32), (int)(Position.Y * 32), 32, _rayColor);
            Raylib.DrawLine(
                (int)(Position.X * 32),
                (int)(Position.Y * 32),
                (int)((Position.X + Forward.X) * 32),
                (int)((Position.Y + Forward.Y) * 32),
                Color.WHITE
            );

            //Changes the color of the console text to be this actors color
            Console.ForegroundColor = _color;

            //Only draws the actor on the console if it is within the bounds of the window
            if (Position.X >= 0 && Position.X < Console.WindowWidth
                && Position.Y >= 0 && Position.Y < Console.WindowHeight)
            {
                Console.SetCursorPosition((int)Position.X, (int)Position.Y);
                Console.Write(_icon);
            }

            //Reset console text color to be default color
            Console.ForegroundColor = Game.DefaultColor;
        }

        public virtual void End()
        {
            Started = false;
        }

    }
}
