using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10;

    private GameManager gm;

    public bool initBallRight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManager>();
    }

    public void InitialBallImpulse()
    {
        int x = 0;
        if (initBallRight)
            x = 1;
        else
            x = -1;
        Vector2 dir = new Vector2(x, 0);
        rb.velocity = dir * speed;
            
            
    }

    public void resetBall()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        GameManager.gameRunning = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gm.handleBallHit(collision,rb);
    }
}

