using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
public class WaitingAction : Action
{
public override Vector3 ActionDestination { get { return performer.gameObject.Position(); } }

        bool isCountdownOver = false;
        int duration;

        public WaitingAction(Animal actionPerformer, int duration)
        {
            this.performer = actionPerformer;
            this.duration = duration;

            CouroutineHelper.Instance.CallCouroutine(WaitSeconds(duration));
        }

        IEnumerator WaitSeconds(int duration)
        {
            yield return new WaitForSeconds(duration);
            isCountdownOver = true;
        }

        public override void Execute()
        {
            performer.NibnePriority = Animal.Priority.None;
            performer.currentAction = null;
        }

        public override void Cancel()
        {
            CouroutineHelper.Instance.StopCouroutine(WaitSeconds(duration));
            performer.NibnePriority = Animal.Priority.None;
            performer.currentAction = null;
        }

        public override bool AreConditionsMet()
        {
            return isCountdownOver;
        }

        public override string ToString()
        {
            return "Ждет";
        }
}
