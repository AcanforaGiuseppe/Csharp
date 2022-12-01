using OpenTK;

namespace Mission_PrincessRescue
{
    abstract class Actor : GameObject
    {
        public float energy;
        public float maxEnergy;
        public ProgressBar energyBar;

        protected Actor(string textureName, DrawLayer layer = DrawLayer.Playground, float w = 0, float h = 0) : base(textureName, layer, w, h)
        {
            energy = maxEnergy = 100;
        }

        public override void OnCollide(Collision collisionInfo)
        {
            // Tile Collision
            OnWallCollides(collisionInfo);
        }

        public virtual void AddDamage(int dmg)
        {
            energy -= dmg;

            if (energy <= 0)
                OnDie();
            else if (energy < maxEnergy)
                energyBar.Scale(energy / maxEnergy);
        }

        public virtual void OnDie()
        { }

        protected virtual void OnWallCollides(Collision collisionInfo)
        {
            if (collisionInfo.Delta.X < collisionInfo.Delta.Y)
            {   // Horizontal Collision
                if (Position.X < collisionInfo.Collider.Position.X)
                {   // Left collision
                    collisionInfo.Delta.X = -collisionInfo.Delta.X;
                }
                else
                {
                    // Right Collision
                }
                X += collisionInfo.Delta.X;
                RigidBody.Velocity.X = 0;
            }
            else
            {   // Vertical Collision
                if (Position.Y < collisionInfo.Collider.Position.Y)
                {   // Upper collision
                    collisionInfo.Delta.Y = -collisionInfo.Delta.Y;
                }
                else
                {
                    // Lower Collision
                }
                if (collisionInfo.Delta.X >= Game.PixelsToUnits(6))
                {
                    RigidBody.Velocity.Y = 0;
                }
                Y += collisionInfo.Delta.Y;
            }
        }

        public void Borders()
        {
            // X
            if (sprite.position.X - sprite.pivot.X < 0)
                sprite.position.X = sprite.Width / 2;
            else if (sprite.position.X + sprite.pivot.X >= Game.Window.OrthoWidth)
                sprite.position.X = Game.Window.OrthoWidth - sprite.Width / 2;

            // Y
            if (sprite.position.Y - sprite.pivot.Y < 0)
                sprite.position.Y = sprite.Height / 2;
            else if (sprite.position.Y + sprite.pivot.Y >= Game.Window.OrthoHeight)
                sprite.position.Y = Game.Window.OrthoHeight - sprite.Height / 2;
        }

        public override void Update()
        { }

    }
}