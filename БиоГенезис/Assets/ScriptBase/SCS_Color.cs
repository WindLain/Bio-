using UnityEngine;

public class SCS_Color : MonoBehaviour
{

private MeshRenderer _mesh;

private void Awake() {
    _mesh = GetComponent<MeshRenderer>();
}
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.G)) {
            _mesh.material.color = Color.yellow;
        }

    }
}
