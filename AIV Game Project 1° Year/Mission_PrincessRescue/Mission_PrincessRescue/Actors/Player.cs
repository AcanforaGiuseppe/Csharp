using System.Collections.Generic;
using OpenTK;

namespace Mission_PrincessRescue
{
    class Player : Actor
    {
        protected Controller controller;
        protected float speed;
        protected StateMachine fsm;

        private GridPathfinder pathFinder;

        public bool IsAlive;
        public Target target;
        public List<Animation> Movements;
        public int PlayerID { get; protected set; }
        public bool IsGrounded { get { return RigidBody.Velocity.Y == 0; } }

        public Player(Controller ctrl, int playerID, GridPathfinder pf, string textureName = "Hero") : base(textureName, DrawLayer.Playground)
        {
            RigidBody = new RigidBody(this);
            RigidBody.Type = RigidBodyType.Player;
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
            RigidBody.AddCollisionType(RigidBodyType.TileObj);
            RigidBody.AddCollisionType(RigidBodyType.Item);
            RigidBody.Friction = 0f;

            PlayerID = playerID;
            controller = ctrl;
            pathFinder = pf;

            IsAlive = true;
            speed = 40;
            sprite.scale = new Vector2(0.5f);
            energyBar = new ProgressBar("barFrame", "blueBar", new Vector2(0, 0));

            fsm = new StateMachine();
            fsm.AddState(StateEnum.Walk, new WalkState(this));
            fsm.AddState(StateEnum.Idle, new IdleState(this));
            fsm.SetFirstState(StateEnum.Idle);

            Movements = new List<Animation>();
            Movements.Add(new Animation(this, 1, 16, 16, 0, 0)); // Idle
            Movements.Add(new Animation(this, 4, 16, 16, 12, 0)); // Down
            Movements.Add(new Animation(this, 4, 16, 16, 12, 48)); // Up
            Movements.Add(new Animation(this, 4, 16, 16, 12, 16)); // Right
            Movements.Add(new Animation(this, 4, 16, 16, 12, 32)); // Left
            Movements[0].IsEnabled = true;
        }

        public virtual void Input()
        {
            if (Game.Window.MouseLeft)
            {
                target = new Target();
                target.Position = Game.Window.MousePosition;
                SetTarget(target);
            }

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

        public virtual void StopAnimations()
        {
            foreach (Animation item in Movements)
            {
                item.IsEnabled = false;
                item.StopIsPlaying();
            }
        }

        public void SetTarget(Target target)
        {
            this.target = target;
            fsm.GoTo(StateEnum.Walk);
        }

        public void ComputePlayerPoint()
        {
            if (target.Position.X > Game.Window.OrthoWidth || target.Position.X < 0 || target.Position.Y > Game.Window.OrthoHeight || target.Position.Y < 0)
                return;

            pathFinder.SelectPathFromTo(Position, target.Position);
        }

        public void HeadToPoint()
        {
            Vector2 dir = pathFinder.NextPathDirection(Position);

            if (dir.LengthSquared == 0)
                fsm.GoTo(StateEnum.Idle);

            RigidBody.Velocity = speed * dir;
        }

        public override void Update()
        {
            if (IsActive)
            {
                base.Update();
                fsm.Update();
                energyBar.Position = Position + new Vector2(-7, -12);
            }
        }

        public override void OnDie()
        {
            Game.CurrentScene.OnExit();
            Game.CurrentScene.GetReader();
            Game.GetGameReader();
            Game.CurrentScene.Start();
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