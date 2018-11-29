using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMario
{
    public static class ResourceManager
    {
        public static Game1 _Game;
        public static T Get<T>(string tag)
        {
            return _Game.Content.Load<T>(tag);
        }
    }
}
