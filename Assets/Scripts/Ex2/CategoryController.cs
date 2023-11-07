using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class CategoryController : MonoBehaviour
{
    public CategoryScriptableObject categoryScriptableObject;

    [SerializeField] private VerticalLayoutGroup verticalLayoutGroupCategory;
    [SerializeField] private VerticalLayoutGroup verticalLayoutGroupChapter;
    [SerializeField] private VerticalLayoutGroup verticalLayoutGroupDoKho;
    [SerializeField] private ScrollRect scrollRectCategory;
    [SerializeField] private ScrollRect scrollRectChapter;
    [SerializeField] private ScrollRect scrollRectDoKho;
    [SerializeField] private Image itemTemp;
    private Image prevSelectedCategory;
    private Image prevSelectedChapter;
    private Image prevSelectedDoKho;

    // Start is called before the first frame update
    void Start()
    {
        itemTemp.gameObject.SetActive(false);
        foreach (var item in categoryScriptableObject.Categories)
        {
            var obj = Instantiate(itemTemp);
            obj.gameObject.SetActive(true);
            var comp = obj.GetComponent<Image>();
            if (comp != null)
            {
                var txt = comp.transform.Find("TxtCategory").GetComponent<TextMeshProUGUI>();
                var btn = comp.transform.Find("BtnClick").GetComponent<Button>();
                var img = comp.transform.Find("ImgChapter").GetComponent<Image>();
                var imgLock = comp.transform.Find("Lock").GetComponent<Image>();
                txt.text = item.Name;
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(delegate { OnClickCategory(obj, item.Name); });
                img.gameObject.SetActive(false);
                imgLock.gameObject.SetActive(false);
            }
            obj.transform.SetParent(verticalLayoutGroupCategory.transform, false);
        }
        scrollRectCategory.verticalNormalizedPosition = 1f;
    }

    private void OnClickCategory(Image item, string title)
    {
        var outline = item.transform.GetComponent<Outline>();
        outline.enabled = true;

        // Update chapter list
        RerawChapterUI(title);

        if (prevSelectedCategory is null)
        {
            prevSelectedCategory = item;
            return;
        }
        prevSelectedCategory.transform.GetComponent<Outline>().enabled = false;
        prevSelectedCategory = item;
        prevSelectedChapter = null;
        prevSelectedDoKho = null;
    }

    private void OnClickChapter(Image item, string category, string chapter)
    {
        var outline = item.transform.GetComponent<Outline>();
        outline.enabled = true;

        // Update doKho list
        RerawDoKhoUI(category, chapter);

        if (prevSelectedChapter is null)
        {
            prevSelectedChapter = item;
            return;
        }
        prevSelectedChapter.transform.GetComponent<Outline>().enabled = false;
        prevSelectedChapter = item;
        prevSelectedDoKho = null;
    }

    private void OnClickDoKho(Image item)
    {
        var outline = item.transform.GetComponent<Outline>();
        outline.enabled = true;

        if (prevSelectedDoKho is null)
        {
            prevSelectedDoKho = item;
            return;
        }
        prevSelectedDoKho.transform.GetComponent<Outline>().enabled = false;
        prevSelectedDoKho = item;
    }

    private void ClearDoKhoUI()
    {
        for (int i = 0; i < verticalLayoutGroupDoKho.transform.childCount; i++)
        {
            Destroy(verticalLayoutGroupDoKho.transform.GetChild(i).gameObject);
        }
    }

    private void RerawDoKhoUI(string category, string chapter)
    {
        ClearDoKhoUI();
        var chapters = categoryScriptableObject.Categories
            .FirstOrDefault(x => x.Name == category)
            ?.Chapters;
        if (chapters is null)
        {
            return;
        }
        var doKho = chapters.FirstOrDefault(x => x.Name == chapter)?.DoKho;
        if (doKho is null)
        {
            return;
        }
        foreach (var item in doKho)
        {
            var obj = Instantiate(itemTemp);
            obj.gameObject.SetActive(true);
            var comp = obj.GetComponent<Image>();
            if (comp != null)
            {
                var txt = comp.transform.Find("TxtCategory").GetComponent<TextMeshProUGUI>();
                var btn = comp.transform.Find("BtnClick").GetComponent<Button>();
                var img = comp.transform.Find("ImgChapter").GetComponent<Image>();
                var imgLock = comp.transform.Find("Lock").GetComponent<Image>();
                txt.text = item;
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(delegate { OnClickDoKho(obj); });
                img.gameObject.SetActive(false);
                imgLock.gameObject.SetActive(true);
            }
            obj.transform.SetParent(verticalLayoutGroupDoKho.transform, false);
            scrollRectDoKho.verticalNormalizedPosition = 1f;
        }
    }

    private void ClearChapterUI()
    {
        for (int i = 0; i < verticalLayoutGroupChapter.transform.childCount; i++)
        {
            Destroy(verticalLayoutGroupChapter.transform.GetChild(i).gameObject);
        }
    }

    private void RerawChapterUI(string category)
    {
        ClearDoKhoUI();
        ClearChapterUI();
        var chapters = categoryScriptableObject.Categories
            .FirstOrDefault(x => x.Name == category)
            ?.Chapters;
        if (chapters is null)
        {
            return;
        }
        foreach (var item in chapters)
        {
            var obj = Instantiate(itemTemp);
            obj.gameObject.SetActive(true);
            var comp = obj.GetComponent<Image>();
            if (comp != null)
            {
                var txt = comp.transform.Find("TxtCategory").GetComponent<TextMeshProUGUI>();
                var btn = comp.transform.Find("BtnClick").GetComponent<Button>();
                var img = comp.transform.Find("ImgChapter").GetComponent<Image>();
                var imgLock = comp.transform.Find("Lock").GetComponent<Image>();
                txt.text = item.Name;
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(delegate { OnClickChapter(obj, category, item.Name); });
                img.gameObject.SetActive(true);
                imgLock.gameObject.SetActive(false);
            }
            obj.transform.SetParent(verticalLayoutGroupChapter.transform, false);
            scrollRectChapter.verticalNormalizedPosition = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
