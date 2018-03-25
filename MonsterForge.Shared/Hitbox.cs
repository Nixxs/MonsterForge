using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterForge
{
    class Hitbox : Entity
    {
        private int Frames;

        /// <summary>
        /// A hitbox for collision with enemies
        /// </summary>
        /// <param name="position">starting position of the hitbox</param>
        /// <param name="velocity">the speed of the hitbox movement</param>
        /// <param name="frames">number of frames the hitbox lasts for</param>
        /// <param name="radius">the size of the hitbox</param>
        public Hitbox (Texture2D texture, Vector2 position, Vector2 velocity, int frames, int radius)
        {
            image = texture;
            Position = position;
            Velocity = velocity;
            Orientation = Velocity.ToAngle();
            Radius = radius;

            Frames = frames;
        }

        public override void Update(GameTime gameTime)
        {
            if (Velocity.LengthSquared() > 0)
            {
                Orientation = Velocity.ToAngle();
            }

            Position += Velocity;

            //Delete hitbox after lifespan has expired
            if (Frames <= 0)
            {
                IsExpired = true;
            }

            // reduce lifespan by 1 frame each frame
            Frames -= 1;
        }
    }
}
