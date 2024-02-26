using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCS_ateble : MonoBehaviour
{
public interface Ateble
    {
        void Consume(Animal eater);

        List<Animal> Eaters { get; set; } // a list of all animals that want to eat this IEatable
        int NutritionalValue { get; }
    }
}
