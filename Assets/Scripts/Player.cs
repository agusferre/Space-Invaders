using UnityEngine;

public class Player : MonoBehaviour
{
    private int direction = 0;
    public int speed = 12;
    public GameObject laser;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            direction = 1;
        } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            direction = -1;
        } else if (((Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) && direction == 1) || ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) && direction == -1)) {
            direction = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) && GameObject.Find("Laser(Clone)") == null) {
            if (GameObject.Find("Laser") == null) {
                Shoot();
            }
        }
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        Vector3 position = transform.position;
        position.x += direction * Time.fixedDeltaTime * speed;
        position.x = position.x > 13 ? 13 : position.x;
        position.x = position.x < -13 ? -13 : position.x;
        transform.position = position;
    }

    private void Shoot() {
        Vector3 position = transform.position;
        position.y += 1;
        Instantiate(laser, position, Quaternion.identity);
    }
}
