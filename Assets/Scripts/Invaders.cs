using UnityEngine;
using System.Collections;
using System;

public class Invaders : MonoBehaviour
{
    [System.Serializable]
    public struct Column {
        [SerializeField] public Invader[] invaders;
    }
    public Column[] columns;
    public Invader invader1;
    public Invader invader2;
    public Invader invader3;
    public GameObject target;
    private int invadersSpeed = 1;
    private float shootingSpeed = 1f;
    private int rightColumn = 10;
    private int leftColumn = 0;
    private int direction = 1;
    private int nextDirection = 1;

    private void Start() {
        target = GameObject.Find("Player");
        InvokeRepeating("MoveInvaders", invadersSpeed, invadersSpeed);
        InvokeRepeating("SelectShooter", shootingSpeed, shootingSpeed);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.K)) {
            SelectShooter();
        }
    }

    public void instanciateInvaders() {
        for (int i = 0; i < 11; i++) {
            columns[i].invaders[0] = Instantiate(invader1, new Vector3(-10 + 2 * i, 8, 0), Quaternion.identity);
            columns[i].invaders[1] = Instantiate(invader2, new Vector3(-10 + 2 * i, 6, 0), Quaternion.identity);
            columns[i].invaders[2] = Instantiate(invader2, new Vector3(-10 + 2 * i, 4, 0), Quaternion.identity);
            columns[i].invaders[3] = Instantiate(invader3, new Vector3(-10 + 2 * i, 2, 0), Quaternion.identity);
            columns[i].invaders[4] = Instantiate(invader3, new Vector3(-10 + 2 * i, 0, 0), Quaternion.identity);
            foreach (Invader inv in columns[i].invaders) {
                inv.transform.parent = GameObject.Find("Invaders").transform;
            }
        }
    }

    private void MoveInvaders() {
        int direction = SetInvadersDirection();
        StartCoroutine(MoveRow(4, 0f));
        StartCoroutine(MoveRow(3, 0.2f));
        StartCoroutine(MoveRow(2, 0.4f));
        StartCoroutine(MoveRow(1, 0.6f));
        StartCoroutine(MoveRow(0, 0.8f));
    }

    private IEnumerator MoveRow(int row, float time) {
        yield return new WaitForSeconds(time/invadersSpeed);
        foreach (Column column in columns) {
            if (column.invaders[row].gameObject.activeInHierarchy) {
                column.invaders[row].Move(direction);
            }
        }
    }

    private int SetInvadersDirection() {
        Vector3 position = GetLastInvader().transform.position;
        if ((direction == 1 && position.x >= 13) || (direction == -1 && position.x <= -13)) {
            nextDirection = nextDirection * -1;
            direction = 0;
        } else if (direction == 0) {
            direction = nextDirection;
        }
        return direction;
    }

    private Invader GetLastInvader() {
        int column = direction == 1 ? rightColumn : leftColumn;
        bool found = false;
        int i = 0;
        if (direction == 0) {
            return columns[0].invaders[0];
        }
        while (!found) {
            foreach (Invader inv in columns[column].invaders) {
                if (inv.gameObject.activeInHierarchy) {
                    found = true;
                    return inv;
                }
            }
            if (direction == 1) {
                rightColumn = --column;
            } else if (direction == -1) {
                leftColumn = ++column;
            }
            if(i == 10) {
                break;
            }
        }
        return null;
    }

    private void SelectShooter() {
        bool shooted = false;
        if (UnityEngine.Random.Range(0f, 1f) > 0.5f) {
            int col = UnityEngine.Random.Range(0, 11);
            int lower = getLowerRow(col);
            if (lower > -1) {
                columns[col].invaders[lower].Shoot();
                shooted = true;
            }
        }
        if (!shooted) {
            float position = target.transform.position.x;
            GetClosest(position).Shoot();
        }
    }

    private Invader GetClosest(float position) {
        float lowerDistance = 100f;
        Invader closest = null;
        for (int i = 0; i < 11; i++) {
            int lower = getLowerRow(i);
            if (lower == -1) {
                continue;
            }
            float distance = Math.Abs(columns[i].invaders[lower].transform.position.x - position);
            if (distance < lowerDistance) {
                lowerDistance = distance;
                closest = columns[i].invaders[lower];
            }
        }
        return closest;
    }

    private int getLowerRow(int col) {
        for (int i = 4; i >= 0; i--) {
            if (columns[col].invaders[i].gameObject.activeInHierarchy) {
                return i;
            }
        }
        return -1;
    }
}
