using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarningFlash : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] Image image;


    void Update()
    {
        text.color = Color.HSVToRGB(0.0f, Mathf.PingPong(Time.time, 0.7f), 1.0f);
        image.color = Color.HSVToRGB(0.0f, Mathf.PingPong(Time.time, 0.7f), 1.0f);
    }
}
