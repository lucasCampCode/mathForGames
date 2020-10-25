using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using MathLib;
using Raylib_cs;

namespace MathForGames
{
    class Enemy: Entity
    {
        private Entity _target;
        public Entity Target { get { return _target; } set { _target = value; } }
        public Enemy(float x, float y, char icon = '■', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {
        }
        public Enemy(float x, float y, Color rayColor, char icon = '■', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
        }
        public bool CheckTargetInSight(float maxAngle,float maxDistance)
        {
            if (Target == null)
                return false;

            System.Numerics.Vector2 center = new System.Numerics.Vector2(Position.X, Position.Y);
            if (Game.Debug)
            {
                Raylib.DrawCircleSectorLines(center * Game.Scale,
                    maxDistance * Game.Scale,
                    (int)((maxAngle * 180 / Math.PI)+ 90),
                    (int)((-maxAngle * 180 / Math.PI)+ 90),
                    1,
                    Color.GREEN);
            }
            Vector2 direction = Target.Position - Position;
            float distance = direction.Magnitude;
            float angle = (float)Math.Acos(Vector2.DotProduct(Forward, direction.Normalized));
            
            
            if (angle <= maxAngle && distance <= maxDistance)
                    return true;

            return false;
        }
        public override void Update(float deltaTime)
        {
            if (CheckTargetInSight(0.5f,10))
            {
                _rayColor = Color.RED;
            }
            else
            {
                _rayColor = Color.SKYBLUE;
            }
            base.Update(deltaTime);
        }
    }
}
