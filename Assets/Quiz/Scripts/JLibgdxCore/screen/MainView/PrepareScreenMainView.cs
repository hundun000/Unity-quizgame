using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PrepareScreenMainView : MonoBehaviour
{
    [SerializeField]
    VisualTreeAsset m_ListEntryTemplate;

    [SerializeField]
    JMatchStrategySelectVM characterListController;

    void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        var uiDocument = GetComponent<UIDocument>();

        // Initialize the character list controller
        characterListController.InitializeCharacterList(uiDocument.rootVisualElement, m_ListEntryTemplate);
    }
}
