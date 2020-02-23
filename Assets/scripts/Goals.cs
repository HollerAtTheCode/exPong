using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goals : MonoBehaviour
{
    private AudioSource goalSound;

    private void Awake()
    {
        goalSound = GetComponent<AudioSource>();
    }
    
    public void playSound()
    {
        goalSound.Play(0);
    }
}
