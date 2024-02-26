using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bio {
 public class Zaya : Animal, Eatable
    {
        private List<Animal> eaters;

        public List<Animal> Eaters 
        { 
            get { return eaters; } 
            set { eaters = value; }
        }

        public int NutritionalValue { get { return (int)(animalStats.Size * 30); } }

        private bool isRunningAway;

        void Start()
        {
             eaters = new List<Animal>();
        }

        protected override void Update()
        {
            base.Update();
            if (enemyColliders.Length != 0)
            {
                if (!isRunningAway && currentAction != null)
                {
                    currentAction.Cancel();
                    isRunningAway = true;
                }
            }
            else if (isRunningAway)
            {
                isRunningAway = false;
            }
        }

        protected override priority GetPriority()
        {          
            if(enemyColliders.Length != 0)
            {
                return priority.RunAway;
            }
            else
            {
                if (Reproduce > 70 && EdaQ > 50 && HydratationQ > 50 && gender.IsAvailableForMating())
                {
                    return priority.Reproduce;
                }

                if (Eda < 100 && EdaQ <= HydratationQ)
                    return priority.FindFood;
                if(Hydratation < 100 && HydratationQ <= EdaQ)
                    return priority.FindWater;

                return priority.None;
            }
        }

        protected override Action GetNextAction()
        {
            switch (NibnePriority)
            {
                case priority.FindFood:
                    if (PlantColliders.Length != 0)
                    {
                        Collider collider = FindNearestCollider(PlantColliders);
                        Vector3 destination =  collider.gameObject.transform.position;
                        return new EatingAction(this, collider.GetComponent<Eatable>(), collider.gameObject);
                    }
                    else
                    {
                        return new SearchAction(this, () => PlantColliders, GetSearchDestiny());
                    }

                case priority.FindWater:
                    if (WaterColliders.Length != 0)
                    {
                        Collider collider = FindNearestCollider(WaterColliders);
                        Vector3 destination = collider.gameObject.transform.position;
                        return new DrinkingAction(this, destination);
                    }
                    else
                    {
                        return new SearchAction(this, () => WaterColliders, GetSearchDestiny());
                    }

                case priority.Reproduce:
                    return gender.HandleReproductionPriority(() => ZayaColliders);
                case priority.RunAway:
                    Collider enemy = FindNearestCollider(enemyColliders);
                    Vector3 runningAwayDestination = GetRunningAwayDestination(enemy);
                    if(!Equals(runningAwayDestination, gameObject.Position()))
                    {
                        return new MoveToAction(this, runningAwayDestination);
                    }
                    else
                    {
                        return new WaitingAction(this, 2);
                    }
                default:
                    return new MoveToAction(this, GetSearchDestiny()); 

            }
        }

        public void Consume(Animal eater)
        {
            float result = eater.EdaQ + NutritionalValue;
            eater.EdaQ = Mathf.Min(100, result);

            IsDead();
        }

        protected override void IsDead()
        {
            Destroy(gameObject);
            gameObject.GetComponent<Collider>().enabled = false;
            foreach (Animal animal in Eaters)
            {
                if (animal.currentAction != null && animal != null) 
                {
                    animal.currentAction.Cancel();
                }
            }
        }
    }

}//namespace