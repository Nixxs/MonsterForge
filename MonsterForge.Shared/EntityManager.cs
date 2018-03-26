using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonsterForge
{
    // static classes can't be instantiated
    static class EntityManager
    {
        // a list for storing all the entity objects
        static List<Entity> entities = new List<Entity>();
        static List<Enemy> enemies = new List<Enemy>();
        static List<AttackBox> attackBoxes = new List<AttackBox>();
        static List<HitBox> hitBoxes = new List<HitBox>();

        static bool isUpdating;
        // list for keeping track of all the entities to be added during this loop
        static List<Entity> addedEntities = new List<Entity>();

        // a method for adding an entity to the entity manager
        public static void Add(Entity entity)
        {
            // only add the entity to the main entities list if we are not running an update
            // the update method of the entities might result in the creation and addition
            // of new entities via this entity manager. While the update methods are being run
            // we put any new entities into a temporary list so that we aren't changing anything
            // to the list while we are iterating over it.
            if (isUpdating == false)
            {
                AddEntity(entity);
            } else
            {
                addedEntities.Add(entity);
            }
        }

        // private add entity method for adding newly created entities to the correct list
        // separate lists are needed later to handle the different types of collisions
        private static void AddEntity(Entity entity)
        {
            entities.Add(entity);
            if (entity is AttackBox)
            {
                attackBoxes.Add(entity as AttackBox);
            } else if (entity is HitBox)
            {
                hitBoxes.Add(entity as HitBox);
            } else if (entity is Enemy)
            {
                enemies.Add(entity as Enemy);
            }
        }

        // detect collision between 2 entities
        private static bool IsColliding(Entity a, Entity b)
        {
            // the minimum distance before the two are colliding
            float radius = a.Radius + b.Radius;
            // if both entities are still alive and if they overlap then they are colliding
            // collision is calculated by checking that the distance between the two entities
            // is less than the sum of thier radii
            if (a.IsExpired == false && b.IsExpired == false && Vector2.DistanceSquared(a.Position, b.Position) < radius * radius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void HandleCollisions()
        {
            // enemy to enemy collision
            for (int i = 0; i < enemies.Count; i++)
            {
                // enemy cant be colliding with itself thus the +1
                for (int j = i + 1; j < enemies.Count; j++)
                {
                    if (IsColliding(enemies[i], enemies[j]))
                    {
                        enemies[i].HandleCollision(enemies[j]);
                        enemies[j].HandleCollision(enemies[i]);
                    }
                }
            }

            // enemy and attack box collision
            for (int i = 0; i < enemies.Count; i++)
            {
                // enemy cant be colliding with itself thus the +1
                for (int j = 0; j < attackBoxes.Count; j++)
                {
                    if (IsColliding(enemies[i], attackBoxes[j]))
                    {
                        // reduce the the health of the enemy by the damage of the attack box
                        enemies[i].UpdateHealth(attackBoxes[j].Damage * -1);

                        // apply an impact on the enemy as the attack box hits it
                        // TO DO: the magnitude of this impact should change depending on weapon attack and the size
                        // of the enemy. Large/heavy weapon attacks should have a larger impact but larger enemies should
                        // not be as affected by the attacks.
                        Vector2 DeflectionVelocity = enemies[i].Position - attackBoxes[j].Position;
                        enemies[i].Velocity += DeflectionVelocity * 0.6f;

                        // the attackbox is expired after a hit lands
                        attackBoxes[j].IsExpired = true;
                    }
                }
            }

            // TO DO: hitbox to player collision

            // TO DO: Enemy to player collision
        }

        public static void Update(GameTime gameTime)
        {
            isUpdating = true;
            HandleCollisions();

            // run the update methods of all the entities
            foreach (Entity entity in entities)
            {
                entity.Update(gameTime);
            }

            isUpdating = false;

            // now that all the updates have been run, we can add the newly created
            // entities to the entity list ready for the next loop
            foreach (Entity entity in addedEntities)
            {
                AddEntity(entity);
            }

            // now that we have added all the new entities we can clear out the addedEntities
            // list ready for the next loop.
            addedEntities.Clear();

            // using the linq library, we filter out any entities that have expired
            // so redefine the entities list to contain only those that have a IsExpired
            // property taht is false
            entities = entities.Where(entity => entity.IsExpired == false).ToList();
            enemies = enemies.Where(entity => entity.IsExpired == false).ToList();
            attackBoxes = attackBoxes.Where(entity => entity.IsExpired == false).ToList();
            hitBoxes = hitBoxes.Where(entity => entity.IsExpired == false).ToList();
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            // run all the draw methods of all the entities
            foreach(Entity entity in entities)
            {
                entity.Draw(spriteBatch);
            }
        }
    }
}
