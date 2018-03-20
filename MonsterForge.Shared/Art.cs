using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterForge
{
    static class Art
    {
        public static Texture2D _player;
        public static Texture2D _pointer;
        public static Texture2D _bullet;
        public static Texture2D _seeker;

        public static Texture2D Player
        {
            get
            {
                return _player;
            }

            private set
            {
                _player = value;
            }
        }

        public static Texture2D Pointer
        {
            get
            {
                return _pointer;
            }

            private set
            {
                _pointer = value;
            }
        }

        public static Texture2D Bullet
        {
            get
            {
                return _bullet;
            }

            private set
            {
                _bullet = value;
            }
        }

        public static Texture2D Seeker
        {
            get
            {
                return _seeker;
            }

            private set
            {
                _seeker = value;
            }
        }

        public static void Load(GameRoot instance)
        {
            Player = instance.Content.Load<Texture2D>("Assets\\Art\\Player");
            Pointer = instance.Content.Load<Texture2D>("Assets\\Art\\Pointer");
            Bullet = instance.Content.Load<Texture2D>("Assets\\Art\\Bullet");
            Seeker = instance.Content.Load<Texture2D>("Assets\\Art\\Seeker");
        }
    }
}
