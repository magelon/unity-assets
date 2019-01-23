using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TurnCounter : MonoBehaviour
{
    public Text turnDispaly;
    int turn = 0;
    // Start is called before the first frame update
    private void OnEnable()
    {
        //Subscribe to time control system
        TimeControlSystem.OnTimeAdvance += Advance;
    }
    private void OnDisable()
    {
        //unsubscribe from time control system
        TimeControlSystem.OnTimeAdvance -= Advance;
    }
   public void Advance() {
        turn++;
        turnDispaly.text = string.Format("Turn: {0}", turn);
   }
}
