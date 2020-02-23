using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private bool hasSchild;
    private bool hasLaser;
    private bool schildIsActive;
    private bool laserIsActive;

    public bool isFrozen;

    public PowerUps pu;

    public GameObject schildPrefab;
    public GameObject freezePrefab;
    public Frost frostBallPrefab;

    private GameObject schild;
    private GameObject freeze;

    public PowerUps playerPowerUpsController;

    private string horizontalAxis;


    public AudioSource pingpong;

    private void FixedUpdate()
    {
        if (horizontalAxis == "HorizontalP1")
        {
            int dir = (int)Input.GetAxisRaw(horizontalAxis);
            Debug.Log(dir);
            if (dir == -1)
            {
                if (hasSchild && !isFrozen)
                {
                    hasSchild = false;
                    pu.hideSchild();
                    schildIsActive = true;
                    gameObject.transform.localScale += new Vector3(0, 0.45f, 0);
                    schild = Instantiate(schildPrefab, gameObject.transform.position, Quaternion.identity);
                    schild.transform.parent = gameObject.transform;
                    Invoke("endShield", 4.0f);
                }
            }
            if(dir == 1)
            {
                if (hasLaser && !isFrozen)
                {
                    hasLaser = false;
                    pu.hideLaser();
                    Frost frost = Instantiate(frostBallPrefab, gameObject.transform.position, Quaternion.identity);
                    frost.shoot(dir);
                }
            }
        }
        else if (horizontalAxis == "HorizontalP2")
        {
            int dir = (int)Input.GetAxisRaw(horizontalAxis);
            if (dir == 1)
            {
                if (hasSchild && !isFrozen)
                {
                    hasSchild = false;
                    pu.hideSchild();
                    schildIsActive = true;
                    gameObject.transform.localScale += new Vector3(0, 0.45f, 0);
                    schild = Instantiate(schildPrefab, gameObject.transform.position, Quaternion.identity);
                    schild.transform.parent = gameObject.transform;
                    Invoke("endShield", 4.0f);
                }
            }
            if (dir == -1)
            {
                if (hasLaser && !isFrozen)
                {
                    hasLaser = false;
                    pu.hideLaser();
                    Frost frost = Instantiate(frostBallPrefab, gameObject.transform.position, Quaternion.identity);
                    frost.shoot(dir);
                }
            }
        }
    }

    public void Awake()
    {
        if (gameObject.name == "player 1")
            horizontalAxis = "HorizontalP1";
        if(gameObject.name == "player 2")
            horizontalAxis = "HorizontalP2";

        hasSchild = false;
        hasLaser = false;
        isFrozen = false;


        pingpong = GetComponent<AudioSource>();
    }

    public void playerGetLaser()
    {
        hasLaser = true;
        playerPowerUpsController.showLaser();
    }

    public void playerGetSchild()
    {
        hasSchild = true;
        playerPowerUpsController.showSchild();
    }

    public void endShield()
    {
        schildIsActive = false;
        gameObject.transform.localScale += new Vector3(0, -0.45f, 0);
        Destroy(schild);
    } 

    public void freezePlayer()
    {
        if (!schildIsActive)
        {
            isFrozen = true;
            freeze = Instantiate(freezePrefab, gameObject.transform.position, Quaternion.identity);
            freeze.transform.parent = gameObject.transform;
            Invoke("defreezePlayer", 2);
        }
        
    }

    public void defreezePlayer()
    {
        isFrozen = false;
        Destroy(freeze);
        freeze = null;
    }

    public void resetPlayer()
    {
        hasSchild = false;
        hasLaser = false;

    }
}
