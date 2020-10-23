using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MathLib;
using Raylib_cs;

namespace MathForGames
{
    class Game
    {
        private static bool _gameOver = false;
        private static Scene[] _scenes;
        private static int _currentSceneIndex = 0;
        public static int CurrentSceneIndex { get { return _currentSceneIndex; } }
        public static ConsoleColor DefaultColor { get; set; } = ConsoleColor.DarkCyan;
        public static bool Debug { get; private set; } = true;
        
        //Static function used to set game over without an instance of game.
        public static void SetGameOver(bool value)
        {
            _gameOver = value;
        }

        public static Scene GetScene(int index)
        {
            return _scenes[index];
        }
        public static Scene GetCurrentScene()
        {
            return _scenes[_currentSceneIndex];
        }
        public static int AddScene(Scene scene)
        {
            Scene[] newArray = new Scene[_scenes.Length + 1];
            //copy values from old array to the old array
            for (int i = 0; i < _scenes.Length; i++)
            {
                newArray[i] = _scenes[i];
            }

            int index = _scenes.Length;

            //sets the new entity at the end of the new array
            newArray[index] = scene;
            //set old array to the new array with the new entity
            _scenes = newArray;

            return index;
        }
        public static bool RemoveScene(Scene scene)
        {
            //checks to see if index is out of bounds of array
            if (scene == null)
            {
                return false;
            }
            bool Removed = false;

            //creating a new array that has a slot taken away from an old array
            Scene[] tempArray = new Scene[_scenes.Length - 1];
            //create variable to access tempArray index
            int j = 0;
            //copy values from the old array to new Array
            for (int i = 0; i < _scenes.Length; i++)
            {
                //If the current index is not the index that needs to be removed
                //add the value into the old array and increment j
                if (scene != _scenes[i])
                {
                    tempArray[j] = _scenes[i];
                    j++;
                }
                else //gives us our return value for debuging
                {
                    Removed = true;
                }
            }
            //set the old arrat to be the tempArray
            _scenes = tempArray;

            return Removed;
        }
        public static void SetCurrentScene(int index)
        {
            if (index < 0 || index >= _scenes.Length)
                return;
            if (_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].End();

            _currentSceneIndex = index;
        }

        public static bool GetKeyDown(int key)
        {
            return Raylib.IsKeyDown((KeyboardKey)key);
        }
        public static bool GetKeyPressed(int key)
        {
            return Raylib.IsKeyPressed((KeyboardKey)key);
        }
       public Game()
        {
            _scenes = new Scene[0];
        }

        //Called when the game begins. Use this for initialization.
        public void Start()
        {
            Raylib.InitWindow(1024, 760, "blank");
            Raylib.SetTargetFPS(15);

            Console.CursorVisible = false;

            Scene scene1 = new Scene();
            Scene scene2 = new Scene();

            Entity entity = new Entity(0, 0, Color.GREEN, '■', ConsoleColor.Green);
            entity.Velocity.X = 1;

            Enemy enemy = new Enemy(10, 10, Color.GREEN, '■', ConsoleColor.Green);
            Player player = new Player(0, 1, Color.SKYBLUE, '@', ConsoleColor.Cyan);
            player.Speed = 5;
            enemy.Target = player;

            scene1.AddEntity(entity);
            scene1.AddEntity(enemy);
            scene1.AddEntity(player);
           

            scene2.AddEntity(player);
            

            int startingSceneIndex = 0;

            startingSceneIndex = AddScene(scene1);
            AddScene(scene2);

            SetCurrentScene(startingSceneIndex);
        }


        //Called every frame.
        public void Update(float deltaTime)
        {

            if (!_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].Start();

            _scenes[_currentSceneIndex].Update(deltaTime);
        }

        //Used to display objects and other info on the screen.
        public void Draw()
        {


            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DARKGRAY);
            Console.Clear();
            _scenes[_currentSceneIndex].Draw();
            Raylib.EndDrawing();
        }


        //Called when the game ends.
        public void End()
        {
            if (_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].End();
        }


        //Handles all of the main game logic including the main game loop.
        public void Run()
        {
            Start();

            while(!_gameOver && !Raylib.WindowShouldClose())
            {
                float deltaTime = Raylib.GetFrameTime();
                Update(deltaTime);
                Draw();
                while (Console.KeyAvailable)
                    Console.ReadKey(true);
            }

            End();
        }
    }
}
