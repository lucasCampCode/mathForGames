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
        private readonly Random rng = new Random();
        private Entity _entity = new Entity(20, 20, Color.GREEN, '0', ConsoleColor.Green);
        private Player _player = new Player(5, 5, Color.RED, '@', ConsoleColor.Red);
        public static ConsoleColor DefaultColor { get; set; } = ConsoleColor.DarkCyan;
        public static int CurrentSceneIndex
        {
            get
            {
                return _currentSceneIndex;
            }
        }
        public Game()
        {
            _scenes = new Scene[0];
        }
        //Static function used to set game over without an instance of game.
        public static void SetGameOver(bool value)
        {
            _gameOver = value;
        }

        public static bool GetKeyDown(int key)
        {
            return Raylib.IsKeyDown((KeyboardKey)key);
        }
        public static bool GetKeyPressed(int key)
        {
            return Raylib.IsKeyPressed((KeyboardKey)key);
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
        public static bool RemoveScene(int index)
        {
            //checks to see if index is out of bounds of array
            if (index < 0 || index >= _scenes.Length)
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
                if (i != index)
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

       

        //Called when the game begins. Use this for initialization.
        public void Start()
        {
            //create a new window for raylib
            Raylib.InitWindow(1024, 760, "blank");
            //sets the framerate
            Raylib.SetTargetFPS(15);

            Scene scene1 = new Scene();
            Scene scene2 = new Scene();


            Console.CursorVisible = false;

            scene1.AddEntity(_entity);
            scene1.AddEntity(_player);

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
            }

            End();
        }
    }
}
