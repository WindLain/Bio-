using System.Collections;
using System.Collections.Generic;
using Bio;
using UnityEngine;

public class Gender : MonoBehaviour
{
protected Animal baseAnimal;

        public AnimalGender Genders { get; set; }

        public bool hasMate = false;

        public static Gender GetRandomGender(Animal animal)
        {
            Gender gender = UnityEngine.Random.value >= 0.5f ? new MaleHandler(animal) : new FemaleHandler(animal, 5);
            return gender;
        }

        protected bool IsOppositeGender(AnimalGender gender)
        {
            if (gender == Genders) 
                return true;
            else 
                return false;
        }

        public abstract Action HandleReproductionPriority(Func<Collider[]> getColliders);
        public abstract void HandleMating(SCS_Gene partnerGenes);
        public abstract bool IsAvailableForMating();
    }

    public enum AnimalGender
    {
        Male, Female
}
