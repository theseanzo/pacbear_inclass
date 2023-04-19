using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PacBear : BaseUnit
{
    [SerializeField] float specialModeDuration = 5f;
    public static event Action<bool> onSpecialModeSwitch;
    [SerializeField] int numLives = 3;
    private bool isSpecial;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            direction = new Vector2Int(-1, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction = new Vector2Int(1, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            direction = new Vector2Int(0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction = new Vector2Int(0, -1);
        }
        Move();
    }

    void OnTriggerEnter(Collider otherCollider)
    {

        if (otherCollider.GetComponent<Pill>() != null)
        {
            Destroy(otherCollider.gameObject);
            GameManager.instance.NumPillsLeft--;
            GameObject effect = PoolManager.instance.Spawn("EatPillEffect");
            effect.transform.position = otherCollider.transform.position;
        }
        
        if (otherCollider.GetComponent<Ghost>() != null)
        {
            Ghost ghost = otherCollider.GetComponent<Ghost>();
            if (isSpecial && !ghost.isReturnToSpawn)
            {
                ghost.Death();
                //Destroy(otherCollider.gameObject);
            }
            else if(!ghost.isReturnToSpawn)
            {
                SceneManager.LoadScene("SampleScene");
            }
           
        }
        if (otherCollider.GetComponent < SpecialPill>() != null)
        {
            Destroy(otherCollider.gameObject);
            isSpecial = true;
            onSpecialModeSwitch?.Invoke(true);
            CancelInvoke();
            Invoke("EndSpecialMode", specialModeDuration);
        }
    }
    void EndSpecialMode()
    {
        isSpecial = false;
        onSpecialModeSwitch?.Invoke(false);
    }
}
