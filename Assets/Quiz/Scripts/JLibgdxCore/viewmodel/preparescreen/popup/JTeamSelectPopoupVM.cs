using hundun.quizlib.prototype;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class JTeamSelectPopoupVM : JAbstractSelectPopoupVM<JTeamNodeVM>
{

    IWaitTeamSelectCallback callback;
    Button _doneButton;

    // ------ unity adapter member ------

    // Use this for initialization
    override protected void Awake()
    {
        base.Awake();

        this.callback = this.GetComponentInParent<JPrepareScreen>();
        this._doneButton = this.transform.Find("_doneButton").gameObject.GetComponent<Button>();

        TeamSelectClickListener listener = new TeamSelectClickListener(callback, null);

        _doneButton.onClick.AddListener(() => listener.clicked());
    }



    public class TeamSelectClickListener
    {
        IWaitTeamSelectCallback callback;
        JTeamNodeVM teamNodeVM;
        internal TeamSelectClickListener(IWaitTeamSelectCallback callback, JTeamNodeVM teamNodeVM) {
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

    public void callShow(List<TeamPrototype> teamPrototypes)
    {
        var candidateVMsAndCandidateVMInstances = teamPrototypes
            .Select(teamPrototype =>
            {
                GameObject vmInstance = Instantiate(candidateVMPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                JTeamNodeVM vm = vmInstance.GetComponent<JTeamNodeVM>();
                vm.postPrefabInitialization(teamPrototype);

                TeamSelectClickListener listener = new TeamSelectClickListener(callback, vm);
                vm._selfAsButton.onClick.AddListener(() => listener.clicked());

                return Tuple.Create(vm, vmInstance);
            })
            .ToList()
            ;
        List<JTeamNodeVM> candidateVMs = candidateVMsAndCandidateVMInstances
            .Select(tuple =>
            {
                return tuple.Item1;
            })
            .ToList()
            ;
        List<GameObject> candidateVMInstances = candidateVMsAndCandidateVMInstances
            .Select(tuple =>
            {
                return tuple.Item2;
            })
            .ToList()
            ;

        updateScrollPane(candidateVMs, candidateVMInstances);
    }
}
