using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bio {
public abstract class Action : MonoBehaviour
{
        public Animal performer;
        public abstract Vector3 ActionDestination { get; }

        public abstract void Execute();
        public abstract void Cancel();
        public abstract bool AreConditionsMet();
    
    }
}//namespace

