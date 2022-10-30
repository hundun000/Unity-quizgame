using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static QuestionOptionAreaVM;

public class OptionNode : MonoBehaviour
{

    public OptionButtonShowState showState;
    int index;
    Sprite correctMask;
    Sprite wrongMask;
    Text textButton;
    Sprite selectedAtlasRegion;
    Sprite unSelectedAtlasRegion;
    Image maskActor;
    Image background;
    Button buttonComponent;
    void Awake()
    {
        this.textButton = this.transform.Find("_content/_textButton").gameObject.GetComponent<Text>();
        this.maskActor = this.transform.Find("_content/_maskActor").gameObject.GetComponent<Image>();
        this.background = this.transform.Find("_content/_background").gameObject.GetComponent<Image>();
        this.buttonComponent = this.transform.Find("_content").gameObject.GetComponent<Button>();
    }

    public void postPrefabInitialization(QuizGdxGame game, int index, QuestionOptionAreaVM.CallerAndCallback callerAndCallback)
    {
        this.index = index;
        this.selectedAtlasRegion = TextureConfig.getPlayScreenUITextureAtlas_findRegion(TextureAtlasKeys.PLAYSCREEN_OPTIONBUTTON, 0);
        this.unSelectedAtlasRegion = TextureConfig.getPlayScreenUITextureAtlas_findRegion(TextureAtlasKeys.PLAYSCREEN_OPTIONBUTTON, 1);
        this.correctMask = TextureConfig.getPlayScreenUITextureAtlas_findRegion(TextureAtlasKeys.MASK_CORRECTOPTION);
        this.wrongMask = TextureConfig.getPlayScreenUITextureAtlas_findRegion(TextureAtlasKeys.MASK_WRONGOPTION);

        buttonComponent.onClick.AddListener(() => {
            this.background.sprite = (selectedAtlasRegion);
            callerAndCallback.onChooseOption(index);
        });
    }

    public void updateForNewQuestion(String optiontext, bool isCorrect)
    {
        this.textButton.text = (optiontext);

        //            this.maskActor.setDrawable(null);
        //            this.setMask(null);
        this.background.sprite = (unSelectedAtlasRegion);
        if (isCorrect)
        {
            this.showState = OptionButtonShowState.HIDE_CORRECT;
        }
        else
        {
            this.showState = OptionButtonShowState.HIDE_WRONG;
        }
        this.maskActor.sprite = (null);
    }

    public void updateShowStateToShow()
    {
        Sprite mask;
        if (this.showState == OptionButtonShowState.HIDE_CORRECT)
        {
            this.showState = OptionButtonShowState.SHOW_CORRECT;
            mask = correctMask;
        }
        else
        {
            this.showState = OptionButtonShowState.SHOW_WRONG;
            mask = wrongMask;
        }
        //this.setMask(new TextureRegionDrawable(texture));

        this.maskActor.sprite = (mask);

        //this.setBackground(new TextureRegionDrawable(texture));
    }
}
