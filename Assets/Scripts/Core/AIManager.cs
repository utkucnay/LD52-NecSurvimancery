using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : Singleton<AIManager>
{
    public List<GameObject> skeletons;

    public CinemachineTargetGroup targetGroup;

    public void AddLilSkeleton(GameObject gameObject)
    {
        var skeleton = ObjectPool.s_Instance.GetObject(ObjectType.LilSkeleton);
        skeleton.SetActive(true);
        skeleton.transform.position = gameObject.transform.position;
        skeletons.Add(skeleton);
        targetGroup.AddMember(skeleton.transform,1,0);
    }

    public void AddLilSkeleton(Vector3 loc)
    {
        var skeleton = ObjectPool.s_Instance.GetObject(ObjectType.LilSkeleton);
        skeleton.SetActive(true);
        skeleton.transform.position = loc;
        skeletons.Add(skeleton);
        targetGroup.AddMember(skeleton.transform, 1, 0);
    }

    public void RemoveLilSkeleton(GameObject gameObject)
    {
        ObjectPool.s_Instance.SetObject(ObjectType.LilSkeleton, gameObject);
        gameObject.SetActive(false);
        skeletons.Remove(gameObject);
        targetGroup.RemoveMember(gameObject.transform);
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
