using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShotSequence : MonoBehaviour
{
    private ShipInput input;
    private ProjectileLauncher launcher;

    [SerializeField]
    private List<ProjectileType> projectileSequence = new List<ProjectileType>();
    private int currentIndex = 0;

    [HideInInspector]
    public event Action ShotSequenceUpdated = delegate { };
    [HideInInspector]
    public event Action ShotSequecteCurrentChanged = delegate { };

    private void OnEnable()
    {
        input.OnPrimaryFire += CycleSequence;   
    }

    private void OnDisable()
    {
        input.OnPrimaryFire -= CycleSequence;
    }

    private void CycleSequence()
    {

        currentIndex++;
        if (currentIndex >= projectileSequence.Count)
            currentIndex = 0;
        launcher.type = projectileSequence[currentIndex];
        ShotSequecteCurrentChanged();
    }

    private void Awake()
    {
        input = GetComponent<ShipInput>();
        launcher = GetComponent<ProjectileLauncher>();
        //projectileSequence = new List<ProjectileType>();
        //projectileSequence.Add(ProjectileType.Simple);
    }

    public void AddToSequence(ProjectileType type)
    {
        projectileSequence.Add(type);
        ShotSequenceUpdated();
        ShotSequecteCurrentChanged();
    }

    public List<ProjectileType> GetSequence()
    {
        return projectileSequence;
    }

    public int GetCurrentIndex()
    {
        return currentIndex;
    }
}
