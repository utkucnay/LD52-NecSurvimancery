using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public float time;
    int SpawnCount = 0;
    float SpawnTimer = 0;

    public Transform SpawnLoc;

    public int souls;

    public UnityEvent eStartGame;

    private void Start()
    {
        eStartGame.AddListener(() =>
        {
            SpawnLogic();
            AIManager.s_Instance.AddLilSkeleton(Vector3.left * 2);
        });
        
    }

    public void StartGame()
    {
       eStartGame.Invoke(); 
    }

    private void Update()
    {
        time += Time.deltaTime;

    }

    void SpawnLogic()
    {
        StartCoroutine(SpawnCor(SpawnTimer,SpawnCount));
    }

    IEnumerator SpawnCor(float SpawnTimer, int SpawnCount)
    {
        
        while (true)
        {
            yield return new WaitForSeconds(SpawnTimer);
            SpawnCount = (int)(time / 52.8) + 1;
            SpawnTimer = Utils.Scale(0, 1800, 6f, .4f, time);
            int SpawnObjectType = (int)(time / 70.6f) + 4;
            SpawnCount = Mathf.Clamp(SpawnCount, 0, 70);
            SpawnTimer = Mathf.Clamp(SpawnTimer, 0.4f, 6f);
            SpawnObjectType = Mathf.Clamp(SpawnObjectType, 4, 9);
            var listRand = new List<int>();
            for (int i = 0; i < SpawnCount; i++)
            {
                var rand = Random.Range(0, 70);
                if (listRand.Contains(rand))
                {
                    continue;
                }
                listRand.Add(rand);
                Spawn(SpawnLoc.GetChild(rand).position, (ObjectType)Random.Range(3, SpawnObjectType));
            }
        }
        
        
    }

    void Spawn(in Vector3 position, ObjectType objectType)
    {
        var objecc = ObjectPool.s_Instance.GetObject(objectType);
        objecc.transform.position = position;
        objecc.SetActive(true);
    }
}
