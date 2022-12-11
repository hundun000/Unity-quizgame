using hundun.quizlib.prototype.match;
using hundun.quizlib.prototype;
using hundun.quizlib.view.team;
using hundun.quizlib;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamInfoBoardVM : MonoBehaviour
{
    QuizGdxGame game;
    List<TeamInfoNode> nodes = new List<TeamInfoNode>();

    GameObject _nodesRoot;
    GameObject teamInfoNodePrefab;

    void Awake()
    {
        this.teamInfoNodePrefab = this.transform.Find("_templates/teamInfoNodePrefab").gameObject;
        this._nodesRoot = this.transform.Find("_nodesRoot").gameObject;

        this.GetComponent<Image>().sprite = TextureConfig.getMyNinePatch();
    }

    public void updateTeamPrototype(List<TeamPrototype> teamPrototypes)
    {

    _nodesRoot.transform.AsTableClear();
    nodes.Clear();
    teamPrototypes.ForEach(it => {
        TeamInfoNode vm = _nodesRoot.transform.AsTableAdd<TeamInfoNode>(teamInfoNodePrefab);
        vm.updatePrototype(it);
        nodes.Add(vm);
    });
    }
    
    public void updateTeamRuntime(MatchStrategyType matchStrategyType, int currentTeamIndex, List<TeamRuntimeView> teamRuntimeViews)
{


    for (int i = 0; i < nodes.Count; i++)
    {
        TeamInfoNode vm = nodes.get(i);
        TeamRuntimeView runtimeView = teamRuntimeViews.get(i);
        vm.updateRuntime(i == currentTeamIndex, runtimeView, matchStrategyType);
    }

}
}
