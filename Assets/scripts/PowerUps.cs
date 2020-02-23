using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public GameObject schild;
    public GameObject laser;

    public void Awake()
    {
        schild.SetActive(false);
        laser.SetActive(false);
    }

    public void showLaser()
    {
        laser.SetActive(true);
    }

    public void showSchild()
    {
        schild.SetActive(true);
    }

    public void hideLaser()
    {
        laser.SetActive(false);
    }

    public void hideSchild()
    {
        schild.SetActive(false);
    }
}
