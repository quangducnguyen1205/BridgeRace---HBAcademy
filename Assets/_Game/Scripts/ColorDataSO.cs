using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorDataSO", menuName = "ScriptableObject/ColorDataSO", order = 1)]
public class ColorDataSO : ScriptableObject
{
    [SerializeField] Material[] colorMats;

    public Material GetColorMat(ColorType colorType){
        return colorMats[(int)colorType];
    }
}
