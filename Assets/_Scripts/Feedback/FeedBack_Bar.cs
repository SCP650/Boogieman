using UnityEngine;
using UnityEngine.UI;

public class FeedBack_Bar : MonoBehaviour
{
    private static Image Bar;
    public float incrementby = 0.01f;
    public float decrementby = 0.1f;
    public float startVal = 0.5f;
    /// <summary>
    /// Sets the health bar value
    /// </summary>
    /// <param name="value">should be between 0 to 1</param>
    public static void SetHealthBarValue(float value)
    {
        Bar.fillAmount = value;
/*        if (Bar.fillAmount < 0.2f)
        {
            SetHealthBarColor(Color.red);
        }
        else if (Bar.fillAmount < 0.4f)
        {
            SetHealthBarColor(Color.yellow);
        }
        else
        {
            SetHealthBarColor(Color.green);
        }*/
    }

    public static float GetHealthBarValue()
    {
        return Bar.fillAmount;
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
        SetHealthBarValue(startVal);
        decrementby *= -1;
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
            return;
        }
        SetHealthBarValue(currVal + value);
    }
    public void GoodHit()
    {
        AddHealthBarValue(incrementby);

    }

    public void BadHit()
    {
        AddHealthBarValue(decrementby);
    }
}
