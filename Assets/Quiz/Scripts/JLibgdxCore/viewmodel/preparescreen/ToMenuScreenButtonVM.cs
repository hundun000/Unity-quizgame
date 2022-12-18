using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Device;

public class ToMenuScreenButtonVM : MonoBehaviour
{
    PrepareScreen screen;
    Button button;
    // Start is called before the first frame update
    void Awake()
    {
        this.screen = this.GetComponentInParent<PrepareScreen>();
        this.button = this.GetComponent<Button>();

        button.onClick.AddListener(JOnClick);
        this.GetComponent<Image>().sprite = TextureConfig.getPlayScreenUITextureAtlas_findRegion(TextureAtlasKeys.PLAYSCREEN_EXITBUTTON);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void JOnClick()
    {
        screen.game.gameSaveCurrent();

        SceneManager.LoadScene("MenuScene");
    }
}
