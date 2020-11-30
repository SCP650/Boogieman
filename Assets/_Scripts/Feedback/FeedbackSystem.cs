using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FeedbackSystem : MonoBehaviour
{
    private enum FeedbackType {Good, Bad};
    public enum SaberSide { Left, Right, DoesntMatter };
    private int feedbackScore;
    public static FeedbackSystem S;
    public AudioSource audioleft;
    public AudioSource audioright;
    void Awake() {
        if (S == null) S = this;
    }

    // where to show the feedback, could be based on where beat is
    // [SerializeField] private Transform feedbackLocation;
    [SerializeField] private int displayTime;

    //TODO: adjust this object's height to the player's height?
    [SerializeField] private GameObject textDisplay;
    [SerializeField] private AudioClip[] goodNoisesLeft;
    [SerializeField] private AudioClip[] goodNoisesRight;

    [SerializeField] private AudioClip[] badNoises;
    private AudioClip Noise;

    public void TriggerVibration(AudioClip vibrationAudio, int controller)
    {
        if (vibrationAudio != null)
        {
            OVRHapticsClip clip = new OVRHapticsClip(vibrationAudio); //we crash here

            if(controller == 0)
            {
                Debug.Log("dies on left controller");
                OVRHaptics.LeftChannel.Preempt(clip);
            }
            else if (controller == 1)
            {
                Debug.Log("dies on right controller");
                OVRHaptics.RightChannel.Preempt(clip);
            }
        }
    }

    public void negativeFeedback() {
        StopAllCoroutines();
        
        StartCoroutine(ShowFeedback(FeedbackType.Bad, SaberSide.DoesntMatter));
    }

    public void positiveFeedback(SaberSide saberSide) {
        // feedbackScore += toAdd;
        StopAllCoroutines();
        StartCoroutine(ShowFeedback(FeedbackType.Good,saberSide));
    }
    
    IEnumerator ShowFeedback(FeedbackType b, SaberSide saberSide) {
        TMP_Text t = textDisplay.GetComponent<TMP_Text>();

        // Note that good and bad noises will cut each other off
        // if accessed at the same time
        switch (b) {
            case FeedbackType.Good:
                t.text = "GOOD";
                t.color = Color.green;
                switch (saberSide)
                {
                    case SaberSide.Left:
                        Noise = goodNoisesLeft[Random.Range(0, goodNoisesLeft.Length)];
                        audioleft.PlayOneShot(Noise);
                        //TriggerVibration(Noise, 0);
                        break;
                    case SaberSide.Right:
                        Noise = goodNoisesRight[Random.Range(0, goodNoisesRight.Length)];
                        audioright.PlayOneShot(Noise);
                        //TriggerVibration(Noise,1);
                        break;
                    default:
                        break;
                }
                break;
            case FeedbackType.Bad:
                t.text = "BAD";
                t.color = Color.red;
                audioright.PlayOneShot(badNoises[Random.Range(0, badNoises.Length)]);
                break;
        }

        yield return new WaitForSeconds(displayTime);
        t.text = "";
        t.color = Color.white;
        yield return null;
    }
}
