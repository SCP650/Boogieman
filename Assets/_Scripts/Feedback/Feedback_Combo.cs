using UnityEngine;
using UnityEngine.UI;

public class Feedback_Combo : MonoBehaviour
{
    public Text comboField;
    public Text scoreField;
    private int combo = 0;
    private int largestCombo = 0;
    private int score = 0;

    private void Start()
    {
        comboField.text = combo + "";
        scoreField.text = score + "";
    }

    private void Awake()
    {
        Messenger.AddListener("UpdateUI", UpdateUI);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener("UpdateUI", UpdateUI);
    }

    public void UpdateUI()
    {
        comboField.text = combo + "";
        scoreField.text = score + "";
    }
}
