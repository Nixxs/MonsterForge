using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterForge
{
    public abstract class Weapon
    {
        protected string name;

        protected int lightAttackDamage;
        protected float lightAttackSpeed;
        protected int lightAttackFrames;
        protected int lightAttackRadius;
        protected int lightAttackCooldown;

        protected int heavyAttackDamage;
        protected float heavyAttackSpeed;
        protected int heavyAttackFrames;
        protected int heavyAttackRadius;
        protected int heavyAttackCooldown;

        public string Name
        {
            get{ return name; }

            protected set { name = value; }
        }

        public int LightAttackDamage
        {
            get { return lightAttackDamage; }

            protected set { lightAttackDamage = value; }
        }

        public float LightAttackSpeed
        {
            get { return lightAttackSpeed; }

            protected set { lightAttackSpeed = value; }
        }

        public int LightAttackFrames
        {
            get { return lightAttackFrames; }

            protected set { lightAttackFrames = value; }
        }

        public int LightAttackRadius
        {
            get { return lightAttackRadius; }

            protected set { lightAttackRadius = value; }
        }

        public int LightAttackCooldown
        {
            get{ return lightAttackCooldown;}

            protected set { lightAttackCooldown = value; }
        }

        public int HeavyAttackDamage
        {
            get { return heavyAttackDamage; }

            protected set { heavyAttackDamage = value; }
        }

        public float HeavyAttackSpeed
        {
            get { return heavyAttackSpeed; }

            protected set { heavyAttackSpeed = value; }
        }

        public int HeavyAttackFrames
        {
            get { return heavyAttackFrames; }

            protected set { heavyAttackFrames = value; }
        }

        public int HeavyAttackRadius
        {
            get { return heavyAttackRadius; }

            protected set { heavyAttackRadius = value; }
        }

        public int HeavyAttackCooldown
        {
            get { return heavyAttackCooldown; }

            protected set { heavyAttackCooldown = value; }
        }


        public abstract void LightAttack(Vector2 position, Vector2 aim);

        public abstract void HeavyAttack(Vector2 position, Vector2 aim);
    }
}
