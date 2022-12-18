using hundun.quizlib.prototype;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TeamManageAreaVM : MonoBehaviour
{
    ICallerAndCallback callerAndCallback;

    List<TeamManageSlotVM> teamSlotVMs = new List<TeamManageSlotVM>();
    TeamManageSlotVM operatingSlotVM;

    // ------ unity adapter member ------
    public GameObject teamManageSlotVMPrefab;
    GameObject _scrollViewContent;

    void Awake()
    {
        this.callerAndCallback = this.GetComponentInParent<PrepareScreen>();
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

                TeamManageSlotVM vm = _scrollViewContent.transform.AsTableAdd<TeamManageSlotVM>(teamManageSlotVMPrefab);
                vm.postPrefabInitialization(callerAndCallback);

                teamSlotVMs.Add(vm);
            }
        }
    }

    void Start()
    {
        this.GetComponent<Image>().sprite = TextureConfig.getMyNinePatch();
    }


    public interface ICallerAndCallback
    {
        void onTeamWantChange(TeamManageSlotVM teamSlotVM);
        void onTeamWantModify(TeamManageSlotVM teamSlotVM);
    }

    public void onTeamWantChangeOrModify(TeamManageSlotVM teamSlotVM)
    {
        this.operatingSlotVM = teamSlotVM;
    }

    public List<String> getSelectedTeamNames()
    {
        return teamSlotVMs
            .Where(it => it.data != null)
            .Select(it => it.data.name)
            .ToList();
    }

    internal List<String> updateWaitChangeDone(TeamPrototype teamPrototype)
    {
        this.operatingSlotVM.updateData(teamPrototype);
        return getSelectedTeamNames();
    }
}