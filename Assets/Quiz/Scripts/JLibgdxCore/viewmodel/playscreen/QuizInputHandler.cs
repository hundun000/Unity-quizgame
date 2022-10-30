using hundun.quizlib.exception;
using hundun.quizlib.model.domain;
using hundun.quizlib.prototype.@event;
using hundun.quizlib.prototype.match;
using hundun.quizlib.prototype;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuizInputHandler : MonoBehaviour,
    CountdownClockVM.CallerAndCallback,
    QuestionOptionAreaVM.CallerAndCallback
{

    PlayScreen owner;

    CountdownClockVM countdownClockVM;
    QuestionStemVM questionStemVM;
    TeamInfoBoardVM teamInfoBoardVM;
    SystemBoardVM systemBoardVM;
    //QuestionResourceAreaVM questionResourceAreaVM;
    QuestionOptionAreaVM questionOptionAreaVM;
    //SkillBoardVM skillBoardVM;


    void Awake()
    {
        this.owner = this.GetComponentInParent<PlayScreen>();
        this.countdownClockVM = this.transform.Find("_countdownClockVM").GetComponent<CountdownClockVM>();
        this.questionStemVM = this.transform.Find("_questionStemVM").GetComponent<QuestionStemVM>();
        this.teamInfoBoardVM = this.transform.Find("_teamInfoBoardVM").GetComponent<TeamInfoBoardVM>();
        this.systemBoardVM = this.transform.Find("_systemBoardVM").GetComponent<SystemBoardVM>();
        this.questionOptionAreaVM = this.transform.Find("_questionOptionAreaVM").GetComponent<QuestionOptionAreaVM>();
    }


    internal void handleCreateAndStartMatch()
    {
        try
        {
            owner.currentMatchSituationView = owner.quizLib.createMatch(owner.matchConfig);
            owner.currentMatchSituationView = owner.quizLib.startMatch(owner.currentMatchSituationView.id);
            StartMatchEvent startMatchEvent = JavaFeatureForGwt.requireNonNull(owner.currentMatchSituationView.startMatchEvent);
            LibgdxFeatureExtension.log(this.GetType().Name, String.Format(
                    "startMatch by QuestionIds = {0}",
                    startMatchEvent.questionIds
                    ));
            // optional more startMatchEvent handle
            owner.teamPrototypes = startMatchEvent.teamPrototypes;

            handleCurrentTeam(true);
            owner.quizInputHandler.handleNewQuestion();
        }
        catch (QuizgameException e)
        {
            LibgdxFeatureExtension.error(this.GetType().Name, "QuizgameException", e);
        }


        owner.notificationCallerAndCallback.callShowPauseConfirm();
    }

    private void handleNewQuestion()
    {
        try
        {
            owner.currentMatchSituationView = owner.quizLib.nextQustion(owner.currentMatchSituationView.id);
        }
        catch (QuizgameException e)
        {
            LibgdxFeatureExtension.error(this.GetType().Name, "QuizgameException", e);
            return;
        }
        SwitchQuestionEvent switchQuestionEvent = JavaFeatureForGwt.requireNonNull(owner.currentMatchSituationView.switchQuestionEvent);

        LibgdxFeatureExtension.log(this.GetType().Name, String.Format(
                "switchQuestion by time second = {0}, Question = {1}",
                switchQuestionEvent.time,
                owner.currentMatchSituationView.question
                ));

        countdownClockVM.resetCountdown(switchQuestionEvent.time);
        questionOptionAreaVM.updateQuestion(owner.currentMatchSituationView.question);
        questionStemVM.updateQuestion(owner.currentMatchSituationView.question);

    }

    internal void rebuildUI()
    {
        countdownClockVM.postPrefabInitialization(owner.game, this, owner.logicFrameHelper);
        // questionStemVM do nothing
    }

    void CountdownClockVM.CallerAndCallback.onCountdownZero()
    {
        LibgdxFeatureExtension.log(this.GetType().Name, "onCountdownZero called");
        onChooseOptionOrCountdownZero(QuestionModel.TIMEOUT_ANSWER_TEXT);
    }

    private void handleCurrentTeam(bool isNewPrototypes)
    {

        if (isNewPrototypes)
        {
            teamInfoBoardVM.updateTeamPrototype(owner.teamPrototypes);
            TeamPrototype currentTeamPrototype = owner.teamPrototypes[owner.currentMatchSituationView.currentTeamIndex];
            // TODO
            //skillBoardVM.updateRole(currentTeamPrototype.getRolePrototype(), currentMatchSituationView.getCurrentTeamRuntimeInfo().getRoleRuntimeInfo());
        }
        teamInfoBoardVM.updateTeamRuntime(owner.matchConfig.matchStrategyType, owner.currentMatchSituationView.currentTeamIndex, owner.currentMatchSituationView.teamRuntimeInfos);
    }

    internal void onLogicFrame()
    {
        if (countdownClockVM.isCountdownState)
        {
            countdownClockVM.updateCoutdownSecond(-1);
        }
    }

    public void onChooseOption(int index)
    {
        throw new NotImplementedException();
    }

    /**
     * @param 一般情况取值ABCD；作为超时传QuestionModel.TIMEOUT_ANSWER_TEXT；作为跳过时传QuestionModel.SKIP_ANSWER_TEXT
     */
    public void onChooseOptionOrCountdownZero(String ansOrControl)
    {
        try
        {
            //---call lib-- -
            owner.currentMatchSituationView = owner.quizLib.teamAnswer(owner.currentMatchSituationView.id, ansOrControl);
            AnswerResultEvent answerResultEvent = JavaFeatureForGwt.requireNonNull(owner.currentMatchSituationView.answerResultEvent);
            LibgdxFeatureExtension.log(this.GetType().Name, String.Format(
                    "answerResultEvent by Result = {0}",
                    answerResultEvent.result
                    ));

            SwitchTeamEvent switchTeamEvent = owner.currentMatchSituationView.switchTeamEvent;
            MatchFinishEvent matchFinishEvent = owner.currentMatchSituationView.finishEvent;
            //---post-- -
            countdownClockVM.clearCountdown();
            //questionResourceAreaVM.stopAudio();
            questionOptionAreaVM.showAllOption();

            //animationQueueHandler.addAnimationTask(()->animationCallerAndCallback.callShowQuestionResultAnimation(answerResultEvent));
            //animationQueueHandler.addAnimationTask(()->animationCallerAndCallback.callShowGeneralDelayAnimation(3.0f));

            //if (matchFinishEvent != null)
            //{
            //    animationQueueHandler.addAnimationTask(()->notificationCallerAndCallback.callShowMatchFinishConfirm());
            //    animationQueueHandler.setAfterAllAnimationDoneTask(()-> {
            //        handleCurrentTeam(false);
            //        handelExitAsFinishMatch(toHistory());
            //    });
            //}
            //else
            //{
            //    if (switchTeamEvent != null)
            //    {
            //        animationQueueHandler.addAnimationTask(()->animationCallerAndCallback.callShowTeamSwitchAnimation(switchTeamEvent));
            //    }
            //    animationQueueHandler.setAfterAllAnimationDoneTask(()-> {
            //        // --- quiz logic ---
            //        handleCurrentTeam(false);
            //        handleNewQuestion();
            //    });
            //}
            //animationQueueHandler.checkNextAnimation();


        }
        catch (QuizgameException e)
        {
            LibgdxFeatureExtension.error(this.GetType().Name, "QuizgameException", e);
        }
    }

}
