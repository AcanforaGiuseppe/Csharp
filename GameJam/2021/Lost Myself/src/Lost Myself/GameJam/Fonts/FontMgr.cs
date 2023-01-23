using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    enum FontType
    {
        START,
        EXIT,
        TITLE,
        LORE1,
        LORE2,
        DAMAGE1,
        DAMAGE2,
        DAMAGE3,
        DAMAGE4,
        END,
        THANKYOU,
        TUTORIALJUMP,
        TUTORIALCROUCH,
        TUTORIALEXIT,
        LAST
    }
    static class FontMgr
    {
        static Queue<Font>[] fonts;
        static List<Font> activeFonts;
        static float counter;

        public static bool HasStartAppeared;
        public static bool HasExitAppeared;
        public static bool HasTitleAppeared;
        public static bool HasTutorialAppeared;
        public static bool IsLore1OnScreen;
        public static bool IsLore2OnScreen;
        public static bool HasEndAppeared;
        public static bool HasCreditsAppeared;
        public static bool LastLife;
        public static bool AreDamageFontsDissolving;

        public static void Init()
        {
            int queueSize = 2;
            activeFonts = new List<Font>();
            fonts = new Queue<Font>[(int)FontType.LAST];
            for (int i = 0; i < fonts.Length; i++)
            {
                fonts[i] = new Queue<Font>(queueSize);
                switch ((FontType)i)
                {
                    case FontType.START:
                        for (int j = 0; j < queueSize; j++)
                        {
                            fonts[i].Enqueue(new Start());
                        }
                        break;
                    case FontType.EXIT:
                        for (int j = 0; j < queueSize; j++)
                        {
                            fonts[i].Enqueue(new Exit());
                        }
                        break;
                    case FontType.TITLE:
                        for (int j = 0; j < queueSize; j++)
                        {
                            fonts[i].Enqueue(new Title());
                        }
                        break;
                    case FontType.LORE1:
                        for (int j = 0; j < queueSize; j++)
                        {
                            fonts[i].Enqueue(new Lore1());
                        }
                        break;
                    case FontType.LORE2:
                        for (int j = 0; j < queueSize; j++)
                        {
                            fonts[i].Enqueue(new Lore2());
                        }
                        break;
                    case FontType.DAMAGE1:
                        for (int j = 0; j < queueSize; j++)
                        {
                            fonts[i].Enqueue(new Damage1());
                        }
                        break;
                    case FontType.DAMAGE2:
                        for (int j = 0; j < queueSize; j++)
                        {
                            fonts[i].Enqueue(new Damage2());
                        }
                        break;
                    case FontType.DAMAGE3:
                        for (int j = 0; j < queueSize; j++)
                        {
                            fonts[i].Enqueue(new Damage3());
                        }
                        break;
                    case FontType.DAMAGE4:
                        for (int j = 0; j < queueSize; j++)
                        {
                            fonts[i].Enqueue(new Damage4());
                        }
                        break;
                    case FontType.END:
                        for (int j = 0; j < queueSize; j++)
                        {
                            fonts[i].Enqueue(new End());
                        }
                        break;
                    case FontType.THANKYOU:
                        for (int j = 0; j < queueSize; j++)
                        {
                            fonts[i].Enqueue(new ThankYou());
                        }
                        break;
                    case FontType.TUTORIALJUMP:
                        for (int j = 0; j < queueSize; j++)
                        {
                            fonts[i].Enqueue(new TutorialJump());
                        }
                        break;
                    case FontType.TUTORIALEXIT:
                        for (int j = 0; j < queueSize; j++)
                        {
                            fonts[i].Enqueue(new TutorialExit());
                        }
                        break;
                    case FontType.TUTORIALCROUCH:
                        for (int j = 0; j < queueSize; j++)
                        {
                            fonts[i].Enqueue(new TutorialCrouch());
                        }
                        break;
                    default:
                        break;
                }
            }
            counter = 0.5f;
        }

        public static void AddFont(Font f, int method, Vector2 pos, Vector2 endPos, Vector2 velocity, bool isTimed)
        {
            f.IsActive = true;
            activeFonts.Add(f);
            f.Spawn(method, pos, endPos, velocity, isTimed);
        }

        public static void RestoreFont(Font f)
        {
            f.IsActive = false;
            activeFonts.Remove(f);
            fonts[(int)f.Type].Enqueue(f);
        }

        public static void RemoveStartScreen()
        {
            //Fai scomparire start e exit
            for (int i = activeFonts.Count - 1; i >= 0; i--)
            {
                RestoreFont(activeFonts[i]);
            }
        }

        public static void DissolveDamageTexts()
        {
            for (int i = activeFonts.Count - 1; i >= 0; i--)
            {
                ((DamageFont)activeFonts[i]).Dissolve();
            }
        }

        public static void Lore1()
        {
            IsLore1OnScreen = true;
            Font lore1 = fonts[(int)FontType.LORE1].Dequeue();
            ((Lore1)lore1).IsAppearing = true;
            AddFont(lore1, 1, new Vector2(635, 625), new Vector2(635, 625), new Vector2(0, 0), true);
            counter = 1f;
        }

        public static void Lore2()
        {
            IsLore2OnScreen = true;
            Font lore2 = fonts[(int)FontType.LORE2].Dequeue();
            ((Lore2)lore2).IsWaitingAppearing = true;
            AddFont(lore2, 1, new Vector2(640, 625), new Vector2(640, 625), new Vector2(0, 0), true);
            counter = 4f;
        }

        public static void Damage1()
        {
            AddFont(fonts[(int)FontType.DAMAGE1].Dequeue(), 1, new Vector2(420, 600), new Vector2(340, 600), Vector2.Zero, false);
        }

        public static void Damage2()
        {
            AddFont(fonts[(int)FontType.DAMAGE2].Dequeue(), 1, new Vector2(640, 600), new Vector2(640, 600), Vector2.Zero, false);
        }

        public static void Damage3()
        {
            AddFont(fonts[(int)FontType.DAMAGE3].Dequeue(), 1, new Vector2(830, 600), new Vector2(840, 600), Vector2.Zero, false);
        }

        public static void Damage4()
        {
            AddFont(fonts[(int)FontType.DAMAGE4].Dequeue(), 1, new Vector2(650, 657), new Vector2(650, 625), Vector2.Zero, false);
            LastLife = true;
            counter = 1.15f;
        }

        public static void Update()
        {
            //SCRIPT PER INIZIO GIOCO
            if (Game.IsStart)
            {
                counter -= Game.DeltaTime;
                if (counter <= 0)
                {
                    if (!HasTitleAppeared)
                    {
                        AddFont(fonts[(int)FontType.TITLE].Dequeue(), 0, new Vector2(625, -100), new Vector2(625, 220), new Vector2(0, 100), false);
                        HasTitleAppeared = true;
                        counter = 2f;
                    }
                    else if (!HasStartAppeared)
                    {
                        AddFont(fonts[(int)FontType.START].Dequeue(), 1, new Vector2(340, 625), new Vector2(340, 625), Vector2.Zero, false);
                        HasStartAppeared = true;
                        //BoundBox bs = new BoundBox(new Vector2(340, 625), 176, 84);
                        counter = 1f;
                    }
                    else if (!HasExitAppeared)
                    {
                        AddFont(fonts[(int)FontType.EXIT].Dequeue(), 1, new Vector2(890, 625), new Vector2(890, 625), Vector2.Zero, false);
                        HasExitAppeared = true;
                        //BoundBox be = new BoundBox(new Vector2(890, 625), 152, 84);
                        counter = 1f;   
                    }
                }
            }

            if (IsLore1OnScreen)
            {
                if (!HasTutorialAppeared)
                {
                    counter -= Game.DeltaTime;
                    if (counter <= 0)
                    {
                        AddFont(fonts[(int)FontType.TUTORIALJUMP].Dequeue(), 0, new Vector2(640, -240), new Vector2(725, 800), new Vector2(0, 100), false);
                        AddFont(fonts[(int)FontType.TUTORIALCROUCH].Dequeue(), 0, new Vector2(640, -100), new Vector2(725, 800), new Vector2(0, 100), false);
                        HasTutorialAppeared = true;
                    } 
                }
            }

            //SCRIPTS PER QUANDO VIENI COLPITO
            if (LastLife)
            {
                counter -= Game.DeltaTime;
                if (counter <= 0)
                {
                    if (!AreDamageFontsDissolving)
                    {
                        AreDamageFontsDissolving = true;
                        DissolveDamageTexts();
                    }
                }
            }

            //SCRIPT PER FINE GIOCO
            if (Game.EndGame)
            {
                if (IsLore2OnScreen)
                {
                    if (!HasEndAppeared)
                    {
                        counter -= Game.DeltaTime;
                        if (counter <= 0)
                        {
                            AddFont(fonts[(int)FontType.END].Dequeue(), 0, new Vector2(625, -100), new Vector2(625, 900), new Vector2(0, 100), false);
                            HasEndAppeared = true;
                            counter = 4f;
                        } 
                    }
                }
                else if (!HasCreditsAppeared)
                {
                    //Mettere scritta Thanks
                    AddFont(fonts[(int)FontType.THANKYOU].Dequeue(), 0, new Vector2(625, -100), new Vector2(625, 220), new Vector2(0, 100), false);
                    //Mettere scritta Esc for exit
                    AddFont(fonts[(int)FontType.TUTORIALEXIT].Dequeue(), 1, new Vector2(640, 625), new Vector2(640, 625), new Vector2(0, 100), false);
                    HasCreditsAppeared = true;
                }
            }
        }
    }
}
