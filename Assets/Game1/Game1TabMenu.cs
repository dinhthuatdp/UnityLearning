using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Game1TabMenu : MonoBehaviour
{
    private Game1TabbedMenuController controller;

    private void OnEnable()
    {
        UIDocument menu = GetComponent<UIDocument>();
        VisualElement root = menu.rootVisualElement;

        controller = new(root);

        controller.RegisterTabCallbacks();
    }
}
