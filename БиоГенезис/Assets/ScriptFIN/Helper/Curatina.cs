using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bio
{
    //Singleton used to start various couroutines for classes which do not derive from MonoBehaviour
    public class CouroutineHelper : MonoBehaviour
    {
        private static CouroutineHelper instance;
        public static CouroutineHelper Instance { get { return instance; } }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
        }

        public void CallCouroutine(IEnumerator couroutine)
        {
            StartCoroutine(couroutine);
        }

        public void StopCouroutine(IEnumerator couroutine)
        {
            StopCoroutine(couroutine);
        }
    }
}

