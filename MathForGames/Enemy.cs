﻿using System;
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
        private Color _alertColor;
        public Entity Target { get { return _target; } set { _target = value; } }
        public Enemy(float x, float y, char icon = '■', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {
        }
        public Enemy(float x, float y, Color rayColor, char icon = '■', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
        }
        public bool CheckTargetInSight(float maxangle,float maxDistance)
        {
            if (Target == null)
                return false;


            Vector2 direction = Target.Position - Position;
            float distance = direction.Magnitude;
            float angle = (float)Math.Acos(Vector2.DotProduct(Forward, direction.Normalized));

            Vector2 topPosition = new Vector2(
                (float)(_position.X + maxDistance * Math.Cos(-maxAngle)),
                (float)(_position.Y + maxDistance * Math.Sin(-maxAngle)));

            // Get the point -maxAngle distance along a circle where radius = maxDistance
            Vector2 bottomPosition = new Vector2(
                (float)(_position.X + maxDistance * Math.Cos(maxAngle)),
                (float)(_position.Y + maxDistance * Math.Sin(maxAngle)));
            if (angle <= maxangle && distance <= maxDistance)
                    return true;

            return false;
        }
        public override void Update(float deltaTime)
        {
            if (CheckTargetInSight(1,10))
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
