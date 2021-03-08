using UnityEngine;
using UnityEngine.UI;

public class FeedBack_Bar : MonoBehaviour
{
    private static Image Bar;
    private float incBy = 1f;
    private float decBy = 10f;

    //this takes a float value 0 <= value <= 100
    public static void SetHealthBarValue(float value)
    {
        if (!(value > 100) && !(value < 0))
        {
            Bar.fillAmount = value / 100;
        }
        else if (value >= 100)
        {
            Bar.fillAmount = 1.0f;
        }
        else // value < 0
        {
            Bar.fillAmount = 0f;
        }
    }

    public static float GetHealthBarValue()
    {
        return Bar.fillAmount * 100.0f;
    }

    /// <summary>
    /// Sets the health bar color
    /// </summary>
    /// <param name="healthColor">Color </param>
    public static void SetHealthBarColor(Color healthColor)
    {
        Bar.color = healthColor;
    }

    /// <summary>
    /// Initialize the variable
    /// </summary>
    private void Start()
    {
        Bar = GetComponent<Image>();
        SetHealthBarValue(ExpManager.instance.currPlayerHealth);
        decBy *= -1;
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

    public void AddHealthBarValue(float value)
    {
        float currVal = GetHealthBarValue();
        if (currVal + value <= 0)
        {
            Messenger.Broadcast("GameOver");
            SetHealthBarValue(0.0f);
        }
        else if (currVal + value <= ExpManager.instance.maxPlayerHealth)
        {
            SetHealthBarValue(currVal + value);
            ExpManager.instance.currPlayerHealth = (currVal) + value;
        }
    }
    public void GoodHit()
    {
        AddHealthBarValue(ExpManager.instance.incHealthBy);
    }

    public void BadHit()
    {
        AddHealthBarValue(ExpManager.instance.decHealthBy);
    }
}
