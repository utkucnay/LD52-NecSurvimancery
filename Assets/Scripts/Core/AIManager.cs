using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : Singleton<AIManager>
{
    public List<GameObject> skeletons;



    public void AddLilSkeleton()
    {
        var skeleton = ObjectPool.s_Instance.GetObject(ObjectType.LilSkeleton);
        skeletons.Add(skeleton);

    }

    public GameObject GetClosestSkeleton(in Vector3 loc)
    {
        float min = float.MaxValue;
        GameObject gameObject = null;

        foreach (var skeleton in skeletons)
        {
            float tempMin = Vector3.Distance(skeleton.transform.position, loc);
            if (min > tempMin)
            {
                min = tempMin;
                gameObject = skeleton;
            }
        }

        return gameObject;
    }
}
