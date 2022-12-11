using hundun.quizlib.prototype.@event;
using hundun.quizlib.prototype.match;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamSwitchAnimationVM : AbstractAnimationVM<SwitchTeamEvent>
{
    CallerAndCallback callerAndCallback;
    //Text resultLable;

    public void postPrefabInitialization(QuizGdxGame game, CallerAndCallback callerAndCallback)
    {
        base.postPrefabInitialization(game, callerAndCallback);
        this.callerAndCallback = callerAndCallback;
    }

    public override void callShow(SwitchTeamEvent switchTeamEvent)
    {

        this.myAnimation = aminationFactory(
                "teamSwitch", 0.25f, PlayMode.REVERSED
                );
        //resultLable.text = (switchTeamEvent.toTeamName);
        base.resetFrame();
    }

    public  interface CallerAndCallback : IAnimationCallback
    {
        void callShowTeamSwitchAnimation(SwitchTeamEvent switchTeamEvent);
    }

}
