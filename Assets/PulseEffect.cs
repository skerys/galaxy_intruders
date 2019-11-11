using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseEffect : MonoBehaviour
{
    [SerializeField] float scaleSpeed = 2.0f;
    [SerializeField] float destroyTimer = 3.0f;


    private void Start()
    {
        Destroy(gameObject, destroyTimer);
    }
    // Update is called once per frame
    void Update()
    {
        transform.localScale += Vector3.one * Time.deltaTime * scaleSpeed;
    }
}
