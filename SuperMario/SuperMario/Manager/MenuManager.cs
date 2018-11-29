using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMario
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    public class MenuManager
    {
        GraphicsDeviceManager _graphics;
        SpriteFont _spriteFont;
        Texture2D _texture;
        Texture2D _animation;
        Rectangle _posStart;
        Rectangle _posOptions;
        Rectangle _posCredits;
        Rectangle _posSave;
        Rectangle _posLoad;
        Rectangle _posQuitMainMenu;
        Rectangle _posQuitInGamePauseMenu;
        Rectangle _posResume;
        Rectangle _posBack;
        Rectangle _posArrowRight;
        Rectangle _posArrowLeft;
        Rectangle _buttonStart;
        Rectangle _buttonOptions;
        Rectangle _buttonCredits;
        Rectangle _buttonSave;
        Rectangle _buttonLoad;
        Rectangle _buttonQuit;
        Rectangle _buttonResume;
        Rectangle _buttonBack;
        Rectangle _buttonArrowRight;
        Rectangle _buttonArrowLeft;
        MouseState _prevMouseState;
        MouseState _currMouseState;
        KeyboardState _currKeyboardState;
        KeyboardState _prevKeyboardState;
        Vector2 _mousePosition;
        Vector2 _ringPosition;
        public Point _pointMiddle;
        Point _ringFrameSize = new Point(75, 75);
        Point _ringCurrentFrame = new Point(0, 0);
        Point _ringSheetSize = new Point(5, 7);
        int _ringTimeSinceLastFrame = 0;
        int _ringMillisecondsPerFrame = 32;
        List<Vector2> _resolutionList;
        int _index;
        enum GameState { STARTSCREEN, STARTMENU, OPTIONSMENU, HIGHSCORE, CREDITSMENU, INGAME, INGAMEPAUSEMENU, GAMEOVERMENU, QUIT };
        GameState _currentGameState;
        GameState _previousGameState;
        Texture2D _debugBox;
        public MenuManager(GraphicsDeviceManager graphicsDeviceManager, Texture2D texture, SpriteFont spriteFont, Texture2D animation, Texture2D debugBox)
        {
            _graphics = graphicsDeviceManager;
            Initialize();
            this._debugBox = debugBox;
            this._texture = texture;
            this._spriteFont = spriteFont;
            this._animation = animation;
        }
        private void Initialize()
        {
            _index = 10;//Krävs för första starten//changed??
            _resolutionList = new List<Vector2>();
            PopulateResolutionList();
            //SetScreenResolution(FileManager.Load("Options"));
            //index = FileManager.Load("test");
            _buttonStart = new Rectangle(0, 0, 68, 26);
            _buttonOptions = new Rectangle(0, 31, 106, 32);
            _buttonCredits = new Rectangle(0, 68, 99, 26);
            _buttonSave = new Rectangle(113, 31, 63, 26);
            _buttonLoad = new Rectangle(113, 62, 72, 26);
            _buttonQuit = new Rectangle(0, 99, 63, 31);
            _buttonResume = new Rectangle(113, 0, 106, 26);
            _buttonBack = new Rectangle(113, 93, 70, 26);
            _buttonArrowRight = new Rectangle(224, 0, 47, 22);
            _buttonArrowLeft = new Rectangle(278, 0, 47, 22);
            _currMouseState = Mouse.GetState();
            SetScreenResolution(_index);
        }
        public string Update(GameTime gameTime, string currentGameState)
        {
            if (currentGameState == "STARTSCREEN")
            {
                this._currentGameState = GameState.STARTSCREEN;
            }
            else if (currentGameState == "MAINMENU")
            {
                this._currentGameState = GameState.STARTMENU;
            }
            else if (currentGameState == "OPTIONSMENU")
            {
                this._currentGameState = GameState.OPTIONSMENU;
            }
            else if (currentGameState == "HIGHSCORE")
            {
                this._currentGameState = GameState.HIGHSCORE;
            }
            else if (currentGameState == "CREDIT")
            {
                this._currentGameState = GameState.CREDITSMENU;
            }
            else if (currentGameState == "PLAYING")
            {
                this._currentGameState = GameState.INGAME;
            }
            else if (currentGameState == "PAUSE")
            {
                this._currentGameState = GameState.INGAMEPAUSEMENU;
            }
            else if (currentGameState == "ENDSCREEN")
            {
                this._currentGameState = GameState.GAMEOVERMENU;
            }
            _currMouseState = Mouse.GetState();
            _currKeyboardState = Keyboard.GetState();
            MouseAnimation(gameTime);
            MouseClick();
            if (_currMouseState != _prevMouseState)
            {
                _mousePosition = new Vector2(_currMouseState.X, _currMouseState.Y);
            }
            _prevMouseState = _currMouseState;
            _prevKeyboardState = _currKeyboardState;
            return this._currentGameState.ToString();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            switch (_currentGameState)
            {
                case GameState.STARTMENU:

                    spriteBatch.Draw(_debugBox, _posStart, _buttonStart, Color.White);
                    spriteBatch.Draw(_debugBox, _posOptions, _buttonOptions, Color.White);
                    spriteBatch.Draw(_debugBox, _posLoad, _buttonLoad, Color.White);
                    spriteBatch.Draw(_debugBox, _posSave, _buttonSave, Color.White);
                    spriteBatch.Draw(_debugBox, _posCredits, _buttonCredits, Color.White);
                    spriteBatch.Draw(_debugBox, _posQuitMainMenu, _buttonQuit, Color.White);
                    spriteBatch.Draw(_texture, _posStart, _buttonStart, Color.White);
                    spriteBatch.Draw(_texture, _posOptions, _buttonOptions, Color.White);
                    spriteBatch.Draw(_texture, _posLoad, _buttonLoad, Color.White);
                    spriteBatch.Draw(_texture, _posSave, _buttonSave, Color.White);
                    spriteBatch.Draw(_texture, _posCredits, _buttonCredits, Color.White);
                    spriteBatch.Draw(_texture, _posQuitMainMenu, _buttonQuit, Color.White);
                    spriteBatch.Draw(_animation, _ringPosition, new Rectangle(_ringCurrentFrame.X * _ringFrameSize.X, _ringCurrentFrame.Y * _ringFrameSize.Y, _ringFrameSize.X, _ringFrameSize.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.2f);
                    break;
                case GameState.CREDITSMENU:
                    spriteBatch.Draw(_animation, _ringPosition, new Rectangle(_ringCurrentFrame.X * _ringFrameSize.X, _ringCurrentFrame.Y * _ringFrameSize.Y, _ringFrameSize.X, _ringFrameSize.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.2f);
                    spriteBatch.Draw(_debugBox, _posBack, _buttonBack, Color.White);
                    spriteBatch.Draw(_texture, _posBack, _buttonBack, Color.White);
                    spriteBatch.DrawString(_spriteFont, "Created by Erik Broman", new Vector2(_graphics.PreferredBackBufferWidth / 2 - _spriteFont.MeasureString("Created by Erik Broman").X / 2, _graphics.PreferredBackBufferHeight / 2), Color.Black);
                    spriteBatch.Draw(_animation, _ringPosition, new Rectangle(_ringCurrentFrame.X * _ringFrameSize.X, _ringCurrentFrame.Y * _ringFrameSize.Y, _ringFrameSize.X, _ringFrameSize.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.2f);
                    break;
                case GameState.OPTIONSMENU:// add more if possible
                    spriteBatch.Draw(_debugBox, _posBack, _buttonBack, Color.White);
                    spriteBatch.Draw(_debugBox, _posArrowRight, _buttonArrowRight, Color.White);
                    spriteBatch.Draw(_debugBox, _posArrowLeft, _buttonArrowLeft, Color.White);
                    spriteBatch.DrawString(_spriteFont, "Resolution: " + _resolutionList[_index], new Vector2(_graphics.PreferredBackBufferWidth / 2 - _spriteFont.MeasureString("Resolution: " + _resolutionList[_index]).X / 2, _posStart.Y), Color.Black);
                    spriteBatch.Draw(_texture, _posBack, _buttonBack, Color.White);
                    spriteBatch.Draw(_texture, _posArrowRight, _buttonArrowRight, Color.White);
                    spriteBatch.Draw(_texture, _posArrowLeft, _buttonArrowLeft, Color.White);
                    spriteBatch.Draw(_animation, _ringPosition, new Rectangle(_ringCurrentFrame.X * _ringFrameSize.X, _ringCurrentFrame.Y * _ringFrameSize.Y, _ringFrameSize.X, _ringFrameSize.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.2f);
                    break;
                case GameState.INGAMEPAUSEMENU:
                    spriteBatch.Draw(_debugBox, _posOptions, _buttonOptions, Color.White);
                    spriteBatch.Draw(_debugBox, _posLoad, _buttonLoad, Color.White);
                    spriteBatch.Draw(_debugBox, _posSave, _buttonSave, Color.White);
                    spriteBatch.Draw(_debugBox, _posQuitInGamePauseMenu, _buttonQuit, Color.White);
                    spriteBatch.Draw(_debugBox, _posResume, _buttonResume, Color.White);
                    spriteBatch.Draw(_texture, _posOptions, _buttonOptions, Color.White);
                    spriteBatch.Draw(_texture, _posLoad, _buttonLoad, Color.White);
                    spriteBatch.Draw(_texture, _posSave, _buttonSave, Color.White);
                    spriteBatch.Draw(_texture, _posQuitInGamePauseMenu, _buttonQuit, Color.White);
                    spriteBatch.Draw(_texture, _posResume, _buttonResume, Color.White);
                    spriteBatch.Draw(_animation, _ringPosition, new Rectangle(_ringCurrentFrame.X * _ringFrameSize.X, _ringCurrentFrame.Y * _ringFrameSize.Y, _ringFrameSize.X, _ringFrameSize.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.2f);
                    break;
                case GameState.GAMEOVERMENU:
                    spriteBatch.Draw(_debugBox, _posQuitMainMenu, _buttonQuit, Color.White);
                    spriteBatch.Draw(_debugBox, _posStart, _buttonStart, Color.White);
                    spriteBatch.Draw(_debugBox, _posLoad, _buttonLoad, Color.White);
                    spriteBatch.Draw(_texture, _posStart, _buttonStart, Color.White);
                    spriteBatch.Draw(_texture, _posLoad, _buttonLoad, Color.White);
                    spriteBatch.Draw(_texture, _posQuitMainMenu, _buttonQuit, Color.White);
                    spriteBatch.Draw(_animation, _ringPosition, new Rectangle(_ringCurrentFrame.X * _ringFrameSize.X, _ringCurrentFrame.Y * _ringFrameSize.Y, _ringFrameSize.X, _ringFrameSize.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.2f); spriteBatch.Draw(_texture, _posOptions, _buttonOptions, Color.White);
                    break;
            }

        }
        private void MouseClick()
        {
            switch (_currentGameState)
            {
                case GameState.STARTMENU:
                    StartMenu();
                    break;
                case GameState.OPTIONSMENU:
                    OptionsMenu();
                    break;
                case GameState.CREDITSMENU:
                    CreditsMenu();
                    break;
                case GameState.INGAMEPAUSEMENU:
                    InGamePauseMenu();
                    break;
                case GameState.GAMEOVERMENU:
                    GameOverMenu();
                    break;
            }
        }
        private bool LeftMouseClick()
        {
            return (_prevMouseState.LeftButton == ButtonState.Released && _currMouseState.LeftButton == ButtonState.Pressed);
        }
        private bool EscapePressed()
        {
            return (_prevKeyboardState.IsKeyUp(Keys.Escape) && _currKeyboardState.IsKeyDown(Keys.Escape));
        }
        private void StartMenu()
        {
            if (_posStart.Contains(_mousePosition) && LeftMouseClick())
            {
                _currentGameState = GameState.INGAME;
            }
            else if (_posOptions.Contains(_mousePosition) && LeftMouseClick())
            {
                _previousGameState = _currentGameState;
                _currentGameState = GameState.OPTIONSMENU;
            }
            else if (_posCredits.Contains(_mousePosition) && LeftMouseClick())
            {
                _previousGameState = _currentGameState;
                _currentGameState = GameState.CREDITSMENU;
            }
            else if (_posSave.Contains(_mousePosition) && LeftMouseClick())
            {
                Debug.WriteLine("Hello World!");
            }
            else if (_posLoad.Contains(_mousePosition) && LeftMouseClick())
            {
                Debug.WriteLine("Hello World!");
            }
            else if (_posQuitMainMenu.Contains(_mousePosition) && LeftMouseClick())
            {
                _currentGameState = GameState.QUIT;
            }
        }
        private void OptionsMenu()
        {
            if (_posBack.Contains(_mousePosition) && LeftMouseClick() || EscapePressed())
            {
                FileManager.Save("test", "Empty");
                _currentGameState = _previousGameState;
            }
            else if (_posArrowRight.Contains(_mousePosition) && LeftMouseClick()
                && _index != 0)
            {
                _index++;
                SetScreenResolution(_index);
            }
            else if (_posArrowLeft.Contains(_mousePosition) && LeftMouseClick()
                && _index != 0)
            {
                _index--;
                SetScreenResolution(_index);
            }
        }
        private void CreditsMenu()
        {
            if (_posBack.Contains(_mousePosition) && LeftMouseClick() || EscapePressed())
            {
                _currentGameState = _previousGameState;
            }
        }
        private void InGamePauseMenu()
        {
            if (_posResume.Contains(_mousePosition) && LeftMouseClick() || EscapePressed())
            {
                _currentGameState = GameState.INGAME;
            }
            else if (_posOptions.Contains(_mousePosition) && LeftMouseClick())
            {
                _previousGameState = _currentGameState;
                _currentGameState = GameState.OPTIONSMENU;
            }
            else if (_posSave.Contains(_mousePosition) && LeftMouseClick())
            {
                Debug.WriteLine("Hello World!");
            }
            else if (_posLoad.Contains(_mousePosition) && LeftMouseClick())
            {
                Debug.WriteLine("Hello World!");
            }
            else if (_posQuitInGamePauseMenu.Contains(_mousePosition) && LeftMouseClick())
            {
                _currentGameState = GameState.QUIT;
            }
        }
        private void GameOverMenu()
        {
            if (_posStart.Contains(_mousePosition) && LeftMouseClick())
            {
                _currentGameState = GameState.STARTMENU;
            }
            else if (_posLoad.Contains(_mousePosition) && LeftMouseClick())
            {
                Debug.WriteLine("Hello World!");
            }
            else if (_posQuitMainMenu.Contains(_mousePosition) && LeftMouseClick())
            {
            }
        }
        private void SetScreenResolution(int index)
        {
            _graphics.PreferredBackBufferHeight = (int)_resolutionList[index].Y;
            _graphics.PreferredBackBufferWidth = (int)_resolutionList[index].X;
            SetPos();
            _graphics.ApplyChanges();

        }
        private void PopulateResolutionList()
        {
            _resolutionList.Add(new Vector2(550, 480));//0
            _resolutionList.Add(new Vector2(600, 400));//1
            _resolutionList.Add(new Vector2(640, 480));//2
            _resolutionList.Add(new Vector2(750, 530));//3
            _resolutionList.Add(new Vector2(800, 600));//4
            _resolutionList.Add(new Vector2(960, 640));//5
            _resolutionList.Add(new Vector2(1024, 768));//6
            _resolutionList.Add(new Vector2(1280, 720));//7
            _resolutionList.Add(new Vector2(1680, 1050));//8
            _resolutionList.Add(new Vector2(672, 704));//9
            _resolutionList.Add(new Vector2(800, 480));//10
        }
        private void SetPos()
        {
            _posStart = new Rectangle(_graphics.PreferredBackBufferWidth / 2 - _buttonStart.Width / 2, _graphics.PreferredBackBufferHeight / 6, 68, 26);
            _posOptions = new Rectangle(_graphics.PreferredBackBufferWidth / 2 - _buttonOptions.Width / 2, _posStart.Y + 92, 106, 32);
            _posCredits = new Rectangle(_graphics.PreferredBackBufferWidth / 2 - _buttonCredits.Width / 2, _posOptions.Y + 52, 99, 26);
            _posSave = new Rectangle(_graphics.PreferredBackBufferWidth / 2 - _buttonSave.Width / 2 + 46, _posStart.Y + 46, 63, 26);
            _posLoad = new Rectangle(_graphics.PreferredBackBufferWidth / 2 - _buttonLoad.Width / 2 - 46, _posStart.Y + 46, 72, 26);
            _posQuitMainMenu = new Rectangle(_graphics.PreferredBackBufferWidth / 2 - _buttonQuit.Width / 2, _posCredits.Y + 46, 63, 31);
            _posQuitInGamePauseMenu = new Rectangle(_graphics.PreferredBackBufferWidth / 2 - _buttonQuit.Width / 2, _posOptions.Y + 52, 63, 31);
            _posResume = new Rectangle(_graphics.PreferredBackBufferWidth / 2 - _buttonResume.Width / 2, _graphics.PreferredBackBufferHeight / 6, 106, 26);
            _posBack = new Rectangle(_graphics.PreferredBackBufferWidth / 4, _graphics.PreferredBackBufferHeight - _graphics.PreferredBackBufferHeight / 8, 70, 26);
            _posArrowRight = new Rectangle(_graphics.PreferredBackBufferWidth / 2 - _buttonArrowRight.Width / 2 + 44, _posStart.Y + 46, 47, 22);
            _posArrowLeft = new Rectangle(_graphics.PreferredBackBufferWidth / 2 - _buttonArrowLeft.Width / 2 - 44, _posStart.Y + 46, 47, 22);
            _pointMiddle = new Point(_graphics.PreferredBackBufferWidth / 2, _posCredits.Y + 46 - _posStart.Y);
        }
        private void MouseAnimation(GameTime gameTime)
        {
            _ringPosition = new Vector2(_posStart.X, _posStart.Y - 75);
            _ringTimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (_ringTimeSinceLastFrame > _ringMillisecondsPerFrame)
            {
                _ringTimeSinceLastFrame -= _ringMillisecondsPerFrame;
                ++_ringCurrentFrame.X;
                if (_ringCurrentFrame.X > _ringSheetSize.X)
                {
                    _ringCurrentFrame.X = 0;
                    ++_ringCurrentFrame.Y;
                    if (_ringCurrentFrame.Y > _ringSheetSize.Y)
                        _ringCurrentFrame.Y = 0;
                }
            }
        }
    }
}


//TO DO LIST
//INPLEMENT SAVE/LOAD



/*
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMario
{
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StartMenu
{
    public class MenuManager
    {
        GraphicsDeviceManager graphics;
        SpriteFont spriteFont;
        Texture2D texture;
        Texture2D animation;
        Rectangle posStart;
        Rectangle posOptions;
        Rectangle posCredits;
        Rectangle posSave;
        Rectangle posLoad;
        Rectangle posQuitMainMenu;
        Rectangle posQuitInGamePauseMenu;
        Rectangle posResume;
        Rectangle posBack;
        Rectangle posArrowRight;
        Rectangle posArrowLeft;
        Rectangle buttonStart;
        Rectangle buttonOptions;
        Rectangle buttonCredits;
        Rectangle buttonSave;
        Rectangle buttonLoad;
        Rectangle buttonQuit;
        Rectangle buttonResume;
        Rectangle buttonBack;
        Rectangle buttonArrowRight;
        Rectangle buttonArrowLeft;
        MouseState prevMouseState;
        MouseState currMouseState;
        Vector2 mousePosition;
        Vector2 ringPosition;
        Point ringFrameSize = new Point(75, 75);
        Point ringCurrentFrame = new Point(0, 0);
        Point ringSheetSize = new Point(5, 7);
        int ringTimeSinceLastFrame = 0;
        int ringMillisecondsPerFrame = 32;
        List<Vector2> resolutionList;
        bool start;
        bool exit;
        int index;
        enum GameState { STARTMENU, OPTIONSMENU, CREDITSMENU, INGAMEPAUSEMENU, GAMEOVERMENU };
        GameState currentGameState;
        GameState previousGameState;
        Texture2D debugBox;
        public MenuManager(GraphicsDeviceManager graphicsDeviceManager, Texture2D texture, SpriteFont spriteFont, Texture2D animation, Texture2D debugBox)
        {
            graphics = graphicsDeviceManager;
            Initialize();
            this.debugBox = debugBox;
            this.texture = texture;
            this.spriteFont = spriteFont;
            this.animation = animation;
        }
        private void Initialize()
        {
            index = 0;//Krävs för första starten//changed??
            exit = false;
            resolutionList = new List<Vector2>();
            PopulateResolutionList();
            SetScreenResolution(FileManager.Load("Options"));
            index = FileManager.Load("test");
            buttonStart = new Rectangle(0, 0, 68, 26);
            buttonOptions = new Rectangle(0, 31, 106, 32);
            buttonCredits = new Rectangle(0, 68, 99, 26);
            buttonSave = new Rectangle(113, 31, 63, 26);
            buttonLoad = new Rectangle(113, 62, 72, 26);
            buttonQuit = new Rectangle(0, 99, 63, 31);
            buttonResume = new Rectangle(113, 0, 106, 26);
            buttonBack = new Rectangle(113, 93, 70, 26);
            buttonArrowRight = new Rectangle(224, 0, 47, 22);
            buttonArrowLeft = new Rectangle(278, 0, 47, 22);
            currMouseState = Mouse.GetState();
        }
        public int Update(GameTime gameTime, int gameStateValue, int stateValue)
        {
            currMouseState = Mouse.GetState();
            MouseAnimation(gameTime);
            MouseClick();
            if (currMouseState != prevMouseState)
            {
                mousePosition = new Vector2(currMouseState.X, currMouseState.Y);
            }
            prevMouseState = currMouseState;
            if (start == true)
            {
                start = false;
                return gameStateValue = 1;
            }
            else if (exit == true)
            {
                return gameStateValue = 2;
            }
            else
            {
                if (stateValue == 4)
                {
                    currentGameState = (GameState)stateValue;
                    stateValue = 0;
                }
                return gameStateValue = 0;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            switch (currentGameState)
            {
                case GameState.STARTMENU:

                    spriteBatch.Draw(debugBox, posStart, buttonStart, Color.White);
                    spriteBatch.Draw(debugBox, posOptions, buttonOptions, Color.White);
                    spriteBatch.Draw(debugBox, posLoad, buttonLoad, Color.White);
                    spriteBatch.Draw(debugBox, posSave, buttonSave, Color.White);
                    spriteBatch.Draw(debugBox, posCredits, buttonCredits, Color.White);
                    spriteBatch.Draw(debugBox, posQuitMainMenu, buttonQuit, Color.White);
                    spriteBatch.Draw(texture, posStart, buttonStart, Color.White);
                    spriteBatch.Draw(texture, posOptions, buttonOptions, Color.White);
                    spriteBatch.Draw(texture, posLoad, buttonLoad, Color.White);
                    spriteBatch.Draw(texture, posSave, buttonSave, Color.White);
                    spriteBatch.Draw(texture, posCredits, buttonCredits, Color.White);
                    spriteBatch.Draw(texture, posQuitMainMenu, buttonQuit, Color.White);
                    spriteBatch.Draw(animation, ringPosition, new Rectangle(ringCurrentFrame.X * ringFrameSize.X, ringCurrentFrame.Y * ringFrameSize.Y, ringFrameSize.X, ringFrameSize.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.2f);
                    break;
                case GameState.CREDITSMENU:
                    spriteBatch.Draw(animation, ringPosition, new Rectangle(ringCurrentFrame.X * ringFrameSize.X, ringCurrentFrame.Y * ringFrameSize.Y, ringFrameSize.X, ringFrameSize.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.2f);
                    spriteBatch.Draw(debugBox, posBack, buttonBack, Color.White);
                    spriteBatch.Draw(texture, posBack, buttonBack, Color.White);
                    spriteBatch.DrawString(spriteFont, "Created by Erik Broman", new Vector2(graphics.PreferredBackBufferWidth / 2 - spriteFont.MeasureString("Created by Erik Broman").X / 2, graphics.PreferredBackBufferHeight / 2), Color.Black);
                    spriteBatch.Draw(animation, ringPosition, new Rectangle(ringCurrentFrame.X * ringFrameSize.X, ringCurrentFrame.Y * ringFrameSize.Y, ringFrameSize.X, ringFrameSize.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.2f);
                    break;
                case GameState.OPTIONSMENU:// add more if possible
                    spriteBatch.Draw(debugBox, posBack, buttonBack, Color.White);
                    spriteBatch.Draw(debugBox, posArrowRight, buttonArrowRight, Color.White);
                    spriteBatch.Draw(debugBox, posArrowLeft, buttonArrowLeft, Color.White);
                    spriteBatch.DrawString(spriteFont, "Resolution: " + resolutionList[index], new Vector2(graphics.PreferredBackBufferWidth / 2 - spriteFont.MeasureString("Resolution: " + resolutionList[index]).X / 2, posStart.Y), Color.Black);
                    spriteBatch.Draw(texture, posBack, buttonBack, Color.White);
                    spriteBatch.Draw(texture, posArrowRight, buttonArrowRight, Color.White);
                    spriteBatch.Draw(texture, posArrowLeft, buttonArrowLeft, Color.White);
                    spriteBatch.Draw(animation, ringPosition, new Rectangle(ringCurrentFrame.X * ringFrameSize.X, ringCurrentFrame.Y * ringFrameSize.Y, ringFrameSize.X, ringFrameSize.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.2f);
                    break;
                case GameState.INGAMEPAUSEMENU:
                    spriteBatch.Draw(debugBox, posOptions, buttonOptions, Color.White);
                    spriteBatch.Draw(debugBox, posLoad, buttonLoad, Color.White);
                    spriteBatch.Draw(debugBox, posSave, buttonSave, Color.White);
                    spriteBatch.Draw(debugBox, posQuitInGamePauseMenu, buttonQuit, Color.White);
                    spriteBatch.Draw(debugBox, posResume, buttonResume, Color.White);
                    spriteBatch.Draw(texture, posOptions, buttonOptions, Color.White);
                    spriteBatch.Draw(texture, posLoad, buttonLoad, Color.White);
                    spriteBatch.Draw(texture, posSave, buttonSave, Color.White);
                    spriteBatch.Draw(texture, posQuitInGamePauseMenu, buttonQuit, Color.White);
                    spriteBatch.Draw(texture, posResume, buttonResume, Color.White);
                    spriteBatch.Draw(animation, ringPosition, new Rectangle(ringCurrentFrame.X * ringFrameSize.X, ringCurrentFrame.Y * ringFrameSize.Y, ringFrameSize.X, ringFrameSize.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.2f);
                    break;
                case GameState.GAMEOVERMENU:
                    spriteBatch.Draw(debugBox, posQuitMainMenu, buttonQuit, Color.White);
                    spriteBatch.Draw(debugBox, posStart, buttonStart, Color.White);
                    spriteBatch.Draw(debugBox, posLoad, buttonLoad, Color.White);
                    spriteBatch.Draw(texture, posStart, buttonStart, Color.White);
                    spriteBatch.Draw(texture, posLoad, buttonLoad, Color.White);
                    spriteBatch.Draw(texture, posQuitMainMenu, buttonQuit, Color.White);
                    spriteBatch.Draw(animation, ringPosition, new Rectangle(ringCurrentFrame.X * ringFrameSize.X, ringCurrentFrame.Y * ringFrameSize.Y, ringFrameSize.X, ringFrameSize.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.2f); spriteBatch.Draw(texture, posOptions, buttonOptions, Color.White);
                    break;
            }

        }
        private void MouseClick()
        {
            switch (currentGameState)
            {
                case GameState.STARTMENU:
                    StartMenu();
                    break;
                case GameState.OPTIONSMENU:
                    OptionsMenu();
                    break;
                case GameState.CREDITSMENU:
                    CreditsMenu();
                    break;
                case GameState.INGAMEPAUSEMENU:
                    InGamePauseMenu();
                    break;
                case GameState.GAMEOVERMENU:
                    GameOverMenu();
                    break;
            }
        }
        private bool LeftMouseClick()
        {
            return (prevMouseState.LeftButton == ButtonState.Released && currMouseState.LeftButton == ButtonState.Pressed);
        }
        private void StartMenu()
        {
            if (posStart.Contains(mousePosition) && LeftMouseClick())
            {
                start = true;
                currentGameState = GameState.INGAMEPAUSEMENU;
            }
            else if (posOptions.Contains(mousePosition) && LeftMouseClick())
            {
                previousGameState = currentGameState;
                currentGameState = GameState.OPTIONSMENU;
            }
            else if (posCredits.Contains(mousePosition) && LeftMouseClick())
            {
                previousGameState = currentGameState;
                currentGameState = GameState.CREDITSMENU;
            }
            else if (posSave.Contains(mousePosition) && LeftMouseClick())
            {
                Debug.WriteLine("Hello World!");
            }
            else if (posLoad.Contains(mousePosition) && LeftMouseClick())
            {
                Debug.WriteLine("Hello World!");
            }
            else if (posQuitMainMenu.Contains(mousePosition) && LeftMouseClick())
            {
                exit = true;
            }
        }
        private void OptionsMenu()
        {
            if (posBack.Contains(mousePosition) && LeftMouseClick())
            {
                FileManager.Save("test", "Empty");
                currentGameState = previousGameState;
            }
            else if (posArrowRight.Contains(mousePosition) && LeftMouseClick()
                && index != 0)
            {
                index++;
                SetScreenResolution(index);
            }
            else if (posArrowLeft.Contains(mousePosition) && LeftMouseClick()
                && index != 0)
            {
                index--;
                SetScreenResolution(index);
            }
        }
        private void CreditsMenu()
        {
            if (posBack.Contains(mousePosition) && LeftMouseClick())
            {
                currentGameState = previousGameState;
            }
        }
        private void InGamePauseMenu()
        {
            if (posResume.Contains(mousePosition) && LeftMouseClick())
            {
                currentGameState = GameState.STARTMENU;
                start = true;
            }
            else if (posOptions.Contains(mousePosition) && LeftMouseClick())
            {
                previousGameState = currentGameState;
                currentGameState = GameState.OPTIONSMENU;
            }
            else if (posSave.Contains(mousePosition) && LeftMouseClick())
            {
                Debug.WriteLine("Hello World!");
            }
            else if (posLoad.Contains(mousePosition) && LeftMouseClick())
            {
                Debug.WriteLine("Hello World!");
            }
            else if (posQuitInGamePauseMenu.Contains(mousePosition) && LeftMouseClick())
            {
                exit = true;
            }
        }
        private void GameOverMenu()
        {
            if (posStart.Contains(mousePosition) && LeftMouseClick())
            {
                start = true;
                currentGameState = GameState.STARTMENU;
            }
            else if (posLoad.Contains(mousePosition) && LeftMouseClick())
            {
                Debug.WriteLine("Hello World!");
            }
            else if (posQuitMainMenu.Contains(mousePosition) && LeftMouseClick())
            {
                exit = true;
            }
        }
        private void SetScreenResolution(int index)
        {
            graphics.PreferredBackBufferHeight = (int)resolutionList[index].Y;
            graphics.PreferredBackBufferWidth = (int)resolutionList[index].X;
            SetPos();
            graphics.ApplyChanges();

        }
        private void PopulateResolutionList()
        {
            resolutionList.Add(new Vector2(550, 480));//0
            resolutionList.Add(new Vector2(600, 400));//1
            resolutionList.Add(new Vector2(640, 480));//2
            resolutionList.Add(new Vector2(750, 530));//3
            resolutionList.Add(new Vector2(800, 600));//4
            resolutionList.Add(new Vector2(960, 640));//5
            resolutionList.Add(new Vector2(1024, 768));//6
            resolutionList.Add(new Vector2(1280, 720));//7
            resolutionList.Add(new Vector2(1680, 1050));//8
            resolutionList.Add(new Vector2(672, 704));//9
        }
        private void SetPos()
        {
            posStart = new Rectangle(graphics.PreferredBackBufferWidth / 2 - buttonStart.Width / 2, graphics.PreferredBackBufferHeight / 6, 68, 26);
            posOptions = new Rectangle(graphics.PreferredBackBufferWidth / 2 - buttonOptions.Width / 2, posStart.Y + 92, 106, 32);
            posCredits = new Rectangle(graphics.PreferredBackBufferWidth / 2 - buttonCredits.Width / 2, posOptions.Y + 52, 99, 26);
            posSave = new Rectangle(graphics.PreferredBackBufferWidth / 2 - buttonSave.Width / 2 + 46, posStart.Y + 46, 63, 26);
            posLoad = new Rectangle(graphics.PreferredBackBufferWidth / 2 - buttonLoad.Width / 2 - 46, posStart.Y + 46, 72, 26);
            posQuitMainMenu = new Rectangle(graphics.PreferredBackBufferWidth / 2 - buttonQuit.Width / 2, posCredits.Y + 46, 63, 31);
            posQuitInGamePauseMenu = new Rectangle(graphics.PreferredBackBufferWidth / 2 - buttonQuit.Width / 2, posOptions.Y + 52, 63, 31);
            posResume = new Rectangle(graphics.PreferredBackBufferWidth / 2 - buttonResume.Width / 2, graphics.PreferredBackBufferHeight / 6, 106, 26);
            posBack = new Rectangle(graphics.PreferredBackBufferWidth / 4, graphics.PreferredBackBufferHeight - graphics.PreferredBackBufferHeight / 8, 70, 26);
            posArrowRight = new Rectangle(graphics.PreferredBackBufferWidth / 2 - buttonArrowRight.Width / 2 + 44, posStart.Y + 46, 47, 22);
            posArrowLeft = new Rectangle(graphics.PreferredBackBufferWidth / 2 - buttonArrowLeft.Width / 2 - 44, posStart.Y + 46, 47, 22);
        }
        private void MouseAnimation(GameTime gameTime)
        {
            ringPosition = new Vector2(posStart.X, posStart.Y - 75);
            ringTimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (ringTimeSinceLastFrame > ringMillisecondsPerFrame)
            {
                ringTimeSinceLastFrame -= ringMillisecondsPerFrame;
                ++ringCurrentFrame.X;
                if (ringCurrentFrame.X > ringSheetSize.X)
                {
                    ringCurrentFrame.X = 0;
                    ++ringCurrentFrame.Y;
                    if (ringCurrentFrame.Y > ringSheetSize.Y)
                        ringCurrentFrame.Y = 0;
                }
            }
        }
    }
}
}

//TO DO LIST
//INPLEMENT SAVE/LOAD
 */
