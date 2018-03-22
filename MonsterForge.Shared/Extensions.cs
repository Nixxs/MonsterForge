using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterForge
{
    public static class Extensions
    {
        public static float ToAngle(this Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }
    }
}
