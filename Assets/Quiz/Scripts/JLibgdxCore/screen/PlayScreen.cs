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
    public SkillEffectHandler skillEffectHandler;
    public BlockingAnimationQueueHandler animationQueueHandler = new BlockingAnimationQueueHandler();
    public AnimationCallerAndCallbackDelegation animationCallerAndCallback;
    public NotificationCallerAndCallbackDelegation notificationCallerAndCallback;
    public QuizInputHandler quizInputHandler;

    // --- for quizLib ---
    public MatchConfig matchConfig;
    public List<TeamPrototype> teamPrototypes;
    public MatchSituationView currentMatchSituationView;

    // ====== onShowLazyInit ======
    //Image backImage;

    // Start is called before the first frame update

    override protected void Awake()
    {
        base.Awake();

        
        this.quizInputHandler = this.UiRoot.transform.Find("_quizInputHandler").GetComponent<QuizInputHandler>();
        this.skillEffectHandler = this.UiRoot.transform.Find("_skillEffectHandler").GetComponent<SkillEffectHandler>();

        this.animationCallerAndCallback = this.transform.Find("_animationCallerAndCallback").GetComponent<AnimationCallerAndCallbackDelegation>();
        this.notificationCallerAndCallback = this.transform.Find("_notificationCallerAndCallbackDelegation").GetComponent<NotificationCallerAndCallbackDelegation>();
    }

    override protected void Start()
    {
        base.Start();

        // FIXME fake as first screen
        game.gameLoadOrNew(false);
        MatchConfig matchConfig = new MatchConfig();
        matchConfig.matchStrategyType = MatchStrategyType.MAIN;
        matchConfig.teamNames = new List<string> { LibDataConfiguration.ZACA_TEAM_NAME_1, LibDataConfiguration.ZACA_TEAM_NAME_2 };
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
        quizInputHandler.postPrefabInitialization(game);
    }


    override protected void onLogicFrame()
    {
        quizInputHandler.onLogicFrame();

    }

    override protected void renderPopupAnimations(float delta)
    {
        animationQueueHandler.render(delta);
    }

}
