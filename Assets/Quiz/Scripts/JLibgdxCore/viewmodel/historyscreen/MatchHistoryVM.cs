using hundun.quizlib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static HistoryScreen;

public class MatchHistoryVM : MonoBehaviour
{
    public static readonly int MatchHistoryVM_WIDTH = 800;
    public static readonly int MatchHistoryVM_HEIGHT = 100;

    static readonly int NUM_SLOT = 2;
    List<TeamScorePairSlotVM> slotVMs = new List<TeamScorePairSlotVM>();

    // ------ unity adapter member ------
    public GameObject teamScorePairSlotVMPrefab;
    GameObject _scrollViewContent;

    void Awake()
    {
        this._scrollViewContent = this.transform.Find("_content").gameObject;
        for (int i = 0; i < NUM_SLOT; i++)
        {
            TeamScorePairSlotVM vm = _scrollViewContent.transform.AsTableAdd<TeamScorePairSlotVM>(teamScorePairSlotVMPrefab);

            slotVMs.Add(vm);
        }
    }

    public static class Factory
    {

        public static MatchHistoryVM fromMap(MatchHistoryVM vm, QuizGdxGame game, Dictionary<String, int> data)
        {

            List<String> keys = new List<string>(data.Keys);
            for (int i = 0; i < NUM_SLOT; i++)
            {
                if (keys.Count > 0)
                {
                    String key = keys[0];
                    keys.RemoveAt(0);
                    vm.slotVMs.get(i).update(key, data.get(key));
                }
                else
                {
                    vm.slotVMs.get(i).update(null, null);
                }
            }

            return vm;
        }

        public static MatchHistoryVM fromBO(MatchHistoryVM vm, QuizGdxGame game, MatchHistoryDTO history)
        {
            return fromMap(vm, game, history.data);
        }
    }

}
