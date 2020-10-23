using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGames
{
    class Scene
    {
        private Entity[] _entities;

        public bool Started { get; private set; }

        public Scene()
        {
            _entities = new Entity[0];
        }

        public void AddEntity(Entity entity)
        {
            //creating a new array that has an aditional slot from an old array
            Entity[] newEntities = new Entity[_entities.Length + 1];
            //copy values from old array to the old array
            for (int i = 0; i < _entities.Length; i++)
            {
                newEntities[i] = _entities[i];
            }
            //sets the new entity at the end of the new array
            newEntities[_entities.Length] = entity;
            //set old array to the new array with the new entity
            _entities = newEntities;
        }

        public bool RemoveEntity(int index)
        {
            //checks to see if index is out of bounds of array
            if (index < 0 || index >= _entities.Length)
            {
                return false;
            }
            bool entityRemoved = false;

            //creating a new array that has a slot taken away from an old array
            Entity[] tempEntity = new Entity[_entities.Length - 1];
            //create variable to access tempArray index
            int j = 0;
            //copy values from the old array to new Array
            for (int i = 0; i < _entities.Length; i++)
            {
                //If the current index is not the index that needs to be removed
                //add the value into the old array and increment j
                if (i != index)
                {
                    tempEntity[j] = _entities[i];
                    j++;
                }
                else //gives us our return value for debuging
                {
                    entityRemoved = true;
                    if (_entities[i].Started)
                    {
                        _entities[i].End();
                    }
                }
            }
            //set the old arrat to be the tempArray
            _entities = tempEntity;

            return entityRemoved;
        }

        public bool RemoveEntity(Entity entity)
        {
            //if no entity was pass through dont run funtion
            if (entity == null)
            {
                return false;
            }

            bool entityRemoved = false;

            //creating a new array that has a slot taken away from an old array
            Entity[] tempEntity = new Entity[_entities.Length - 1];
            //create variable to access tempArray index
            int j = 0;
            //copy values from the old array to new Array
            for (int i = 0; i < _entities.Length; i++)
            {
                //If the current index is not the index that needs to be removed
                //add the value into the old array and increment j
                if (entity != _entities[i])
                {
                    tempEntity[j] = _entities[i];
                    j++;
                }
                else //gives us our return value for debuging
                {
                    entityRemoved = true;
                    if (entity.Started)
                    {
                        entity.End();
                    }
                }
            }
            //set the old arrat to be the tempArray
            _entities = tempEntity;

            return entityRemoved;
        }

        public virtual void Start()
        {
            Started = true;
        }//start

        public virtual void Update(float deltaTime)
        {
            //for each entity
            for (int i = 0; i < _entities.Length; i++)
            {
                if (!_entities[i].Started)
                    _entities[i].Start();

                _entities[i].Update(deltaTime);
            }
        }//update

        public virtual void Draw()
        {

            //for each entity
            for (int i = 0; i < _entities.Length; i++)
            {
                _entities[i].Draw();
            }
        }//Draw

        public virtual void End()
        {
            //for each entity
            for (int i = 0; i < _entities.Length; i++)
            {
                if (_entities[i].Started)
                    _entities[i].End();
            }
            Started = false;
        }//End
    }
}

