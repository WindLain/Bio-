using System.Collections;
using System.Collections.Generic;
using Bio;
using UnityEngine;

public class MaleHandler : Gender
{
public MaleHandler(Animal animal)
        {
            this.baseAnimal = animal;
            Genders = AnimalGender.Male;
        }

        public override Action HandleReproductionPriority(Func <Collider[]> getColliders)
        {
            if (getColliders().Length != 0)
            {
                foreach (Collider collider in getColliders())
                {
                    Animal matingAnimal = collider.GetComponent<Animal>();
                    if (IsPartnerAppropiate(matingAnimal))
                    {
                        if(matingAnimal.currentAction != null)
                            matingAnimal.currentAction.Cancel();

                        matingAnimal.gender.hasMate = true;
                        matingAnimal.currentAction = null;
                        Transform partnerTransform = matingAnimal.gameObject.transform;
                        baseAnimal.StartCoroutine(RotateToDirection(partnerTransform, baseAnimal.gameObject.position(), 0.5f));
                        
                        return new MatingAction(baseAnimal, matingAnimal);
                    }
                }
                return new MoveToAction(baseAnimal, baseAnimal.GetSearchDestiny());
            }
            else
            {
                return new SearchAction(baseAnimal, getColliders, baseAnimal.GetSearchDestiny());
            }
        }

        public override void HandleMating(Gen partnerGenes) { }

        private bool IsPartnerAppropiate(Animal partner)
        {
            return (partner.NibnePriority == Animal.priority.Reproduce && partner.gender.gender == AnimalGender.Female && !partner.genderHandler.hasMate);
        }

        public IEnumerator RotateToDirection(Transform transform, Vector3 positionToLook, float timeToRotate)
        {
            Quaternion startRotation = transform.rotation;
            Vector3 direction = positionToLook - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            float time = 0f;

            while (time <= 1f)
            {
                time += Time.deltaTime / timeToRotate;
                transform.rotation = Quaternion.Lerp(startRotation, lookRotation, time);
                yield return null;
            }
            transform.rotation = lookRotation;
        }

        public override bool IsAvailableForMating()
        {
            return baseAnimal.IsReady;
        }
}
