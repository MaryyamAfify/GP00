using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineHandler : MonoBehaviour
{
    [SerializeField] private Transform pivot;

    public Transform Pivot { get => pivot; set => pivot = value; }
}
