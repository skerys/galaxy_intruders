using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossInput : MonoBehaviour, IShipInput
{
    public float Horizontal { get; set; }
    public float Vertical { get; set; }

    public event Action OnPrimaryFire = delegate { };

    [SerializeField] private List<GameObject> triShotGroups;
    [SerializeField] private List<GameObject> sineShots;

    [SerializeField] private float patternChangeDelay = 3.0f;
    private float patternChangeTimer = 0.0f;

    [SerializeField] private float sineShotChangeDelay = 0.5f;
    private float sineShotChangeTimer = 0.0f;

    [SerializeField] private float shootDelay = 0.1f;
    private float shootTimer = 0.0f;

    [SerializeField] private LayerMask boundsLayer = default;

    private int currentPattern = 0;
    private int patternCount = 2;

    private int xSign = 1;

    void Start()
    {

    }

    void Update()
    {
        if(patternChangeTimer >= patternChangeDelay)
        {
            ChangePattern();
            patternChangeTimer = 0.0f;
        }
        else
        {
            patternChangeTimer += Time.deltaTime;
        }

        //Sinshotpattern
        if(currentPattern == 1)
        {
            if (sineShotChangeTimer >= sineShotChangeDelay)
            {
                SetupSineShotsPattern();
                sineShotChangeTimer = 0.0f;
            }
            else
            {
                sineShotChangeTimer += Time.deltaTime;
            }
        }

        if (shootTimer >= shootDelay)
        {
            OnPrimaryFire();
            shootTimer = 0.0f;
        }
        else
        {
            shootTimer += Time.deltaTime;
        }

        var hit = Physics2D.Raycast(transform.position, Vector2.left, 4.0f, boundsLayer);
        if (hit)
        {
            xSign = -1;
        }
        hit = Physics2D.Raycast(transform.position, Vector2.right, 4.0f, boundsLayer);
        if (hit)
        {
            xSign = 1;
        }

        Horizontal = xSign;



    }

    void ChangePattern()
    {
        int newPattern = UnityEngine.Random.Range(0, patternCount); 
        while(newPattern == currentPattern)
        {
            newPattern = UnityEngine.Random.Range(0, patternCount);
        }
        currentPattern = newPattern;

        switch (currentPattern)
        {
            case 0: SetupTrishotPattern(); break;
            case 1: SetupSineShotsPattern(); break;
        }
    }

    void SetupTrishotPattern()
    {
        foreach(var shot in sineShots)
        {
            shot.SetActive(false);
        }
        foreach(var trishot in triShotGroups)
        {
            trishot.SetActive(false);
        }

        var rand = new System.Random();
        var selectedTrishots = new List<GameObject>();

        //Select two random trishots
        for(int i = 0; i < 2; i++)
        {
            var index = rand.Next(triShotGroups.Count);
            selectedTrishots.Add(triShotGroups[index]);
        }

        foreach(var trishot in selectedTrishots)
        {
            trishot.SetActive(true);
        }

    }

    void SetupSineShotsPattern()
    {
        foreach (var shot in sineShots)
        {
            shot.SetActive(false);
        }
        foreach (var trishot in triShotGroups)
        {
            trishot.SetActive(false);
        }

        //select one sineshot

        var rand = new System.Random();
        int index = rand.Next(sineShots.Count);

        sineShots[index].SetActive(true);
    }
}
