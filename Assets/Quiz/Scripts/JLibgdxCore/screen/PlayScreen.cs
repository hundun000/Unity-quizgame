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

        // temp when as first scece
        game.gameLoadOrNew(false);

        this.notificationCallerAndCallback = new NotificationCallerAndCallbackDelegation(this, _pauseNotificationBoardVM);

        // FIXME fake
        MatchConfig matchConfig = new MatchConfig();
        matchConfig.matchStrategyType = MatchStrategyType.PRE;
        matchConfig.teamNames = new List<string> { JLibDataConfiguration.ZACA_TEAM_NAME_1 };
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


    override protected void onLogicFrame()
    {
        quizInputHandler.onLogicFrame();

    }

}
