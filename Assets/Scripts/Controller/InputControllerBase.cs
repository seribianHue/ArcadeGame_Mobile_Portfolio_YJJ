using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputControllerBase : MonoBehaviour
{
    public Vector2 _inputDir { get; protected set; }

    public abstract void Update();
}
