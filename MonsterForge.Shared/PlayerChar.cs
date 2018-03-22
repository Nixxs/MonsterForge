using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterForge
{
    class PlayerChar : Entity
    {
        private static PlayerChar _instance;

        public static PlayerChar Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlayerChar();
                }

                return _instance;
            }
        }

        // values set here are defined by the parent class
        private PlayerChar()
        {
            image = Art.Player; // here we can use the very convieniant art class to get the image
            Position = GameRoot.ScreenSize / 2; // the centre of the screen
            Radius = 10; // size of the player character
        }

        public override void Update(GameTime gameTime)
        {
            const float speed = 6;

            Velocity = speed * Input.GetMovementDirection();
            Position += Velocity;

            Position = Vector2.Clamp(Position, Size / 2, GameRoot.ScreenSize - Size / 2);

            if (Input.GetAimDirection().LengthSquared() > 0)
            {
                Orientation = Input.GetAimDirection().ToAngle();
            }
        }
    }
}
