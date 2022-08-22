using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Invaders invaders;
    public int score;
    public int lives;

    private void Start() {
        invaders.instanciateInvaders();
    }

    private void Update() {

    }

    public void killPlayer() {
        player.Kill();
    }
}
