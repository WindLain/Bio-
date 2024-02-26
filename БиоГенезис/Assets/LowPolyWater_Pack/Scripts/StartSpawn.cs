using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartSpawn : MonoBehaviour
{
    // Start
    void Start()
    {


        MeshRenderer meshRenderdragon = GetComponent<MeshRenderer>();
        meshRenderdragon.enabled = true;
        meshRenderdragon.transform.position = new Vector3(50, -0.6f, 50);
        meshRenderdragon.transform.localScale = new Vector3(0.5f, 1, 0.5f);



        MeshFilter MeshFilterer = GetComponent<MeshFilter>();
        MeshFilterer.transform.position = new Vector3(50, -0.6f, 50);
    }

}
