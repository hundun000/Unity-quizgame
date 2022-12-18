using hundun.quizlib.prototype.match;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchStrategyNode : MonoBehaviour
{

    internal MatchStrategyType type;

    internal static int SIGN_SIZE = 50;
    internal Sprite signDrawable;
    internal static int NAME_WIDTH = 200;

    internal Text _nameLabel;
    internal Image _signSlotImage;

    // Start is called before the first frame update
    void Awake()
    {
        this._nameLabel = this.transform.Find("_nameLabel").GetComponent<Text>();
        this._signSlotImage = this.transform.Find("_signSlotImage").GetComponent<Image>();
        this.signDrawable = TextureConfig.getPlayScreenUITextureAtlas_findRegion(TextureAtlasKeys.pLAYSCREEN_CURRENTTEAMSIGN_BLACK);

        _signSlotImage.sprite = (signDrawable);
    }

    // Update is called once per frame
    void Update()
    {
        //var rectTransform = this.GetComponent<RectTransform>();
        //rectTransform.sizeDelta = new Vector2(NAME_WIDTH - rectTransform.rect.width, SIGN_SIZE - rectTransform.rect.height);
    }

    internal void postPrefabInitialization(MatchStrategySelectVM parent)
    {
        this.GetComponent<Button>().onClick.AddListener(() => parent.checkSlotNum(this.type));
    }

    internal void updatePrototype(MatchStrategyType type)
    {
        this.type = type;
        _nameLabel.text = (MatchStrategyInfoVM.toMatchStrategyTypeChinese(type));
    }

    internal void updateRuntime(bool isCurrent)
    {
        if (isCurrent)
        {
            _signSlotImage.enabled = true;
        }
        else
        {
            _signSlotImage.enabled = false;
        }
    }
}
