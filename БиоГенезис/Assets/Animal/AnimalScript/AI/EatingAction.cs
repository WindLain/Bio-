using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bio {
public class EatingAction : Action
{
public override Vector3 ActionDestination { get { return foodObject.position(); } }

        private Eatable food;
        private GameObject foodObject;

        public EatingAction(Animal actionPerformer, Eatable food, GameObject foodObject)
        {
            this.performer = actionPerformer;
            this.food = food;
            this.foodObject = foodObject;
            food.Eaters.Add(performer);
        }

        public override void Execute()
        {        
            food.Consume(performer);
            performer.currentAction = null;
            performer.NibnePriority = Animal.priority.none;
        }

        public override void Cancel()
        {
            performer.NibnePriority = Animal.priority.none;
            performer.currentAction = null;
        }

        public override bool AreConditionsMet()
        {
            return performer.gameObject.Position().IsClose(ActionDestination, 1f);
        }

        public override string ToString()
        {
            return "Кушает";
        }
}

    internal class Eatable
    {
    }
}//namespace