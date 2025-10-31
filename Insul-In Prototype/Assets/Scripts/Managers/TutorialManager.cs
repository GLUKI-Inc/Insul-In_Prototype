using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialManager : MonoBehaviour
{

    [SerializeField] private GameObject playerHudCanvas;
    [SerializeField] private GameObject controlCanvas;

    [SerializeField] private GameObject bslLabel;
    [SerializeField] private GameObject bslNumber;
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject bloodSugarLevel;
    [SerializeField] private GameObject ammo;
    [SerializeField] private GameObject insulinBall;
    [SerializeField] private GameObject insulinLauncher;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;
    [SerializeField] private GameObject paddle;

    [SerializeField] private GameObject glukis;
    [SerializeField] private GameObject glukiSpawner;

    [SerializeField] private Dialogue dialogueList;
    [SerializeField] private GameObject textObject;

    // Start is called before the first frame update
    void Start()
    {
        playerHudCanvas = GameObject.Find("PlayerHUDCanvas");
        controlCanvas = GameObject.Find("ControlCanvas");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //CheckLines();
        // Debug.Log(tutorialStep);
    }


}
