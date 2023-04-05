using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{

    private string folderPath = "PoolObjects";

    private Dictionary<string, Stack<GameObject>> nameToObjects = new Dictionary<string, Stack<GameObject>>();

    void Awake()
    {
        //We load all GameObjects from the Resources folder
        GameObject[] resources = Resources.LoadAll<GameObject>(folderPath);
        foreach (GameObject objPrefab in resources)
        {
            //We create a stack of objects, in which we store the objects we have used before
            Stack<GameObject> objStack = new Stack<GameObject>();
            //This first element in each stack, is not an object in our scene, but the prefab itself
            objStack.Push(objPrefab);
            //We place the stack in our Dictionary, and use the name of the object as a key
            nameToObjects.Add(objPrefab.name, objStack);
        }
    }

    public GameObject Spawn(string name)
    {
        Stack<GameObject> objStack = nameToObjects[name];
        //If only 1 element is left in the Stack, we should Instantiate a new item
        if (objStack.Count == 1)
        {
            //We Instantiate a new object, based on the prefab
            GameObject newObject = Instantiate(objStack.Peek());
            //Make sure the new object doesn't have '(Clone)' behind it
            newObject.name = name;
            return newObject;
        }

        //If there are actually objects in our Stack, we don't Instantiate one, but we give the top object of our Stack
        GameObject topObject = objStack.Pop();
        topObject.SetActive(true);
        return topObject;
    }

    public void Despawn(GameObject obj)
    {
        obj.SetActive(false);
        //We Push the despawned object to it's Stack
        nameToObjects[obj.name].Push(obj);
    }
}
