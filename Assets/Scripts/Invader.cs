using UnityEngine;

public class Invader : MonoBehaviour
{
    public Missile missile;   
    private void Start() {
    }

    private void FixedUpdate() {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser")) {
            gameObject.SetActive(false);
            Destroy(other.gameObject);
        } else if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            Destroy(other.gameObject);
            Destroy(GameObject.Find("Invaders"));
        }
    }

    public void Move(int direction) {
        Vector3 position = transform.position;
        position.x += direction * 0.25f;
        if (direction == 0) {
            position.y--;
        }
        transform.position = position;
    }

    public void Shoot() {
        Vector3 position = transform.position;
        position.y -= 1;
        Instantiate(missile, position, Quaternion.identity);
    }
}
