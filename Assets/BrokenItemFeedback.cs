using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenItemFeedback : MonoBehaviour
{
    public static GameObject staticfeedbackparticle;

    public GameObject feedbackparticle;

    void Start()
    {
        staticfeedbackparticle = feedbackparticle;
    }
}
