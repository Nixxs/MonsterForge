using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterForge
{
    class Enemy : Entity
    {
        private int timeUntilStart = 60;
        private List<IEnumerator<int>> behaviours = new List<IEnumerator<int>>();

        private float _maxHealth;
        private float _health;

        public float Health
        {
            get
            {
                return _health;
            }
            private set
            {
                _health = value;
            }
        }

        public void UpdateHealth(float adjustment)
        {
            Health += adjustment;

            if (Health > _health)
            {
                Health = _maxHealth;
            }

            if (Health <= 0)
            {
                IsExpired = true;
            }
        }

        public bool IsActive
        {
            get
            {
                return timeUntilStart <= 0;
            }
        }

        public Enemy(Texture2D image, Vector2 position, float maxHealth)
        {
            this.image = image;
            Position = position;
            Radius = image.Width / 2f;
            _maxHealth = maxHealth;
            Health = _maxHealth;
            color = Color.Transparent;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                // enemy is active 
                ApplyBehaviors();
            }
            else
            {
                timeUntilStart -= 1;
                // this is the fade in based on the amount of time until start
                color = Color.White * (1 - timeUntilStart / 60f);
            }

            Position += Velocity;
            Position = Vector2.Clamp(Position, Size / 2, GameRoot.ScreenSize - Size / 2);

            Velocity *= 0.8f;
        }

        private void AddBehaviour(IEnumerable<int> behaviour)
        {
            behaviours.Add(behaviour.GetEnumerator());
        }

        // runs through one frame for each of the behaviours that the enemy has
        private void ApplyBehaviors()
        {
            for (int i = 0; i < behaviours.Count; i++)
            {
                // remove the behviour from the list if there is nothing left to do
                if (behaviours[i].MoveNext() == false)
                {
                    behaviours.RemoveAt(i--);
                }
            }
        }

        // COLLISION Handling
        public void HandleCollision(Enemy other)
        {
            // when running into another enemy bounce off in the other direction
            // force is larger the cloase they get
            Vector2 d = Position - other.Position;
            Velocity += 10 * d / (d.LengthSquared() + 1);
        }

        // CREATE ENEMY
        public static Enemy CreateSeeker(Vector2 position)
        {
            Enemy enemy = new Enemy(Art.Seeker, position, 5); // create enemy object
            enemy.AddBehaviour(enemy.FollowPlayer(0.45f)); // add the follow player behaviour

            return enemy;
        }

        // BEHAVIOURS
        IEnumerable<int> FollowPlayer(float acceleration = 1f)
        {
            while (true)
            {
                // apply velocity in the direction of the player
                Velocity += (PlayerChar.Instance.Position - Position).ScaleTo(acceleration);

                // rotate the image of the enemy
                if (Velocity != Vector2.Zero)
                {
                    Orientation = (PlayerChar.Instance.Position - Position).ToAngle();
                }

                yield return 0;
            }
        }
    }
}
