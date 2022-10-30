using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float Speed;
    public GG gg;
    public Spawners Spa;
    Vector3 Target;
    bool First;
    int Crit;
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target, Speed * Time.deltaTime);
        if (transform.position == Target) Destroy(gameObject);
    }
    /// <summary>
    /// when hitting an enemy, roll a chance to fly through or ricochet
    /// </summary>
    /// <param name="body"></param>
    void OnTriggerEnter(Collider body)
    {
        if (body.tag == "enemy")
        {
            gg.HitEnemy(First, body.GetComponent<Bot>().boss);
            if (!First)
            {
                Crit = Stats.CritInstant + Stats.MaxHp - gg.Hp;
                int Penetrate = Random.Range(1, 101);
                if (Crit > Penetrate)
                {
                    if (Spa.EnemyCount > 1)
                    {
                        int rand = Random.Range(0, 2);
                        if (rand > 0) Target = Spa.Ricochet(body.gameObject, gameObject.transform.position);
                    }
                    First = true;
                    Spa.Dead(body.gameObject);
                    Destroy(body.gameObject);
                }
                else Dead(body.gameObject);
            }
            else Dead(body.gameObject);
        }
        if (body.tag == "wall" || body.tag == "flor") Destroy(gameObject);
    }
    public void Scripts(GG a, Spawners b)
    {
        gg = a;
        Spa = b;
    }
    public void Line(Vector3 a)
    {
        Target = a;
    }
    void Dead(GameObject a)
    {
        Spa.Dead(a);
        Destroy(a);
        Destroy(gameObject);
    }
}