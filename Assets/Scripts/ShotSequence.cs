using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSequence : MonoBehaviour
{
    private ShipInput input;
    private ProjectileLauncher launcher;

    [SerializeField]
    private List<ProjectileType> projectileSequence = new List<ProjectileType>();
    private int currentIndex = 0;

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
    }
}
