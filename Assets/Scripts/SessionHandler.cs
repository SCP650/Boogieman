using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionHandler : MonoBehaviour
{
    [SerializeField] List<Session> trueSessions;
    [SerializeField] List<Session> falseSessions;
    void Start() 
    {
        foreach (Session s in trueSessions) {
            s.SetToggle(true);
        }

        foreach (Session s in falseSessions) {
            s.SetToggle(false);
        }
    }
}
