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
    public class GameManager : DrawableGameComponent
    {
        List<GameObject> _staticGameObjects;
        List<GameObject> _movingGameObjects;
        List<GameObject> _allGameObjects;
        public Player _player;
        Texture2D _texturePlatform;
        KeyboardState _currKeyboardState;
        double _pauseTimer;
        public GameManager(Game1 game1, int World, int Map) : base(game1)
        {
            _texturePlatform = ResourceManager.Get<Texture2D>("platform");
            _staticGameObjects = new List<GameObject>();//do we need this list?
            _movingGameObjects = new List<GameObject>();
            _allGameObjects = new List<GameObject>();

            _allGameObjects.AddRange(FileManager.World(World, Map));
            for (int i = 0; i < _allGameObjects.Count; i++)
            {
                if (_allGameObjects.ElementAt(i) is Platform)
                {
                    _staticGameObjects.Add(_allGameObjects.ElementAt(i));
                }
                else if (_allGameObjects.ElementAt(i) is Player)
                {
                    _movingGameObjects.Add(_allGameObjects.ElementAt(i));
                    _player = _allGameObjects.ElementAt(i) as Player;
                }
            }
            _currKeyboardState = Keyboard.GetState();
            _pauseTimer = 0;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in _allGameObjects)
            {
                item.Draw(spriteBatch);
            }
        }
        public new string Update(GameTime gameTime)
        {
            foreach (var item in _movingGameObjects)
            {
                foreach (var other in _allGameObjects)
                {
                    if (item._position.Intersects(other._position) && other != item)
                    {
                        item.Collision(other);
                    }
                    
                }
                item.Update(gameTime);
            }
            //Pause game with escape
            _currKeyboardState = Keyboard.GetState();
            _pauseTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_currKeyboardState.IsKeyDown(Keys.Escape) && _pauseTimer <= 0)
            {
                _pauseTimer = 250;
                return "INGAMEPAUSEMENU";
            }
            return "INGAME";
        }
    }
}
