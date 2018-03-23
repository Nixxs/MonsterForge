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
        private Vector2 _friction;
        private float _maxSpeed;

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

            _weapon = new Weapons.Sword("Sword", 1, 8f, 8, 8, 10);
            _maxSpeed = 14; // the players max movement speed
        }

        public override void Update(GameTime gameTime)
        {
            // when the player has movement on cooldown he cant move as responsively
            // this is added after the player attacks
            if (_movementCooldownFramesRemaining <= 0)
            {
                _friction = new Vector2(0.1f,0.1f);
            } else
            {
                _friction = new Vector2(0.5f, 0.5f);
            }

            // add to the velocity of the player if the player isn't already on max speed
            if (Velocity.LengthSquared() < _maxSpeed)
            {
                Velocity = Velocity + Input.GetMovementDirection();
            } 
            
            Velocity -= Velocity * _friction; // simulate friction
            Position += Velocity; // update the players position
            Position = Vector2.Clamp(Position, Size / 2, GameRoot.ScreenSize - Size / 2); // keeps the player inside the bounds of the screen

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
                // add a movement cooldown that is twice the length of the weapon attack cooldown
                _movementCooldownFramesRemaining = _weapon.LightAttackCooldown * 2;
            }

            // if there coooldown frames have been added, reduce the cooldown by one frame
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
