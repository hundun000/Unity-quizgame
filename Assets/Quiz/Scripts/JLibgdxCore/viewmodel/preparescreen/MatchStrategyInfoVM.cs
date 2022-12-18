using hundun.quizlib.prototype.match;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class MatchStrategyInfoVM : MonoBehaviour
{

    internal Text nameLabel;
    internal Text[,] labelMap;

    internal GameObject[] _labelEntryGameObject;
    internal GameObject[,] _labelGameObject;

    const int MAP_LINE_SIZE = 5;

    void Awake()
    {
        this.nameLabel = this.transform.Find("_nameLabel").GetComponent<Text>();
        this.labelMap = new Text[MAP_LINE_SIZE, 2];
        this._labelEntryGameObject = new GameObject[MAP_LINE_SIZE];
        this._labelGameObject = new GameObject[MAP_LINE_SIZE, 2];

        for (int x = 0; x < _labelGameObject.GetLength(0); x += 1)
        {
            _labelEntryGameObject[x] = this.transform.Find(String.Format("_labelMapTable/_entry ({0})", x)).gameObject;
            for (int y = 0; y < _labelGameObject.GetLength(1); y += 1)
            {
                _labelGameObject[x, y] = _labelEntryGameObject[x].transform.Find(String.Format("_labelMap_X{0}", y)).gameObject;
                labelMap[x, y] = _labelGameObject[x, y].GetComponent<Text>();
            }
        }

        labelMap[0, 0].text = ("每题时间限制：");
        labelMap[1, 0].text = ("每局答题总数限制：");
        labelMap[2, 0].text = ("是否可使用技能：");
        labelMap[3, 0].text = ("轮换队伍的答题数：");
        labelMap[4, 0].text = ("参赛队伍数量限制：");

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
                nameLabel.text = (toMatchStrategyTypeChinese(type));
                labelMap[0, 1].text = ("20秒");
                labelMap[1, 1].text = ("5");
                labelMap[2, 1].text = ("否");
                labelMap[3, 1].text = ("");
                labelMap[4, 1].text = ("1");
                break;
            case MatchStrategyType.MAIN:
                nameLabel.text = (toMatchStrategyTypeChinese(type));
                labelMap[0, 1].text = ("20秒");
                labelMap[1, 1].text = ("");
                labelMap[2, 1].text = ("是");
                labelMap[3, 1].text = ("1");
                labelMap[4, 1].text = ("2");
                break;
            default:
                nameLabel.text = (toMatchStrategyTypeChinese(type));
                labelMap[1, 0].text = ("");
                labelMap[1, 1].text = ("");
                labelMap[1, 2].text = ("");
                labelMap[1, 3].text = ("");
                labelMap[1, 4].text = ("");
                break;
        }

        for (int x = 0; x < _labelGameObject.GetLength(0); x += 1)
        {
            
            if (labelMap[x, 1].text.Length == 0)
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


    void Start()
    {
        this.GetComponent<Image>().sprite = TextureConfig.getMyNinePatch();
    }

}
