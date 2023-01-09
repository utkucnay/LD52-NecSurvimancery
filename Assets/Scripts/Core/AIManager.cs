using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIManager : Singleton<AIManager>
{
    public List<GameObject> lilSkeletons;
    public List<GameObject> midSkeletons;
    public List<GameObject> hugeSkeletons;

    public CinemachineTargetGroup targetGroup;

    public void AddLilSkeleton(GameObject gameObject)
    {
        AddLilSkeleton(gameObject.transform.position);
    }

    public void AddLilSkeleton(Vector3 loc)
    {
        var skeleton = ObjectPool.s_Instance.GetObject(ObjectType.LilSkeleton);
        skeleton.SetActive(true);
        skeleton.transform.position = loc;
        lilSkeletons.Add(skeleton);
        targetGroup.AddMember(skeleton.transform, 1, 0);
        if (lilSkeletons.Count % 10 == 0)
        {
            int length = lilSkeletons.Count;
            Vector3 avrVec = Vector3.zero;
            var lilSkeletonsTween = new List<GameObject>();
            for (int i = lilSkeletons.Count - 10; i < length; i++)
            {
                avrVec += lilSkeletons[i].transform.position;
                lilSkeletonsTween.Add(lilSkeletons[i]);
            }

            avrVec = avrVec / length;

            var seq = DOTween.Sequence();
            seq.Append(lilSkeletonsTween[0].transform.DOMove(avrVec, 2f).SetEase(Ease.InQuad));
            seq.AppendCallback(() =>
            {
                AddMidSkeleton(avrVec);
                for (int i = 0; i < 10; i++)
                {
                    RemoveLilSkeleton(lilSkeletonsTween[i]);
                }
                lilSkeletonsTween.Clear();
            });
            for (int i = 1; i < 10; i++)
            {
                lilSkeletonsTween[i].transform.DOMove(avrVec, 2f).SetEase(Ease.InQuad);
            }
            
        }
    }

    public void RemoveLilSkeleton(GameObject gameObject)
    {
        ObjectPool.s_Instance.SetObject(ObjectType.LilSkeleton, gameObject);
        gameObject.SetActive(false);
        lilSkeletons.Remove(gameObject);
        targetGroup.RemoveMember(gameObject.transform);
    }

    public void AddMidSkeleton(Vector3 loc)
    {
        var midSkeleton = ObjectPool.s_Instance.GetObject(ObjectType.MidSkeleton);
        midSkeleton.transform.position = loc;
        midSkeleton.SetActive(true);
        midSkeletons.Add(midSkeleton);
        targetGroup.AddMember(midSkeleton.transform, 1, 0);

        if (midSkeletons.Count % 8 == 0)
        {
            int lenght = midSkeletons.Count;
            Vector3 avrVec = Vector3.zero;
            var midSkeletonsTween = new List<GameObject>();
            for (int i = midSkeletons.Count - 4; i < lenght; i++)
            {
                avrVec += midSkeletons[i].transform.position;
                midSkeletonsTween.Add(midSkeletons[i]);
            }

            avrVec /= lenght;

            var seq = DOTween.Sequence();
            seq.Append(midSkeletonsTween[0].transform.DOMove(avrVec, 3.5f).SetEase(Ease.InQuad));
            seq.AppendCallback(() =>
            {
                AddHugeSkeleton(avrVec);
                for (int i = 0; i < 4; i++)
                {
                    RemoveMidSkeleton(midSkeletonsTween[i]);
                }
                midSkeletonsTween.Clear();
            });
            for (int i = 1; i < 4; i++)
            {
                midSkeletonsTween[i].transform.DOMove(avrVec, 3.5f).SetEase(Ease.InQuad);
            }
        }
    }

    public void RemoveMidSkeleton(GameObject gameObject)
    {
        ObjectPool.s_Instance.SetObject(ObjectType.MidSkeleton, gameObject);
        gameObject.SetActive(false);
        midSkeletons.Remove(gameObject);
        targetGroup.RemoveMember(gameObject.transform);
    }

    public void AddHugeSkeleton(Vector3 loc)
    {
        var hugeSkeleton = ObjectPool.s_Instance.GetObject(ObjectType.HugeSkeleton);
        hugeSkeleton.transform.position = loc;
        hugeSkeleton.SetActive(true);
        hugeSkeletons.Add(hugeSkeleton);
        //targetGroup.AddMember(hugeSkeleton.transform, 1, 0);
    }

    public void RemoveHugeSkeleton(GameObject gameObject)
    {
        ObjectPool.s_Instance.SetObject(ObjectType.HugeSkeleton, gameObject);
        gameObject.SetActive(false);
        hugeSkeletons.Remove(gameObject);
        //targetGroup.RemoveMember(gameObject.transform);
    }

    public GameObject GetClosestSkeleton(in Vector3 loc)
    {
        float min = float.MaxValue;
        GameObject gameObject = null;
        var allSkeleton = new List<GameObject>();
        allSkeleton.AddRange(lilSkeletons);
        allSkeleton.AddRange(midSkeletons);
        allSkeleton.AddRange(hugeSkeletons);

        if (allSkeleton.Count <= 0) return null;

        foreach (var skeleton in allSkeleton)
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
