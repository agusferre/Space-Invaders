using UnityEngine;

public class Missile : MonoBehaviour
{
    public int speed = 15;

    private void FixedUpdate() {
        Vector3 position = transform.position;
        position.y -= Time.fixedDeltaTime * speed;
        transform.position = position;
        if (transform.position.y < -15) {
            Destroy(gameObject);
        }
    }
}
