using hundun.quizlib.prototype.match;
using hundun.quizlib.prototype;
using hundun.quizlib.view.team;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamInfoNode : MonoBehaviour
{
    //static int SIGN_SIZE = 50;
    Image signSlotImage;
    Sprite signDrawable;
    //static int NAME_WIDTH = 200;
    Text teamInfoLabel;
    Text teamInfoLabel2;

    private void Awake()
    {
        this.signSlotImage = this.transform.Find("_signSlotImage").gameObject.GetComponent<Image>();
        this.teamInfoLabel = this.transform.Find("_teamInfoLabelGroup/_teamInfoLabel").gameObject.GetComponent<Text>();
        this.teamInfoLabel2 = this.transform.Find("_teamInfoLabelGroup/_teamInfoLabel2").gameObject.GetComponent<Text>();

        this.signDrawable = TextureConfig.getPlayScreenUITextureAtlas_findRegion(TextureAtlasKeys.pLAYSCREEN_CURRENTTEAMSIGN_BLACK);
        
        signSlotImage.sprite = signDrawable;
    }



    public void updatePrototype(TeamPrototype teamPrototype)
    {
        teamInfoLabel.text = (teamPrototype.name);

    }

    public void updateRuntime(bool isCurrent, TeamRuntimeView runtimeView, MatchStrategyType matchStrategyType)
    {
        if (isCurrent)
        {
            signSlotImage.enabled = true;
        }
        else
        {
            signSlotImage.enabled = false;
        }
        String healthInfoText;
        switch (matchStrategyType)
        {
            case MatchStrategyType.PRE:
                healthInfoText = String.Format(
                        "剩余题数：{0}  分数：{1}",
                        runtimeView.health,
                        runtimeView.matchScore
                        );
                break;
            case MatchStrategyType.MAIN:
                healthInfoText = String.Format(
                        "剩余生命：{0}  分数：{1}",
                        runtimeView.health,
                        runtimeView.matchScore
                        );
                break;
            default:
                healthInfoText = "";
                break;
        }
        teamInfoLabel2.text = (healthInfoText);
    }
}
