using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToAction : Action
    {
        public override Vector3 ActionDestination { get { return destination; } }

        private Vector3 destination;

        public MoveToAction(Animal actionPerformer, Vector3 destination)
        {
            performer = actionPerformer;
            this.destination = destination;
        }


        public override void Execute()
        {
            performer.NibnePriority = Animal.Priority.None;
            performer.currentAction = null;
        }

        public override void Cancel()
        {
            performer.NibnePriority = Animal.Priority.None;
            performer.currentAction = null;
        }

        public override bool AreConditionsMet()
        {
            return (Vector3.Distance(performer.gameObject.Position(), ActionDestination) < 0.7f);
        }

        public override string ToString()
        {
            string moveString;
            switch (performer.NibnePriority)
            {
                case Animal.Priority.RunAway:
                    moveString = "Убегает";
                    break;
                default:
                    moveString = "Стоит";
                    break;

            }
            return moveString;
        }
    }
