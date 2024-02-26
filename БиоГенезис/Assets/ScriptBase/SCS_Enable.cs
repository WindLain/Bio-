
using UnityEngine;

public class SCS_Enable : MonoBehaviour
{
    public Light _BaseLight;

    private void Update() {
        if(Input.GetKeyUp(KeyCode.E))
        _BaseLight.enabled = !_BaseLight.enabled;
    }


}
