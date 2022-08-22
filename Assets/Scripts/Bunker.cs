using UnityEngine;

public class Bunker : MonoBehaviour
{
    public int lives = 8;
        private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser") || other.gameObject.layer == LayerMask.NameToLayer("Missile")) {
            Destroy(other.gameObject);
            lives--;
            if (lives < 1) {
                Destroy(gameObject);
            }
        } else if (other.gameObject.layer == LayerMask.NameToLayer("Invader")) {
            Destroy(gameObject);
        }
    }
}
