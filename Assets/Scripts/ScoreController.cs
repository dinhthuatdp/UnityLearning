using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static Dictionary<string, float> Scores = new();

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI txtScore;
    [SerializeField] private Button btnClickMe;
    [SerializeField] private Button btnConfirm;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 0;
        txtScore.text = "0";
        btnClickMe.onClick.RemoveAllListeners();
        btnClickMe.onClick.AddListener(SliderChange);
        btnConfirm.onClick.RemoveAllListeners();
        btnConfirm.onClick.AddListener(Confirm);
        slider.onValueChanged.RemoveAllListeners();
        slider.onValueChanged.AddListener(ScoreChange);
    }

    public void Confirm()
    {
        AddScores(slider.value);
        slider.value = 0.0f;
        txtScore.text = "0";
    }

    public void SliderChange()
    {
        slider.value += 1;
        txtScore.text = slider.value.ToString();
    }

    private void ScoreChange(float value)
    {
        txtScore.text = value.ToString("0");
    }

    private void AddScores(float score)
    {
        if ((int)score == 0)
        {
            return;
        }
        Scores.Add(Guid.NewGuid().ToString("n").Substring(0, 6), score);
    }
}
