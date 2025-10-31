using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BloodSugarLevel : MonoBehaviour
{
    [Header("Blood Sugar Settings")]
    [SerializeField] private int minBSL = 40;
    [SerializeField] private int maxBSL = 350;
    [SerializeField] private int minGoodBSL = 80;
    [SerializeField] private int maxGoodBSL = 140;
    [SerializeField] private int initBSL = 100;
    [SerializeField] private int GLUKIBSLVal = 10;

    [Header("UI")]
    [SerializeField] private TMP_Text BSLVal;
    [SerializeField] private Slider slider;

    [Header("Animation")]
    [SerializeField] private float slideSpeed = 300f; // units per sec
    [SerializeField] private float slideDuration = 0.3f;

    private int prevNumGLUKI;
    private int targetBSL;
    private Coroutine slideCo;

    // Allow scripts to grab but not set BSL:
    public int BSL { get; private set;}

    // Start is called before the first frame update
    private void Start()
    {
        BSL = initBSL;
        targetBSL = BSL;

        slider.minValue = minBSL;
        slider.maxValue = maxBSL;
        slider.wholeNumbers = true; // if we want integer ticks

        slider.value = BSL;
        BSLVal.text = BSL.ToString();

        prevNumGLUKI = GameObject.FindGameObjectsWithTag("GLUKI").Length;
    }

    // Update is called once per frame
    private void Update()
    {
        int numGLUKI = GameObject.FindGameObjectsWithTag("GLUKI").Length;
        int GLUKIDif = numGLUKI - prevNumGLUKI;

        if (GLUKIDif != 0)
        {
            BSL += GLUKIDif * GLUKIBSLVal;
            BSL = Mathf.Clamp(BSL, minBSL, maxBSL);
            prevNumGLUKI = numGLUKI; //update prevNumGLUKI
            SetTarget(BSL);
        }
    }

    /// <summary>
    /// Starts sliding animation toward new BSL target.
    /// </summary>
    private void SetTarget(int newTarget)
    {
        newTarget = Mathf.Clamp(newTarget, minBSL, maxBSL);

        // stop existing slide animation
        if (slideCo != null)
            StopCoroutine(slideCo);

        // start new one
        slideCo = StartCoroutine(SlideTo(newTarget, slideDuration));
    }

    /// <summary>
    /// Coroutine that interpolates the slider smoothly.
    /// </summary>
    private IEnumerator SlideTo(int target, float duration)
    {
        float start = slider.value;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // SmoothStep for nice ease-in/ease-out feel
            float value = Mathf.Lerp(start, target, Mathf.Lerp(0f, 1f, t));
            slider.value = value;

            // update text during animation
            BSLVal.text = Mathf.RoundToInt(slider.value).ToString();

            yield return null;
        }

        // ensure final value is exact
        slider.value = target;
        BSLVal.text = target.ToString();
        slideCo = null;
    }

    public bool HealthyBloodLevel()
    {
        // if blood meter is between good blood sugar level: 80 <= BSL <= 130
        if (minGoodBSL <= BSL && BSL <= maxGoodBSL)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
