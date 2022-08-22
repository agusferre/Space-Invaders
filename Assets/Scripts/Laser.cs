using UnityEngine;

public class Laser : MonoBehaviour
{
    private int speed = 30;
    private void Start() {
        
    }

    private void FixedUpdate() {
        Vector3 position = transform.position;
        position.y += Time.fixedDeltaTime * speed;
        transform.position = position;
        if (transform.position.y > 15) {
            Destroy(gameObject);
        }
    }
}
