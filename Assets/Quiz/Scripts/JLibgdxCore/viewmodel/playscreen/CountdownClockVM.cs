using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;

public class CountdownClockVM : MonoBehaviour
{

    private CallerAndCallback callerAndCallback;
    private LogicFrameHelper logicFrameHelper;
    
    private static readonly String WORD = "√Î";
    private Image image;
    GameObject textAreaTable;
    private Text wordPart;
    private Text countdownPart;
    
    private NumberFormat format;
    
    int currentCountdownFrame;
    public bool isCountdownState;
    Sprite[] clockDrawables;

    void Awake()
    {
        this.image = this.transform.Find("_image").GetComponent<Image>();
        this.textAreaTable = this.transform.Find("_textAreaTable").gameObject;
        this.wordPart = textAreaTable.transform.Find("_wordPart").GetComponent<Text>();
        this.countdownPart = textAreaTable.transform.Find("_countdownPart").GetComponent<Text>();
    }

    public void postPrefabInitialization(
        QuizGdxGame game,
        CallerAndCallback callerAndCallback,
        LogicFrameHelper logicFrameHelper
        )
    {
        this.callerAndCallback = callerAndCallback;
        this.logicFrameHelper = logicFrameHelper;
        this.format = NumberFormat.getFormat(1, 0);
        this.clockDrawables = new Sprite[TextureAtlasKeys.PLAYSCREEN_CLOCK_SIZE];
        for (int i = 0; i < TextureAtlasKeys.PLAYSCREEN_CLOCK_SIZE; i++)
        {
            clockDrawables[i] = TextureConfig.getPlayScreenUITextureAtlas_findRegion(TextureAtlasKeys.PLAYSCREEN_CLOCK, i);
        }
        wordPart.text = WORD;

        clearCountdown();
    }


    public void updateCoutdownSecond(int countdownModify)
    {
        currentCountdownFrame += countdownModify;
        double second = logicFrameHelper.frameNumToSecond(currentCountdownFrame);

        countdownPart.text = (format.format(second));
        int clockImageIndex = ((int)second) % clockDrawables.Length;
        image.sprite = (clockDrawables[clockImageIndex]);

        if (second < 0.0000001)
        {
            clearCountdown();
            callerAndCallback.onCountdownZero();
        }
    }

    public void resetCountdown(double second)
    {
        this.isCountdownState = true;
        this.currentCountdownFrame = logicFrameHelper.secondToFrameNum(second);
        textAreaTable.SetActive(true);
        updateCoutdownSecond(0);
    }

    public void clearCountdown()
    {
        this.isCountdownState = false;
        this.currentCountdownFrame = 0;
        textAreaTable.SetActive(false);
    }

    public interface CallerAndCallback
    {
        void onCountdownZero();
    }
}
