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
                entities.Add(entity);
            } else
            {
                addedEntities.Add(entity);
            }
        }

        public static void Update()
        {
            isUpdating = true;

            // run the update methods of all the entities
            foreach (Entity entity in entities)
            {
                entity.Update();
            }

            isUpdating = false;

            // now that all the updates have been run, we can add the newly created
            // entities to the entity list ready for the next loop
            foreach (Entity entity in addedEntities)
            {
                entities.Add(entity);
            }

            // now that we have added all the new entities we can clear out the addedEntities
            // list ready for the next loop.
            addedEntities.Clear();

            // using the linq library, we filter out any entities that have expired
            // so redefine the entities list to contain only those that have a IsExpired
            // property taht is false
            entities = entities.Where(entity => entity.IsExpired == false).ToList();
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
