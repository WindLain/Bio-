using System;
using System.Collections;
using System.Collections.Generic;
using Bio;
using UnityEngine;

public class MatingAction : Action
{
 private Animal matingPartner;
        public override Vector3 ActionDestination { get { return matingPartner.gameObject.Position(); } }

        public MatingAction(Animal actionPerformer, Animal matingPartner)
        {
            performer = actionPerformer;
            this.matingPartner = matingPartner;
        }

        public override void Execute()
        {
            matingPartner.gender.HandleMating(matingPartner.Gen);
            performer.NibnePriority = Animal.Priority.None;
            performer.currentAction = null;
        }

        public override void Cancel()
        {
            matingPartner.gender.hasMate = false;
            performer.NibnePriority = Animal.Priority.None;
            performer.currentAction = null;
        }

        public override bool AreConditionsMet()
        {
            return performer.gameObject.Position().IsClose(matingPartner.gameObject.Position(), 1);
        }

        public override string ToString()
        {
            return "Спаривание";
        }
}
