using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class PointShower : MonoBehaviour
{
    public Animator animator;
    public Text pointsText;

    public GameObject perfect;
    public void Initialize(int points, Vector3 pos)
    {
        var canvasRect = GetComponent<RectTransform>();

        canvasRect.anchoredPosition = pos;
        pointsText.text = points.ToString();
        animator.SetTrigger("Show");

        perfect.SetActive(points >= GameEvents.GetMaxpoints());
    }
}
