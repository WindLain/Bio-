
using UnityEngine;

public class SCS_Movement : MonoBehaviour
{
    public float speed = 5f;

    private void Update() {
        float V = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(-1,0,0) * Time.deltaTime * speed * V);

        float H = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(0,0,1) * Time.deltaTime * speed * H);
    }


}