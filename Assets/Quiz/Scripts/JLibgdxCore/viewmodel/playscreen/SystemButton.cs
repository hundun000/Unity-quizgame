using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemButton : MonoBehaviour
{
    Image image;
    Button selfButton;

    SystemButtonType type;

    void Awake()
    {
        image = this.transform.Find("_content/_image").GetComponent<Image>();
        selfButton = this.transform.Find("_content").GetComponent<Button>();
    }

    public void postPrefabInitialization(
        QuizGdxGame game,
        SystemBoardVM.CallerAndCallback callerAndCallback,
        SystemButtonType type,
        Sprite buttonAtlasRegion
        )
    {
        this.type = type;
        this.image.sprite = buttonAtlasRegion;
        selfButton.onClick.AddListener(() =>
        {
            callerAndCallback.onChooseSystem(type);
        });

    }
}
