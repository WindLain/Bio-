using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bio {
public class enemy : Animal
{

        protected override priority GetPriority()
        {
            if (Reproduce > 70 && EdaQ > 20 && HydratationQ > 20 && gender.IsAvailableForMating())
            {
                return priority.Reproduce;
            }

            if (Eda < 90 && EdaQ <= HydratationQ)
                return priority.FindFood;
            if (Hydratation < 90 && HydratationQ <= EdaQ)
                return priority.FindWater;

            return priority.none;
        }

        protected override Action GetNextAction()
        {
            switch (NibnePriority)
            {
                case priority.FindFood:
                    if (ZayaColliders.Length != 0)
                    {
                        Collider collider = FindNearestCollider(ZayaColliders);
                        Vector3 destination = collider.gameObject.transform.position;
                        Eateble prey = collider.GetComponent<Eateble>();
                        return new EatingAction(this, Zaya, collider.gameObject);
                    }
                    else
                    {
                        return new SearchAction(this, () => PreyColliders, GetSearchDestiny());
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
                    return gender.HandleReproductionPriority(() => PredatorColliders);
                default:
                    return new MoveToAction(this, GetSearchDestiny());

            }
        }

        protected override void IsDead()
        {
            Destroy(gameObject);
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
