using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEITGameEngine.Objects.Base;

namespace NEITGameEngine.Objects
{
    public class Turret:BaseGameObject
    {
        public Turret(Texture2D sprite)
        {
            _texture = sprite;
    }
    }
}
