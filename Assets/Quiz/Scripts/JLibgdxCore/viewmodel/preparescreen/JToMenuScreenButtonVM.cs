using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JToMenuScreenButtonVM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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
        SceneManager.LoadScene("MenuScene");
    }
}
