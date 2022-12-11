using hundun.quizlib.model.domain;
using hundun.quizlib;
using hundun.quizlib.prototype.@event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static hundun.quizlib.service.BuiltinDataConfiguration;
using Unity.VisualScripting;

public class SkillEffectHandler : MonoBehaviour
{
    PlayScreen owner;

    void Awake()
    {
        this.owner = this.GetComponentInParent<PlayScreen>();
    }

    internal void handle(SkillResultEvent skillResultEvent)
    {
        if (skillResultEvent.skillName == BuiltinSkill.SKILL_5050.ToString())
        {
            this.handle5050(skillResultEvent.args);
        }
        else if (skillResultEvent.skillName == BuiltinSkill.SKILL_SKIP.ToString())
        {
            this.handleSkip(skillResultEvent.args);
        }
        else if (skillResultEvent.skillName == BuiltinSkill.SKILL_HELP_1.ToString())
        {
            this.handleHelp(skillResultEvent.args);
        }
        else if (skillResultEvent.skillName == BuiltinSkill.SKILL_HELP_2.ToString())
        {
            this.handleHelp(skillResultEvent.args);
        }
        else
        {
            LibgdxFeatureExtension.error(this.GetType().Name, String.Format(
                    "unhandle SkillName = {0}",
                    skillResultEvent.skillName
                    ));
        }
    }

    internal void handleHelp(List<String> args)
    {
        int addSecond = Int32.Parse(args.get(0));
        owner.quizInputHandler.countdownClockVM.updateCoutdownSecond(addSecond);
    }

    internal void handleSkip(List<String> args)
    {
        owner.quizInputHandler.onChooseOptionOrCountdownZero(QuestionModel.SKIP_ANSWER_TEXT);
    }

    internal void handle5050(List<String> args)
    {
        int showOptionAmout = Int32.Parse(args.get(0));
        owner.quizInputHandler.questionOptionAreaVM.showRandomOption(showOptionAmout);
    }
}
