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
    public abstract class GameObject
    {
        protected Texture2D _texture;
        public Rectangle _position;
        protected Color _color;
        public Vector2 _location;

        public GameObject(Texture2D texture, Rectangle position)
        {
            this._texture = texture;
            this._position = position;
            _location = new Vector2(position.X, position.Y);
            _color = Color.White;
        }
    public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, _color);
        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Collision(GameObject gameObject)
        {

        }
    }
}
