using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLib;

namespace MathForGames
{
    class Player : Entity
    {
        public Player(float x, float y, Color rayColor, char icon = '■', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
        }
        public override void Update(float deltaTime)
        {
            float speed = 15;

            int xVelocity = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_A))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_D));

            int yVelocity = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_W))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_S));

            Velocity = new Vector2(xVelocity, yVelocity);

            Velocity = Velocity.Normalized * speed;


            //Velocity = Velocity.Normalized
            base.Update(deltaTime);
        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}
