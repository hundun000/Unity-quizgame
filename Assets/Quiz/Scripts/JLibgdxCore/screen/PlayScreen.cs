using hundun.quizlib.prototype.match;
using hundun.quizlib.prototype;
using hundun.quizlib.view.match;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using hundun.quizlib.service;
using System;
using hundun.quizlib.model.domain;
using hundun.quizlib.exception;
using hundun.quizlib.prototype.@event;
using Unity.VisualScripting;
using UnityEngine.Device;

public class PlayScreen : BaseHundunScreen
{

    public GameService quizLib;
    // --- inner class ---
    //private SkillEffectHandler skillEffectHandler = new SkillEffectHandler();
    //private BlockingAnimationQueueHandler animationQueueHandler = new BlockingAnimationQueueHandler();
    //private AnimationCallerAndCallbackDelegation animationCallerAndCallback = new AnimationCallerAndCallbackDelegation();
    public NotificationCallerAndCallbackDelegation notificationCallerAndCallback;
    public QuizInputHandler quizInputHandler;

    // --- for quizLib ---
    public MatchConfig matchConfig;
    public List<TeamPrototype> teamPrototypes;
    public MatchSituationView currentMatchSituationView;

    // ====== onShowLazyInit ======
    //Image backImage;

    // Start is called before the first frame update

    GameObject _pauseNotificationBoardVM;

    override protected void Awake()
    {
        base.Awake();

        
        this.quizInputHandler = _uiRoot.transform.Find("_quizInputHandler").GetComponent<QuizInputHandler>();

        this._pauseNotificationBoardVM = _popoupRoot.transform.Find("_pauseNotificationBoardVM").gameObject;
    }

    override protected void Start()
    {
        base.Start();

        this.notificationCallerAndCallback = new NotificationCallerAndCallbackDelegation(this, _pauseNotificationBoardVM);

        // FIXME fake
        MatchConfig matchConfig = new MatchConfig();
        matchConfig.matchStrategyType = MatchStrategyType.PRE;
        matchConfig.teamNames = new List<string> { BuiltinDataConfiguration.DEMO_LIST_TEAM_NAME_0 };
        matchConfig.questionPackageName = QuestionLoaderService.RELEASE_PACKAGE_NAME;

        LibgdxFeatureExtension.SetScreenChangePushParams(new System.Object[] { matchConfig });

        this.quizLib = game.quizLibBridge.quizComponentContext.gameService;
        this.logicFrameHelper = new LogicFrameHelper(QuizGdxGame.LOGIC_FRAME_PER_SECOND);

        this.matchConfig = (MatchConfig)LibgdxFeatureExtension.GetScreenChangePushParams()[0];
        LibgdxFeatureExtension.log(this.GetType().Name, String.Format(
                "pushParams by matchConfig = {0}",
                matchConfig.ToString()
                ));

        rebuildUI();

        quizInputHandler.handleCreateAndStartMatch();

    }

    private void rebuildUI()
    {
        quizInputHandler.rebuildUI();
    }

    /**
         * @param 一般情况取值ABCD；作为超时传QuestionModel.TIMEOUT_ANSWER_TEXT；作为跳过时传QuestionModel.SKIP_ANSWER_TEXT
         */
    public void onChooseOptionOrCountdownZero(String ansOrControl)
    {
        try
        {
            // --- call lib ---
            //currentMatchSituationView = quizLib.teamAnswer(currentMatchSituationView.id, ansOrControl);
            //AnswerResultEvent answerResultEvent = JavaFeatureForGwt.requireNonNull(currentMatchSituationView.answerResultEvent);
            //LibgdxFeatureExtension.log(this.GetType().Name, String.Format(
            //        "answerResultEvent by Result = {0}",
            //        answerResultEvent.result
            //        ));

            //SwitchTeamEvent switchTeamEvent = currentMatchSituationView.switchTeamEvent;
            //MatchFinishEvent matchFinishEvent = currentMatchSituationView.finishEvent;
            // --- post ---
            //countdownClockVM.clearCountdown();
            //questionResourceAreaVM.stopAudio();
            //questionOptionAreaVM.showAllOption();

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
