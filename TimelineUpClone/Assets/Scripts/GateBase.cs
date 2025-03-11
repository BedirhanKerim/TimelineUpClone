using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GateBase : MonoBehaviour
{
    [SerializeField] protected int valueCount;
    protected bool _bIsTaken = false;
    [SerializeField] protected GameObject redPlane, greenPlane;
    [SerializeField] protected TextMeshProUGUI valueCountText;
    protected bool bIsGreen = false;
    [SerializeField] protected int minValue, maxValue;
    
    protected void Start()
    {
        valueCount = Mathf.Clamp(valueCount, minValue, maxValue);
        if (valueCount<0)
        {
            valueCountText.text = valueCount.ToString();
            bIsGreen = false;
            greenPlane.SetActive(false);
            redPlane.SetActive(true);
        }
        else
        {
            valueCountText.text = "+" + valueCount;
            bIsGreen = true;
            greenPlane.SetActive(true);
            redPlane.SetActive(false);

        }
    }
}
