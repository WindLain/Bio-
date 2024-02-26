using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAction : Action

    {
        public override Vector3 ActionDestination { get { return destination; } }

        private Vector3 destination;
        private Func<Collider[]> getColliders;

        public SearchAction(Animal actionPerformer, Func<Collider[]> getColliders, Vector3 destination)
        {
            this.performer = actionPerformer;
            this.destination = destination;
            this.getColliders = getColliders;
        }


        public override void Execute()
        {
            performer.NibnePriority = Animal.priority.None;
            performer.currentAction = null;
        }

        public override void Cancel()
        {
            performer.NibnePriority = Animal.priority.None;
            performer.currentAction = null;
        }

        public override bool AreConditionsMet()
        {
            return (getColliders().Length != 0 || (
                Mathf.Abs(performer.gameObject.position().x - ActionDestination.x) < 0.7f && //Doing this with Abs instead of Vector3.Distance for performance
               Mathf.Abs(performer.gameObject.position().z - ActionDestination.z) < 0.7f));
        }

        public override string ToString()
        {
            string searchString;
            switch(performer.NibnePriority)
            {
                case Animal.priority.FindFood:
                    searchString = "за едой";
                    break;
                case Animal.priority.FindWater:
                    searchString = "За водой";
                    break;
                case Animal.priority.Reproduce:
                    searchString = "За спариванием";
                    break;
                default:
                    searchString = "";
                    break;

            }
            return $"Поиск {searchString}";
        }
    }
