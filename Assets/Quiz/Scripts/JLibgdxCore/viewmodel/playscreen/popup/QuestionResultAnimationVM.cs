using hundun.quizlib.prototype.@event;
using hundun.quizlib.prototype.match;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionResultAnimationVM : AbstractAnimationVM<AnswerResultEvent>
{
    CallerAndCallback callerAndCallback;

    public void postPrefabInitialization(QuizGdxGame game, CallerAndCallback callerAndCallback)
    {
        base.postPrefabInitialization(game, callerAndCallback);
        this.callerAndCallback = callerAndCallback;
    }

    public override void callShow(AnswerResultEvent answerResultEvent)
    {
        GdxAnimation<Sprite> animation;
        if (answerResultEvent.result == AnswerType.CORRECT)
        {
            animation = aminationFactory(
                    "break", 0.25f, PlayMode.REVERSED
                    );
        }
        else if (answerResultEvent.result == AnswerType.WRONG
                || answerResultEvent.result == AnswerType.SKIPPED)
        {
            animation = aminationFactory(
                    "continue", 0.25f, PlayMode.REVERSED
                    );
        }
        else if (answerResultEvent.result == AnswerType.TIMEOUOT_WRONG)
        {
            animation = aminationFactory(
                    "timeout", 0.25f, PlayMode.REVERSED
                    );
        }
        else
        {
            throw new Exception("cannot handle AnswerType = " + answerResultEvent.result);
        }

        this.myAnimation = (animation);
        base.resetFrame();
    }

    public interface CallerAndCallback : IAnimationCallback
    {
        void callShowQuestionResultAnimation(AnswerResultEvent answerResultEvent);
    }

}
