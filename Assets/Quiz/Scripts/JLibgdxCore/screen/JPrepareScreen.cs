using hundun.quizlib.prototype.match;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JPrepareScreen : MonoBehaviour
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



    // Start is called before the first frame update
    void Start()
    {
        _toPlayScreenButtonVM = GameObject.Find("_ToPlayScreenButtonVM");


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


