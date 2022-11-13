using hundun.quizlib.prototype.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using static SkillBoardVM;

public class SkillNode : MonoBehaviour
{
    static readonly int LENGTH = 160;

    Image backgroundImage;
    Text mainLabel;
    Text textLabel;
    Sprite normalBackground;
    Sprite useOutBackground;

    Button selfButton;

    void Awake()
    {
        this.backgroundImage = this.transform.Find("_content/_buttonArea").gameObject.GetComponent<Image>();
        this.mainLabel = this.transform.Find("_content/_mainLabel").gameObject.GetComponent<Text>();
        this.textLabel = this.transform.Find("_content/_textLabel").gameObject.GetComponent<Text>();
        this.selfButton = this.transform.Find("_content/_buttonArea").gameObject.GetComponent<Button>();
    }

    public void postPrefabInitialization(
        CallerAndCallback callerAndCallback,
        int index
        )
    {
        selfButton.onClick.AddListener(() =>
        {
            callerAndCallback.onChooseSkill(index);
        });
    }

    public void updatePrototy(SkillSlotPrototype skillSlotPrototype)
    {
        mainLabel.text = (skillSlotPrototype.showName);
        String regionId = TextureAtlasKeys.PLAYSCREEN_SKILLBUTTON_TEMPLATE.Replace("%s", skillSlotPrototype.name);
        normalBackground = TextureConfig.getPlayScreenUITextureAtlas_findRegion(regionId);
        useOutBackground = TextureConfig.getPlayScreenUITextureAtlas_findRegion(TextureAtlasKeys.PLAYSCREEN_SKILLBUTTONUSEOUT);
    }

    public void updateRuntime(int skillRemainTime)
    {
        textLabel.text = (String.Format("Ê£Óà´ÎÊý£º{0}", skillRemainTime));
        Sprite background;
        if (skillRemainTime > 0)
        {
            selfButton.enabled = true;
            background = normalBackground;
        }
        else
        {
            selfButton.enabled = false;
            background = useOutBackground;
        }
        backgroundImage.sprite = (background);
    }
}
