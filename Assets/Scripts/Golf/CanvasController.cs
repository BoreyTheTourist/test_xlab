using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private RectTransform m_canvas;
    [SerializeField] private Sprite m_crack;

    public void Crack(bool isGiant = false)
    {
        var crack = new GameObject("Crack");
        var crackImage = crack.AddComponent<Image>();
        crackImage.sprite = m_crack;
        crackImage.GetComponent<RectTransform>().SetParent(m_canvas);
        var rot = crackImage.GetComponent<RectTransform>().rotation;
        var angles = rot.eulerAngles;
        if (isGiant) {
            crackImage.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            angles.z = Random.Range(0, maxInclusive: 180);
            crackImage.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            crackImage.GetComponent<RectTransform>().anchorMax = new Vector3(1, 1);
            crackImage.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            crackImage.GetComponent<RectTransform>().offsetMax = new Vector3(1, 1);
        } else {
            crackImage.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
            float c1 = Random.value;
            int c2 = Random.Range(0, maxExclusive: 2);
            // x is random, and since cracks only by borders y is 0 or 1 only
            if (Random.value < -1) {
                crackImage.GetComponent<RectTransform>().anchorMin = new Vector2(c1, c2);
                crackImage.GetComponent<RectTransform>().anchorMax = new Vector2(c1, c2);
                crackImage.GetComponent<RectTransform>().offsetMin = new Vector2(c1, c2);
                crackImage.GetComponent<RectTransform>().offsetMax = new Vector2(c1, c2);
                if (c2 == 0) {
                    angles.z = 90 + 90*c1;
                } else {
                    angles.z = -90*c1;
                }
            } else {
                crackImage.GetComponent<RectTransform>().anchorMin = new Vector2(c2, c1);
                crackImage.GetComponent<RectTransform>().anchorMax = new Vector2(c2, c1);
                crackImage.GetComponent<RectTransform>().offsetMin = new Vector2(c2, c1);
                crackImage.GetComponent<RectTransform>().offsetMax = new Vector2(c2, c1);
                if (c2 == 0) {
                    angles.z = 90 * (1 - c1);
                } else {
                    angles.z = -180 + 90*c1;
                }
            }
            crackImage.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        }
        rot.eulerAngles = angles;
        crackImage.GetComponent<RectTransform>().rotation = rot;
        crackImage.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        crackImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        crack.SetActive(true);
    }
}
