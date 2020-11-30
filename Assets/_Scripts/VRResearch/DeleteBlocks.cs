using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBlocks : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.transform.gameObject.tag == "beat") {
            beat beatObject = other.transform.gameObject.GetComponent<beat>();
            if (!beatObject) Debug.LogError("No beat component attached to gameObject with tag beat");
            DataTracker.on_miss(!beatObject.isStroop, beatObject.time_since_creation());
            Destroy(other.gameObject);
        } else if (other.transform.gameObject.tag == "bomb") {
            Destroy(other.gameObject);
        }
    }
}
