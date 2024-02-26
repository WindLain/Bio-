using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bio {
public class Plant : MonoBehaviour, Eateble
{
        private List<Animal> eaters;      

        [SerializeField]
        public float rostProgress { get; set; }
        private const float growthTick = 0.001f;

        private bool isDamaged;

        private float size;
        public List<Animal> Eaters
        {
            get { return eaters; }
            set { eaters = value; }
        }

        private int nutritionalValue;
        public int NutritionalValue { get { return (int)(nutritionalValue * rostProgress); } }

        public void Start()
        {
            size = gameObject.transform.lossyScale.x;
            gameObject.transform.localScale = new Vector3(size, size, size) * rostProgress;
        }

        public void Update()
        {
            if(!isDamaged && rostProgress != 1)
            {
                rostProgress = Mathf.Clamp01(rostProgress += (growthTick * Time.timeScale));
                gameObject.transform.localScale = new Vector3(size, size, size) * rostProgress;
            }
        

            if(!isDamaged && Time.frameCount % 120 == 0)
            {
                if (rostProgress == 1 && Random.value > 0.98f)
                {
                    DisperseSeeds();
                }
            }
        }

        public void Consume(Animal eater) 
        {
            float result = eater.EdaQ + NutritionalValue;
            eater.EdaQ = Mathf.Min(100, result);

            if(result <= 100)
            {
                IsDead();
            }
            else 
            {
                isDamaged = true;

                float remainder = result - 100;
                rostProgress = remainder / nutritionalValue;

                if(rostProgress <= 0.1f) 
                {
                    IsDead();
                    return;
                }
                gameObject.transform.localScale = new Vector3(size, size, size) * rostProgress;
            }
        }

        private void IsDead()
        {
            Destroy(gameObject);
            gameObject.GetComponent<Collider>().enabled = false;
        

            MapHelper.Instance.SetTileOccupancy((int)gameObject.position().x, (int)gameObject.position().y, false);
        }

        public void Init(int nutritionalValue, float growthProgress)
        {
            Eaters = new List<Animal>();
            rostProgress = growthProgress;
            this.nutritionalValue = nutritionalValue;
            size = gameObject.transform.lossyScale.x;
        }

        private void DisperseSeeds()
        {
            Vector3 offset = new Vector3(1f, 0, 1f);

            if (Random.value < 0.7f)
                offset.x *= -1;

            if (Random.value < 0.7f)
                offset.z *= -1;

            Vector3 plantPosition = gameObject.transform.position + offset;
            if (MapHelper.Instance.IsInaccessible(plantPosition) || MapHelper.Instance.IsOccupiedByFlora(plantPosition))
                return;

            GameObject plant = Instantiate(gameObject, plantPosition, Quaternion.identity);
            plant.GetComponent<Plant>().Init(NutritionalValue, 0);
            plant.transform.SetParent(gameObject.transform.parent);
            MapHelper.Instance.SetTileOccupancy((int)plantPosition.x, (int)plantPosition.y, true);
        }
    }

}//namespace
