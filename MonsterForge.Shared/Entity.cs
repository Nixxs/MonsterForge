using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonsterForge
{
    // abstract class means it is a parent class only and cannot be instantiated
    // by it self. It needs to be inherited by a child class.
    abstract class Entity
    {
        protected Texture2D image;
        protected Color color = Color.White;

        public Vector2 Position;
        public Vector2 Velocity;

        public float Orientation;
        public float Radius = 20;
        public bool IsExpired;

        public Vector2 Size
        {
            get
            {
                if (image == null)
                {
                    return Vector2.Zero;
                }
                else
                {
                    return new Vector2(image.Width, image.Height);
                }
            }
        }

        // this method abstract must be implemented by the child class
        public abstract void Update();

        // virtual methods can be overritten by the child class. If they are
        // not overwritten then this code is executed.
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, null, color, Orientation, Size/2f, 1f, 0, 0);
        }
    }
}
