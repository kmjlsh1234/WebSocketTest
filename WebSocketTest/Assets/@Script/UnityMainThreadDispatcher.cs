using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnityMainThreadDispatcher : MonoBehaviour
{
    private static readonly Queue<System.Action> _excutionQueue = new Queue<System.Action>();
    private static UnityMainThreadDispatcher instance;
    public static UnityMainThreadDispatcher Instance()
    {
        if(instance == null)
        {
            GameObject go = new GameObject($"@{typeof(UnityMainThreadDispatcher)}");
            instance = go.AddComponent<UnityMainThreadDispatcher>();
            DontDestroyOnLoad(go);
            
        }
        return instance;
    }

    public void Update()
    {
        lock(_excutionQueue)
        {
            {
                while(_excutionQueue.Count > 0)
                    _excutionQueue.Dequeue().Invoke();
            } }
    }

    public void Enqueue(System.Action action)
    {
        lock(_excutionQueue)
        {
            _excutionQueue.Enqueue(action);
        }
    }
}
