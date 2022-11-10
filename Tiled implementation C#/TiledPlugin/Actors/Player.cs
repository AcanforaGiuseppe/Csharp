using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledPlugin
{
    class Player : Actor
    {
        private bool isFirePressed;

        public Player(Vector2 position) : base("player")
        {
            RigidBody.Type = RigidBodyType.Player;
            sprite.position = position;

            Energy = 100;
            cannonOffset = new Vector2(sprite.pivot.X-5,5);

            IsActive = true;
        }
       
        public void Input()
        {
            if (Game.Win.GetKey(KeyCode.D))
                RigidBody.Velocity.X = speed;
            else if (Game.Win.GetKey(KeyCode.A))
                RigidBody.Velocity.X = -speed;
            else
                RigidBody.Velocity.X = 0;

            if (Game.Win.GetKey(KeyCode.W))
                RigidBody.Velocity.Y = -speed;
            else if (Game.Win.GetKey(KeyCode.S))
                RigidBody.Velocity.Y = speed;
            else
                RigidBody.Velocity.Y = 0;

            if(RigidBody.Velocity.X!=0 && RigidBody.Velocity.Y != 0)
                Velocity = Velocity.Normalized() * speed;

            if (Game.Win.GetKey(KeyCode.Space))
            {
                if (!isFirePressed)
                {
                    isFirePressed = true;
                    Shoot();
                }
            }
            else if(isFirePressed)
            {
                isFirePressed = false;
            }
        }

        public override void Update()
        {
            if (IsActive)
            {
                if (sprite.position.X - sprite.pivot.X < 0)
                    sprite.position.X = sprite.pivot.X;
                else if (sprite.position.X + sprite.pivot.X >= Game.Win.Width)
                    sprite.position.X = Game.Win.Width - sprite.pivot.X;

                if (sprite.position.Y - sprite.pivot.Y < 0)
                    sprite.position.Y = sprite.pivot.Y;
                else if (sprite.position.Y + sprite.pivot.Y >= Game.Win.Height)
                    sprite.position.Y = Game.Win.Height - sprite.pivot.Y;
            }
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void OnDie()
        {
            Game.IsRunning = false;
        }

    }
}