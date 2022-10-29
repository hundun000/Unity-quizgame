using hundun.quizlib.prototype.match;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class JMatchStrategyInfoVM : MonoBehaviour
{

    internal Text _nameLabel;
    internal Text[,] _labelMap;

    internal GameObject[] _labelEntryGameObject;
    internal GameObject[,] _labelGameObject;

    const int MAP_LINE_SIZE = 5;

    void Awake()
    {
        this._nameLabel = this.transform.Find("_nameLabel").GetComponent<Text>();
        this._labelMap = new Text[MAP_LINE_SIZE, 2];
        this._labelEntryGameObject = new GameObject[MAP_LINE_SIZE];
        this._labelGameObject = new GameObject[MAP_LINE_SIZE, 2];

        for (int x = 0; x < _labelGameObject.GetLength(0); x += 1)
        {
            _labelEntryGameObject[x] = this.transform.Find(String.Format("_labelMapTable/_entry ({0})", x)).gameObject;
            for (int y = 0; y < _labelGameObject.GetLength(1); y += 1)
            {
                _labelGameObject[x, y] = _labelEntryGameObject[x].transform.Find(String.Format("_labelMap_X{0}", y)).gameObject;
                _labelMap[x, y] = _labelGameObject[x, y].GetComponent<Text>();
            }
        }

        _labelMap[0, 0].text = ("每题时间限制：");
        _labelMap[1, 0].text = ("每局答题总数限制：");
        _labelMap[2, 0].text = ("是否可使用技能：");
        _labelMap[3, 0].text = ("轮换队伍的答题数：");
        _labelMap[4, 0].text = ("参赛队伍数量限制：");

    }


    internal static string toMatchStrategyTypeChinese(MatchStrategyType type)
    {
        switch (type)
        {
            case MatchStrategyType.PRE:
                return "预赛";
            case MatchStrategyType.MAIN:
                return "决赛";
            default:
                throw new Exception();
        }
    }

    internal void updateStrategy(MatchStrategyType type)
    {
        switch (type)
        {
            case MatchStrategyType.PRE:
                _nameLabel.text = (toMatchStrategyTypeChinese(type));
                _labelMap[0, 1].text = ("20秒");
                _labelMap[1, 1].text = ("5");
                _labelMap[2, 1].text = ("否");
                _labelMap[3, 1].text = ("");
                _labelMap[4, 1].text = ("1");
                break;
            case MatchStrategyType.MAIN:
                _nameLabel.text = (toMatchStrategyTypeChinese(type));
                _labelMap[0, 1].text = ("20秒");
                _labelMap[1, 1].text = ("");
                _labelMap[2, 1].text = ("是");
                _labelMap[3, 1].text = ("1");
                _labelMap[4, 1].text = ("2");
                break;
            default:
                _nameLabel.text = (toMatchStrategyTypeChinese(type));
                _labelMap[1, 0].text = ("");
                _labelMap[1, 1].text = ("");
                _labelMap[1, 2].text = ("");
                _labelMap[1, 3].text = ("");
                _labelMap[1, 4].text = ("");
                break;
        }

        for (int x = 0; x < _labelGameObject.GetLength(0); x += 1)
        {
            
            if (_labelMap[x, 1].text.Length == 0)
            {
                _labelEntryGameObject[x].SetActive(false);
                //labelEntryGameObjectMap[x].transform.SetParent(null);

            }
            else
            {
                _labelEntryGameObject[x].SetActive(true);
                //labelEntryGameObjectMap[x].transform.SetParent(labelGameObjectTable.transform);
            }

        }
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
