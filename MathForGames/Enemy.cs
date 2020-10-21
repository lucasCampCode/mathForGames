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
            
            

            return true;
        }
    }
}
