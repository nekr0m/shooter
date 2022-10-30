using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] BotHp BH;
    [SerializeField] GameObject Shoot;
    [SerializeField] bool BossOrNot;
    [SerializeField] int RedSpeed;
    [SerializeField] int BlueSpeed;
    public bool boss { get; private set; }
    Vector3 Offset;
    public Spawners Spa;
    public Transform GG;
    int Hp;
    int Speed;
    float ShootTimer;
    float RedUp;
    float RedSleep;
    /// <summary>
    /// defining creep/boss parameters by prefab boolean
    /// </summary>
    void Awake()
    {
        if (BossOrNot)
        {
            Hp = Stats.BlueHp;
            Speed = BlueSpeed;
            boss = true;
            BH.BothpText.text = $"{Hp}/{Stats.BlueHp}";
        }
        else
        {
            Hp = Stats.RedHp;
            Speed = RedSpeed;
            BH.BothpText.text = $"{Hp}/{Stats.RedHp}";
            RedUp = Stats.UpTimer;
            RedSleep = Stats.SleepTimer;
            Offset = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);
        }
    }
    /// <summary>
    /// checking and one-time timer for red to fly up, waiting + general movement and shooting for blue with a timer
    /// </summary>
    void Update()
    {
        if (RedUp > 0)
        {
            RedUp -= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, Offset, Time.deltaTime);
        }
        else
        {
            if (RedSleep > 0) RedSleep -= Time.deltaTime;
            else
            {
                if (BossOrNot)
                {
                    ShootTimer += Time.deltaTime;
                    if (ShootTimer >= Stats.BotShootTimerLimit)
                    {
                        ShootTimer = 0;
                        ShootBullet();
                    }
                }
                transform.position = Vector3.MoveTowards(transform.position, GG.position, Speed * Time.deltaTime);
            }
        }
        transform.rotation.SetLookRotation(GG.position);
    }
    public void Scripts(Transform a, Spawners b)
    {
        GG = a;
        Spa = b;
        Shoot.GetComponent<Bullet2>().Scripts(a, b);
    }
    void ShootBullet()
    {
        GameObject bullet = Instantiate(Shoot, transform.position, transform.rotation);
        Spa.Bullets(bullet.GetComponent<Bullet2>(), true);
    }
}
