using UnityEngine;

public class Frost : MonoBehaviour
{
    private float speed = 40;

    private Rigidbody2D rb;
    private GameManager gm;

    private string shootingPlayer;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void shoot(int dir)
    {
        if (dir == 1)
            shootingPlayer = "Player1";
        else
            shootingPlayer = "Player2";

        Vector2 dirVec = new Vector2(dir, 0);
        rb.velocity = dirVec * speed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        gm.freezeShothit(collision,gameObject,shootingPlayer);
    }
}
