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
    public class Platform : GameObject
    {
        public Platform(Texture2D texture, Rectangle position) :base (texture,position)
        {
            this._texture = texture;
            this._position = position;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture/*ResourceManager.Get<Texture2D>("TestHitbox")*/, _position, _color);
        }
    }
}

/*
rec är hit, kolla top bot pos i förhållande 
 */