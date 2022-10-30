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
        CountdownClockVM.CallerAndCallback
{

    PlayScreen owner;

    CountdownClockVM countdownClockVM;
    QuestionStemVM questionStemVM;
    TeamInfoBoardVM teamInfoBoardVM;
    SystemBoardVM systemBoardVM;
    //QuestionResourceAreaVM questionResourceAreaVM;
    //QuestionOptionAreaVM questionOptionAreaVM;
    //SkillBoardVM skillBoardVM;


    void Awake()
    {
        this.owner = this.GetComponentInParent<PlayScreen>();
        this.countdownClockVM = this.transform.Find("_countdownClockVM").GetComponent<CountdownClockVM>();
    }


    internal void handleCreateAndStartMatch()
    {
        try
        {
            owner.currentMatchSituationView = owner.quizLib.createMatch(owner.matchConfig);
            owner.currentMatchSituationView = owner.quizLib.startMatch(owner.currentMatchSituationView.id);
            StartMatchEvent startMatchEvent = JavaFeatureForGwt.requireNonNull(owner.currentMatchSituationView.startMatchEvent);
            LibgdxFeatureExtension.log(this.GetType().Name, String.Format(
                    "startMatch by QuestionIds = %s",
            startMatchEvent.questionIds
                    ));
            // optional more startMatchEvent handle
            owner.teamPrototypes = startMatchEvent.teamPrototypes;

            handleCurrentTeam(true);
            // TODO
            //owner.quizInputHandler.handleNewQuestion();
        }
        catch (QuizgameException e)
        {
            LibgdxFeatureExtension.error(this.GetType().Name, "QuizgameException", e);
        }


        owner.notificationCallerAndCallback.callShowPauseConfirm();
    }

    internal void rebuildUI()
    {
        countdownClockVM.postPrefabInitialization(owner.game, this, owner.logicFrameHelper);
        // questionStemVM do nothing
    }

    void CountdownClockVM.CallerAndCallback.onCountdownZero()
    {
        LibgdxFeatureExtension.log(this.GetType().Name, "onCountdownZero called");
        owner.onChooseOptionOrCountdownZero(QuestionModel.TIMEOUT_ANSWER_TEXT);
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
}
