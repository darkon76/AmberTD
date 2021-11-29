using System;
using UnityEngine;

/// <summary>
/// Move object to target. 
/// </summary>
public abstract class SOMover : MonoBehaviour
{
    public virtual GameObject Target { get; set; }
    public abstract event Action OnTargetReached;
    public float Speed;
}
