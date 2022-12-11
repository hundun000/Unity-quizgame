using hundun.quizlib.prototype;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static TeamManageAreaVM;

public class TagSelectPopoupVM : AbstractSelectPopoupVM<TagManageSlotVM>
{

    Button _doneButton;
    TeamPrototype currenTeamPrototype;

    IWaitTagSelectCallback callback;


    override protected void Awake()
    {
        base.Awake();

        this.callback = this.GetComponentInParent<PrepareScreen>();
        this._doneButton = this.transform.Find("_doneButton").gameObject.GetComponent<Button>();

        var listener = new TagSelectDoneClickListener(this);
        _doneButton.onClick.AddListener(() => listener.clicked());
    }

    public void callShow(TeamPrototype currenTeamPrototype, HashSet<String> allTags)
    {
        this.currenTeamPrototype = currenTeamPrototype;
        var candidateVMsAndCandidateVMInstances = allTags
            .Select(tag => 
            {
                GameObject vmInstance = Instantiate(candidateVMPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                TagManageSlotVM vm = vmInstance.GetComponent<TagManageSlotVM>();

                vm.updateData(tag, currenTeamPrototype);
                return Tuple.Create(vm, vmInstance);
            })
            .ToList();
        List<TagManageSlotVM> candidateVMs = candidateVMsAndCandidateVMInstances
            .Select(tuple => tuple.Item1)
            .ToList()
            ;
        List<GameObject> candidateVMInstances = candidateVMsAndCandidateVMInstances
            .Select(tuple => tuple.Item2)
            .ToList()
            ;
        updateScrollPane(candidateVMs, candidateVMInstances);
    }

    public class TagSelectDoneClickListener
    {
        TagSelectPopoupVM owner;

        internal TagSelectDoneClickListener(TagSelectPopoupVM owner) {
            this.owner = owner;
        }

        public void clicked()
        {
            owner.callback.onTagSelectDone(owner.currenTeamPrototype);
        }
    }

    public interface IWaitTagSelectCallback
    {
        void callShowTagSelectPopoup(TeamPrototype currenTeamPrototype, HashSet<String> allTags);
        void onTagSelectDone(TeamPrototype currenTeamPrototype);
    }
}
