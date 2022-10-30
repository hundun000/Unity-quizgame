using hundun.quizlib.prototype;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static JTeamManageAreaVM;

public class JTeamManageSlotVM : MonoBehaviour
{

    public TeamPrototype data { get; private set; }

    GameObject _noTeamLabelPrefab;
    GameObject _teamNodeAreaContainer;

    Button _changeTeamButton;
    Button _modifyTeamButton;

    ICallerAndCallback callerAndCallback;

    // ------ unity adapter member ------
    public GameObject teamNodeVMPrefab;
    

    /// <summary>
    /// [unity adapter method]
    /// </summary>
    internal void postPrefabInitialization(ICallerAndCallback callerAndCallback)
    {
        this.callerAndCallback = callerAndCallback;
    }

    void Awake()
    {
        this._noTeamLabelPrefab = GameObject.FindObjectOfType<JPrepareScreen>().GetTemplatesDuringAwake().transform.Find("_noTeamLabelPrefab").gameObject;
        this._teamNodeAreaContainer = this.transform.Find("_teamNodeAreaContainer").gameObject;
        this._changeTeamButton = this.transform.Find("_changeTeamButtonCell/_changeTeamButton").gameObject.GetComponent<Button>();
        this._modifyTeamButton = this.transform.Find("_modifyTeamButtonCell/_modifyTeamButton").gameObject.GetComponent<Button>();
        //this.GetComponent<Image>().sprite = TextureConfig.getHistoryAreaVMBackgroundDrawable();

        _changeTeamButton.onClick.AddListener(() => {
            callerAndCallback.onTeamWantChange(this);
        });

        _modifyTeamButton.onClick.AddListener(() => {
            callerAndCallback.onTeamWantModify(this);
        });

    }

    public void ModifyTeamButtonOnClick()
    {
        callerAndCallback.onTeamWantModify(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        updateData(null);
    }


    public void updateData(TeamPrototype data)
    {
        this.data = data;

        _teamNodeAreaContainer.transform.AsTableClear();

        if (data != null)
        {

            JTeamNodeVM vm = _teamNodeAreaContainer.transform.AsTableAdd<JTeamNodeVM>(teamNodeVMPrefab);
            vm.postPrefabInitialization(data);

            _modifyTeamButton.enabled = true;
        }
        else
        {
            GameObject vm = _teamNodeAreaContainer.transform.AsTableAdd<GameObject>(_noTeamLabelPrefab);

            _modifyTeamButton.enabled = false;
        }
    }
}
