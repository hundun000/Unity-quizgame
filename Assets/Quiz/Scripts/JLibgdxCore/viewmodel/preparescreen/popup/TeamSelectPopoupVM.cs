using hundun.quizlib.prototype;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TeamSelectPopoupVM : AbstractSelectPopoupVM<TeamNodeVM>
{

    IWaitTeamSelectCallback callback;
    Button _doneButton;

    // ------ unity adapter member ------

    // Use this for initialization
    override protected void Awake()
    {
        base.Awake();

        this.callback = this.GetComponentInParent<PrepareScreen>();
        this._doneButton = this.transform.Find("_doneButton").gameObject.GetComponent<Button>();

        TeamSelectClickListener listener = new TeamSelectClickListener(callback, null);

        _doneButton.onClick.AddListener(() => listener.clicked());
    }



    public class TeamSelectClickListener
    {
        IWaitTeamSelectCallback callback;
        TeamNodeVM teamNodeVM;
        internal TeamSelectClickListener(IWaitTeamSelectCallback callback, TeamNodeVM teamNodeVM) {
            this.callback = callback;
            this.teamNodeVM = teamNodeVM;
        }

        public void clicked()
        {
            callback.onTeamSelectDone(teamNodeVM != null ? teamNodeVM.data : null);
        }
    }

    public interface IWaitTeamSelectCallback
    {
        void callShowTeamSelectPopoup();
        void onTeamSelectDone(TeamPrototype teamPrototype);
    }

    public void callShow(List<TeamPrototype> teamPrototypes, List<string> selectedTeamNames)
    {
        var candidateVMsAndCandidateVMInstances = teamPrototypes
            .Where(teamPrototype => !selectedTeamNames.Contains(teamPrototype.name))
            .Select(teamPrototype =>
            {
                GameObject vmInstance = Instantiate(candidateVMPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                TeamNodeVM vm = vmInstance.GetComponent<TeamNodeVM>();
                vm.postPrefabInitialization(teamPrototype);

                TeamSelectClickListener listener = new TeamSelectClickListener(callback, vm);
                vm._selfAsButton.onClick.AddListener(() => listener.clicked());

                return Tuple.Create(vm, vmInstance);
            })
            .ToList()
            ;
        List<TeamNodeVM> candidateVMs = candidateVMsAndCandidateVMInstances
            .Select(tuple => tuple.Item1)
            .ToList()
            ;
        List<GameObject> candidateVMInstances = candidateVMsAndCandidateVMInstances
            .Select(tuple => tuple.Item2)
            .ToList()
            ;

        updateScrollPane(candidateVMs, candidateVMInstances);
    }
}
