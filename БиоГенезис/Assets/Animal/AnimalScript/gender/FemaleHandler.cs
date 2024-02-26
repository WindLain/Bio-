using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Bio;
using UnityEngine;
using UnityEngine.Analytics;
namespace Bio {
public class FemaleHandler : Gender
{
public bool isPregnant = false;

        readonly int gestationPeriod;

        public FemaleHandler(Animal animal, int gestationPeriod)
        {
            this.baseAnimal = animal;
            Gender = AnimalGender.Female;
            this.gestationPeriod = gestationPeriod;
        }

        public override Action HandleReproductionPriority(Func<Collider[]> getColliders)
        {
            if (hasMate)
                return new WaitingAction(baseAnimal, 5);

            if (getColliders().Length == 0)
                return new SearchAction(baseAnimal, getColliders, baseAnimal.GetSearchDestiny());
            else
                return new MoveToAction(baseAnimal, baseAnimal.GetSearchDestiny());
        }

        public override void HandleMating(Genes fatherGenes)
        {
            if(baseAnimal.currentAction != null)
                baseAnimal.currentAction.Cancel();
            baseAnimal.NibnePriority = Animal.priority.none;
            hasMate = false;
            isPregnant = true;
            baseAnimal.StartCoroutine(PregnancyCouroutine(gestationPeriod, fatherGenes));
        }

        void GiveBirth(GameObject gameObject, Genes fatherGenes)
        {
            for(int i = 0; i < 2; i++)
            {
                GameObject childObject = UnityEngine.Object.Instantiate(gameObject, gameObject.transform.position, Quaternion.identity);
                Animal child = childObject.GetComponent<Animal>();


                AnimalStats childGenes = baseAnimal.genes.GetInheritedGenes(fatherGenes, baseAnimal.genes);
                child.Init(childObject, 20, 20, childGenes, 0.25f, GetRandomGender(Child));
                childObject.transform.SetParent(gameObject.transform.parent);
                Debug.Log("Исчез");
            }
        }

        IEnumerator PregnancyCouroutine(int gestationPeriod, Gender fatherGenes)
        {
            yield return new WaitForSeconds(gestationPeriod);
            if(baseAnimal != null) 
                GiveBirth(baseAnimal.gameObject, fatherGenes);
        }

        public override bool IsAvailableForMating()
        {
            return !isPregnant && baseAnimal.IsReady;
        }
}
}//namespace