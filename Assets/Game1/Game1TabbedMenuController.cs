using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Game1TabbedMenuController : MonoBehaviour
{
    /* Define member variables*/
    private const string tabClassName = "tab";
    private const string currentlySelectedTabClassName = "currentlySelectedTab";
    private const string unselectedContentClassName = "unselectedContent";
    // Tab and tab content have the same prefix but different suffix
    // Define the suffix of the tab name
    private const string tabNameSuffix = "Tab";
    // Define the suffix of the tab content name
    private const string contentNameSuffix = "Content";
    private const string btnCountName = "BtnCountPlayContent";
    private const string lblScoreName = "CountContent";
    private const string lstViewRankName = "ListRankContent";
    private const string playTabName = "PlayTab";

    public int count = 0;
    public List<int> rank100 = new List<int>();

    private readonly VisualElement _root;
    private readonly Button _btnCount;
    private readonly Label _lblScore;
    private ListView _lstViewRank;

    public Game1TabbedMenuController(VisualElement root)
    {
        this._root = root;
        _btnCount = root.Q<Button>(btnCountName);
        _lblScore = root.Q<Label>(lblScoreName);
        _lstViewRank = root.Q<ListView>(lstViewRankName);
    }

    public void RegisterTabCallbacks()
    {
        UQueryBuilder<Label> tabs = GetAllTabs();
        tabs.ForEach((Label tab) => {
            tab.RegisterCallback<ClickEvent>(TabOnClick);
        });
    }

    /* Method for the tab on-click event: 

       - If it is not selected, find other tabs that are selected, unselect them 
       - Then select the tab that was clicked on
    */
    private void TabOnClick(ClickEvent evt)
    {
        Label clickedTab = evt.currentTarget as Label;
        if (!TabIsCurrentlySelected(clickedTab))
        {
            GetAllTabs().Where(
                (tab) => tab != clickedTab && TabIsCurrentlySelected(tab)
            ).ForEach(UnselectTab);
            SelectTab(clickedTab);
        }
    }
    //Method that returns a Boolean indicating whether a tab is currently selected
    private static bool TabIsCurrentlySelected(Label tab)
    {
        return tab.ClassListContains(currentlySelectedTabClassName);
    }

    private UQueryBuilder<Label> GetAllTabs()
    {
        return _root.Query<Label>(className: tabClassName);
    }

    /* Method for the selected tab: 
       -  Takes a tab as a parameter and adds the currentlySelectedTab class
       -  Then finds the tab content and removes the unselectedContent class */
    private void SelectTab(Label tab)
    {
        tab.AddToClassList(currentlySelectedTabClassName);
        VisualElement content = FindContent(tab);
        content.RemoveFromClassList(unselectedContentClassName);
        if (tab.name == playTabName)
        {
            AddScore();
        }
        else
        {
            AddRank100();
        }
    }

    private void AddRank100()
    {
        if (!rank100.Contains(count))
        {
            rank100.Add(count);
        }
        count = 0;
        if (_lstViewRank != null)
        {
            _lstViewRank.itemsSource = rank100?.OrderByDescending(x => x)?
                .Take(100)
                .ToList();
        }
    }

    private void AddScore()
    {
        if (_btnCount != null)
        {
            _btnCount.RemoveFromClassList(unselectedContentClassName);
            _btnCount.clicked -= Increase;
            _btnCount.clicked += Increase;
        }
        if (_lblScore != null)
        {
            _lblScore.RemoveFromClassList(unselectedContentClassName);
            _lblScore.text = "0";
        }
    }

    private void Increase()
    {
        if (_lblScore != null)
        {
            count += 1;
            _lblScore.text = $"{count}";
        }
    }

    /* Method for the unselected tab: 
       -  Takes a tab as a parameter and removes the currentlySelectedTab class
       -  Then finds the tab content and adds the unselectedContent class */
    private void UnselectTab(Label tab)
    {
        if (tab.name == playTabName)
        {
            if (_lblScore != null)
            {
                _lblScore.AddToClassList(unselectedContentClassName);
            }
            if (_btnCount != null)
            {
                _btnCount.AddToClassList(unselectedContentClassName);
            }
        }

        tab.RemoveFromClassList(currentlySelectedTabClassName);
        VisualElement content = FindContent(tab);
        content.AddToClassList(unselectedContentClassName);
    }

    // Method to generate the associated tab content name by for the given tab name
    private static string GenerateContentName(Label tab) =>
        tab.name.Replace(tabNameSuffix, contentNameSuffix);

    // Method that takes a tab as a parameter and returns the associated content element
    private VisualElement FindContent(Label tab)
    {
        return _root.Q(GenerateContentName(tab));
    }
}
