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

        private float _baseSpeed;
        private float _maxSpeed;

        private float _maxStamina;
        private float _stamina;

        private float _maxHealth;
        private float _health;

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

        public float Stamina
        {
            get
            {
                return _stamina;
            }
            private set
            {
                _stamina = value;
            }
        }

        private void UpdateStamina(float adjustment)
        {
            Stamina = Stamina + adjustment;

            if (Stamina > _maxStamina)
            {
                Stamina = _maxStamina;
            }

            if (Stamina < 0)
            {
                Stamina = 0;
            }
        }

        private void UpdateHealth(float adjustment)
        {
            Health = Health + adjustment;

            if (Health > _maxHealth)
            {
                Health = _maxHealth;
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            _weapon = weapon;
        }

        // values set here are defined by the parent class
        private PlayerChar()
        {
            image = Art.Player; // here we can use the very convieniant art class to get the image
            Position = GameRoot.ScreenSize / 2; // the centre of the screen
            Radius = 10; // size of the player character
            _maxHealth = 100f;
            Health = _maxHealth;
            _maxStamina = 100f;
            Stamina = _maxStamina;

            EquipWeapon(new Weapons.Sword("Sword", 1, 8f, 8, 8, 10, 3, 21f, 8, 8, 20));
            _baseSpeed = 14; // the players max movement speed
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 movementDirection = Input.GetMovementDirection();
            Vector2 aim = Input.GetAimDirection();

            // stamina costs
            float DashStaminaCost = 25f;
            float LightAttackStaminaCost = 20f;
            float HeavyAttackStaminaCost = 40f;
            float SprintStaminaCost = 1.5f;

            //if sprint button is held down player is sprinting so increase max speed
            if (Input.IsSprintPressed() && Stamina >= SprintStaminaCost)
            {
                _maxSpeed = _baseSpeed * 4f;
                UpdateStamina(-SprintStaminaCost);
            }
            else
            {
                _maxSpeed = _baseSpeed;
            }

            // check if the player dashed
            if (Input.WasDashPressed() && Stamina >= DashStaminaCost)
            {
                // create a vector 2 from the movement direction and a magnitude for dash
                Vector2 DashVector = MathUtil.FromPolar(movementDirection.ToAngle(), 22f);
                Velocity = Velocity + DashVector;

                // dashing removes the movement frames cooldown
                _movementCooldownFramesRemaining = 0;
                // apply the dash stamina cost
                UpdateStamina(-DashStaminaCost);
            }

            // when the player has movement on cooldown he cant move as responsively
            // if he is dashing movement isn't slowed. This is added after the player attacks
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
                Velocity = Velocity + movementDirection;
            } 
            
            Velocity -= Velocity * _friction; // simulate friction
            Position += Velocity; // update the players position
            Position = Vector2.Clamp(Position, Size / 2, GameRoot.ScreenSize - Size / 2); // keeps the player inside the bounds of the screen

            // set orientation of the player for drawing
            if (aim.LengthSquared() > 0)
            {
                Orientation = aim.ToAngle();
            } else if (movementDirection.LengthSquared() > 0)
            {
                Orientation = movementDirection.ToAngle();
            }

            // cannot attack again unless he is back to 0 cooldown remaining and has enough stam to do it
            if (Input.WasLightAttackPressed() && _attackCooldownFramesRemaining <= 0 && Stamina >= LightAttackStaminaCost)
            {
                _weapon.LightAttack(Position, aim);
                _attackCooldownFramesRemaining = _weapon.LightAttackCooldown;
                // add a movement cooldown that is twice the length of the weapon attack cooldown
                _movementCooldownFramesRemaining = _weapon.LightAttackCooldown * 2;

                // the stamina cost of a light attack
                UpdateStamina(-LightAttackStaminaCost);
            }

            if (Input.WasHeavyAttackPressed() && _attackCooldownFramesRemaining <= 0 && Stamina >= HeavyAttackStaminaCost)
            {
                _weapon.HeavyAttack(Position, aim);
                _attackCooldownFramesRemaining = _weapon.HeavyAttackCooldown;
                // add a movement cooldown that is twice the length of the weapon attack cooldown
                _movementCooldownFramesRemaining = _weapon.HeavyAttackCooldown * 2;

                // the stamina cost of a light attack
                UpdateStamina(-HeavyAttackStaminaCost);
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

            // regen stamina a set amount each frame
            UpdateStamina(0.6f);
        }
    }
}
