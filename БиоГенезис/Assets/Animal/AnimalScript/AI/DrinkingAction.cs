using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Bio {
public class DrinkingAction : Action
{
         public override Vector3 ActionDestination { get { return destination; } }
        private Vector3 destination;

        public DrinkingAction(Animal actionPerformer, Vector3 destination)
        {
            performer = actionPerformer;
            this.destination = destination;

        }

        public override void Execute()
        {
            performer.HydratationQ = 100;
            performer.NibnePriority = Animal.priority.none;
            performer.currentAction = null;
        }

        public override void Cancel()
        {
            performer.NibnePriority = Animal.priority.none;
            performer.currentAction = null;
        }

        public override bool AreConditionsMet()
        {
            return Mathf.Abs(performer.gameObject.Position().x - ActionDestination.x) < 0.7f &&
               Mathf.Abs(performer.gameObject.Position().z - ActionDestination.z) < 0.7f;
        }

        public override string ToString()
        {
            return "Пьет";
        }
    }
}//namespace
