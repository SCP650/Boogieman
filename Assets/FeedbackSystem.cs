using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackSystem : MonoBehaviour
{
    private enum FeedbackType {Good, Bad};
    private int feedbackScore;
    public static FeedbackSystem S;

    // where to show the feedback, could be based on where beat is
    // [SerializeField] private Transform feedbackLocation;
    [SerializeField] private int displayTime;
    [SerializeField] private GameObject goodDisplay;
    [SerializeField] private GameObject badDisplay;

    public void negativeFeedback(int toRemove, Transform spawnLocation) {
        feedbackScore -= toRemove;
        StartCoroutine(ShowFeedback(FeedbackType.Bad, spawnLocation));
    }

    public void positiveFeedback(int toAdd, Transform spawnLocation) {
        feedbackScore += toAdd;
        StartCoroutine(ShowFeedback(FeedbackType.Good, spawnLocation));
    }

    IEnumerator ShowFeedback(FeedbackType b, Transform spawnLocation) {
        switch (b) {
            case FeedbackType.Good:
                GameObject g = Instantiate(goodDisplay, spawnLocation);
                yield return new WaitForSeconds(displayTime);
                Destroy(g);
                break;
            case FeedbackType.Bad:
                GameObject g2 = Instantiate(badDisplay, spawnLocation);
                yield return new WaitForSeconds(displayTime);
                Destroy(g2);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
