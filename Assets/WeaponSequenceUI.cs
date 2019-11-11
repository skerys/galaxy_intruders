using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSequenceUI : MonoBehaviour
{
    [SerializeField] ShotSequence sequence;
    [SerializeField] private List<Sprite> typeSprites;
    [SerializeField] RectTransform currentMarker;

    private List<ProjectileType> sequenceReference;
    private float currentX = 25.0f;

    private void Start()
    {
        sequenceReference = sequence.GetSequence();
    }

    private void OnEnable()
    {
        sequence.ShotSequenceUpdated += UpdateShotSequenceUI;
        sequence.ShotSequecteCurrentChanged += UpdateShotSequenceCurrentUI;
    }

    private void OnDisable()
    {
        sequence.ShotSequenceUpdated -= UpdateShotSequenceUI;
        sequence.ShotSequecteCurrentChanged -= UpdateShotSequenceCurrentUI;
    }

    private void UpdateShotSequenceUI()
    {
        currentX += 50.0f;
        GameObject newObj = new GameObject();
        Image newImage = newObj.AddComponent<Image>();
        newImage.sprite = typeSprites[(int)sequenceReference[sequenceReference.Count - 1]];
        RectTransform rect = newObj.GetComponent<RectTransform>();
        rect.SetParent(this.transform);
        rect.position = new Vector3(currentX, 25.0f, 0f);
        rect.sizeDelta = new Vector2(50.0f, 50.0f);
        newObj.SetActive(true);
    }

    private void UpdateShotSequenceCurrentUI()
    {
        currentMarker.position = new Vector3(sequence.GetCurrentIndex() * 50.0f + 25.0f, 25.0f, 0.0f);
    }

}
