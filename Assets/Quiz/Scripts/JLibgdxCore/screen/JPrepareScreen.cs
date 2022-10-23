using hundun.quizlib.prototype;
using hundun.quizlib.prototype.match;
using hundun.quizlib.service;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static JMatchStrategySelectVM;

public class JPrepareScreen : BaseHundunScreen,
    JTeamSelectPopoupVM.IWaitTeamSelectCallback,
    JTeamManageAreaVM.ICallerAndCallback,
    IMatchStrategyChangeListener
{

    TeamService teamService;
    QuestionService questionService;

    MatchStrategyType currenType;
    int targetTeamNum;
    String currentQuestionPackageName;
    List<String> selectedTeamNames;

    private JTeamSelectPopoupVM teamSelectPopoupVM;
    private JTagSelectPopoupVM tagSelectPopoupVM;
    private JMatchStrategySelectVM matchStrategySelectVM;
    private JTeamManageAreaVM teamManageAreaVM;
    private JMatchStrategyInfoVM matchStrategyInfoVM;
    private JToPlayScreenButtonVM toPlayScreenButtonVM;
    private JToMenuScreenButtonVM toMenuScreenButtonVM;

    // ------ unity adapter member ------
    private GameObject _teamSelectPopoupVM;
    private GameObject _tagSelectPopoupVM;
    private GameObject _matchStrategySelectVM;
    private GameObject _teamManageAreaVM;
    private GameObject _matchStrategyInfoVM;
    private GameObject _toPlayScreenButtonVM;
    private GameObject _toMenuScreenButtonVM;

    override protected void Awake()
    {
        base.Awake();


        _teamSelectPopoupVM = _popoupRoot.transform.Find("_teamSelectPopoupVM").gameObject;
        //_tagSelectPopoupVM = this.transform.Find("_popupRoot/_tagSelectPopoupVM").gameObject;

        _matchStrategySelectVM = _uiRoot.transform.Find("_matchStrategySelectVM").gameObject;
        _teamManageAreaVM = _uiRoot.transform.Find("_teamManageAreaVM").gameObject;
        _matchStrategyInfoVM = _uiRoot.transform.Find("_matchStrategyInfoVM").gameObject;
        _toPlayScreenButtonVM = _uiRoot.transform.Find("_toPlayScreenButtonVM").gameObject;
        _toMenuScreenButtonVM = _uiRoot.transform.Find("_toMenuScreenButtonVM").gameObject;

        teamSelectPopoupVM = _teamSelectPopoupVM.GetComponent<JTeamSelectPopoupVM>();
        //tagSelectPopoupVM = _tagSelectPopoupVM.GetComponent<JTagSelectPopoupVM>();
        matchStrategySelectVM = _matchStrategySelectVM.GetComponent<JMatchStrategySelectVM>();
        teamManageAreaVM = _teamManageAreaVM.GetComponent<JTeamManageAreaVM>();
        matchStrategyInfoVM = _matchStrategyInfoVM.GetComponent<JMatchStrategyInfoVM>();
        toPlayScreenButtonVM = _toPlayScreenButtonVM.GetComponent<JToPlayScreenButtonVM>();
        toMenuScreenButtonVM = _toMenuScreenButtonVM.GetComponent<JToMenuScreenButtonVM>();

    }

    override protected void Start()
    {
        base.Start();

        this.teamService = game.quizLibBridge.quizComponentContext.teamService;
        this.questionService = game.quizLibBridge.quizComponentContext.questionService;

        this.currentQuestionPackageName = QuestionLoaderService.RELEASE_PACKAGE_NAME;

        foreach (Transform child in _popoupRoot.transform)
        {
            child.gameObject.SetActive(false);
            //child.SetParent(null);
        }


        // ------ post vm init ------ 
        matchStrategySelectVM.checkSlotNum(MatchStrategyType.PRE);
        validateMatchConfig();


        // temp when as first scece
        game.gameLoadOrNew(false);
    }

    private void validateMatchConfig()
    {
        if (selectedTeamNames != null && selectedTeamNames.Count == targetTeamNum)
        {
            _toPlayScreenButtonVM.GetComponent<JToPlayScreenButtonVM>().JsetTouchable(true);
        }
        else
        {
            _toPlayScreenButtonVM.GetComponent<JToPlayScreenButtonVM>().JsetTouchable(false);
        }
    }

    void JTeamManageAreaVM.ICallerAndCallback.onTeamWantChange(JTeamManageSlotVM teamSlotVM)
    {
        teamManageAreaVM.onTeamWantChange(teamSlotVM);
        ((JTeamSelectPopoupVM.IWaitTeamSelectCallback)this).callShowTeamSelectPopoup();
    }

    void JTeamManageAreaVM.ICallerAndCallback.onTeamWantModify(JTeamManageSlotVM teamSlotVM)
    {
        throw new NotImplementedException();
    }

    void JTeamSelectPopoupVM.IWaitTeamSelectCallback.callShowTeamSelectPopoup()
    {
        // --- ui ---
        _teamSelectPopoupVM.SetActive(true);
        //_teamSelectPopoupVM.transform.SetParent(_popoupRoot.transform);

        // --- logic ---
        teamSelectPopoupVM.callShow(teamService.listTeams());
    }

    void JTeamSelectPopoupVM.IWaitTeamSelectCallback.onTeamSelectDone(TeamPrototype currenTeamPrototype)
    {
        // --- ui ---
        foreach (Transform child in _popoupRoot.transform)
        {
            child.gameObject.SetActive(false);
            //child.SetParent(null);
        }

        // --- logic ---
        // do nothing
        teamManageAreaVM.updateWaitChangeDone(currenTeamPrototype);
    }

    void IMatchStrategyChangeListener.onMatchStrategyChange(MatchStrategyType newType)
    {
        this.currenType = newType;

        if (currenType == MatchStrategyType.PRE)
        {
            targetTeamNum = 1;
        }
        else if (currenType == MatchStrategyType.MAIN)
        {
            targetTeamNum = 2;
        }
        else
        {
            Debug.LogWarning(this.GetType().Name + ": " + "onChange cannot handle type: " + currenType);
        }

        matchStrategyInfoVM.updateStrategy(currenType);
        teamManageAreaVM.updateSlotNum(targetTeamNum);
    }
}


