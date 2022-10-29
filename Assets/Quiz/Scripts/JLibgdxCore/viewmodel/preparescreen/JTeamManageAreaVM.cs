using hundun.quizlib.prototype;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JTeamManageAreaVM : MonoBehaviour
{
    ICallerAndCallback callerAndCallback;

    List<JTeamManageSlotVM> teamSlotVMs = new List<JTeamManageSlotVM>();
    JTeamManageSlotVM operatingSlotVM;

    // ------ unity adapter member ------
    public GameObject teamManageSlotVMPrefab;
    GameObject _scrollViewContent;

    void Awake()
    {
        this.callerAndCallback = this.GetComponentInParent<JPrepareScreen>();
        this._scrollViewContent = this.transform.Find("_content").gameObject;
    }


    internal void updateSlotNum(int targetSlotNum)
    {
        if (targetSlotNum != teamSlotVMs.Count)
        {
            foreach (Transform child in _scrollViewContent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            teamSlotVMs.Clear();

            for (int i = 0; i < targetSlotNum; i++)
            {

                GameObject vmInstance = Instantiate(teamManageSlotVMPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                JTeamManageSlotVM vm = vmInstance.GetComponent<JTeamManageSlotVM>();
                vmInstance.transform.SetParent(_scrollViewContent.transform);
                vm.postPrefabInitialization(callerAndCallback);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public interface ICallerAndCallback
    {
        void onTeamWantChange(JTeamManageSlotVM teamSlotVM);
        void onTeamWantModify(JTeamManageSlotVM teamSlotVM);
    }

    public void onTeamWantChange(JTeamManageSlotVM teamSlotVM)
    {
        this.operatingSlotVM = teamSlotVM;
    }

    internal void updateWaitChangeDone(TeamPrototype teamPrototype)
    {
        this.operatingSlotVM.updateData(teamPrototype);
    }
}