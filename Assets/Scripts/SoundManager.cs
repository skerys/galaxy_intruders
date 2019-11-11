using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] AudioSource shootSimple;
    [SerializeField] AudioSource shootSine;
    [SerializeField] AudioSource shootBomb;
    [SerializeField] AudioSource shootMissile;
    [SerializeField] AudioSource explosion;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayShootSimple()
    {
        shootSimple.Play();
    }
    public void PlayShootSine()
    {
        shootSine.Play();
    }
    public void PlayShootBomb()
    {
        shootBomb.Play();
    }
    public void PlayShootMissile()
    {
        shootMissile.Play();
    }
    public void PlayExplosion()
    {
        explosion.Play();
    }
}
