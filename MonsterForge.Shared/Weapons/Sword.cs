using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterForge.Weapons
{
    class Sword : Weapon
    {
        /// <summary>
        /// Creates a sword with light attacks and heavy attacks.
        /// Adjust weapon feel using imput parameters
        /// </summary>
        /// <param name="name">The name of the weapon</param>
        /// <param name="damage">the base damage it does</param>
        /// <param name="lightAttackSpeed">the speed modifier for the weapon, affects range (higher speed improves range)</param>
        /// <param name="lightAttackFrames">The number of frames the hitbox lasts for, affects range (higher frames increases range)</param>
        /// <param name="lightAttackRadius">the size of the hitbox</param>
        /// <param name="lightAttackCooldown">number of frames the player needs to wait before launching another attack</param>
        public Sword(string name, int lightAttackDamage, float lightAttackSpeed, int lightAttackFrames, int lightAttackRadius, int lightAttackCooldown)
        {
            Name = name;
            LightAttackDamage = lightAttackDamage;
            LightAttackSpeed = lightAttackSpeed;
            LightAttackFrames = lightAttackFrames;
            LightAttackRadius = lightAttackRadius;
            LightAttackCooldown = lightAttackCooldown;
        }

        public override void LightAttack(Vector2 position, Vector2 aim)
        {
            float aimAngle = aim.ToAngle();
            float inverseAimAngle = (aim * -1).ToAngle();
            Vector2 LightAttackRecoil = MathUtil.FromPolar(inverseAimAngle, 4f);
            Vector2 AttackVelocity = MathUtil.FromPolar(aimAngle, LightAttackSpeed);
            EntityManager.Add(new Hitbox(position, AttackVelocity, LightAttackFrames, LightAttackRadius));

            PlayerChar.Instance.Velocity = PlayerChar.Instance.Velocity + LightAttackRecoil;
        }
    }
}
