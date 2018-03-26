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
        /// <param name="lightAttackDamage">the base damage it does</param>
        /// <param name="lightAttackSpeed">the speed modifier for the weapon, affects range (higher speed improves range)</param>
        /// <param name="lightAttackFrames">The number of frames the hitbox lasts for, affects range (higher frames increases range)</param>
        /// <param name="lightAttackRadius">the size of the hitbox</param>
        /// <param name="lightAttackCooldown">number of frames the player needs to wait before launching another attack</param>
        /// <param name="heavyAttackDamage">the base damage it does</param>
        /// <param name="heavyAttackSpeed">the speed modifier for the weapon, affects range (higher speed improves range)</param>
        /// <param name="heavyAttackFrames">The number of frames the hitbox lasts for, affects range (higher frames increases range)</param>
        /// <param name="heavyAttackRadius">the size of the hitbox</param>
        /// <param name="heavyAttackCooldown">number of frames the player needs to wait before launching another attack</param>
        public Sword(string name, 
            int lightAttackDamage, float lightAttackSpeed, int lightAttackFrames, int lightAttackRadius, int lightAttackCooldown,
            int heavyAttackDamage, float heavyAttackSpeed, int heavyAttackFrames, int heavyAttackRadius, int heavyAttackCooldown)
        {
            Name = name;

            LightAttackDamage = lightAttackDamage;
            LightAttackSpeed = lightAttackSpeed;
            LightAttackFrames = lightAttackFrames;
            LightAttackRadius = lightAttackRadius;
            LightAttackCooldown = lightAttackCooldown;

            HeavyAttackDamage = heavyAttackDamage;
            HeavyAttackSpeed = heavyAttackSpeed;
            HeavyAttackFrames = heavyAttackFrames;
            HeavyAttackRadius = heavyAttackRadius;
            HeavyAttackCooldown = heavyAttackCooldown;
        }

        public override void LightAttack(Vector2 position, Vector2 aim)
        {
            float aimAngle = aim.ToAngle();
            float inverseAimAngle = (aim * -1).ToAngle();
            Vector2 LightAttackRecoil = MathUtil.FromPolar(inverseAimAngle, 4f);
            Vector2 AttackVelocity = MathUtil.FromPolar(aimAngle, LightAttackSpeed);
            EntityManager.Add(new AttackBox(Art.Bullet, position, AttackVelocity, LightAttackFrames, LightAttackRadius, LightAttackDamage));

            PlayerChar.Instance.Velocity = PlayerChar.Instance.Velocity + LightAttackRecoil;
        }

        public override void HeavyAttack(Vector2 position, Vector2 aim)
        {
            float aimAngle = aim.ToAngle();
            Vector2 HeavyAttackCharge = MathUtil.FromPolar(aimAngle, 75f);
            Vector2 AttackVelocity = MathUtil.FromPolar(aimAngle, HeavyAttackSpeed);
            EntityManager.Add(new AttackBox(Art.Hitbox, position, AttackVelocity, HeavyAttackFrames, HeavyAttackRadius, HeavyAttackDamage));

            PlayerChar.Instance.Velocity = PlayerChar.Instance.Velocity + HeavyAttackCharge;
        }
    }
}
