using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FeedbackSystem : MonoBehaviour
{
    private enum FeedbackType {Good, Bad};
    private int feedbackScore;
    public static FeedbackSystem S;

    void Awake() {
        if (S == null) S = this;
    }

    // where to show the feedback, could be based on where beat is
    // [SerializeField] private Transform feedbackLocation;
    [SerializeField] private int displayTime;

    //TODO: adjust this object's height to the player's height?
    [SerializeField] private GameObject textDisplay;
    [SerializeField] private AudioClip goodNoise;
    [SerializeField] private AudioClip badNoise;

    public void negativeFeedback(/*int toRemove*/) {
        // feedbackScore -= toRemove;
        StopAllCoroutines();
        StartCoroutine(ShowFeedback(FeedbackType.Bad));
    }

    public void positiveFeedback(/*int toAdd*/) {
        // feedbackScore += toAdd;
        StopAllCoroutines();
        StartCoroutine(ShowFeedback(FeedbackType.Good));
    }

    IEnumerator ShowFeedback(FeedbackType b) {
        TMP_Text t = textDisplay.GetComponent<TMP_Text>();

        // Note that good and bad noises will cut each other off
        // if accessed at the same time
        switch (b) {
            case FeedbackType.Good:
                t.text = "GOOD";
                t.color = Color.green;
                GetComponent<AudioSource>().PlayOneShot(goodNoise);
                break;
            case FeedbackType.Bad:
                t.text = "BAD";
                t.color = Color.red;
                GetComponent<AudioSource>().PlayOneShot(badNoise);
                break;
        }

        yield return new WaitForSeconds(displayTime);
        t.text = "";
        t.color = Color.white;
        yield return null;
    }
}
