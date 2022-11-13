using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamScorePairSlotVM : MonoBehaviour
{
    Text leftPart;
    Text rightPart;


    void Awake()
    {
        this.leftPart = this.transform.Find("_leftPart").gameObject.GetComponent<Text>();
        this.rightPart = this.transform.Find("_rightPart").gameObject.GetComponent<Text>();
    }

    public void update(String name, int? score)
    {
        if (name != null)
        {
            leftPart.text = name;
            rightPart.text = score + "·Ö";
        }
        else
        {
            leftPart.text = "";
            rightPart.text = "";
        }
    }
}