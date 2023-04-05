using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class PillEffect : MonoBehaviour
{

    float lifeTime = 2;

    // Use this for initialization
    void OnEnable()
    {
        Invoke("Despawn", lifeTime);
    }

    void Despawn()
    {
      // PoolManager.instance.Despawn(this.gameObject);
    }


}
