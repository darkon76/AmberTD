using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SOMover : MonoBehaviour
{
    public virtual GameObject Target { get; set; }
    public abstract event Action OnTargetReached;
    public float Speed;
}
