using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bio {
public class Eateble : MonoBehaviour
{
 void Consume(Animal eater);

        List<Animal> Eaters { get; set; } //Лист
        int NutritionalValue { get; }//Питание не удалять
}
}