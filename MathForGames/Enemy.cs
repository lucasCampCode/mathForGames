using System;
using System.Collections.Generic;
using System.Text;
using MathLib;
using Raylib_cs;

namespace MathForGames
{
    class Enemy: Entity
    {
        public Entity target { get; private set; }
        public Enemy(float x, float y, Color rayColor, char icon = '■', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
        }
        public bool CheckTargetInSight()
        {
            if (target == null)
                return false;

            Vector2 direction = Vector2.Normalize(Position - target.Position);

            if (Vector2.DotProduct(Forward, direction) > 0)
                return true;

            return false;
        }
        public override void Update(float deltaTime)
        {
            if (CheckTargetInSight())
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
