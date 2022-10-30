using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    [SerializeField] Spawners Spa;
    [SerializeField] UI ui;
    [SerializeField] Transform GG;
    [SerializeField] GameObject Boss;
    [SerializeField] GameObject Criper;
    [SerializeField] GameObject[] SpawnPoint;
    List<int> SpawnPointNum = new List<int>();
    List<GameObject> AllEnemy = new List<GameObject>();
    List<Bullet2> EnemyBullet = new List<Bullet2>();
    public int EnemyCount { get; private set; }
    float SpawnTimer;
    float NewTime;
    int BossOrNot;
    /// <summary>
    /// I pass parameters directly to prefabs, so as not to call methods every time an object is created and disable spawn points
    /// </summary>
    void Awake()
    {
        Boss.GetComponent<Bot>().Scripts(GG, Spa);
        Criper.GetComponent<Bot>().Scripts(GG, Spa);
        for (int i = 0; i < SpawnPoint.Length; i++)
        {
            SpawnPoint[i].SetActive(false);
            SpawnPointNum.Add(i);
        }
        SpawnPointAndTimer();
        SpawnTimer = NewTime;
    }
    void Update()
    {
        SpawnTimer -= Time.deltaTime;
        if (SpawnTimer <= 0)
        {
            if (SpawnPointNum.Count > 0 || SpawnPointNum.Count == 0 && NewTime > Stats.SpawnMinTimer) NewTime -= Stats.TimerDeley;
            if (NewTime < Stats.SpawnMinTimer) SpawnPointAndTimer();
            SpawnTimer = NewTime;
            for (int i = 0; i < SpawnPoint.Length; i++) if (SpawnPoint[i].active) CreateEnemy(i);
        }
    }
    /// <summary>
    /// activating a random spawn point and resetting the timer
    /// </summary>
    void SpawnPointAndTimer()
    {
        NewTime = Stats.SpawnMaxTimer;
        int Rand = UnityEngine.Random.Range(0, SpawnPointNum.Count);
        SpawnPoint[SpawnPointNum[Rand]].SetActive(true);
        SpawnPointNum.RemoveAt(Rand);
    }
    /// <summary>
    /// spawn enemies with a creep/boss counter
    /// </summary>
    /// <param name="a">spawn order in array</param>
    void CreateEnemy(int a)
    {
        GameObject NewEnemy = null;
        BossOrNot++;
        if (BossOrNot == Stats.BossLimit)
        {
            BossOrNot = 0;
            NewEnemy = Instantiate(Boss, SpawnPoint[a].transform);
        }
        else NewEnemy = Instantiate(Criper, SpawnPoint[a].transform);
        AllEnemy.Add(NewEnemy);
        EnemyCount++;
    }
    /// <summary>
    /// remove the enemy from the list (required for the ult to work correctly)
    /// </summary>
    /// <param name="a"></param>
    public void Dead(GameObject a)
    {
        AllEnemy.Remove(a);
        EnemyCount--;
    }
    /// <summary>
    /// add/remove bullets from the list (required to specify the character's teleportation point when touching a wall)
    /// </summary>
    /// <param name="a">obj</param>
    /// <param name="b">add/remove</param>
    public void Bullets(Bullet2 a, bool b)
    {
        if (b) EnemyBullet.Add(a);
        else EnemyBullet.Remove(a);
    }
    /// <summary>
    /// transferring coordinates to each bullet before teleporting the character
    /// </summary>
    public void WallShoot()
    {
        if (EnemyBullet.Count > 0)
        {
            Transform WallPos = null;
            WallPos.position = new Vector3(GG.position.x, GG.position.y, GG.position.z);
            for (int i = 0; i < EnemyBullet.Count; i++) EnemyBullet[i].Scripts(WallPos, Spa);
        }
    }
    /// <summary>
    /// search for an enemy to ricochet and return his position
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public Vector3 Ricochet(GameObject a, Vector3 b)
    {
        double Distance = 0;
        int selector = 0;
        for (int i = 0; i < AllEnemy.Count; i++)
        {
            if (AllEnemy[i] != a)
            {
                if (Distance > Math.Abs((a.transform.position.x - AllEnemy[i].transform.position.x) * (a.transform.position.z - AllEnemy[i].transform.position.z)))
                {
                    Distance = Math.Abs((a.transform.position.x - AllEnemy[i].transform.position.x) * (a.transform.position.z - AllEnemy[i].transform.position.z));
                    selector = i;
                }
            }
        }
        return AllEnemy[selector].transform.position;
    }
    /// <summary>
    /// kill them all!!!!!!
    /// </summary>
    public void ult()
    {
        int result = 0;
        while (AllEnemy.Count > 0)
        {
            Destroy(AllEnemy[0]);
            AllEnemy.RemoveAt(0);
            EnemyCount--;
            result++;
        }
        ui.Hit(result);
        ui.HpMpChange();
    }
}
