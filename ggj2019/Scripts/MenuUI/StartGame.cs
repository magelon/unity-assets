using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject PlayManager;
    public GameObject PlayButton;
    public GameObject resteButton;
    public GameObject ScoreUi;
   
    // Start is called before the first frame update
    void Awake()
    {
      // PlayManager.SetActive(false);
        resteButton.SetActive(false);
        ScoreUi.SetActive(false);

    }

    private void Start()
    {
        
    }

    public void ActiveGame() {
       // PlayManager.SetActive(true);
        resteButton.SetActive(false);
        ScoreUi.SetActive(true);
        PlayButton.SetActive(false);

    }

    public void Rest() {
        //PlayManager.SetActive(true);
        SpawnBlocks.score = 0;
        SpawnBlocks.life = 3;
        
        resteButton.SetActive(false);


    }


    private void Update()
    {
        if (SpawnBlocks.life == 0)
        {
            //PlayManager.SetActive(false);
            resteButton.SetActive(true);

        }
        
    }
}
