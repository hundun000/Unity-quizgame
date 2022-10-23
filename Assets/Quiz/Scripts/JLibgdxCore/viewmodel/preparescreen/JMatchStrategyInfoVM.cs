using hundun.quizlib.prototype.match;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JMatchStrategyInfoVM : MonoBehaviour
{
    internal static string toMatchStrategyTypeChinese(MatchStrategyType type)
    {
        switch (type)
        {
            case MatchStrategyType.PRE:
                return "Ô¤Èü";
            case MatchStrategyType.MAIN:
                return "¾öÈü";
            default:
                throw new Exception();
        }
    }

    internal void updateStrategy(MatchStrategyType currenType)
    {
        // TODO
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
