using System.Collections.Generic;
using OpenTK;

namespace Mission_PrincessRescue
{
    class Dog : Actor
    {
        public List<Animation> Movements;
        public SoundEmitter soundEmitter;

        protected float speed;

        GridPathfinder pathFinder;

        public Dog(GridPathfinder pf, string textureName = "Dog") : base(textureName, DrawLayer.Playground)
        {
            RigidBody = new RigidBody(this);
            RigidBody.Type = RigidBodyType.Player;
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
            RigidBody.AddCollisionType(RigidBodyType.TileObj);
            RigidBody.AddCollisionType(RigidBodyType.Item);

            RigidBody.Friction = 0f;

            speed = 15;
            sprite.scale = new Vector2(0.25f);
            pathFinder = pf;
            soundEmitter = new SoundEmitter(this, "Dog_Sound");

            Movements = new List<Animation>();
            Movements.Add(new Animation(this, 1, 16, 16, 0, 0)); // Idle
            Movements.Add(new Animation(this, 4, 16, 16, 12, 0)); // Down
            Movements.Add(new Animation(this, 4, 16, 16, 12, 16)); // Up
            Movements.Add(new Animation(this, 4, 16, 16, 12, 32)); // Right
            Movements.Add(new Animation(this, 4, 16, 16, 12, 48)); // Left
            Movements[0].IsEnabled = true;
        }

        public virtual void StopAnimations()
        {
            foreach (Animation item in Movements)
            {
                item.IsEnabled = false;
                item.StopIsPlaying();
            }
        }

        public void HeadToPlayer()
        {
            Vector2 dir = pathFinder.NextPathDirection(Position - new Vector2(10));
            RigidBody.Velocity = speed * dir;
        }

        public override void Update()
        {
            if (IsActive)
            {
                base.Update();

                HeadToPlayer();

                if (RigidBody.Velocity == Vector2.Zero)
                {
                    StopAnimations();
                    Movements[0].IsEnabled = true;
                    Movements[0].Play();
                }

                if (RigidBody.Velocity.Y > speed - 5 && RigidBody.Velocity.Y <= speed)
                {
                    StopAnimations();
                    Movements[1].IsEnabled = true;
                    Movements[1].Play();
                }
                else if (RigidBody.Velocity.Y < -(speed - 5) && RigidBody.Velocity.Y >= -speed)
                {
                    StopAnimations();
                    Movements[2].IsEnabled = true;
                    Movements[2].Play();
                }

                if (RigidBody.Velocity.X > speed - 5 && RigidBody.Velocity.X <= speed)
                {
                    StopAnimations();
                    Movements[3].IsEnabled = true;
                    Movements[3].Play();
                }
                else if (RigidBody.Velocity.X < -(speed - 5) && RigidBody.Velocity.X >= -speed)
                {
                    StopAnimations();
                    Movements[4].IsEnabled = true;
                    Movements[4].Play();
                }
            }
        }

        public override void Draw()
        {
            if (IsActive)
            {
                for (int i = 0; i < Movements.Count; i++)
                {
                    if (Movements[i].IsEnabled)
                        sprite.DrawTexture(texture, (int)Movements[i].Offset.X, (int)Movements[i].Offset.Y, Movements[i].FrameWidth, Movements[i].FrameHeight);
                }
            }
        }

    }
}