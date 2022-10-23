using hundun.quizlib.prototype.match;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JMatchStrategyNode : MonoBehaviour
{

    private MatchStrategyType type;
    Text m_NameLabel;


    // Start is called before the first frame update
    void Awake()
    {
        this.m_NameLabel = this.gameObject.transform.Find("text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void updatePrototypeAKASetCharacterData(MatchStrategyType type)
    {
        this.type = type;
        m_NameLabel.text = (JMatchStrategyInfoVM.toMatchStrategyTypeChinese(type));
    }
}
