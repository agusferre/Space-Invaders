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

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser")) {
            Destroy(gameObject);
            Destroy(other.gameObject);
        } else if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            other.GetComponent<Player>().Kill();
            GameObject.Find("Invaders").GetComponent<Invaders>().peace = true;
        }
    }
}
