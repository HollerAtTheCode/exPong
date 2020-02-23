using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody2D rb;

    Player player;

    private void Awake()
    {
        player = gameObject.GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float v = 0;
        if (!player.isFrozen)
        {
            if (rb.name == "player 1")
            {
                v = Input.GetAxisRaw("VerticalP1") * speed;
            }
            else if (rb.name == "player 2")
            {
                v = Input.GetAxisRaw("VerticalP2") * speed;
            }
            rb.velocity = new Vector2(0, v);
        }
    }
}
