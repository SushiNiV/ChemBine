using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode() ]

public class ProgressBar : MonoBehaviour
{
    public int max;
    public int current;
    public Image mask;

    void Start()
    {
        
    }

    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill ()
    {
        float fillAmount = (float)current / (float)max;
        mask.fillAmount = fillAmount;
    }
}
