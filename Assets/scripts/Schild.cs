using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schild : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Schild gehittet");
        FindObjectOfType<GameManager>().schildHit();
    }
}
