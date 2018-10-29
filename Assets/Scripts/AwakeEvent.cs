using UnityEngine;
using UnityEngine.Events;

public class AwakeEvent : MonoBehaviour {

    public UnityEvent onAwake;

    private void Awake()
    {
        onAwake.Invoke();
    }
}
