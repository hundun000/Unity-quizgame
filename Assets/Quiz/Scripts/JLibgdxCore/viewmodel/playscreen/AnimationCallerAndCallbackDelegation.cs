using hundun.quizlib.prototype.@event;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class AnimationCallerAndCallbackDelegation : MonoBehaviour,
    QuestionResultAnimationVM.CallerAndCallback
{
    // --- runtime add to stage ---
    QuestionResultAnimationVM questionResultAnimationVM;
    //TeamSwitchAnimationVM teamSwitchAnimationVM;
    //SkillAnimationVM skillAnimationVM;
    //GeneralDelayAnimationVM generalDelayAnimationVM;
    PlayScreen owner;
    GameObject _questionResultAnimationVM;

    void Awake()
    {
        this.owner = this.GetComponentInParent<PlayScreen>();
        this._questionResultAnimationVM = owner.PopoupRoot.transform.Find("_questionResultAnimationVM").gameObject;
        this.questionResultAnimationVM = _questionResultAnimationVM.GetComponent<QuestionResultAnimationVM>();
    }

    void Start()
    {
        questionResultAnimationVM.postPrefabInitialization(owner.game, this);
    }

    public void callShowQuestionResultAnimation(AnswerResultEvent answerResultEvent)
    {
        generalCallShowAnimation(_questionResultAnimationVM, questionResultAnimationVM, answerResultEvent, true);
    }

    public void onAnimationDone()
    {
        LibgdxFeatureExtension.log(this.GetType().Name, "onAnimationDone called");
        // --- for screen ---
        foreach (Transform child in owner.PopoupRoot.transform)
        {
            child.gameObject.SetActive(false);
        }
        owner.logicFrameHelper.logicFramePause = (false);
        // --- for animationVM ---
        owner.animationQueueHandler.currentAnimationVM = (null);
        owner.animationQueueHandler.checkNextAnimation();
    }


    /**
         * popupRootTable-cell always expand(), fill is optional by argument.
         */
    private void generalCallShowAnimation<T>(
            GameObject _animationVM,
            AbstractAnimationVM<T> animationVM,
            T arg,
            bool fill
            )
    {
        LibgdxFeatureExtension.log(this.GetType().Name, String.Format(
                "generalCallShowAnimation called, animationVM = {0}",
                animationVM.GetType().Name
                ));
        // --- for screen ---
        foreach (Transform child in owner.PopoupRoot.transform)
        {
            child.gameObject.SetActive(false);
        }
        _animationVM.SetActive(true);
        owner.logicFrameHelper.logicFramePause = (true);
        // --- for animationVM ---
        owner.animationQueueHandler.currentAnimationVM = (animationVM);
        animationVM.callShow(arg);
    }

}