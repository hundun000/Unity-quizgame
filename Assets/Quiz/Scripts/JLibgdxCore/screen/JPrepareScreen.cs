using hundun.quizlib.prototype.match;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static JMatchStrategySelectVM;

public class JPrepareScreen : MonoBehaviour,
    IMatchStrategyChangeListener
{
    private const String ownerName = "PrepareScene";

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
        _teamSelectPopoupVM = GameObject.Find("_teamSelectPopoupVM");
        _tagSelectPopoupVM = GameObject.Find("_tagSelectPopoupVM");
        _matchStrategySelectVM = GameObject.Find("_matchStrategySelectVM");
        _teamManageAreaVM = GameObject.Find("_teamManageAreaVM");
        _matchStrategyInfoVM = GameObject.Find("_matchStrategyInfoVM");
        _toPlayScreenButtonVM = GameObject.Find("_toPlayScreenButtonVM");
        _toMenuScreenButtonVM = GameObject.Find("_toMenuScreenButtonVM");

        //teamSelectPopoupVM = _teamSelectPopoupVM.GetComponent<JTeamSelectPopoupVM>();
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
}


