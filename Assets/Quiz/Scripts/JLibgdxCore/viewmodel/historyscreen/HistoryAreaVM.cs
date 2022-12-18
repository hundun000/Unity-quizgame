using hundun.quizlib.prototype;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static HistoryScreen;

public class HistoryAreaVM : AbstractSelectPopoupVM<MatchHistoryVM>
{
    // Start is called before the first frame update
    override protected void Awake()
    {
        base.Awake();
        this.candidateVMPrefab = this.transform.Find("_templates/MatchHistoryVM").gameObject;
    }

    internal void updateScrollPane(QuizGdxGame game, List<MatchHistoryDTO> histories)
    {
        var candidateVMsAndCandidateVMInstances = histories
            .Select(it =>
            {
                GameObject vmInstance = Instantiate(candidateVMPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                MatchHistoryVM vm = vmInstance.GetComponent<MatchHistoryVM>();

                MatchHistoryVM.Factory.fromBO(vm, game, it);
                return Tuple.Create(vm, vmInstance);
            })
            .ToList();

        List<MatchHistoryVM> candidateVMs = candidateVMsAndCandidateVMInstances
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
