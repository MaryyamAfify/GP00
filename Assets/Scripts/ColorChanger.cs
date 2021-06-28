using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] Material mat;
    public void ChangeColor(Color c) {
        mat.color = c; 
    }
}
