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

public class JPrepareScreen : MonoBehaviour,
    JTeamSelectPopoupVM.IWaitTeamSelectCallback,
    JTeamManageAreaVM.ICallerAndCallback,
    IMatchStrategyChangeListener
{
    private const String ownerName = "PrepareScene";
    private static readonly List<TeamPrototype> teamPrototypes = new()
    {
        new TeamPrototype("砍口垒同好组", new List<String>(), new List<String>(), null),
        new TeamPrototype("少前同好组", new List<String>(), new List<String>(), null)
    };

    MatchStrategyType currenType;
    int targetTeamNum;
    String currentQuestionPackageName;
    List<String> selectedTeamNames;

    private GameObject _teamSelectPopoupVM;
    private GameObject _tagSelectPopoupVM;
    private GameObject _matchStrategySelectVM;
    private GameObject _teamManageAreaVM;
    private GameObject _matchStrategyInfoVM;
    private GameObject _toPlayScreenButtonVM;
    private GameObject _toMenuScreenButtonVM;

    private JTeamSelectPopoupVM teamSelectPopoupVM;
    private JTagSelectPopoupVM tagSelectPopoupVM;
    private JMatchStrategySelectVM matchStrategySelectVM;
    private JTeamManageAreaVM teamManageAreaVM;
    private JMatchStrategyInfoVM matchStrategyInfoVM;
    private JToPlayScreenButtonVM toPlayScreenButtonVM;
    private JToMenuScreenButtonVM toMenuScreenButtonVM;

    // ------ unity adapter member ------
    private GameObject _popoupRoot;


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

    void Awake()
    {
        _popoupRoot = GameObject.Find("_popupRoot");
        


        _teamSelectPopoupVM = this.transform.Find("_popupRoot/_teamSelectPopoupVM").gameObject;
        //_tagSelectPopoupVM = this.transform.Find("_popupRoot/_tagSelectPopoupVM").gameObject;

        _matchStrategySelectVM = this.transform.Find("_uiRoot/_matchStrategySelectVM").gameObject;
        _teamManageAreaVM = this.transform.Find("_uiRoot/_teamManageAreaVM").gameObject;
        _matchStrategyInfoVM = this.transform.Find("_uiRoot/_matchStrategyInfoVM").gameObject;
        _toPlayScreenButtonVM = this.transform.Find("_uiRoot/_toPlayScreenButtonVM").gameObject;
        _toMenuScreenButtonVM = this.transform.Find("_uiRoot/_toMenuScreenButtonVM").gameObject;

        teamSelectPopoupVM = _teamSelectPopoupVM.GetComponent<JTeamSelectPopoupVM>();
        //tagSelectPopoupVM = _tagSelectPopoupVM.GetComponent<JTagSelectPopoupVM>();
        matchStrategySelectVM = _matchStrategySelectVM.GetComponent<JMatchStrategySelectVM>();
        teamManageAreaVM = _teamManageAreaVM.GetComponent<JTeamManageAreaVM>();
        matchStrategyInfoVM = _matchStrategyInfoVM.GetComponent<JMatchStrategyInfoVM>();
        toPlayScreenButtonVM = _toPlayScreenButtonVM.GetComponent<JToPlayScreenButtonVM>();
        toMenuScreenButtonVM = _toMenuScreenButtonVM.GetComponent<JToMenuScreenButtonVM>();

    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in _popoupRoot.transform)
        {
            child.gameObject.SetActive(false);
            //child.SetParent(null);
        }


        // ------ post vm init ------ 
        matchStrategySelectVM.checkSlotNum(MatchStrategyType.PRE);
        validateMatchConfig();

    }

    // Update is called once per frame
    void Update()
    {

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
        teamSelectPopoupVM.callShow(teamPrototypes);
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
}


