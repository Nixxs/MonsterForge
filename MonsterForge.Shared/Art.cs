using Microsoft.Xna.Framework.Graphics;

namespace MonsterForge
{
    /// <summary>
    /// A class for loading and storing all the textures in the game
    /// like the player, enemies and bullets. a static class cannot and
    /// does not need to be instantiated before execution.
    /// </summary>
    static class Art
    {
        private static Texture2D _player;
        private static Texture2D _seeker;
        private static Texture2D _bullet;
        private static Texture2D _pointer;
        private static Texture2D _hitbox;
        private static SpriteFont _debugFont;

        public static Texture2D Player
        {
            get { return _player; }
        }

        public static Texture2D Seeker
        {
            get { return _seeker; }
        }

        public static Texture2D Bullet
        {
            get { return _bullet; }
        }

        public static Texture2D Pointer
        {
            get { return _pointer; }
        }

        public static SpriteFont DebugFont
        {
            get { return _debugFont; }
        }

        public static Texture2D Hitbox
        {
            get { return _hitbox; }
        }

        /// <summary>
        /// A helpful and tidy load method to load all the textures from a one line
        /// Art.Load() in the LoadContent() of GameRoot
        /// </summary>
        /// <param name="game"></param>
        public static void Load(GameRoot instance)
        {
            _player = instance.Content.Load<Texture2D>("Art\\Player");
            _seeker = instance.Content.Load<Texture2D>("Art\\Seeker");
            _bullet = instance.Content.Load<Texture2D>("Art\\Bullet");
            _pointer = instance.Content.Load<Texture2D>("Art\\Pointer");
            _hitbox = instance.Content.Load<Texture2D>("Art\\Wanderer");
            _debugFont = instance.Content.Load<SpriteFont>("DebugFont");
        }
    }
}
