using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] Bullet2 bu;
    public Transform Target;
    public Spawners Spa;
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        if (transform.position == Target.transform.position)
        {
            Spa.Bullets(bu, false);
            Destroy(gameObject);
        }
    }
    public void Scripts(Transform a, Spawners b)
    {
        Target = a;
        Spa = b;
    }
}
