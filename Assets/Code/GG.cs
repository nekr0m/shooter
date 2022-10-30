using UnityEngine;

public class GG : MonoBehaviour
{
    [SerializeField] GG gg;
    [SerializeField] UI ui;
    [SerializeField] Spawners Spa;
    [SerializeField] GameObject Shoot;
    [SerializeField] Transform GGpos;
    [SerializeField] Transform ShootPoint;
    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;
    public int Hp { get; private set; }
    public int Energy { get; private set; }
    public float ShootTimer { get; private set; }
    bool CanShoot;
    void Awake()
    {
        Shoot.GetComponent<Bullet>().Scripts(gg, Spa);
        Hp = Stats.MaxHp;
        Energy = Stats.StartEnergy;
        ShootTimer = Stats.ShootTimerLimit;
        CanShoot = true;
    }
    void Update()
    {
        if (ShootTimer < Stats.ShootTimerLimit)
        {
            ShootTimer += Time.deltaTime;
            if (ShootTimer >= Stats.ShootTimerLimit) CanShoot = true;
        }
    }
    void OnCollisionEnter(Collision body)
    {
        if (body.gameObject.tag == "enemy")
        {
            Hp -= Stats.Demage;
            ui.HpMpChange();
            Spa.Dead(body.gameObject);
            Destroy(body.gameObject);
            if (Hp < 1) ui.Dead();
        }
        if (body.gameObject.tag == "wall")
        {
            Spa.WallShoot();
            float x = Random.Range(pos1.position.x, pos2.position.x);
            float z = Random.Range(pos1.position.z, pos2.position.z);
            GGpos.position = new Vector3(x, GGpos.position.y, z);
        }
    }
    void OnTriggerEnter(Collider body)
    {
        if (body.tag == "bullet2")
        {
            Energy -= Stats.MpDemage;
            if (Energy < 0) Energy = 0;
            ui.HpMpChange();
            Bullet2 bu = body.GetComponent<Bullet2>();
            Spa.Bullets(bu, false);
            Destroy(body.gameObject);
        }
    }
    /// <summary>
    /// hitting the target: HP/MP recovery, value change, creep/boss definitions, ricochet/or not
    /// </summary>
    /// <param name="a">ricochet/or not</param>
    /// <param name="b">creep/boss</param>
    public void HitEnemy(bool a, bool b)
    {
        ui.Hit(1);
        if (b) Energy += Stats.EnergyBlue;
        else Energy += Stats.EnergyRed;
        if (a)
        {
            int HpOrMp = Random.Range(0, 2);
            int MpBust = Random.Range(5, 21);
            if (HpOrMp > 0) Hp += Stats.KillHeal;
            else Energy += MpBust;
        }
        if (Hp > Stats.MaxHp) Hp = Stats.MaxHp;
        if (Energy > Stats.MaxEnergy) Energy = Stats.MaxEnergy;
        ui.HpMpChange();
    }
    /// <summary>
    /// a shot if the bool allows, the parameters of the vector are indicated, and not the transform during creation, so that the trajectory of the bullet does not change when moving and turning
    /// </summary>
    public void ShootBullet()
    {
        if (CanShoot)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.forward, out hit);
            GameObject bullet = Instantiate(Shoot, new Vector3(ShootPoint.position.x, ShootPoint.position.y, ShootPoint.position.z), GGpos.rotation);
            bullet.GetComponent<Bullet>().Line(hit.point);
            ShootTimer = 0;
            CanShoot = false;
        }
    }
    public void ult()
    {
        if (Energy == Stats.MaxEnergy)
        {
            Energy -= Stats.MaxEnergy;
            Spa.ult();
        }
    }
}
