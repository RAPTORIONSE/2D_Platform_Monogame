using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperMario
{
    public class Player : GameObject
    {
        KeyboardState _keyboardState;
        KeyboardState _prevKeyboardState;
        Vector2 _direction;
        Point _size;
        Vector2 _speed;
        bool _isOnGround;
        protected Rectangle _hitBox;
        public Player(Texture2D texture, Rectangle position, Vector2 location) : base(texture, position)
        {
            this._texture = texture;
            location = new Vector2(position.X, position.Y);
            _size = position.Size;
            _speed = new Vector2(1, 0);
            _isOnGround = false;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, _color);
        }
        public override void Update(GameTime gameTime)
        {
            _prevKeyboardState = _keyboardState;
            _keyboardState = Keyboard.GetState();
            if (_keyboardState.IsKeyDown(Keys.Left))
                _direction = new Vector2(-1, 0);
            else if (_keyboardState.IsKeyDown(Keys.Right))
                _direction = new Vector2(1, 0);

            if (_keyboardState.IsKeyDown(Keys.Space) && _isOnGround)
            {
                _speed.Y = -5;
                _isOnGround = false;
            }
            if (_speed.Y < 7)
                _speed.Y += 0.16366f;
            if (_speed.Y > 0.01f)
            {
                _isOnGround = false;
            }
            _location += new Vector2(_direction.X * _speed.X, _speed.Y);
            _position = new Rectangle(_location.ToPoint(), _size);
            _direction = Vector2.Zero;
            if (_location.Y > 1050)
            {
                _speed.Y = 0;
                _location.Y = 0;
                _position = new Rectangle(_location.ToPoint(), _size);
                _isOnGround = true;
            }
            _speed.X = 1;
        }
        public override void Collision(GameObject collision)
        {
            if (_position.Intersects(new Rectangle(collision._position.Left+1, collision._position.Top, collision._position.Size.X-2, 0)))
            {
                _location.Y = collision._position.Y - collision._position.Height + 0;
                _isOnGround = true;
                _speed.Y = 0;
            }
            if (_position.Intersects(new Rectangle(collision._position.Left+1, collision._position.Bottom, collision._position.Size.X-2, 0)))
            {
                _speed.Y = 0;
            }
            if (_position.Intersects(new Rectangle(collision._position.Left, collision._position.Top+1, 0, collision._position.Size.Y-2)))
            {
                _speed.X = 0;
            }
            if (_position.Intersects(new Rectangle(collision._position.Right, collision._position.Top+1, 0, collision._position.Size.Y-2)))
            {
                _speed.X = 0;
            }
        }
    }
}
