using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;

namespace SuperMario
{
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        Background _background;
        Viewport _viewport;
        Camera _camera;
        enum GameState { STARTSCREEN, MAINMENU, OPTIONSMENU, HIGHSCORE, LOADMENU, MAPEDIT, CREDIT, PLAYING, PAUSE, ENDSCREEN, QUIT };
        GameState _currentGameState;
        MenuManager _menuManager;
        GameManager _gameManager;
        MapEditor _mapEditor;
        Vector2 _screenDimension;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            ResourceManager._Game = this;
            _currentGameState = GameState.MAINMENU;
            _menuManager = new MenuManager(_graphics, ResourceManager.Get<Texture2D>("Words Button Sheet"), ResourceManager.Get<SpriteFont>("Text"), ResourceManager.Get<Texture2D>("threerings"), ResourceManager.Get<Texture2D>("TestHitbox"));
            _gameManager = new GameManager(this, 1, 0);
            _viewport.Bounds = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            _screenDimension = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            _graphics.ApplyChanges();
            IsMouseVisible = true;
            _mapEditor = new MapEditor();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _background = new Background(_viewport);
            _camera = new Camera(_viewport);
        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            switch (_currentGameState)
            {
                case GameState.STARTSCREEN:
                    CheckGameState(_menuManager.Update(gameTime, _currentGameState.ToString()));
                    break;
                case GameState.MAINMENU:
                    _camera.SetPosition(new Vector2(400, 240));
                    CheckGameState(_menuManager.Update(gameTime, _currentGameState.ToString()));
                    break;
                case GameState.OPTIONSMENU:
                    _camera.SetPosition(new Vector2(400, 240));
                    CheckGameState(_menuManager.Update(gameTime, _currentGameState.ToString()));
                    break;
                case GameState.HIGHSCORE:
                    _camera.SetPosition(new Vector2(400, 240));
                    CheckGameState(_menuManager.Update(gameTime, _currentGameState.ToString()));
                    break;
                case GameState.LOADMENU:
                    _camera.SetPosition(new Vector2(400, 240));
                    CheckGameState(_menuManager.Update(gameTime, _currentGameState.ToString()));
                    break;
                case GameState.MAPEDIT:
                    _camera.SetPosition(_mapEditor.CameraMovement());
                    CheckGameState(_mapEditor.Update());
                    break;
                case GameState.CREDIT:
                    _camera.SetPosition(new Vector2(400, 240));
                    CheckGameState(_menuManager.Update(gameTime, _currentGameState.ToString()));
                    break;
                case GameState.PLAYING:
                    _camera.SetPosition(_gameManager._player._location);
                    CheckGameState(_gameManager.Update(gameTime));
                    _background.Update();//reuires a value to say if its moving left or right or standing still
                    break;
                case GameState.PAUSE:
                    _camera.SetPosition(new Vector2(400, 240));
                    CheckGameState(_menuManager.Update(gameTime, _currentGameState.ToString()));
                    break;
                case GameState.ENDSCREEN:
                    _camera.SetPosition(new Vector2(400, 240));
                    CheckGameState(_menuManager.Update(gameTime, _currentGameState.ToString()));
                    break;
                case GameState.QUIT:
                    Exit();
                    break;
            }

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _camera.GetPosition());
            _background.Draw(_spriteBatch);

            switch (_currentGameState)
            {
                case GameState.STARTSCREEN:
                    _menuManager.Draw(_spriteBatch);
                    break;
                case GameState.MAINMENU:
                    _menuManager.Draw(_spriteBatch);
                    break;
                case GameState.OPTIONSMENU:
                    _menuManager.Draw(_spriteBatch);
                    break;
                case GameState.HIGHSCORE:
                    _menuManager.Draw(_spriteBatch);
                    break;
                case GameState.LOADMENU:

                    break;
                case GameState.MAPEDIT:
                    _mapEditor.Draw(_spriteBatch);
                    break;
                case GameState.CREDIT:
                    _menuManager.Draw(_spriteBatch);
                    break;
                case GameState.PLAYING:
                    _gameManager.Draw(_spriteBatch);
                    break;
                case GameState.PAUSE:
                    _gameManager.Draw(_spriteBatch);
                    _menuManager.Draw(_spriteBatch);
                    break;
                case GameState.ENDSCREEN:
                    _menuManager.Draw(_spriteBatch);
                    break;
                case GameState.QUIT:
                    Exit();
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        protected void CheckGameState(string MenuState)
        {
            if (MenuState == "STARTSCREEN")
            {
                _currentGameState = GameState.STARTSCREEN;
            }
            else if (MenuState == "STARTMENU")
            {
                _currentGameState = GameState.MAINMENU;
            }
            else if (MenuState == "OPTIONSMENU")
            {
                _currentGameState = GameState.OPTIONSMENU;
            }
            else if (MenuState == "HIGHSCORE")
            {
                _currentGameState = GameState.HIGHSCORE;
            }
            else if (MenuState == "LOADMENU")
            {
                _currentGameState = GameState.LOADMENU;
            }
            else if (MenuState == "MAPEDIT")
            {
                _currentGameState = GameState.MAPEDIT;
            }
            else if (MenuState == "CREDITSMENU")
            {
                _currentGameState = GameState.CREDIT;
            }
            else if (MenuState == "INGAME")
            {
                _currentGameState = GameState.PLAYING;
            }
            else if (MenuState == "INGAMEPAUSEMENU")
            {
                _currentGameState = GameState.PAUSE;
            }
            else if (MenuState == "GAMEOVERMENU")
            {
                _currentGameState = GameState.ENDSCREEN;
            }
            else if (MenuState == "QUIT")
            {
                _currentGameState = GameState.QUIT;
            }
        }
    }
}

//Vector2 direction = Vector2.Zero;
//if (kbState.IsKeyDown(Keys.Up))
//    direction = new Vector2(0, -1);
//else if (kbState.IsKeyDown(Keys.Down))
//    direction = new Vector2(0, 1);
//if (kbState.IsKeyDown(Keys.Left))
//    direction += new Vector2(-1, 0);
//else if (kbState.IsKeyDown(Keys.Right))
//    direction += new Vector2(1, 0);
//foreach (Background background in backgrounds)