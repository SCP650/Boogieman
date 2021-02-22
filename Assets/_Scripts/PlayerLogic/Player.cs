using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private PostProcessVolume ObstacleVolume;
    private Camera m_MainCamera;
    bool isInObstacle = false;
    float obstacleDMG = 0.1f;
    float avoidanceHealth = 100.0f;


    // Start is called before the first frame update
    void Start()
    {
        //This gets the Main Camera (player's Camera) from the Scene
        m_MainCamera = Camera.main; //This gets a camera with the tag "MainCamera" on it. 
        m_MainCamera.enabled = true; //Should still be enabled by default, I believe...
        ObstacleVolume.gameObject.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(avoidanceHealth);
    }

    IEnumerator DamageSelf()
    {
        while (avoidanceHealth > 0)
        {
            avoidanceHealth -= obstacleDMG;
            Debug.Log("hurt player");
            yield return null;
        }
        if (avoidanceHealth <= 1.0f)//is this not needed? 
        {
            Messenger.Broadcast("GameOver");
        }
        yield return "yoo he dead";
    }

    IEnumerator HealSelf()
    {
        while (avoidanceHealth < 100)
        {
            if (avoidanceHealth <= 50)
            {
                avoidanceHealth = 65;
            }
            else
            {
                Debug.Log("heal player");
                avoidanceHealth += obstacleDMG;
            }
            yield return null;
        }
        yield return "he healed";
    }

    public void StartDealingDamage()
    {
        //Stop a coroutine called HealSelf, if running
        StopCoroutine(HealSelf());
        //Start a coroutine called DamageSelf
        StartCoroutine(DamageSelf());
        ObstacleVolume.gameObject.SetActive(true);

    }


    public void StopDealingDamage()
    {
        //Stop a coroutine called DamageSelf, if running
        StopCoroutine(DamageSelf());
        //Start a coroutine called HealSelf
        StartCoroutine(HealSelf());
        ObstacleVolume.gameObject.SetActive(false);
    }

    /*    private void OnTriggerEnter(Collider other)
        {
            if (other.transform.gameObject.tag == "obstacle")
            {
                Debug.Log("We Hit A Wall");

            }
        }
    */

}
