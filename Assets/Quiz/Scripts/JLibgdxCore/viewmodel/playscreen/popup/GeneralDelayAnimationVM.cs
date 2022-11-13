using hundun.quizlib.prototype.@event;
using hundun.quizlib.prototype.match;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static AbstractAnimationVM;

public class GeneralDelayAnimationVM : AbstractAnimationVM<float>
{
    CallerAndCallback callerAndCallback;

    public void postPrefabInitialization(QuizGdxGame game, CallerAndCallback callerAndCallback)
    {
        base.postPrefabInitialization(game, callerAndCallback);
        this.callerAndCallback = callerAndCallback;
    }

    public override void callShow(float delaySecond)
    {
        FakeAnimation animation;
        int fakeRegionArraySize = 1;
        float frameDuration = delaySecond / fakeRegionArraySize;
        animation = new FakeAnimation(
                    TextureConfig.getAnimationsTextureAtlas("delay"),
                    frameDuration, fakeRegionArraySize
                    );

        this.myAnimation = (animation);
        base.resetFrame();
    }

    public  interface CallerAndCallback : IAnimationCallback
    {
        void callShowGeneralDelayAnimation(float second);
    }
}
