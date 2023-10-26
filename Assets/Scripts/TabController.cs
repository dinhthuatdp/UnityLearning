using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    [SerializeField] private Button btnTab1;
    [SerializeField] private Button btnTab2;
    [SerializeField] private Image tab1Content;
    [SerializeField] private Image tab2Content;
    [SerializeField] private Image itemTemplate;
    [SerializeField] private ScrollRect scrollRects;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private TextMeshProUGUI txtSelectedName;
    [SerializeField] private TextMeshProUGUI txtSelectedScore;

    // Start is called before the first frame update
    void Start()
    {
        ChangeTab(1);
        btnTab1.onClick.RemoveAllListeners();
        btnTab1.onClick.AddListener(Tab1Click);
        btnTab2.onClick.RemoveAllListeners();
        btnTab2.onClick.AddListener(Tab2Click);
        scrollRects.horizontal = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Tab1Click()
    {
        ChangeTab(1);
    }

    private void Tab2Click()
    {
        ChangeTab(2);
    }

    private void ChangeTab(int tabIndex)
    {
        switch (tabIndex)
        {
            case 1:
                ShowHideTab2Content(false);
                ShowHideTab1Content(true);
                break;
            case 2:
                ShowHideTab1Content(false);
                ShowHideTab2Content(true);
                break;
        }
    }

    private void ShowHideTab1Content(bool isShow)
    {
        if (isShow)
        {
            txtSelectedName.text = string.Empty;
            txtSelectedScore.text = string.Empty;
            SetLeaderBoard();
        }
        tab1Content.gameObject.SetActive(isShow);
    }

    private void ShowHideTab2Content(bool isShow)
    {
        tab2Content.gameObject.SetActive(isShow);
    }

    private void SetLeaderBoard()
    {
        // Destroy all item is list.
        for (int i = 0; i < gridLayoutGroup.transform.childCount; i++)
        {
            Destroy(gridLayoutGroup.transform.GetChild(i).gameObject);
        }
        
        if (ScoreController.Scores == null ||
            !ScoreController.Scores.Any())
        {
            return;
        }
        var topRank = ScoreController.Scores.OrderByDescending(x => x.Value)
            .Take(100)
            .ToList();
        float itemHight = 60;
        float spacingY = 5;
        foreach (var s in topRank.Select((v, i) => (v, i)))
        {
            var obj = Instantiate(itemTemplate);
            var comp = obj.GetComponent<Image>(); // gameObject (item list view)
            if (comp != null)
            {
                var no = comp.transform.Find("txtNo").GetComponent<TextMeshProUGUI>();
                var name = comp.transform.Find("txtName").GetComponent<TextMeshProUGUI>();
                var score = comp.transform.Find("txtScore").GetComponent<TextMeshProUGUI>();
                var btn = comp.transform.Find("Button").GetComponent<Button>();
                no.text = $"{s.i + 1}";
                name.text = s.v.Key;
                score.text = s.v.Value.ToString("0");
                btn.onClick.AddListener(delegate { ItemClick(obj, no, name, score); });
            }
            obj.gameObject.SetActive(true);
            obj.transform.SetParent(gridLayoutGroup.transform, false);
        }
        var totalItems = topRank.Count;
        var newContentHeight = itemHight * totalItems + spacingY * totalItems;
        scrollRects.content.sizeDelta = new Vector2(scrollRects.content.sizeDelta.x, newContentHeight);
        scrollRects.content.anchoredPosition = new Vector3(scrollRects.content.anchoredPosition.x, newContentHeight * 0.5f);
        scrollRects.verticalNormalizedPosition = 1;
    }

    private Image prevSelected;

    private void ItemClick(Image obj,
        TextMeshProUGUI no,
        TextMeshProUGUI name,
        TextMeshProUGUI score)
    {
        if (prevSelected != null)
        {
            var noPrev = prevSelected.transform.Find("txtNo").GetComponent<TextMeshProUGUI>();
            var namePrev = prevSelected.transform.Find("txtName").GetComponent<TextMeshProUGUI>();
            var scorePrev = prevSelected.transform.Find("txtScore").GetComponent<TextMeshProUGUI>();
            prevSelected.color = new Color32(236, 207, 106, 255);
            var textColor = Color.white;
            noPrev.color = textColor;
            namePrev.color = textColor;
            scorePrev.color = textColor;
        }
        prevSelected = obj;
        txtSelectedName.text = name.text;
        txtSelectedScore.text = score.text;
        var textSelectedColor = Color.black;
        obj.color = Color.cyan;
        no.color = textSelectedColor;
        name.color = textSelectedColor;
        score.color = textSelectedColor;
    }
}
