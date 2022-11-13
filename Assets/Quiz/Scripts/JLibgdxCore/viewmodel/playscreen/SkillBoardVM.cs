using hundun.quizlib;
using hundun.quizlib.prototype;
using hundun.quizlib.prototype.skill;
using hundun.quizlib.view.role;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillBoardVM : MonoBehaviour
{
    CallerAndCallback callerAndCallback;

    List<SkillNode> nodes;
    QuizGdxGame game;


    void Awake()
    {
        nodes = this.transform.GetComponentsInChildren<SkillNode>().ToList();
    }

    public void postPrefabInitialization(CallerAndCallback callerAndCallback)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].postPrefabInitialization(callerAndCallback, i);
        }
    }

    internal void updateSkillRuntime(int index, int skillRemainTime)
    {
        nodes.get(index).updateRuntime(skillRemainTime);
    }

    public void updateRole(RolePrototype rolePrototype, RoleRuntimeView roleRuntimeView)
    {
        if (rolePrototype == null)
        {
            return;
        }


        for (int i = 0; i < rolePrototype.skillSlotPrototypes.Count; i++)
        {
            SkillSlotPrototype skillSlotPrototype = rolePrototype.skillSlotPrototypes.get(i);
            int remainUseTime = roleRuntimeView.skillSlotRuntimeViews.get(i).remainUseTime;
            SkillNode node = nodes[i];
            node.updatePrototy(skillSlotPrototype);
            node.updateRuntime(remainUseTime);
        }

    }

    public interface CallerAndCallback
    {
        void onChooseSkill(int index);
    }
}
