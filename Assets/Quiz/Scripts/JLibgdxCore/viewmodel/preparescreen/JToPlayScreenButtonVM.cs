using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JToPlayScreenButtonVM : MonoBehaviour
{
    private Image _imageComponent;
    
    private Sprite enableDrawable;
    private Sprite disableDrawable;
    private bool touchable;

    // Start is called before the first frame update
    void Start()
    {
        _imageComponent = this.GetComponent<Image>();

        enableDrawable = Resources.Load<Sprite>("Quiz/playScreenUI/systemButton");
        disableDrawable = Resources.Load<Sprite>("Quiz/playScreenUI/skillButtonUseOut");

        //JsetTouchable(true);
        Button button = this.GetComponent<Button>();
        button.onClick.AddListener(JOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JOnClick() 
    {
        // TODO temp
        SceneManager.LoadScene("PlayScene");
    }

    public void JsetTouchable(bool touchable) 
    {
        this.touchable = touchable;
        if (touchable)
        {
            _imageComponent.sprite = enableDrawable;
        } 
        else
        {
            _imageComponent.sprite = disableDrawable;
        }
    }
}
