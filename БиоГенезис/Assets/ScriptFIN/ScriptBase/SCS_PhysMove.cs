using UnityEngine;

public class SCS_PhysMove : MonoBehaviour
{

    public float speed = 50f, Hspeed = 60f, _thrust = 500f;
private Rigidbody _rb;

private void Awake() {
    _rb = GetComponent<Rigidbody>();
}

private void FixedUpdate() {
    float H = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
    float V = Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime;

    _rb.velocity = transform.TransformDirection(new Vector3(H, _rb.velocity.y, V));
}

private void OnCollisionEnter(Collision other) {
    if(other.gameObject.name == "Block") {
        _rb.AddForce(new Vector3(0,1,0) * _thrust);
    }
}
private void OnTriggerEnter(Collider other) {
    if(other.gameObject.name == "TriggerName") {
        Debug.Log("Trigger Works");
    }
}
private void OnTriggerExit(Collider other) {
    if(other.gameObject.name == "TriggerName"){
                 Destroy(gameObject);            
                 }
}
}


