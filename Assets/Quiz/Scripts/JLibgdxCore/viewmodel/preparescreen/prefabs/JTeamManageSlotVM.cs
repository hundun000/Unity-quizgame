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

    // ------ unity adapter member ------
    public GameObject teamNodeVMPrefab;
    ICallerAndCallback callerAndCallback;

    /// <summary>
    /// [unity adapter method]
    /// </summary>
    internal void postPrefabInitialization(ICallerAndCallback callerAndCallback)
    {
        this.callerAndCallback = callerAndCallback;
    }

    void Awake()
    {
        this._noTeamLabelPrefab = this.transform.Find("_noTeamLabelPrefab").gameObject;
        this._teamNodeAreaContainer = this.transform.Find("_teamNodeAreaContainer").gameObject;
        this._changeTeamButton = this.transform.Find("_changeTeamButton").gameObject.GetComponent<Button>();
        this._modifyTeamButton = this.transform.Find("_modifyTeamButton").gameObject.GetComponent<Button>();
        //this.GetComponent<Image>().sprite = TextureConfig.getHistoryAreaVMBackgroundDrawable();

        _changeTeamButton.onClick.AddListener(() => {
            callerAndCallback.onTeamWantChange(this);
        });

        _modifyTeamButton.onClick.AddListener(() => {
            callerAndCallback.onTeamWantModify(this);
        });

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void updateData(TeamPrototype data)
    {
        this.data = data;

        foreach (Transform child in _teamNodeAreaContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        if (data != null)
        {
            GameObject vmInstance = Instantiate(teamNodeVMPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            JTeamNodeVM vm = vmInstance.GetComponent<JTeamNodeVM>();
            vmInstance.transform.SetParent(_teamNodeAreaContainer.transform);
            vm.postPrefabInitialization(data);
        }
        else
        {
            GameObject vmInstance = Instantiate(_noTeamLabelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Text vm = vmInstance.GetComponent<Text>();
            vmInstance.transform.SetParent(_teamNodeAreaContainer.transform);
        }
    }
}
