using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace SuperMario
{
    class Camera
    {
        private Matrix _transform;
        private Vector2 _position;
        private Viewport _view;
        public Camera(Viewport view)
        {
            this._view = view;
        }
        public void SetPosition(Vector2 position)
        {
            this._position = position;//linjer interpolation look up

            _transform = Matrix.CreateTranslation(new Vector3(-position, 0))* Matrix.CreateTranslation(new Vector3(_view.Width/2,_view.Height/2,-0));
        }
        public Matrix GetPosition()
        {
            return _transform;
        }
    }
}