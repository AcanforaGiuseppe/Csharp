using Aiv.Audio;
using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Player : Actor
    {
        int jumpPower;
        bool isSpacePressed;
        bool isRightMousePressed;
        public bool isCtrlPressed;
        bool isJumping;
        bool isRunning;
        bool isCrouching;
        int numAnimations;
        float counter = 1;

        public AudioSource grassEmitter;
        public AudioSource ouchEmitter;

        bool hasBeenHit;
        int redCounter;
        float redTimeCounter;
        float redTimeCounterMax = 0.05f;
        bool isDamagedColor;
        int life = Game.Lifes;

        public Colliders Colliders;

        public Player(Vector2 startPosition) : base("player_spritesheet", startPosition, 667, 1243)
        {
            grassEmitter = new AudioSource();
            grassEmitter.Volume = 0.05f;
            ouchEmitter = new AudioSource();
            ouchEmitter.Volume = 0.8f;
            isRunning = true;
            isDamagedColor = false;
            jumpPower = 775;
            IsActive = true;
            sprite.scale = new Vector2(0.15f);
            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height);

            numAnimations = 3;
            animations = new Animation[numAnimations];

            for (int i = 0; i < animations.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        // Run animation
                        animations[0] = new Animation(11, (int)sprite.Width, 20);
                        break;
                    case 1:
                        // Jump animation
                        animations[1] = new Animation(8, (int)sprite.Width, 10);
                        break;
                    case 2:
                        // Crouch animation
                        animations[2] = new Animation(20, (int)sprite.Width, 18);
                        //animations[2].Loop = false;
                        break;
                }
            }

            animations[0].Start();
            Colliders = new Colliders();
            SetStandingCollider();
        }

        void SetStandingCollider()
        {
            Colliders.ClearAll();
            Colliders.AddCollider(new CircleCollider(this, new Vector2(20, -40), 50));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(20, -90), 50));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(20, -140), 50));
            Colliders.SetActive(true);
        }

        void SetCrouchingCollider()
        {
            Colliders.ClearAll();
            Colliders.AddCollider(new CircleCollider(this, new Vector2(20, -40), 50));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(20, -90), 50));
            Colliders.AddCollider(new CircleCollider(this, new Vector2(20, -100), 50));
            Colliders.SetActive(true);
        }

        public void Input()
        {
            if (!Game.IsStart && !Game.EndGame)
            {
                // Jump
                if (!isCrouching)
                {
                    if (Game.Win.GetKey(KeyCode.Space))
                    {
                        if (!isSpacePressed)
                        {
                            if (!isJumping)
                            {
                                animations[0].Stop();
                                //animations[2].Stop();
                                animations[1].Start();
                                isSpacePressed = true;
                                isJumping = true;
                                rigidBody.IsGravityAffected = true;
                                rigidBody.Velocity.Y -= jumpPower;
                                isRunning = false;
                            }
                        }
                    }
                    else
                    {
                        isSpacePressed = false;
                    }
                }

                // Crouch
                if (!isJumping)
                {
                    if (Game.Win.GetKey(KeyCode.CtrlLeft))
                    {
                        if (!isCtrlPressed)
                        {// crouch
                            if (!isCrouching)
                            {
                                UpdateMgr.SetSpeedAll(Game.CrouchingGlobalSpeed);
                                counter = (1f / 27) * 20;
                                animations[0].Stop();
                                animations[2].Restart();
                                isCtrlPressed = true;
                                isCrouching = true;
                                SetCrouchingCollider();
                            }
                        }
                    }
                    else
                    {
                        isCtrlPressed = false;
                    }
                } 
            }
            else if (Game.IsStart)
            { //INPUT DEL MOUSE CLICK
                if (Game.Win.MouseLeft)
                {
                    if (!isRightMousePressed)
                    {
                        isRightMousePressed = true;
                        //Box Start
                        if (!FontMgr.IsLore1OnScreen)
                        {
                            if (Game.Win.MouseY <= 667 && Game.Win.MouseY >= 583 && Game.Win.MouseX >= 252 && Game.Win.MouseX <= 428 && FontMgr.HasStartAppeared && FontMgr.HasExitAppeared)
                            {
                                FontMgr.RemoveStartScreen();
                                FontMgr.Lore1();
                            }
                            //Box Exit 
                            if (Game.Win.MouseY <= 667 && Game.Win.MouseY >= 583 && Game.Win.MouseX >= 814 && Game.Win.MouseX <= 966 && FontMgr.HasExitAppeared && FontMgr.HasStartAppeared)
                            {
                                Game.Exit = true;
                            } 
                        }
                    }
                }
                else
                {
                    isRightMousePressed = false;
                }
            }
        }

        public override void Update()
        {
            if (isRunning)
            {
                AudioMgr.StreamFx("RunningGrass_2", grassEmitter);
            }
            else
            {
                grassEmitter.Stop();
            }

            if (isCrouching)
            {
                counter -= Game.DeltaTime;
                if (counter <= 0)
                {
                    animations[0].Start();
                    animations[2].Stop();
                    isCrouching = false;
                    SetStandingCollider();
                    UpdateMgr.SetSpeedAll(Game.GlobalSpeed);
                }
            }

            if (isJumping)
            {
                if (Position == new Vector2(Position.X, TerrainMgr.YPos))
                {
                    animations[0].Start();
                    animations[1].Stop();
                    isJumping = false;
                    isRunning = true;
                }
            }

            if (hasBeenHit)
            {
                redTimeCounter -= Game.DeltaTime;
                if (redTimeCounter <= 0)
                {
                    redCounter++;
                    redTimeCounter = redTimeCounterMax;
                    isDamagedColor = !isDamagedColor;
                    if (redCounter >= 14)
                    {
                        hasBeenHit = false;
                    }
                } 
            }

            foreach (Animation a in animations)
            {
                a.Update();
            }

            if (Position.Y >= TerrainMgr.YPos)
            {
                rigidBody.IsGravityAffected = false;
                rigidBody.Velocity = Vector2.Zero;
                Position = new Vector2(Position.X, TerrainMgr.YPos);
            }

            CheckCollision();
        }

        public void CheckCollision()
        {
            if (ObstacleMgr.CheckCollision(Colliders))
            {
                //CODICE VERO PER QUANDO VIENI COLPITO
                if (!hasBeenHit)
                {
                    AudioMgr.PlayFx("Ouch_3", ouchEmitter);
                    if (!FontMgr.LastLife)
                    {
                        hasBeenHit = true;
                        redCounter = 0;
                        redTimeCounter = 0;
                        life--;
                        switch (life)
                        {
                            case 3:
                                FontMgr.Damage1();
                                break;
                            case 2:
                                FontMgr.Damage2();
                                break;
                            case 1:
                                FontMgr.Damage3();
                                break;
                            case 0:
                                FontMgr.Damage4();
                                break;
                        } 
                    }
                }
                if (life <= 0)
                {
                    //Reset del progresso
                    life = Game.Lifes;
                    ObstacleMgr.ObstaclePassed = 0;
                    ObstacleMgr.HeartsAquired = 0;
                    ObstacleMgr.ClearAllHearts();
                    Game.bg.Reset();
                }
            }
        }

        public override void Draw()
        {
            int yOffset = (int)sprite.Height;

            if (!isDamagedColor)
            {
                sprite.SetAdditiveTint(0, 0, 0, 0);
            }
            else
            {
                sprite.SetAdditiveTint(255, 0, 0, 0);
            }

            if (IsActive)
            {
                if (animations[0].IsPlaying)
                {
                    sprite.DrawTexture(texture, animations[0].GetCurrentFrameOffset(), 0, (int)sprite.Width, (int)sprite.Height);
                }
                else if (animations[1].IsPlaying)
                {
                    sprite.DrawTexture(texture, animations[1].GetCurrentFrameOffset(), yOffset, (int)sprite.Width, (int)sprite.Height);
                }
                else if (animations[2].IsPlaying)
                {
                    sprite.DrawTexture(texture, animations[2].GetCurrentFrameOffset(), yOffset * 2, (int)sprite.Width, (int)sprite.Height);
                }
            }
        }
    }
}

