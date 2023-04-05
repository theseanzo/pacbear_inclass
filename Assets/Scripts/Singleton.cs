using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance
    {
        //This is called a get-variable, everything we put inside
        //the 'get' brackets, will be executed when we read this variable
        get
        {
            //if _instance has not been assigned yet...
            if (_instance == null)
            {
                //Search the scene for this instance of this Type
                _instance = FindObjectOfType<T>();
            }
            return _instance;
        }
    }

    private static T _instance;
}
