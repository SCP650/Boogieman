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
        Messenger.AddListener("Goodhit", GoodHit);
        Messenger.AddListener("Badhit", BadHit);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener("Goodhit", GoodHit);
        Messenger.RemoveListener("Badhit", BadHit);
    }

    public void GoodHit()
    {
        combo++;
        comboField.text = combo + "";
        score++;
        scoreField.text = score + "";

    }

    public void BadHit()
    {
        comboField.text = 0 + "";
        largestCombo = combo;
        combo = 0;
    }
}
