using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControlSystem : MonoBehaviour
{
    public float turnDuration = 1f;
    public float fastForwardMultiplier = 5f;
    public bool paused;
    public bool fastForward;

    public delegate void OnTimeAdvanceHandler();
    public static event OnTimeAdvanceHandler OnTimeAdvance;

    private float advanceTimer;
    // Start is called before the first frame update
    void Start()
    {
        advanceTimer = turnDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            advanceTimer -= Time.deltaTime * (fastForward ? fastForwardMultiplier : 1f);
            if (advanceTimer <= 0)
            {
                advanceTimer += turnDuration;
                OnTimeAdvance?.Invoke();
            }
        }
    }
    //do this with a state mechine future
    public void Step() {
        OnTimeAdvance?.Invoke();
    }
    public void Pause() {
        paused = true;
        fastForward = false;
    }
    public void Play() {
        paused = false;
        fastForward = false;
    }
    public void FastForward() {
        paused = false;
        fastForward = true;
    }
}
