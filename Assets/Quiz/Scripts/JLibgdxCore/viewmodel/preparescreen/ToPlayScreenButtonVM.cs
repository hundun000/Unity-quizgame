using hundun.quizlib.prototype.match;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToPlayScreenButtonVM : MonoBehaviour
{
    private Image _imageComponent;
    
    private Sprite enableDrawable;
    private Sprite disableDrawable;
    private bool touchable;

    PrepareScreen screen;
    Button button;
    // Start is called before the first frame update
    void Awake()
    {
        this.screen = this.GetComponentInParent<PrepareScreen>();
        this._imageComponent = this.GetComponent<Image>();

        enableDrawable = TextureConfig.getPlayScreenUITextureAtlas_findRegion(TextureAtlasKeys.PLAYSCREEN_EMPTY_BUTTON);
        disableDrawable = TextureConfig.getPlayScreenUITextureAtlas_findRegion(TextureAtlasKeys.PLAYSCREEN_SKILLBUTTONUSEOUT);

        //JsetTouchable(true);
        this.button = this.GetComponent<Button>();
        button.onClick.AddListener(JOnClick);
    }


    public void JOnClick() 
    {
        screen.game.gameSaveCurrent();

        MatchConfig matchConfig = new MatchConfig();
        matchConfig.teamNames = (screen.selectedTeamNames);
        matchConfig.questionPackageName = (screen.currentQuestionPackageName);
        matchConfig.matchStrategyType = (screen.currenType);

        LibgdxFeatureExtension.SetScreenChangePushParams(new System.Object[]{matchConfig});
        SceneManager.LoadScene("PlayScene");
    }

    public void JsetTouchable(bool touchable) 
    {
        this.touchable = touchable;
        if (touchable)
        {
            _imageComponent.sprite = enableDrawable;
            button.enabled = true;
        } 
        else
        {
            _imageComponent.sprite = disableDrawable;
            button.enabled = false;
        }
    }
}
