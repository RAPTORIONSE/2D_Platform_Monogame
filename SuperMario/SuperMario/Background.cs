using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SuperMario
{
    public class Background
    {
        List<Vector2> _close, _middle, _far;
        int _closeSpace, _middleSpace, _farSpace;
        float _closeSpeed, _middleSpeed, _farSpeed;//make public or add bool to decide movement direction of things
        Viewport _viewport;
        public Background(Viewport viewport)
        {
            this._viewport = viewport;
            Initialize();
        }
        private void Initialize()
        {
            _close = new List<Vector2>();
            _closeSpace = ResourceManager.Get<Texture2D>("Ground").Width;
            _closeSpeed = 0.8f;
            _middle = new List<Vector2>();
            _middleSpace = _viewport.Width / 5;
            _middleSpeed = 02f;
            _far = new List<Vector2>();
            _farSpace = _viewport.Width / 3;
            _farSpeed = 0.4f;
            for (int i = 0; i < (_viewport.Width / _closeSpace) + 2; i++)
            {
                _close.Add(new Vector2(i * _closeSpace, _viewport.Height - ResourceManager.Get<Texture2D>("Ground").Height));
            }
            for (int i = 0; i < (_viewport.Width / _middleSpace) + 2; i++)
            {
                _middle.Add(new Vector2(i * _middleSpace, _viewport.Height / 2 - ResourceManager.Get<Texture2D>("Ground").Height - ResourceManager.Get<Texture2D>("Cloud").Height));
            }
            for (int i = 0; i < (_viewport.Width / _farSpace) + 2; i++)
            {
                _far.Add(new Vector2(i * _farSpace, _viewport.Height / 2 - ResourceManager.Get<Texture2D>("Ground").Height - (int)(ResourceManager.Get<Texture2D>("Cloud").Height * 1.5)));
            }
        }
        public void Update()
        {
            for (int i = 0; i < _close.Count; i++)
            {
                _close[i] = new Vector2(_close[i].X - _closeSpeed, _close[i].Y);
                if (_close[i].X <= -_closeSpace)
                {
                    int j = i - 1;
                    if (j < 0)
                    {
                        j = _close.Count - 1;
                    }

                    _close[i] = new Vector2(_close[j].X + _closeSpace - 1, _close[i].Y);
                }
            }
            for (int i = 0; i < _middle.Count; i++)
            {
                _middle[i] = new Vector2(_middle[i].X - _middleSpeed, _middle[i].Y);
                if (_middle[i].X <= -_middleSpace)
                {
                    int j = i - 1;
                    if (j < 0)
                    {
                        j = _middle.Count - 1;
                    }
                    _middle[i] = new Vector2(_middle[j].X + _middleSpace - 1, _middle[i].Y);
                }
            }

            for (int i = 0; i < _far.Count; i++)
            {
                _far[i] = new Vector2(_far[i].X - _farSpeed, _far[i].Y);
                if (_far[i].X <= -_farSpace)
                {
                    int j = i - 1;
                    if (j < 0)
                    {
                        j = _far.Count - 1;
                    }
                    _far[i] = new Vector2(_far[j].X + _farSpace - 1, _far[i].Y);
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in _close)
            {
                spriteBatch.Draw(ResourceManager.Get<Texture2D>("Ground"), item, Color.White);
            }
            foreach (var item in _middle)
            {
                spriteBatch.Draw(ResourceManager.Get<Texture2D>("Cloud"), item, Color.White);
            }
            foreach (var item in _far)
            {
                spriteBatch.Draw(ResourceManager.Get<Texture2D>("Cloud"), item, Color.White);
            }
        }

    }
}