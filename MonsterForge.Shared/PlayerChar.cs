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
        private Weapon _weapon;
        private int _attackCooldownFramesRemaining = 0;
        private int _movementCooldownFramesRemaining = 0;
        private float _speed;

        
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

            Weapon sword = new Weapon("Sword", 1, 8f, 8, 8, 10);
            _weapon = sword;
        }

        public override void Update(GameTime gameTime)
        {
            if (_movementCooldownFramesRemaining <= 0)
            {
                _speed = 6;
            } else
            {
                _speed = 2;
            }

            Velocity = _speed * Input.GetMovementDirection();
            Position += Velocity;
            Position = Vector2.Clamp(Position, Size / 2, GameRoot.ScreenSize - Size / 2);

            Vector2 aim = Input.GetAimDirection();

            if (aim.LengthSquared() > 0)
            {
                Orientation = aim.ToAngle();
            }

            // cannot attack again unless he is back to 0 cooldown remaining
            if (Input.WasLightAttackPressed() && _attackCooldownFramesRemaining <= 0)
            {
                _weapon.LightAttack(Position, aim);
                _attackCooldownFramesRemaining = _weapon.LightAttackCooldown;
                _movementCooldownFramesRemaining = _weapon.LightAttackCooldown * 2;
            }

            if (_attackCooldownFramesRemaining > 0)
            {
                _attackCooldownFramesRemaining -= 1;
            }
            if (_movementCooldownFramesRemaining > 0)
            {
                _movementCooldownFramesRemaining -= 1;
            }
        }
    }
}
