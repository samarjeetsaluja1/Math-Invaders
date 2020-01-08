using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Interactive_Text_Based_RPG
{
    class Entity
    {
        //Class Variables
        public int HP;
        public int AMMO = Program.ammo;
        public int MAX_HP = 100;
        public int SCORE = 0;
        public int LVL;
        public string NAME;

        //Constructor
        public Entity(string name, int hp, int level)
        {
            NAME = name;
            HP = hp;
            LVL = level;
        }

        //Displays Player Information
        public void DrawStats()
        {
            int a = 10;
            Console.WriteLine("________________________________________________________________________________________________________________________");
            Console.SetCursorPosition(a, 1);
            Console.Write("Name: {0}", NAME);
            Console.SetCursorPosition(a + 10 + NAME.Length, 1);
            Console.Write("AMMO: {0}", AMMO);
            Console.SetCursorPosition(80, 1);
            Console.WriteLine("                  ");
            Console.SetCursorPosition(80, 1);
            Console.WriteLine("HP: {0}/{1}", HP, MAX_HP);
            Console.SetCursorPosition(95, 1);
            Console.WriteLine("SCORE: {0}", SCORE);
            Console.WriteLine("________________________________________________________________________________________________________________________");
        }

        public void Shoot() { AMMO--; }
        public void Reload() { AMMO = Program.ammo; }

    }

    //Inheritance of Enemy Class
    class Enemy : Entity
    {
        //Class Variables
        public float x = 116 / 5;
        public int y;
        static int dir = -1;
        public bool move = true;
        public static int y_val = 4;
        public int y_diff = 0;


        //Constructor
        public Enemy(string name, int hp, int level)
            : base(name, hp, level)
        {

        }

        //Creates Instance of Enemy Class on Screen
        public void Draw()
        {
            // Re-seed the RNG every time Draw() is called using Guid's hashcode
            Random rng = new Random(Guid.NewGuid().GetHashCode()); //.........................................................................Dont Completely Understand
            if (NAME == "e1") { x = 22.0f; } else if (NAME == "e2") { x = 44.0f; } else if (NAME == "e3") { x = 66.0f; } else if (NAME == "e4") { x = 88.0f; } else if (NAME == "e5") { x = 110.0f; }
            y = y_val;
            //Physically Draws Enemy
            Console.SetCursorPosition(Convert.ToInt32(x), y);
            Console.WriteLine("@ @ @");
            Console.SetCursorPosition(Convert.ToInt32(x + 1), y + 1);
            Console.WriteLine("@:@");

        }


        //Deletes Previous Drawing of Instance of Enemy Class
        public void Clear()
        {
            Console.SetCursorPosition(Convert.ToInt32(x), y);
            Console.WriteLine("      ");
            Console.SetCursorPosition(Convert.ToInt32(x), y - 1);
            Console.WriteLine("      ");
            Console.SetCursorPosition(Convert.ToInt32(x + 1), y + 1);
            Console.WriteLine("   ");
        }

        //Moves Each Instance of Enemy Class
        public void Move()
        {
            Clear();
            if (dir == -1)
            {
                if (NAME == "e1" && x >= 3 && dir == -1) { x -= 1; y = y_val - y_diff; }
                if (NAME == "e2" && x >= 25 && dir == -1) { x -= 1; y = y_val - y_diff; }
                if (NAME == "e3" && x >= 47 && dir == -1) { x -= 1; y = y_val - y_diff; }
                if (NAME == "e4" && x >= 69 && dir == -1) { x -= 1; y = y_val - y_diff; }
                if (NAME == "e5" && x >= 91 && dir == -1) { x -= 1; y = y_val - y_diff; }
            }

            if (NAME == "e1" && x == 3) { }
            if (NAME == "e2" && x == 25) { }
            if (NAME == "e3" && x == 47) { }
            if (NAME == "e4" && x == 69) { }
            if (NAME == "e5" && Convert.ToInt32(x) == 91) { dir = 1; y_val += 1; }

            if (dir == 1)
            {
                if (NAME == "e1" && x <= 25 && dir == 1) { x += 1; y = y_val - y_diff; }
                if (NAME == "e2" && x <= 47 && dir == 1) { x += 1; y = y_val - y_diff; }
                if (NAME == "e3" && x <= 69 && dir == 1) { x += 1; y = y_val - y_diff; }
                if (NAME == "e4" && x <= 91 && dir == 1) { x += 1; y = y_val - y_diff; }
                if (NAME == "e5" && x <= 113 && dir == 1) { x += 1; y = y_val - y_diff; }
            }

            if (NAME == "e1" && x == 25) { }
            if (NAME == "e2" && x == 47) { }
            if (NAME == "e3" && x == 69) { }
            if (NAME == "e4" && x == 91) { }
            if (NAME == "e5" && Convert.ToInt32(x) == 113) { dir = -1; y_val += 1; }

            Console.SetCursorPosition(Convert.ToInt32(x), y);
            Console.WriteLine("@ @ @");
            Console.SetCursorPosition(Convert.ToInt32(x + 1), y + 1);
            Console.WriteLine("@:@");
        }

    }



    class Program
    {
        public static int ammo = 5;
        public static int speed = 32;
        public static string LvL;
        static void Main()
        {
        //Return Location
        Main:

            //Variables
            string name;
            string textfilepath = "highscores.txt";
            string textfilepath2 = "highscores2.txt";
            int t_scores;
            int t_scores2;
            int Move_LR = 55;
            int Move_UD = 23;
            bool GameOver = false;
            bool GameStart = false;
            bool GameLvl = false;
            bool paused = false;
            bool instructions = false;
            bool scores = false;
            bool arrow_h = false;
            bool arrow_i = false;
            int W_WIDTH = Console.WindowWidth;
            int W_HEIGHT = Console.WindowHeight;
            DirectoryInfo myDataDir = new DirectoryInfo(".");
            //Used Later for Difficulty Progresson
            int counter = 0;

            //Creates Easy Mode High Score Dictionary
            string[] scores_1 = new string[6];
            Dictionary<int, string> highScores = new Dictionary<int, string>();

            //Creates Hard Mode High Score Dictionary
            string[] scores_2 = new string[6];
            Dictionary<int, string> highScores2 = new Dictionary<int, string>();

            //Checks if High Score Files Exists
            if (File.Exists(textfilepath2) == false)
            {
                scores_2[0] = "1. Samar : 10"; File.WriteAllLines(textfilepath2, scores_2);
            }
            if (File.Exists(textfilepath) == false)
            {
                scores_1[0] = "1. Samar : 10"; File.WriteAllLines(textfilepath, scores_1);
            }

        repeat:
            // Get all txt files 
            FileInfo[] txtFiles = myDataDir.GetFiles("*.txt",
                SearchOption.AllDirectories);

            //Adding a High Score to File if File is Empty - For User to Beat
            foreach (FileInfo file in txtFiles)
            {
                if (file.Length < 9 && file.Length > 0) { File.Create(file.Name).Close(); goto repeat; }
                if (file.Length == 0) { File.WriteAllText(file.Name, "1. Samar : 10"); }
            }

            //Reads from each highscore file, adding the scores to each dictionary
            foreach (var i in File.ReadAllLines(textfilepath).Where(arg => !string.IsNullOrWhiteSpace(arg)))
            {
                highScores[Convert.ToInt32(i.Split(':')[1])] = i.Split(':')[0].Substring(1, i.Split(':')[0].Length - 1).Replace(".", string.Empty);
            }

            foreach (var i in File.ReadAllLines(textfilepath2).Where(arg => !string.IsNullOrWhiteSpace(arg)))
            {
                highScores2[Convert.ToInt32(i.Split(':')[1])] = i.Split(':')[0].Substring(1, i.Split(':')[0].Length - 1).Replace(".", string.Empty);
            }

            //Soundtrack
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = "Soundtrack.wav";
            player.PlayLooping();

        //Main Menu
        Menu:
            //Prevents Window From Resizing
            if (Console.WindowHeight != W_HEIGHT || Console.WindowWidth != W_WIDTH)
            {
                Console.SetWindowSize(W_WIDTH, W_HEIGHT);
            }
            Console.SetCursorPosition(1, 1);
            Console.WriteLine(@"                    _____          __  .__      .___                         .___                   
                    /     \ _____ _/  |_|  |__   |   | _______  _______     __| _/___________  ______
                   /  \ /  \\__  \\   __\  |  \  |   |/    \  \/ /\__  \   / __ |/ __ \_  __ \/  ___/
                  /    Y    \/ __ \|  | |   Y  \ |   |   |  \   /  / __ \_/ /_/ \  ___/|  | \/\___ \ 
                  \____|__  (____  /__| |___|  / |___|___|  /\_/  (____  /\____ |\___  >__|  /____  >
                          \/     \/          \/           \/           \/      \/    \/           \/ ");
            if (arrow_h == false && arrow_i == false)
            {
                Console.SetCursorPosition(51, 10);
                Console.WriteLine("Start Game <--");
                Console.SetCursorPosition(51, 15);
                Console.WriteLine("High Scores");
                Console.SetCursorPosition(51, 20);
                Console.WriteLine("Instructions");
                Console.SetCursorPosition(55, 25);
                Console.WriteLine("Exit");
                Console.SetCursorPosition(51, 10);
            }
            else if (arrow_h == true && arrow_i == false)
            {
                Console.SetCursorPosition(51, 10);
                Console.WriteLine("Start Game");
                Console.SetCursorPosition(51, 15);
                Console.WriteLine("High Scores <--");
                Console.SetCursorPosition(51, 20);
                Console.WriteLine("Instructions");
                Console.SetCursorPosition(55, 25);
                Console.WriteLine("Exit");
                Console.SetCursorPosition(51, 15);
            }
            else if (arrow_h == false && arrow_i == true)
            {
                Console.SetCursorPosition(51, 10);
                Console.WriteLine("Start Game");
                Console.SetCursorPosition(51, 15);
                Console.WriteLine("High Scores");
                Console.SetCursorPosition(51, 20);
                Console.WriteLine("Instructions <--");
                Console.SetCursorPosition(55, 25);
                Console.WriteLine("Exit");
                Console.SetCursorPosition(51, 20);
            }
            while (GameStart == false && instructions == false && scores == false)
            {
                //Prevents Window From Resizing
                if (Console.WindowHeight != W_HEIGHT || Console.WindowWidth != W_WIDTH)
                {
                    Console.SetWindowSize(W_WIDTH, W_HEIGHT);
                }
                //Input for Main Menu
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo KeyInfo;
                    KeyInfo = Console.ReadKey(true);
                    switch (KeyInfo.Key)
                    {
                        //Changing What The Arrow is Pointing To
                        case ConsoleKey.DownArrow:
                            if (Console.CursorTop == 10)
                            {
                                Console.SetCursorPosition(51, 10);
                                Console.WriteLine("Start Game      ");
                                Console.SetCursorPosition(51, 15);
                                Console.WriteLine("High Scores <--");
                                Console.SetCursorPosition(51, 15);
                            }
                            else if (Console.CursorTop == 15)
                            {
                                Console.SetCursorPosition(51, 15);
                                Console.WriteLine("High Scores      ");
                                Console.SetCursorPosition(51, 20);
                                Console.WriteLine("Instructions <--");
                                Console.SetCursorPosition(51, 20);

                            }
                            else if (Console.CursorTop == 20)
                            {
                                Console.SetCursorPosition(51, 20);
                                Console.WriteLine("Instructions      ");
                                Console.SetCursorPosition(55, 25);
                                Console.WriteLine("Exit <--");
                                Console.SetCursorPosition(55, 25);
                            }
                            break;

                        case ConsoleKey.UpArrow:
                            if (Console.CursorTop == 15)
                            {
                                Console.SetCursorPosition(51, 15);
                                Console.WriteLine("High Scores      ");
                                Console.SetCursorPosition(51, 10);
                                Console.WriteLine("Start Game <--");
                                Console.SetCursorPosition(51, 10);
                            }
                            else if (Console.CursorTop == 20)
                            {
                                Console.SetCursorPosition(51, 20);
                                Console.WriteLine("Instructions      ");
                                Console.SetCursorPosition(51, 15);
                                Console.WriteLine("High Scores <--");
                                Console.SetCursorPosition(51, 15);
                            }
                            else if (Console.CursorTop == 25)
                            {
                                Console.SetCursorPosition(55, 25);
                                Console.WriteLine("Exit      ");
                                Console.SetCursorPosition(51, 20);
                                Console.WriteLine("Instructions <--");
                                Console.SetCursorPosition(51, 20);
                            }
                            break;
                        //Checking for Selection
                        case ConsoleKey.Enter:
                            if (Console.CursorTop == 10)
                            {
                                Console.Clear();
                                GameStart = true;
                            }
                            else if (Console.CursorTop == 15)
                            {
                                Console.Clear();
                                scores = true;
                                arrow_h = true;
                                arrow_i = false;
                            }
                            else if (Console.CursorTop == 20)
                            {
                                Console.Clear();
                                instructions = true;
                                arrow_i = true;
                                arrow_h = false;
                            }
                            else if (Console.CursorTop == 25)
                            {
                                Environment.Exit(-1);
                            }
                            break;
                    }
                }
            }

            if (instructions)
            {
                Console.SetCursorPosition(22, 0);//--------------------------------------------------------------------------------------Working on Instruction Screen Currently
                Console.WriteLine(@" .___                 __                        __  .__                      
                       |   | ____   _______/  |________ __ __   _____/  |_|__| ____   ____   ______
                       |   |/    \ /  ___/\   __\_  __ \  |  \_/ ___\   __\  |/  _ \ /    \ /  ___/
                       |   |   |  \\___ \  |  |  |  | \/  |  /\  \___|  | |  (  <_> )   |  \\___ \ 
                       |___|___|  /____  > |__|  |__|  |____/  \___  >__| |__|\____/|___|  /____  >
                                \/     \/                          \/                    \/     \/ ");
                //Instructions
                Console.SetCursorPosition(2, 8);
                Console.Write("Welcome to");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(" Math Invaders");
                Console.ResetColor();
                Console.WriteLine(@", where you prevent aliens invading your planet, while improving those ever so important 
  mental math skills. Not only learn about math, but learn about numerous other matters such as Newton's Law of 
  Innertia firsthand, by observing the workings of the physics engine and also learn the layout of the keyboard, 
  improving your hand-eye coordination for everyday tasks.");
                Console.ResetColor();

                //Game Controls
                Console.SetCursorPosition(2, 13);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Game Controls");
                Console.ResetColor();
                Console.SetCursorPosition(2, 14);
                Console.WriteLine("Left Arrow Key: Move Left");
                Console.SetCursorPosition(2, 15);
                Console.WriteLine("Right Arrow Key: Move Right");
                Console.SetCursorPosition(2, 16);
                Console.WriteLine("Spacebar: Shoot");
                Console.SetCursorPosition(2, 17);
                Console.WriteLine("Escape: Pause Game");
                Console.SetCursorPosition(2, 18);
                Console.WriteLine("Reload: Use the Number Keys to Correctly Answer the Math Question.");

                //Minimum System Requirements
                Console.SetCursorPosition(2, 20);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Minimum System Requirements");
                Console.ResetColor();
                Console.SetCursorPosition(2, 21);
                Console.WriteLine("Operating System: Windows 7 (x86)/(x64) or Newer ");
                Console.SetCursorPosition(2, 22);
                Console.WriteLine("CPU: 1 GHz or faster (Minimum Recommendation: Intel Core 2 Duo) ");
                Console.SetCursorPosition(2, 23);
                Console.WriteLine("GPU: DirectX 9 Graphics Device with WDDM 1.0 or Higher Driver ");
                Console.SetCursorPosition(2, 24);
                Console.WriteLine("RAM: 512 MB or More ");
                Console.SetCursorPosition(2, 25);
                Console.WriteLine("Storage: 20 GB or More ");


                //Return to Main Screen
                ConsoleKeyInfo KeyInfo;
                do
                {
                    KeyInfo = Console.ReadKey(true);
                    if (KeyInfo.Key == ConsoleKey.Escape)
                    {
                        Console.Clear();
                        instructions = false;
                        goto Menu;
                    }
                } while (KeyInfo.Key != ConsoleKey.Escape);
            }

            if (scores)
            {
                Console.SetCursorPosition(23, 0);//--------------------------------------------------------------------------------------Working on High Scores Screen Currently
                Console.WriteLine(@" ___ ___ .__       .__        _________                                  
                       /   |   \|__| ____ |  |__    /   _____/ ____  ___________   ____   ______
                      /    ~    \  |/ ___\|  |  \   \_____  \_/ ___\/  _ \_  __ \_/ __ \ /  ___/
                      \    Y    /  / /_/  >   Y  \  /        \  \__(  <_> )  | \/\  ___/ \___ \ 
                       \___|_  /|__\___  /|___|  / /_______  /\___  >____/|__|    \___  >____  >
                             \/   /_____/      \/          \/     \/                  \/     \/ ");

                //--Easy Mode High Scores
                Console.SetCursorPosition(2, 7);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Easy Mode High Scores");
                Console.ResetColor();

                //Reads from Dictionary to List
                var k = highScores.Keys.ToList();

                //Displays High Scores in Order
                int s = 0;
                foreach (float i in k)
                {
                    Console.SetCursorPosition(2, 8 + s);
                    Console.WriteLine(Convert.ToString(s + 1) + ". " + highScores[Convert.ToInt32(i)].Trim() + " : " + Convert.ToString(Math.Floor(i / 10) * 10));
                    s += 1;
                }

                //--Hard Mode High Scores
                Console.SetCursorPosition(2, 14);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hard Mode High Scores");
                Console.ResetColor();

                //Reads from Dictionary to List
                var l = highScores2.Keys.ToList();

                //Displays High Scores in Order
                int t = 0;
                foreach (float i in l)
                {
                    Console.SetCursorPosition(2, 15 + t);
                    Console.WriteLine(Convert.ToString(t + 1) + ". " + highScores2[Convert.ToInt32(i)].Trim() + " : " + Convert.ToString(Math.Floor(i / 10) * 10));
                    t += 1;
                }

                //Return to Main Screen
                ConsoleKeyInfo KeyInfo;
                do
                {
                    KeyInfo = Console.ReadKey(true);
                    if (KeyInfo.Key == ConsoleKey.Escape)
                    {
                        Console.Clear();
                        scores = false;
                        goto Menu;
                    }
                } while (KeyInfo.Key != ConsoleKey.Escape);
            }

            //Level Selection
            if (instructions == false && scores == false)
            {
                Console.SetCursorPosition(51, 10);
                Console.WriteLine("Easy <--");
                Console.SetCursorPosition(51, 15);
                Console.WriteLine("Hard");
                Console.SetCursorPosition(51, 10);
            }
            while (GameLvl == false && GameStart == true)
            {
                if (Console.WindowHeight != W_HEIGHT || Console.WindowWidth != W_WIDTH)
                {
                    Console.SetWindowSize(W_WIDTH, W_HEIGHT);
                }
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo KeyInfo;
                    KeyInfo = Console.ReadKey(true);
                    switch (KeyInfo.Key)
                    {
                        //Moving the Arrow
                        case ConsoleKey.DownArrow:
                            if (Console.CursorTop == 10)
                            {
                                Console.SetCursorPosition(51, 10);
                                Console.WriteLine("Easy      ");
                                Console.SetCursorPosition(51, 15);
                                Console.WriteLine("Hard <--");
                                Console.SetCursorPosition(51, 15);
                            }
                            break;

                        case ConsoleKey.UpArrow:
                            if (Console.CursorTop == 15)
                            {
                                Console.SetCursorPosition(51, 15);
                                Console.WriteLine("Hard      ");
                                Console.SetCursorPosition(51, 10);
                                Console.WriteLine("Easy <--");
                                Console.SetCursorPosition(51, 10);

                            }
                            break;
                        //Checking for Selection
                        case ConsoleKey.Enter:
                            if (Console.CursorTop == 10)
                            {
                                LvL = "easy";
                                ammo = 5;
                                counter = 0;
                                Enemy.y_val = 4;
                                Console.Clear();
                                GameLvl = true;
                                speed = 32;
                            }
                            else if (Console.CursorTop == 15)
                            {
                                LvL = "hard";
                                ammo = 3;
                                counter = 5;
                                Enemy.y_val = 4;
                                Console.Clear();
                                GameLvl = true;
                                speed = 16;
                            }
                            break;
                        case ConsoleKey.Escape:
                            Console.Clear();
                            GameStart = false;
                            goto Menu;
                    }
                }
            }
            //Character Creation
            do
            {
                if (instructions == false && scores == false)
                {
                    Console.SetCursorPosition(45, 12);
                    Console.Write("Name: ");
                }
                name = Console.ReadLine();

                //Random Checks that Would Cause Errors if Not Done So
                if (name.Length >= 50)
                {
                    Console.Clear();
                    Console.SetCursorPosition(33, 12);
                    Console.WriteLine("Your name is too long! Press any key to continue...");
                    Console.ReadKey();
                }
                if (string.IsNullOrWhiteSpace(name)) { name = "Unknown"; }
                if (name.Contains(":"))
                {
                    Console.SetCursorPosition(33, 12);
                    Console.WriteLine("Your name may not include a ':'. Sorry.");
                    Console.SetCursorPosition(33, 13);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                if (name.ToLower().Contains("dunne") || name.ToLower().Contains("ryan")) { name = "Ryan David Dunne"; }
                Console.Clear();
            } while (name.Length >= 50 || name.Contains(":"));
            Console.Clear();
            Console.SetWindowSize(W_WIDTH, W_HEIGHT);
            Console.Clear();
            Entity p1 = new Entity(name, 100, 1);
            Console.SetCursorPosition(45, 12);

            //Enemy Creation
            Enemy e1 = new Enemy("e1", 20, 1);
            Enemy e2 = new Enemy("e2", 20, 1);
            Enemy e3 = new Enemy("e3", 20, 1);
            Enemy e4 = new Enemy("e4", 20, 1);
            Enemy e5 = new Enemy("e5", 20, 1);
            if (paused == false)
            {
                e1.Draw();
                e2.Draw();
                e3.Draw();
                e4.Draw();
                e5.Draw();
            }
            //Game Loop
            do
            {
                if (Console.WindowHeight != W_HEIGHT || Console.WindowWidth != W_WIDTH)
                {
                    Console.Clear();
                    Console.SetWindowSize(W_WIDTH, W_HEIGHT);
                    Console.Clear();
                }

                //Game Over Check
                if (e1.y >= 21) { GameOver = true; }
                if (e2.y >= 21) { GameOver = true; }
                if (e3.y >= 21) { GameOver = true; }
                if (e4.y >= 21) { GameOver = true; }
                if (e5.y >= 21) { GameOver = true; }
                if (p1.HP <= 0) { GameOver = true; }

                //Drawing User Interface
                if (paused == false)
                {
                    Console.SetCursorPosition(0, 0);
                    p1.DrawStats();
                    Console.SetCursorPosition(0, 3);
                    Console.WriteLine("                                                                                                                       ");
                    for (int a = 3; a <= 27; a++)
                    {
                        Console.SetCursorPosition(0, a);
                        Console.WriteLine('|');
                        Console.SetCursorPosition(119, a);
                        Console.WriteLine('|');
                    }
                    Console.SetCursorPosition(1, 26);
                    Console.Write("______________________________________________________________________________________________________________________");
                    Console.SetCursorPosition(0, 28);
                    Console.Write("________________________________________________________________________________________________________________________");
                    //Draw Player
                    Console.SetCursorPosition(Move_LR, Move_UD);
                    Console.WriteLine("X");
                    Console.SetCursorPosition(Move_LR - 1, Move_UD + 1);
                    Console.WriteLine("X X");
                    Console.SetCursorPosition(Move_LR - 2, Move_UD + 2);
                    Console.WriteLine("X X X");
                }

                //Ememy Movement
                if (paused == false)
                {
                    System.Threading.Thread.Sleep(speed);
                    if (e1.move) { e1.Move(); }
                    if (e2.move) { e2.Move(); }
                    if (e3.move) { e3.Move(); }
                    if (e4.move) { e4.Move(); }
                    if (e5.move) { e5.Move(); }
                }

                //User Input
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo KeyInfo;
                    KeyInfo = Console.ReadKey(true);
                    switch (KeyInfo.Key)
                    {
                        //Movement Built in With Innertia
                        //Move Right
                        case ConsoleKey.RightArrow:
                            if (Move_LR < 115)
                            {
                                if (paused == false)
                                {
                                    Console.SetCursorPosition(Move_LR, Move_UD);
                                    Console.Write(' ');
                                    Console.SetCursorPosition(Move_LR - 1, Move_UD + 1);
                                    Console.WriteLine(' ');
                                    Console.SetCursorPosition(Move_LR - 2, Move_UD + 2);
                                    Console.WriteLine(' ');
                                    Move_LR += 2;
                                    Console.SetCursorPosition(Move_LR, Move_UD);
                                    Console.WriteLine("X");
                                    Console.SetCursorPosition(Move_LR - 1, Move_UD + 1);
                                    Console.WriteLine("X X");
                                    Console.SetCursorPosition(Move_LR - 2, Move_UD + 2);
                                    Console.WriteLine("X X X");
                                }
                            }
                            break;

                        //Move Left
                        case ConsoleKey.LeftArrow:
                            if (Move_LR > 3)
                            {
                                if (paused == false)
                                {
                                    Console.SetCursorPosition(Move_LR, Move_UD);
                                    Console.Write(' ');
                                    Console.SetCursorPosition(Move_LR + 1, Move_UD + 1);
                                    Console.WriteLine(' ');
                                    Console.SetCursorPosition(Move_LR + 2, Move_UD + 2);
                                    Console.WriteLine(' ');
                                    Move_LR -= 2;
                                    Console.SetCursorPosition(Move_LR, Move_UD);
                                    Console.WriteLine("X");
                                    Console.SetCursorPosition(Move_LR - 1, Move_UD + 1);
                                    Console.WriteLine("X X");
                                    Console.SetCursorPosition(Move_LR - 2, Move_UD + 2);
                                    Console.WriteLine("X X X");
                                }
                            }
                            break;

                        //Attack
                        case ConsoleKey.Spacebar:
                            if (Console.KeyAvailable == false)
                            {
                                if (paused == false)
                                {
                                    //Ammo Check
                                    if (p1.AMMO > 0)
                                    {
                                        //Drawing Bullet
                                        p1.Shoot();
                                        int y = Move_UD - 1;
                                        while (y > 3)
                                        {
                                            Console.SetCursorPosition(Move_LR, y);
                                            Console.WriteLine(' ');
                                            y--;
                                            Console.SetCursorPosition(Move_LR, y);
                                            Console.WriteLine('|');
                                            //Checking If Bullet Hits Enemy
                                            if (y == e1.y && Move_LR >= Convert.ToInt32(e1.x) && Move_LR <= Convert.ToInt32(e1.x + 4))
                                            {
                                                e1.Clear();
                                                p1.SCORE += 10;
                                                e1.move = false;
                                                e1.y_diff = Enemy.y_val - 4;
                                                e1.move = true;
                                            }
                                            if (y == e2.y && Move_LR >= Convert.ToInt32(e2.x) && Move_LR <= Convert.ToInt32(e2.x + 4))
                                            {
                                                e2.Clear();
                                                p1.SCORE += 10;
                                                e2.move = false;
                                                e2.y_diff = Enemy.y_val - 4;
                                                e2.move = true;

                                            }
                                            if (y == e3.y && Move_LR >= Convert.ToInt32(e3.x) && Move_LR <= Convert.ToInt32(e3.x + 4))
                                            {
                                                e3.Clear();
                                                p1.SCORE += 10;
                                                e3.move = false;
                                                e3.y_diff = Enemy.y_val - 4;
                                                e3.move = true;
                                            }
                                            if (y == e4.y && Move_LR >= Convert.ToInt32(e4.x) && Move_LR <= Convert.ToInt32(e4.x + 4))
                                            {
                                                e4.Clear();
                                                p1.SCORE += 10;
                                                e4.move = false;
                                                e4.y_diff = Enemy.y_val - 4;
                                                e4.move = true;
                                            }
                                            if (y == e5.y && Move_LR >= e5.x && Move_LR <= Convert.ToInt32(e5.x + 4))
                                            {
                                                e5.Clear();
                                                p1.SCORE += 10;
                                                e5.move = false;
                                                e5.y_diff = Enemy.y_val - 4;
                                                e5.move = true;
                                            }
                                            System.Threading.Thread.Sleep(30);
                                        }
                                    }
                                    else
                                    {
                                        //Counter Used for Difficulty Progression. Maths Question Generation to Reload Ammo.
                                        Console.SetCursorPosition(1, 27);
                                        Console.WriteLine("                                                                    ");
                                        Console.SetCursorPosition(1, 27);
                                        Random rnd = new Random();
                                        int n1;
                                        int n2;
                                        if (counter < 2)
                                        {
                                            n1 = rnd.Next(1, 25);
                                            n2 = rnd.Next(1, 25);
                                        }
                                        else if (counter < 5)
                                        {
                                            n1 = rnd.Next(25, 50);
                                            n2 = rnd.Next(25, 50);
                                        }
                                        else if (counter < 8)
                                        {
                                            n1 = rnd.Next(50, 100);
                                            n2 = rnd.Next(50, 100);
                                        }
                                        else
                                        {
                                            n1 = rnd.Next(100, 200);
                                            n2 = rnd.Next(100, 200);
                                        }
                                        Console.Write("{0}+{1}=? ", n1, n2);
                                        string answer = Console.ReadLine().Replace(" ", String.Empty);
                                        int value;
                                        if (int.TryParse(answer, out value)) { }


                                        //If Answer is Incorrect
                                        while (value != n1 + n2)
                                        {
                                            Console.SetCursorPosition(1, 27);
                                            Console.WriteLine("                                                                                                                                                      ");
                                            Console.SetCursorPosition(0, 29);
                                            Console.WriteLine("                                                                                                                                                      ");
                                            Console.SetCursorPosition(1, 27);
                                            Console.WriteLine("Incorrect. Try Again.");
                                            p1.HP -= 10;
                                            break;
                                        }

                                        //If Answer is Correct
                                        while (value == n1 + n2)
                                        {
                                            counter++;
                                            p1.Reload();
                                            Console.SetCursorPosition(1, 27);
                                            Console.WriteLine("                                                                                                                                                      ");
                                            Console.SetCursorPosition(0, 29);
                                            Console.WriteLine("                                                                                                                                                      ");
                                            Console.SetCursorPosition(1, 27);
                                            Console.WriteLine("Correct!! ");
                                            break;
                                        }
                                    }
                                }
                            }
                            break;

                        //Pause Screen
                        case ConsoleKey.Escape:
                            if (paused == false)
                            {
                                paused = true;
                                Console.Clear();
                                Console.SetCursorPosition(2, 8);
                                Console.WriteLine(@"                         __________  _____   ____ ___  ____________________________   
                           \______   \/  _  \ |    |   \/   _____/\_   _____/\______ \  
                            |     ___/  /_\  \|    |   /\_____  \  |    __)_  |    |  \ 
                            |    |  /    |    \    |  / /        \ |        \ |    `   \
                            |____|  \____|__  /______/ /_______  //_______  //_______  /
                                            \/                 \/         \/         \/ ");
                                Console.SetCursorPosition(37, 15);
                                Console.WriteLine("Press Enter to Return to the Main Menu...");

                                do
                                {
                                    KeyInfo = Console.ReadKey(true);
                                    switch (KeyInfo.Key)
                                    {
                                        //Checking to Return
                                        case ConsoleKey.Enter:
                                            Console.Clear();
                                            goto Main;

                                        case ConsoleKey.Escape:
                                            Console.Clear();
                                            paused = false;
                                            break;
                                    }
                                } while (KeyInfo.Key != ConsoleKey.Enter && KeyInfo.Key != ConsoleKey.Escape);
                            }
                            break;

                        default:
                            break;
                    }
                }
            } while (GameOver == false);


            //Game End Screen
            Console.Clear();
            Console.SetCursorPosition(27, 6);
            Console.WriteLine(@" ________                        ________                     
                           /  _____/_____    _____   ____   \_____  \___  __ ___________ 
                          /   \  ___\__  \  /     \_/ __ \   /   |   \  \/ // __ \_  __ \
                          \    \_\  \/ __ \|  Y Y  \  ___/  /    |    \   /\  ___/|  | \/
                           \______  (____  /__|_|  /\___  > \_______  /\_/  \___  >__|   
                                  \/     \/      \/     \/          \/          \/       ");
            Console.SetCursorPosition(42, 13);
            Console.WriteLine("Name: {0}", p1.NAME);
            Console.SetCursorPosition(42, 14);
            Console.WriteLine("Score: {0}", p1.SCORE);

            //For Hard Mode
            if (LvL == "hard")
            {
                //Check if High Score Was Created
                var temp_index = highScores2.Keys.ToList();
                temp_index.Sort();

                //Sorts Key Out so Multiple Users can Have the Same Score
                t_scores2 = p1.SCORE;
                while (highScores2.ContainsKey(t_scores2))
                {
                    t_scores2 = Convert.ToInt32(t_scores2 + 1);
                }

                if (p1.SCORE >= temp_index[0] && p1.SCORE != 0)
                {
                    Console.SetCursorPosition(42, 14);
                    Console.WriteLine("Congratulations! Your have one of the 5 best scores!");


                    //Adds High Score to Dictionary
                    highScores2[t_scores2] = p1.NAME;

                    //Adds High Scores to Dictionary in Order of Score Value
                    var list = highScores2.Keys.ToList();
                    list.Sort();
                    list.Reverse();
                    int temp = 0;
                    foreach (int i in list)
                    {
                        scores_2[temp] = Convert.ToString(temp + 1) + ". " + highScores2[i] + " : " + Convert.ToString(i);
                        temp += 1;
                    }

                    //Adds High Scores to Text File After Removing 6th High Score in Present
                    if (scores_2.Length > 5)
                    {
                        scores_2 = scores_2.Reverse().Skip(1).Reverse().ToArray();
                    }

                    File.WriteAllLines(textfilepath2, scores_2);
                }

            }
            //For Easy Mode
            else if (LvL == "easy")
            {
                //Check if High Score Was Created
                var temp_index1 = highScores.Keys.ToList();
                temp_index1.Sort();

                //Sorts Key Out so Multiple Users can Have the Same Score
                t_scores = p1.SCORE;
                while (highScores2.ContainsKey(t_scores))
                {
                    t_scores = Convert.ToInt32(t_scores + 1);
                }

                if (p1.SCORE >= temp_index1[0] && p1.SCORE != 0)
                {
                    Console.SetCursorPosition(42, 14);
                    Console.WriteLine("Congratulations! Your have one of the 5 best scores!");

                    //Adds Current Score to Dictionary
                    highScores[t_scores] = p1.NAME;

                    //Adds High Scores to Dictionary in Order of Score Value
                    var list = highScores.Keys.ToList();
                    list.Sort();
                    list.Reverse();
                    int temp1 = 0;
                    foreach (int i in list)
                    {
                        scores_1[temp1] = Convert.ToString(temp1 + 1) + ". " + highScores[i] + " : " + Convert.ToString(i);
                        temp1 += 1;
                    }

                    //Adds High Scores to Text File After Removing 6th High Score in Present
                    if (scores_1.Length > 5)
                    {
                        scores_1 = scores_1.Reverse().Skip(1).Reverse().ToArray();
                    }

                    File.WriteAllLines(textfilepath, scores_1);
                }
            }

            Console.SetCursorPosition(42, 16);
            Console.WriteLine("Press any key to proceed...");
            Console.ReadKey();
            Console.Clear();
            goto Main;
        }
    }
}