using UnityEngine;
using UnityEngine.Events;

public class JointEvents : MonoBehaviour
{
    public UnityEvent onJointBreak;

    private void OnJointBreak()
    {
        onJointBreak.Invoke();
    }
}
