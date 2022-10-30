using hundun.quizlib.prototype;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            _scrollViewContent.transform.AsTableClear();
            teamSlotVMs.Clear();

            for (int i = 0; i < targetSlotNum; i++)
            {

                JTeamManageSlotVM vm = _scrollViewContent.transform.AsTableAdd<JTeamManageSlotVM>(teamManageSlotVMPrefab);
                vm.postPrefabInitialization(callerAndCallback);

                teamSlotVMs.Add(vm);
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

    public void onTeamWantChangeOrModify(JTeamManageSlotVM teamSlotVM)
    {
        this.operatingSlotVM = teamSlotVM;
    }

    internal List<String> updateWaitChangeDone(TeamPrototype teamPrototype)
    {
        this.operatingSlotVM.updateData(teamPrototype);
        return teamSlotVMs
            .Where(it => it.data != null)
            .Select(it => it.data.name)
            .ToList();
    }
}