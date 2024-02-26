// using System.Collections; //kurutina need
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEngine;
// using System; //kurutina need


// public class SCS_SpawnDelay : MonoBehaviour 
// {

//  //gameobject
//  public GameObject[] objct;

//  //start
//  private void Update() 
//  {  
//     //Invoke("Create", 2f);
//     if(Input.GetKey(KeyCode.U))
//     StartCoroutine(CreateObj(3f)); 
// }


//  //Commande to create Blocks
//   private void Create() 
// {
//     for (int i = 0; i < 5; i++) {
//         Instantiate(objct[UnityEngine.Random.Range(0, objct.Length)], new Vector3(RandomNumber(), RandomNumber(), RandomNumber()), Quaternion.Euler(RandomNumber(), -15f, 40f));
//     }
// }

//  //Randomaizer
//  private int RandomNumber() 
// {
// return UnityEngine.Random.Range(0,10);
// }

//  //kurutina, Должен быть Return
//   private IEnumerator CreateObj(float wait) 
// {
//    yield return new WaitForSeconds(wait);
// Create();
// }
   
   
   
//     //while(true){
//         //Instantiate(objct[UnityEngine.Random.Range(0, objct.Length)], new Vector3(RandomNumber(), RandomNumber(), RandomNumber()), Quaternion.Euler(RandomNumber(), -15f, 40f));
// //yield return new WaitForSeconds(wait);
//     //}

 
//  //endC
// }