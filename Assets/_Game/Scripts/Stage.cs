using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType{ Default, Blue, Red, Green, Pink }
public class Stage : MonoBehaviour
{
    public Transform[] brickPoints;
    public List<Vector3> emptyPoints = new List<Vector3>();
}
