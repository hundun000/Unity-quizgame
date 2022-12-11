using hundun.quizlib.prototype;
using hundun.quizlib.prototype.match;
using hundun.quizlib.service;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MatchStrategySelectVM;

public class PrepareScreen : BaseHundunScreen,
    TeamSelectPopoupVM.IWaitTeamSelectCallback,
    TagSelectPopoupVM.IWaitTagSelectCallback,
    TeamManageAreaVM.ICallerAndCallback,
    IMatchStrategyChangeListener
{

    TeamService teamService;
    QuestionService questionService;

    public MatchStrategyType currenType;
    int targetTeamNum;
    public String currentQuestionPackageName;
    public List<String> selectedTeamNames;

    private TeamSelectPopoupVM teamSelectPopoupVM;
    private TagSelectPopoupVM tagSelectPopoupVM;
    private MatchStrategySelectVM matchStrategySelectVM;
    private TeamManageAreaVM teamManageAreaVM;
    private MatchStrategyInfoVM matchStrategyInfoVM;
    private ToPlayScreenButtonVM toPlayScreenButtonVM;
    private ToMenuScreenButtonVM toMenuScreenButtonVM;

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


        _teamSelectPopoupVM = this.PopoupRoot.transform.Find("_teamSelectPopoupVM").gameObject;
        _tagSelectPopoupVM = this.PopoupRoot.transform.Find("_tagSelectPopoupVM").gameObject;

        _matchStrategySelectVM = this.UiRoot.transform.Find("_matchStrategySelectVM").gameObject;
        _teamManageAreaVM = this.UiRoot.transform.Find("_teamManageAreaVM").gameObject;
        _matchStrategyInfoVM = this.UiRoot.transform.Find("_matchStrategyInfoVM").gameObject;
        _toPlayScreenButtonVM = this.UiRoot.transform.Find("_toPlayScreenButtonVM").gameObject;
        _toMenuScreenButtonVM = this.UiRoot.transform.Find("_toMenuScreenButtonVM").gameObject;

        teamSelectPopoupVM = _teamSelectPopoupVM.GetComponent<TeamSelectPopoupVM>();
        tagSelectPopoupVM = _tagSelectPopoupVM.GetComponent<TagSelectPopoupVM>();
        matchStrategySelectVM = _matchStrategySelectVM.GetComponent<MatchStrategySelectVM>();
        teamManageAreaVM = _teamManageAreaVM.GetComponent<TeamManageAreaVM>();
        matchStrategyInfoVM = _matchStrategyInfoVM.GetComponent<MatchStrategyInfoVM>();
        toPlayScreenButtonVM = _toPlayScreenButtonVM.GetComponent<ToPlayScreenButtonVM>();
        toMenuScreenButtonVM = _toMenuScreenButtonVM.GetComponent<ToMenuScreenButtonVM>();

    }

    override protected void Start()
    {
        base.Start();

        this.teamService = game.quizLibBridge.quizComponentContext.teamService;
        this.questionService = game.quizLibBridge.quizComponentContext.questionService;

        this.currentQuestionPackageName = QuestionLoaderService.RELEASE_PACKAGE_NAME;

        foreach (Transform child in this.PopoupRoot.transform)
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
            _toPlayScreenButtonVM.GetComponent<ToPlayScreenButtonVM>().JsetTouchable(true);
        }
        else
        {
            _toPlayScreenButtonVM.GetComponent<ToPlayScreenButtonVM>().JsetTouchable(false);
        }
    }

    void TeamManageAreaVM.ICallerAndCallback.onTeamWantChange(TeamManageSlotVM teamSlotVM)
    {
        teamManageAreaVM.onTeamWantChangeOrModify(teamSlotVM);
        ((TeamSelectPopoupVM.IWaitTeamSelectCallback)this).callShowTeamSelectPopoup();
    }

    void TeamManageAreaVM.ICallerAndCallback.onTeamWantModify(TeamManageSlotVM teamSlotVM)
    {
        teamManageAreaVM.onTeamWantChangeOrModify(teamSlotVM);
        ((TagSelectPopoupVM.IWaitTagSelectCallback)this).callShowTagSelectPopoup(
            teamSlotVM.data, 
            questionService.getTags(currentQuestionPackageName)
            );
    }

    void TeamSelectPopoupVM.IWaitTeamSelectCallback.callShowTeamSelectPopoup()
    {
        // --- ui ---
        _teamSelectPopoupVM.SetActive(true);

        // --- logic ---
        teamSelectPopoupVM.callShow(teamService.listTeams());
    }

    void TeamSelectPopoupVM.IWaitTeamSelectCallback.onTeamSelectDone(TeamPrototype currenTeamPrototype)
    {
        // --- ui ---
        foreach (Transform child in this.PopoupRoot.transform)
        {
            child.gameObject.SetActive(false);
        }

        // --- logic ---
        selectedTeamNames = teamManageAreaVM.updateWaitChangeDone(currenTeamPrototype);
        validateMatchConfig();
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

    void TagSelectPopoupVM.IWaitTagSelectCallback.callShowTagSelectPopoup(TeamPrototype currenTeamPrototype, HashSet<string> allTags)
    {
        // --- ui ---
        _tagSelectPopoupVM.SetActive(true);

        // --- logic ---
        tagSelectPopoupVM.callShow(currenTeamPrototype, allTags);
    }

    void TagSelectPopoupVM.IWaitTagSelectCallback.onTagSelectDone(TeamPrototype currenTeamPrototype)
    {
        // --- ui ---
        foreach (Transform child in this.PopoupRoot.transform)
        {
            child.gameObject.SetActive(false);
        }

        // --- logic ---
        teamManageAreaVM.updateWaitChangeDone(currenTeamPrototype);
    }
}


