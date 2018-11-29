using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace SuperMario
{
    public class MapEditor
    {
        Vector2 _CameraPosition;
        MouseState _currMouseState;
        MouseState _prevMouseState;
        Vector2 _mousePosition;
        KeyboardState _currKeyboardState;
        Rectangle _buttonStart;
        Rectangle _buttonGoal;
        Rectangle _buttonPlatform;
        Texture2D _startTexture;
        Texture2D _goalTexture;
        Texture2D _platformTexture;
        StringBuilder _allPlatforms = new StringBuilder();

        enum _SelectedObject
        {
            _STARTSELECTED, _GOALSELECTED, _PLATFORMSELECTED
        }
        _SelectedObject _currentSelectedObject;
        List<GameObject> _gameObjects;
        public MapEditor()
        {
            _gameObjects = new List<GameObject>();
            _currentSelectedObject = _SelectedObject._STARTSELECTED;
            _CameraPosition = new Vector2(0, 0);
            _buttonStart = new Rectangle(0, 0, 10, 10);
            _buttonGoal = new Rectangle(0, 20, 10, 10);
            _buttonPlatform = new Rectangle(0, 40, 10, 10);
            _startTexture = ResourceManager.Get<Texture2D>("TestHitbox");
            _goalTexture = ResourceManager.Get<Texture2D>("cursor1");
            _platformTexture = ResourceManager.Get<Texture2D>("platform");
        }

        public string Update()
        {
            InputHandler();
            SelectObjectForPlacement();
            PlaceObject();
            RemoveObject();
            _prevMouseState = _currMouseState;
            return LeaveEditor();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DisplaySelectionChoices(spriteBatch);
            DisplaySelected(spriteBatch);
            if (_gameObjects.Count > 0)
            {
                foreach (var item in _gameObjects)
                {
                    item.Draw(spriteBatch);
                }
            }
        }

        private void RemoveObject()
        {
            if (RightMouseClick())
            {
                if (_gameObjects.Count > 0)
                {
                    foreach (var item in _gameObjects)
                    {
                        if (item._position.Contains(_CameraPosition + _mousePosition + new Vector2(-400, -240)))
                        {
                            if (item is Player)
                            {
                                if (!_gameObjects.OfType<Goal>().Any())
                                {
                                    _gameObjects.Remove(item);
                                }
                            }
                            else
                            {
                                _gameObjects.Remove(item);
                            }

                            break;
                        }
                    }
                }
            }
        }

        private void PlaceObject()
        {
            if (_currentSelectedObject == _SelectedObject._STARTSELECTED && !_gameObjects.OfType<Player>().Any() && LeftMouseClick())
            {
                _gameObjects.Add(new Player(_startTexture, new Rectangle((int)(_CameraPosition + _mousePosition + new Vector2(-400, -240)).X, (int)(_CameraPosition + _mousePosition + new Vector2(-400, -240)).Y, _startTexture.Width, _startTexture.Height), Vector2.Zero));
            }
            else if (_currentSelectedObject == _SelectedObject._GOALSELECTED && !_gameObjects.OfType<Goal>().Any() && _gameObjects.OfType<Player>().Any() && LeftMouseClick())
            {
                _gameObjects.Add(new Goal(_goalTexture, new Rectangle((int)(_CameraPosition + _mousePosition + new Vector2(-400, -240)).X, (int)(_CameraPosition + _mousePosition + new Vector2(-400, -240)).Y, _goalTexture.Width, _goalTexture.Height)));

            }
            else if (_currentSelectedObject == _SelectedObject._PLATFORMSELECTED && LeftMouseClick())
            {
                _gameObjects.Add(new Platform(_platformTexture, new Rectangle((int)(_CameraPosition + _mousePosition + new Vector2(-400, -240)).X, (int)(_CameraPosition + _mousePosition + new Vector2(-400, -240)).Y, _platformTexture.Width, _platformTexture.Height)));
            }
        }

        private void SelectObjectForPlacement()
        {
            if (LeftMouseClick() && _buttonStart.Contains(_mousePosition))
            {
                CheckingSelection("start");
            }
            if (LeftMouseClick() && _buttonGoal.Contains(_mousePosition))
            {
                CheckingSelection("goal");
            }
            if (LeftMouseClick() && _buttonPlatform.Contains(_mousePosition))
            {
                CheckingSelection("platform");
            }
        }

        private void CheckingSelection(string selection)
        {
            if (selection == "start")
            {
                _currentSelectedObject = _SelectedObject._STARTSELECTED;

            }
            else if (selection == "goal")
            {
                _currentSelectedObject = _SelectedObject._GOALSELECTED;
            }
            else if (selection == "platform")
            {
                _currentSelectedObject = _SelectedObject._PLATFORMSELECTED;
            }
        }

        private void DisplaySelected(SpriteBatch spriteBatch)
        {
            if (_currentSelectedObject == _SelectedObject._STARTSELECTED)
            {
                spriteBatch.Draw(_startTexture, _CameraPosition + _mousePosition + new Vector2(-400, -240), Color.White);
            }
            else if (_currentSelectedObject == _SelectedObject._GOALSELECTED)
            {
                spriteBatch.Draw(_goalTexture, _CameraPosition + _mousePosition + new Vector2(-400, -240), Color.White);
            }
            else if (_currentSelectedObject == _SelectedObject._PLATFORMSELECTED)
            {
                spriteBatch.Draw(_platformTexture, _CameraPosition + _mousePosition + new Vector2(-400, -240), Color.White);
            }
        }

        private void DisplaySelectionChoices(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_startTexture, new Vector2(_CameraPosition.X - 400, _CameraPosition.Y - 240), Color.White);
            spriteBatch.Draw(_goalTexture, new Vector2(_CameraPosition.X - 400, _CameraPosition.Y - 220), Color.White);
            spriteBatch.Draw(_platformTexture, new Vector2(_CameraPosition.X - 400, _CameraPosition.Y - 200), Color.White);
        }

        private void InputHandler()
        {
            _currKeyboardState = Keyboard.GetState();
            _currMouseState = Mouse.GetState();
            _mousePosition = new Vector2(_currMouseState.X, _currMouseState.Y);
        }

        public Vector2 CameraMovement()
        {
            Vector2 direction = Vector2.Zero;
            if (_currKeyboardState.IsKeyDown(Keys.W))
                direction = new Vector2(0, -1);
            else if (_currKeyboardState.IsKeyDown(Keys.S))
                direction = new Vector2(0, 1);
            if (_currKeyboardState.IsKeyDown(Keys.A))
                direction += new Vector2(-1, 0);
            else if (_currKeyboardState.IsKeyDown(Keys.D))
                direction += new Vector2(1, 0);
            if (_currKeyboardState.IsKeyDown(Keys.LeftShift))
                direction *= 5;
            return _CameraPosition = _CameraPosition + direction;
        }

        private bool LeftMouseClick()
        {
            return (_prevMouseState.LeftButton == ButtonState.Released && _currMouseState.LeftButton == ButtonState.Pressed);
        }

        private bool RightMouseClick()
        {
            return (_prevMouseState.RightButton == ButtonState.Released && _currMouseState.RightButton == ButtonState.Pressed);
        }

        private string LeaveEditor()
        {
            if (_currKeyboardState.IsKeyDown(Keys.Escape))
            {
                //create prompt for name, set defult to Map_1_x
                List<string> mapNumbers = new List<string>();
                string start;
                string goal;
                try
                {
                    for (int i = 0; i < _gameObjects.Count; i++)
                    {
                        if (_gameObjects.ElementAt(i) is Player)
                        {
                            int x = (_gameObjects.ElementAt(i) as Player)._position.X;
                            int y = (_gameObjects.ElementAt(i) as Player)._position.Y;
                            int w = (_gameObjects.ElementAt(i) as Player)._position.Width;
                            int h = (_gameObjects.ElementAt(i) as Player)._position.Height;

                            start = (x.ToString() + "," + y.ToString() + "," + w.ToString() + "," + h.ToString() + ";\n");
                            _allPlatforms.Append(start);
                        }
                        else if (_gameObjects.ElementAt(i) is Platform)
                        {
                            int x = (_gameObjects.ElementAt(i) as Platform)._position.X;
                            int y = (_gameObjects.ElementAt(i) as Platform)._position.Y;
                            int w = (_gameObjects.ElementAt(i) as Platform)._position.Width;
                            int h = (_gameObjects.ElementAt(i) as Platform)._position.Height;

                            mapNumbers.Add(x.ToString() + "," + y.ToString() + "," + w.ToString() + "," + h.ToString() + ";");

                        }
                        else if (_gameObjects.ElementAt(i) is Goal)
                        {
                            int x = (_gameObjects.ElementAt(i) as Goal)._position.X;
                            int y = (_gameObjects.ElementAt(i) as Goal)._position.Y;
                            int w = (_gameObjects.ElementAt(i) as Goal)._position.Width;
                            int h = (_gameObjects.ElementAt(i) as Goal)._position.Height;

                            goal = (x.ToString() + "," + y.ToString() + "," + w.ToString() + "," + h.ToString() + ";\n");
                            _allPlatforms.Append(goal);
                        }
                    }

                    foreach (string platformPosition in mapNumbers)
                    {
                        _allPlatforms.Append(platformPosition);
                    }
                    string result = _allPlatforms.ToString();
                    FileManager.Save("Map_1_0", result);
                }
                catch (Exception)
                {
                    Console.WriteLine("Looks like there is something missing.");
                }
                return "STARTMENU";
            }
            return "MAPEDIT";
        }

    }
}
/*
Exit editor
Save Level
scale objects mouse wheel or drag toolbar
Fix background

 */
