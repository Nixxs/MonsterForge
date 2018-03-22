using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterForge
{
    public class Weapon
    {
        private string _name;
        private int _damage;
        private float _lightAttackSpeed;
        private int _lightAttackFrames;
        private int _lightAttackRadius;
        private int _lightAttackCooldown;

        public string Name
        {
            get
            {
                return _name;
            }
            private set
            {
                _name = value;
            }
        }

        public int LightAttackCooldown
        {
            get
            {
                return _lightAttackCooldown;
            }
        }

        /// <summary>
        /// Creates a weapon with light attacks and heavy attacks.
        /// Adjust weapon feel using imput parameters
        /// </summary>
        /// <param name="name">The name of the weapon</param>
        /// <param name="damage">the base damage it does</param>
        /// <param name="lightAttackSpeed">the speed modifier for the weapon, affects range (higher speed improves range)</param>
        /// <param name="lightAttackFrames">The number of frames the hitbox lasts for, affects range (higher frames increases range)</param>
        /// <param name="lightAttackRadius">the size of the hitbox</param>
        /// <param name="lightAttackCooldown">number of frames the player needs to wait before launching another attack</param>
        public Weapon(string name, int damage, float lightAttackSpeed, int lightAttackFrames, int lightAttackRadius, int lightAttackCooldown)
        {
            Name = name;
            _damage = damage;
            _lightAttackSpeed = lightAttackSpeed;
            _lightAttackFrames = lightAttackFrames;
            _lightAttackRadius = lightAttackRadius;
            _lightAttackCooldown = lightAttackCooldown;
        }

        public void LightAttack(Vector2 position, Vector2 aim)
        {
            float aimAngle = aim.ToAngle();
            Vector2 AttackVelocity = MathUtil.FromPolar(aimAngle, _lightAttackSpeed);
            EntityManager.Add(new Hitbox(position, AttackVelocity, _lightAttackFrames, _lightAttackRadius));
        }
    }
}
