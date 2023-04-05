using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : Singleton<UIManager>
{
    // Start is called before the first frame update
    
    TMP_Text numLivesText;
    TMP_Text numPillsText;
    void Start()
    {
        GameObject.Find("Title").GetComponent<TMP_Text>().SetText("Pacbear");
        numLivesText = GameObject.Find("Lives").GetComponent<TMP_Text>();
        numPillsText = GameObject.Find("Pills").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateNumLives(int numLives)
    {

    }
    public void UpdatePillsRemaining(int numPills, int totalPills)
    {

    }
}
