using hundun.quizlib.prototype;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TagManageSlotVM : MonoBehaviour
{
    public static readonly int NODE_WIDTH = 800;
    public static readonly int NODE_HEIGHT = 100;
    String tagName;
    TeamPrototype currenTeamPrototype;


    GameObject _label;
    GameObject _normalCheckBox;
    GameObject _pickedCheckBox;
    GameObject _bannedCheckBox;
    GameObject _buttonGroup;

    Toggle normalCheckBox;
    Toggle pickedCheckBox;
    Toggle bannedCheckBox;
    ToggleGroup buttonGroup;

    // Use this for initialization
    void Awake()
    {
        this._label = this.transform.Find("_label").gameObject;
        this._buttonGroup = this.transform.Find("_buttonGroup").gameObject;

        this._normalCheckBox = _buttonGroup.transform.Find("_normalCheckBox").gameObject;
        this._pickedCheckBox = _buttonGroup.transform.Find("_pickedCheckBox").gameObject;
        this._bannedCheckBox = _buttonGroup.transform.Find("_bannedCheckBox").gameObject;

        this.buttonGroup = _buttonGroup.GetComponent<ToggleGroup>();
        this.normalCheckBox = _normalCheckBox.GetComponent<Toggle>();
        this.pickedCheckBox = _pickedCheckBox.GetComponent<Toggle>();
        this.bannedCheckBox = _bannedCheckBox.GetComponent<Toggle>();

        // ------ business ------
        _normalCheckBox.transform.Find("_label").gameObject.GetComponent<Text>().text = "normal";
        _pickedCheckBox.transform.Find("_label").gameObject.GetComponent<Text>().text = "picked";
        _bannedCheckBox.transform.Find("_label").gameObject.GetComponent<Text>().text = "banned";

        normalCheckBox.group = buttonGroup;
        pickedCheckBox.group = buttonGroup;
        bannedCheckBox.group = buttonGroup;

        normalCheckBox.onValueChanged.AddListener((bool cuttrntState) => { 
            if (cuttrntState)
            {
                currenTeamPrototype.pickTags.Remove(tagName);
                currenTeamPrototype.banTags.Remove(tagName);
            }
        });
        pickedCheckBox.onValueChanged.AddListener((bool cuttrntState) => {
            if (cuttrntState)
            {
                currenTeamPrototype.pickTags.Add(tagName);
                currenTeamPrototype.banTags.Remove(tagName);
            }
        });
        bannedCheckBox.onValueChanged.AddListener((bool cuttrntState) => {
            if (cuttrntState)
            {
                currenTeamPrototype.pickTags.Remove(tagName);
                currenTeamPrototype.banTags.Add(tagName);
            }
        });

    }

    public void updateData(String tagName, TeamPrototype currenTeamPrototype)
    {
        this.tagName = tagName;
        this.currenTeamPrototype = currenTeamPrototype;

        _label.GetComponent<Text>().text = (tagName);
        if (currenTeamPrototype.pickTags.Contains(tagName))
        {
            pickedCheckBox.isOn = (true);
        }
        else if (currenTeamPrototype.banTags.Contains(tagName))
        {
            bannedCheckBox.isOn = (true);
        }
        else
        {
            normalCheckBox.isOn = (true);
        }
    }
}
