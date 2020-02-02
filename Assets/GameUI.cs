using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class GameUI : MonoBehaviour
{
    public GameObject ScoreText;
    public GameObject FinalScreen;

    Vector2 offset;
    RectTransform rect;

    public RectTransform canvasRect;
    public Text FinalScoreText;
    public Text pointsText;
    void Start()
    {
        rect = GetComponent<RectTransform>();
        offset = new Vector2((float)canvasRect.sizeDelta.x / 2f, (float)canvasRect.sizeDelta.y / 2f);
        pointsText.text ="0";
        FinalScreen.SetActive(false);
    }
    public void ShowPoints(int points, Vector3 worldPos)
    {
        
        var pointsShow = Instantiate(ScoreText, canvasRect);
        pointsShow.GetComponent<PointShower>().Initialize(points, WorldToCanvasPosition(canvasRect, Camera.main, worldPos));
    }

    public void UpdateTotalPoints(int totalPoints)
    {
        pointsText.text = totalPoints.ToString();
    }

    public void ShowEndScreen()
    {
        FinalScoreText.text = GameEvents.CurrentPoints.ToString();
        FinalScreen.SetActive(true);
    }

    private Vector2 WorldToCanvasPosition(RectTransform canvas, Camera camera, Vector3 position) {
         //Vector position (percentage from 0 to 1) considering camera size.
         //For example (0,0) is lower left, middle is (0.5,0.5)
         Vector2 temp = camera.WorldToViewportPoint(position);
    	temp -= new Vector2(0.5f, .5f);
 
         //Calculate position considering our percentage, using our canvas size
         //So if canvas size is (1100,500), and percentage is (0.5,0.5), current value will be (550,250)
          temp.x *= canvas.sizeDelta.x;
          temp.y *= canvas.sizeDelta.y;

 
         //The result is ready, but, this result is correct if canvas recttransform pivot is 0,0 - left lower corner.
         //But in reality its middle (0.5,0.5) by default, so we remove the amount considering cavnas rectransform pivot.
         //We could multiply with constant 0.5, but we will actually read the value, so if custom rect transform is passed(with custom pivot) , 
         //returned value will still be correct.
 
 
         return temp;
     }

}
