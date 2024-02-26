using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;

public class SCS_Animal : MonoBehaviour
{

        public abstract class Animal : MonoBehaviour 
        {
            public enum priority
            {
                none,
                FindWater, FindFood,
                Reproduce,
                RunAway
            }//enumend

            public priority NibnePriority;

            [SerializeField]
            private Vector3 currentDestiny;
            public Vector3 CurrentDestiny 
            {
               get
               {
                currentDestiny = currentAction.ActionDestiny;
                return currentDestiny;
               }//getend
            }//PublDestenyend
            public Action currentAction;
            public GameObject AnimalObj;
            protected FieldOfView FOV;

            protected AnimalStats AnimalStatsQ;

            public Genes Gen;

            [SerializeField]
            protected float HP;
            [SerializeField]
            protected float Eda;
            [SerializeField]
            protected float Hydratation;
            [SerializeField]
            protected float Reproduce;



            private const float EdaTick = 0.01f;

            private const float HydratationTick = 0.01f;

            private const float rostTick = 0.01f;

            public float rostProgress {get; set;}
            [SerializeField]

            public bool IsReady {get {return rostProgress == 1;} }

            public Gender gender;

            public float HPQ
            {
            get {return HP; }
            set {HP = value; }
            }

            public float EdaQ
            {
                get {return Eda;}
                set {Eda = value;}
            }

            public float HydratationQ
            {
                get {return Hydratation; }
                set {Hydratation = value; }
            }

            public float ReproduceQ
            {
                get {return (Eda + Hydratation) / 2; }
                set {Reproduce = value; }
            }

            protected bool NadoDestiny { get {return AnimalObj.transform.position == CurrentDestiny;} }

            protected bool NadoAction { get {return currentAction == null; } }

            [SerializeField]
            private NavMeshAgent navAgent;

            public Collider[] enemyColliders { get { return FOV.GetNearbyColliders(AnimalObj.transform.position, AnimalStatsQ.LineViewRadius, FOV.EnemyViewMask); } }
            public Collider[] WaterColliders { get { return FOV.GetNearbyColliders(AnimalObj.transform.position, AnimalStatsQ.LineViewRadius, FOV.WaterViewMask); } }
            public Collider[] PlantCollider { get { return FOV.GetNearbyColliders(AnimalObj.transform.position, AnimalStatsQ.LineViewRadius, FOV.PlantViewMask); } }
            public Collider[] ZayaCollider { get { return FOV.GetNearbyColliders(AnimalObj.transform.position, AnimalStatsQ.LineViewRadius, FOV.ZayaViewMask); } }

            protected virtual void Update()
            {
                NibnePriority = GetPriority();

                EdaQ = Mathf.Clamp(EdaQ - EdaTick * Time.timeScale, 0, 100);
                HydratationQ = Mathf.Clamp(HydratationQ - HydratationTick * Time.timeScale, 0, 100);

                Reproduce = ReproduceQ;

                if (rostProgress !=1)
                {
                    rostProgress = Mathf.Clamp01(rostProgress += rostTick * Time.timeScale);

                    GameObject.transform.localScale = new Vector3(AnimalStatsQ.Size, AnimalStatsQ.Size, AnimalStatsQ.Size) * rostProgress;
                    navAgent.speed = (AnimalStatsQ.MovementSpeed / AnimalStatsQ.Size) * rostProgress;
                }

                if(EdaQ == 0)
                HPQ -= EdaTick;

                if(HydratationQ == 0)
                HPQ -= HydratationTick;

                if(HPQ <= 0)
                isDead();
            }//UpdateEnd

            public void Init(GameObject AnimalObj, float EdaBase, float HydratationBase, float speedBase, float ViewRadiusBase, float rostProgressQ, Gender genderQ, Color cvet)
            {
                NibnePriority = priority.none;
                currentAction = null;

                this.AnimalObj = AnimalObj;
                FOV = new FieldOfView();

                Renderer Renderdraragon = gameObject.Child().GetComponent<Renderer>();
                Renderdraragon.material.color = cvet;

                EdaQ = EdaBase;
                HydratationQ = HydratationBase;
                
                AnimalStatsQ = new AnimalStats(gameObject.transform.lossyScale.x, speedBase, ViewRadiusBase, cvet);
                AnimalStatsQ.Size = gameObject.transform.lossyScale.x;
                rostProgress = rostProgressQ;
                navAgent.speed = rostProgress * (AnimalStatsQ.MovementSpeed / AnimalStatsQ.Size);
                Gen = new Genes(AnimalStatsQ);

                gender = genderQ;
                HPQ = 100;
            }//initend

            public void Init(GameObject AnimalObj, float EdaBase, float HydratationBase, AnimalStats stats, float rostProgressQ, Gender genderQ)
            {
                NibnePriority = priority.none;
                currentAction = null;

                this.AnimalObj = AnimalObj;
                FOV = new FieldOfView();

                Renderer renderdragon = gameObject.Child().GetComponent<Renderer>();
                renderdragon.material.color = stats.cvet;
                EdaQ = EdaBase;
                HydratationQ = HydratationBase;
                AnimalObj = stats;
                AnimalStatsQ.Size = gameObject.transform.lossyScale.x;
                rostProgress = rostProgressQ;
                navAgent.speed = rostProgress * AnimalStatsQ.MovementSpeed;
                Gen = new Genes(AnimalStatsQ);

                Gender = genderQ;
                HPQ = 100;
            }//initend2

            public Collider FindNearestCollider(Collider[] colliders)
            {
                Collider nearestCollider = colliders[0];
                float nearestDistance = float.MaxValue;
                float distance;
                foreach (Collider collider in colliders)
                {
                    distance = (AnimalObj.transform.position - collider.gameObject.transform.position).sqrMagnitude;
                    if(distance < nearestDistance)
                    {
                     nearestDistance = distance;
                     nearestCollider = collider;
                    }//if1
                }//foreachend
                return nearestCollider;
            }

            public Vector3 GetSearchDestiny()
            {
                Vector3 position = gameObject.transform.position;
            Vector3 searchDestiny;
            float randomUgl;

            float x;
            float z;

            int loopCounter = 0;
            do
            {
                randomUgl = Random.Range(-45, 45);
                Vector3 searchDirection = transform.forward.RotateByAngle(randomUgl);
                searchDestiny = (searchDirection * AnimalStatsQ.LineViewRadius) + position;
                if (IsInaccessible(searchDestiny))
                {
                    //Debug.Log("changed path" + searchDestination);
                    searchDirection = -searchDirection;                    
                }

                searchDestiny = (searchDirection * AnimalStatsQ.LineViewRadius) + position;
                loopCounter++;
            } while (IsInaccessible(searchDestiny) && loopCounter < 32);
            return searchDestiny;
            }//endDo
            protected Vector3 GetRunningAwayDestination(Collider predator)
        {
            Vector3 position = gameObject.transform.Position;
            Vector3 runningDestination;
            float randomaUgl;

            float x;
            float z;

            int loopIteration = 0;
            int directionCount = 0;
            int nDirection = 0;

            do
            {
                Vector3 searchDirection = (position - enemy.gameObject.transform.position).normalized;
                randomUgl = Random.Range((90 * directionCount) - 45, (90 * directionCount) + 45);
                runningDestination = (searchDirection.RotateByAngle(randomUgl) * AnimalStatsQ.LineViewRadius) + position;

                loopIteration++;
                if (loopIteration == 31)
                {
                    loopIteration = 0;
                    if (nDirection % 2 == 0)
                    {
                        directionCount++;
                    }

                    directionCount *= -1;
                    nDirection++;
                }

            } while (IsInaccessible(runningDestination) && loopIteration < 32 && nDirection < 3);
            
            return runningDestination;
        }

        public bool IsInaccessible(Vector3 position)
        {
            return MapHelper.Instance.IsInaccessible(position);
        }

        protected abstract Priority GetPriority();
        protected abstract void isDead();
    
        }

        }//publicabstractend

        public struct AnimalStats
        { 
        public AnimalStats(float size, float movementSpeed, float lineViewRadius, Color cvet)
        {
            this.size = size;
            this.movementSpeed = movementSpeed;
            this.lineViewRadius = lineViewRadius;
            this.cvet = cvet;
        }

        public float Size { get { return size; } set { size = value; } }
        public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
        public float LineViewRadius { get { return lineViewRadius; } set { lineViewRadius = value; } }
        public Color Cvet { get { return cvet; } set { cvet = value; } }

        private float size;
        private float movementSpeed;
        private float lineViewRadius;
        private Color cvet;
    

}//SCS_Animalend
