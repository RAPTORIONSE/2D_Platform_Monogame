using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
namespace SuperMario
{
    public static class FileManager
    {
        public static void Start(string path)
        {
            List<string> strings = new List<string>();
            StreamReader sr = new StreamReader(path);
            do
            {
                strings.Add(sr.ReadLine());
            } while (!sr.EndOfStream);
            sr.Close();
        }
        public static void Save(string path, string contents)
        {
            StreamWriter sw = new StreamWriter(path+".txt");
            sw.Write(contents);
            sw.Close();
        }
        public static int Load(string path)
        {
            string textRead = File.ReadAllText(path).ToString();
            return 0;//logic to convert into what i need
        }
        public static List<GameObject> World(int World, int Map)// change int to string instead?
        {
            List<GameObject> gameObjects = new List<GameObject>();
            List<string> strings = new List<string>();
            StreamReader sr = new StreamReader("Map_" + World + "_" + Map + ".txt");
            do
            {
                strings.Add(sr.ReadLine());
                gameObjects.AddRange(Parse(strings));
            } while (!sr.EndOfStream);
            sr.Close();
            return gameObjects;
        }
        private static List<GameObject> Parse(List<string> strings)
        {
            List<GameObject> gameObjects = new List<GameObject>();
            //Add code to create a list of the objects and return them
            //Better way of doing this?
            if (strings.Count == 1)
            {
                string[] coodinates = strings[0].Split(';');
                for (int i = 0; i < coodinates.Length; i++)
                {
                    string[] xywh = coodinates[i].Split(',');
                    try
                    {
                        int x = Convert.ToInt32(xywh[0]);
                        int y = Convert.ToInt32(xywh[1]);
                        int w = Convert.ToInt32(xywh[2]);
                        int h = Convert.ToInt32(xywh[3]);
                        Rectangle pos = new Rectangle(x, y, w, h);
                        gameObjects.Add(new Player(ResourceManager.Get<Texture2D>("TestHitbox"), pos,Vector2.Zero));
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Input string is not a sequence of digits.");
                    }
                }
            }     //specifies players starting position
            else if (strings.Count == 2)
            {
                string[] coodinates = strings[1].Split(';');
                for (int i = 0; i < coodinates.Length; i++)
                {
                    string[] xywh = coodinates[i].Split(',');
                    try
                    {
                        int x = Convert.ToInt32(xywh[0]);
                        int y = Convert.ToInt32(xywh[1]);
                        int w = Convert.ToInt32(xywh[2]);
                        int h = Convert.ToInt32(xywh[3]);
                        Rectangle pos = new Rectangle(x, y, w, h);
                        gameObjects.Add(new Goal(ResourceManager.Get<Texture2D>("cursor1"), pos));
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Input string is not a sequence of digits.");
                    }
                }
            }//specifies maps goal location
            else if (strings.Count == 3)
            {
                string[] coodinates = strings[2].Split(';');
                for (int i = 0; i < coodinates.Length; i++)
                {
                    string[] xywh = coodinates[i].Split(',');
                    try
                    {
                        int x = Convert.ToInt32(xywh[0]);
                        int y = Convert.ToInt32(xywh[1]);
                        int w = Convert.ToInt32(xywh[2]);
                        int h = Convert.ToInt32(xywh[3]);
                        Rectangle pos = new Rectangle(x, y, w, h);
                        gameObjects.Add(new Platform(ResourceManager.Get<Texture2D>("platform"), pos));
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Input string is not a sequence of digits.");
                    }
                }
            }//specifies where platforms are located
            else if (strings.Count == 4)
            {
                string[] coodinates = strings[3].Split(';');
                for (int i = 0; i < coodinates.Length; i++)
                {
                    string[] xywh = coodinates[i].Split(',');
                    try
                    {
                        int x = Convert.ToInt32(xywh[0]);
                        int y = Convert.ToInt32(xywh[1]);
                        int w = Convert.ToInt32(xywh[2]);
                        int h = Convert.ToInt32(xywh[3]);
                        Rectangle pos = new Rectangle(x, y, w, h);
                        //platforms[i] = new Trap(ResourceManager.Get<Texture2D>("platform"), pos);
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Input string is not a sequence of digits.");
                    }
                }
            }//specifies where static traps are located
            else if (strings.Count == 5)
            {
                string[] coodinates = strings[4].Split(';');
                for (int i = 0; i < coodinates.Length; i++)
                {
                    string[] xywh = coodinates[i].Split(',');
                    try
                    {
                        int x = Convert.ToInt32(xywh[0]);
                        int y = Convert.ToInt32(xywh[1]);
                        int w = Convert.ToInt32(xywh[2]);
                        int h = Convert.ToInt32(xywh[3]);
                        Rectangle pos = new Rectangle(x, y, w, h);
                        //platforms[i] = new Score(ResourceManager.Get<Texture2D>("platform"), pos);
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Input string is not a sequence of digits.");
                    }
                }
            }//specifies where score objects are located
            else if (strings.Count == 6)
            {
                string[] coodinates = strings[5].Split(';');
                for (int i = 0; i < coodinates.Length; i++)
                {
                    string[] xywh = coodinates[i].Split(',');
                    try
                    {
                        int x = Convert.ToInt32(xywh[0]);
                        int y = Convert.ToInt32(xywh[1]);
                        int w = Convert.ToInt32(xywh[2]);
                        int h = Convert.ToInt32(xywh[3]);
                        Rectangle pos = new Rectangle(x, y, w, h);
                        //platforms[i] = new Enemy(ResourceManager.Get<Texture2D>("platform"), pos);
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Input string is not a sequence of digits.");
                    }
                }
            }//specifies where moving enemies are located
            return gameObjects;
        }
    }
}