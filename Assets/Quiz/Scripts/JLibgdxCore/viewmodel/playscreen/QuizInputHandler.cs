using hundun.quizlib.exception;
using hundun.quizlib.model.domain;
using hundun.quizlib.prototype.@event;
using hundun.quizlib.prototype.match;
using hundun.quizlib.prototype;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static HistoryScreen;
using System.Linq;
using UnityEngine.SceneManagement;
using static QuestionResourceAreaVM;
using hundun.quizlib.prototype.skill;

public class QuizInputHandler : MonoBehaviour,
    CountdownClockVM.CallerAndCallback,
    QuestionOptionAreaVM.CallerAndCallback,
    SkillBoardVM.CallerAndCallback,
    SystemBoardVM.CallerAndCallback,
    IAudioCallback
{

    PlayScreen owner;

    internal CountdownClockVM countdownClockVM;
    internal QuestionStemVM questionStemVM;
    internal TeamInfoBoardVM teamInfoBoardVM;
    internal SystemBoardVM systemBoardVM;
    internal QuestionResourceAreaVM questionResourceAreaVM;
    internal QuestionOptionAreaVM questionOptionAreaVM;
    internal SkillBoardVM skillBoardVM;


    void Awake()
    {
        this.owner = this.GetComponentInParent<PlayScreen>();
        this.countdownClockVM = this.transform.Find("_countdownClockVM").GetComponent<CountdownClockVM>();
        this.questionStemVM = this.transform.Find("_questionStemVM").GetComponent<QuestionStemVM>();
        this.teamInfoBoardVM = this.transform.Find("_teamInfoBoardVM").GetComponent<TeamInfoBoardVM>();
        this.systemBoardVM = this.transform.Find("_systemBoardVM").GetComponent<SystemBoardVM>();
        this.questionResourceAreaVM = this.transform.Find("_questionResourceAreaVM").GetComponent<QuestionResourceAreaVM>();
        this.questionOptionAreaVM = this.transform.Find("_questionOptionAreaVM").GetComponent<QuestionOptionAreaVM>();
        this.skillBoardVM = this.transform.Find("_skillBoardVM").GetComponent<SkillBoardVM>();
    }

    public void postPrefabInitialization(
        QuizGdxGame game
        )
    {
        countdownClockVM.postPrefabInitialization(owner.game, this, owner.logicFrameHelper);
        questionResourceAreaVM.postPrefabInitialization(game, this);
        skillBoardVM.postPrefabInitialization(this);
        systemBoardVM.postPrefabInitialization(game, this);
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
                    string.Join(",", startMatchEvent.questionIds)
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
        questionResourceAreaVM.updateQuestion(owner.currentMatchSituationView.question);
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
            skillBoardVM.updateRole(currentTeamPrototype.rolePrototype, owner.currentMatchSituationView.currentTeamRuntimeInfo.roleRuntimeInfo);
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
        LibgdxFeatureExtension.log(this.GetType().Name, "onChooseOption called");
        String ans;
        switch (index)
        {
            default:
            case 0:
                ans = "A";
                break;
            case 1:
                ans = "B";
                break;
            case 2:
                ans = "C";
                break;
            case 3:
                ans = "D";
                break;
        }
        onChooseOptionOrCountdownZero(ans);
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
            questionResourceAreaVM.stopAudio();
            questionOptionAreaVM.showAllOption();

            owner.animationQueueHandler.addAnimationTask((voidIt)=> owner.animationCallerAndCallback.callShowQuestionResultAnimation(answerResultEvent));
            owner.animationQueueHandler.addAnimationTask((voidIt) => owner.animationCallerAndCallback.callShowGeneralDelayAnimation(3.0f));

            if (matchFinishEvent != null)
            {
                owner.animationQueueHandler.addAnimationTask((voidIt) => owner.notificationCallerAndCallback.callShowMatchFinishConfirm());
                owner.animationQueueHandler.afterAllAnimationDoneTask = ((voidIt) => {
                    handleCurrentTeam(false);
                    handelExitAsFinishMatch(toHistory());
                });
            }
            else
            {
                if (switchTeamEvent != null)
                {
                    owner.animationQueueHandler.addAnimationTask((voidIt) => owner.animationCallerAndCallback.callShowTeamSwitchAnimation(switchTeamEvent));
                }
                owner.animationQueueHandler.afterAllAnimationDoneTask = ((voidIt) => {
                    // --- quiz logic ---
                    handleCurrentTeam(false);
                    handleNewQuestion();
                });
            }
            owner.animationQueueHandler.checkNextAnimation();


        }
        catch (QuizgameException e)
        {
            LibgdxFeatureExtension.error(this.GetType().Name, "QuizgameException", e);
        }
    }

    public MatchHistoryDTO toHistory()
    {

        MatchHistoryDTO history = new MatchHistoryDTO();
        history.data = (owner.currentMatchSituationView.teamRuntimeInfos
                .ToDictionary(
                        teamRuntimeInfo =>teamRuntimeInfo.name,
                        teamRuntimeInfo =>teamRuntimeInfo.matchScore
                        )
                );
        return history;
    }

    internal void handelExitAsFinishMatch(MatchHistoryDTO history)
    {
        exitClear();

        LibgdxFeatureExtension.SetScreenChangePushParams(new object[] { history });
        SceneManager.LoadScene("HistoryScene");
    }

    private void exitClear()
    {
        owner.matchConfig = null;
        owner.currentMatchSituationView = null;

        owner.animationQueueHandler.clear();
    }

    public void onFirstPlayDone()
    {
        LibgdxFeatureExtension.log(this.GetType().Name, "onFirstPlayDone called");
        owner.logicFrameHelper.logicFramePause = (false);
    }

    public void onPlayReady()
    {
        LibgdxFeatureExtension.log(this.GetType().Name, "onPlayReady called");
        owner.logicFrameHelper.logicFramePause = (true);
    }

    public void onChooseSkill(int index)
    {
        LibgdxFeatureExtension.log(this.GetType().Name, "onChooseSkill called");
        TeamPrototype currentTeamPrototype = owner.teamPrototypes[owner.currentMatchSituationView.currentTeamIndex];
        SkillSlotPrototype skillSlotPrototype = currentTeamPrototype.rolePrototype.skillSlotPrototypes[index];
        try
        {
            owner.currentMatchSituationView = owner.quizLib.teamUseSkill(owner.currentMatchSituationView.id, skillSlotPrototype.name);
            SkillResultEvent skillResultEvent = JavaFeatureForGwt.requireNonNull(owner.currentMatchSituationView.skillResultEvent);
            LibgdxFeatureExtension.log(this.GetType().Name, String.Format(
                    "skillResultEvent by Type = {0}",
                    skillResultEvent.type
                    ));
            owner.animationQueueHandler.addAnimationTask((voidIt) => owner.animationCallerAndCallback.callShowSkillAnimation(skillResultEvent));
            owner.animationQueueHandler.afterAllAnimationDoneTask = ((voidIt) => {
                skillBoardVM.updateSkillRuntime(index, skillResultEvent.skillRemainTime);
                owner.skillEffectHandler.handle(skillResultEvent);
            });
            owner.animationQueueHandler.checkNextAnimation();

        }
        catch (QuizgameException e)
        {
            LibgdxFeatureExtension.error(this.GetType().Name, "QuizgameException", e);
        }
    }

    public void onChooseSystem(SystemButtonType type)
    {
        LibgdxFeatureExtension.log(this.GetType().Name, "onChooseSystem called");
        switch (type)
        {
            case SystemButtonType.PAUSE:
                owner.notificationCallerAndCallback.callShowPauseConfirm();
                break;
            case SystemButtonType.EXIT:
                owner.animationQueueHandler.addAnimationTask((voidIt) => owner.notificationCallerAndCallback.callShowMatchFinishConfirm());
                owner.animationQueueHandler.afterAllAnimationDoneTask = ((voidIt) => {
                    handleCurrentTeam(false);
                    handelExitAsFinishMatch(toHistory());
                });
                owner.animationQueueHandler.checkNextAnimation();
                break;
            default:
                break;
        }
    }
}
