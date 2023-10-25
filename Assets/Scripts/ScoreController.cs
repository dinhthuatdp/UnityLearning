using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static Dictionary<string, float> Scores = new();

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI txtScore;
    [SerializeField] private Button btnClickMe;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 0;
        txtScore.text = "0";
        btnClickMe.onClick.RemoveAllListeners();
        btnClickMe.onClick.AddListener(SliderChange);
    }

    public void SliderChange()
    {
        slider.value += 1;
        txtScore.text = slider.value.ToString();
    }
}
