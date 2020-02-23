using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameRunning;
    public Ball ball;

    public Player player1;
    public Player player2;
    public Goals goal;

    public Laser laserPrefab;
    public Schild schildPrefab;

    private Laser laser;
    private Schild schild;

    private int score1;
    private int score2;

    public Text scoreText1;
    public Text scoreText2;
    public Text winText;

    public Collider2D paddleLeft;
    public Collider2D paddleRight;
    public Collider2D wallLeft;
    public Collider2D wallRight;

    private bool gameEnd;


    private Player lastPlayer = null;

    private AudioSource titleMusic;


    private void Awake()
    {
        titleMusic = GetComponent<AudioSource>();
        titleMusic.loop = true;
        titleMusic.Play(0);
        spawnPowerUp(0);
        spawnPowerUp(1);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            startRound();
    }

    private void startRound()
    {
        if (gameEnd)
        {
            updateScoreTexts();
            gameEnd = false;
            winText.text = "";
        }
        
        if (gameRunning)
            return;

        gameRunning = true;
        ball.InitialBallImpulse();
        Debug.Log("Round started!");
    }

    public void increaseScore(bool player1)
    {
        lastPlayer = null;
        if (player1)
            score1++;
        else
            score2++;
        updateScoreTexts();
        checkEndMatch();
    }

    private void checkEndMatch()
    {
        if (score1 >= 10 || score2 >= 10)
        {
            if(score1 == 10)
            {
                winText.text = "PLAYER 1 WINS!!!";
            }
            if(score2 == 10)
            {
                winText.text = "PLAYER 2 WINS!!!";
            }
            score1 = 0;
            score2 = 0;
            gameEnd = true;

        }
    }

    private void updateScoreTexts()
    {
        scoreText1.text = score1.ToString();
        scoreText2.text = score2.ToString();
    }

    public void setLastPlayer(bool isPlayer1)
    {
        if (isPlayer1)
            lastPlayer = player1;
        else
            lastPlayer = player2;
    }

    public void laserHit()
    {
        if(lastPlayer != null)
        {
            Destroy(laser.gameObject);
            laser = null;
            lastPlayer.playerGetLaser();
            Invoke("spawnLaser", getRandomNum(15,21));
        }
        
    }
    public void schildHit()
    {
        if(lastPlayer != null)
        {
            Destroy(schild.gameObject);
            schild = null;
            lastPlayer.playerGetSchild();
            Invoke("spawnSchild", getRandomNum(10, 16));
        }
        
    }

    private void spawnLaser()
    {
        spawnPowerUp(0);
    }

    private void spawnSchild()
    {
        spawnPowerUp(1);
    }

    private void spawnPowerUp(int powerUpIndex)
    {
        float posX = getRandomNum(-3, 4);
        float posY = getRandomNum(-3, 4);
        if (powerUpIndex == 0)
        {
            laser = Instantiate(laserPrefab,new Vector3(posX,posY,0),Quaternion.identity);
        }
        else if(powerUpIndex == 1)
        {
            schild = Instantiate(schildPrefab, new Vector3(posX, posY, 0), Quaternion.identity);
        }
    }

    private float getRandomNum(int min, int max)
    {
        return Random.Range(min, max);
    }

    public void freezeShothit(Collider2D collision,GameObject frostball, string shootingPlayer)
    {
        Debug.Log(collision);
        if(collision == wallRight || collision == wallLeft)
        {
            Destroy(frostball);
        }
        if(shootingPlayer == "Player2" && collision == paddleLeft)
        {
            player1.freezePlayer();
            Destroy(frostball);
        }
        if (shootingPlayer == "Player1" && collision == paddleRight)
        {
            player2.freezePlayer();
            Destroy(frostball);
        }
    }

    public void handleBallHit(Collision2D collision,Rigidbody2D rb)
    {
        //Collision with paddle
        if (collision.collider == paddleLeft || collision.collider == paddleRight)
        {
            titleMusic.pitch += 0.005f;
            ball.speed = ball.speed + (float)0.1;
            float y = transform.position.y - collision.transform.position.y;
            Debug.Log(y);

            float x = 0;

            if (collision.collider == paddleLeft)
            {
                player1.pingpong.Play(0);
                x = 1;
                setLastPlayer(true);
            }
            else
            {
                player2.pingpong.Play(0);
                x = -1;
                setLastPlayer(false);
            }

            Vector2 dir = new Vector2(x, y).normalized;
            rb.velocity = dir * ball.speed;
        }
        //Collision with wall
        if (collision.collider == wallLeft || collision.collider == wallRight)
        {
            goal.playSound();
            titleMusic.pitch = 1;
            ball.speed = 10;
            if (collision.collider == wallRight)
            {
                ball.initBallRight = true;
                increaseScore(true);
            }
            else
            {
                ball.initBallRight = false;
                increaseScore(false);
            }
            ball.resetBall();
        }
    }
}
