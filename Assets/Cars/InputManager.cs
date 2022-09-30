using UnityEngine;

public abstract class InputManager : MonoBehaviour
{
    [HideInInspector]
    public float x;
    [HideInInspector]
    public float y;
    [HideInInspector]
    public float[] ins;

    public abstract void Setup();
    public abstract void DoUpdate();
}