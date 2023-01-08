using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChangeColorCommand : Command
{
    Material material;
    Color targetColor;
    float duration;

    public static MaterialChangeColorCommand Init(Material material, in Color targetColor,float duration) 
        { return new MaterialChangeColorCommand { material = material, targetColor = targetColor, duration = duration }; }

    public override bool CheckCondition()
    {
        return true;
    }

    public override void Execute()
    {
        material.DOColor(targetColor, duration);
    }

    public override void ResetVariable()
    {
        
    }
}
