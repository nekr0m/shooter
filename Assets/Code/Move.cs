using UnityEngine;

/// <summary>
/// I downloaded the controls from the asset store, I can track by the first wheelbarrow, and not by the object
/// </summary>
public class Move : MonoBehaviour
{
    [SerializeField] Transform body;
    [SerializeField] Joystick Joy1;
    [SerializeField] Joystick Joy2;
    Vector3 rotate;
    void Update()
    {
        if (Joy1.Horizontal >= 0.2f) body.position += new Vector3(Stats.MoveStep, 0, 0);
        if (Joy1.Horizontal <= -0.2f) body.position += new Vector3(-Stats.MoveStep, 0, 0);
        if (Joy1.Vertical >= 0.2f) body.position += new Vector3(0, 0, Stats.MoveStep);
        if (Joy1.Vertical <= -0.2f) body.position += new Vector3(0, 0, -Stats.MoveStep);

        if (Joy2.Horizontal >= 0.2f) rotate += new Vector3(0, Stats.MoveRotate, 0);
        if (Joy2.Horizontal <= -0.2f) rotate += new Vector3(0, -Stats.MoveRotate, 0);
        if (Joy2.Vertical >= 0.2f && rotate.x > -30) rotate += new Vector3(-Stats.MoveRotate, 0, 0);
        if (Joy2.Vertical <= -0.2f && rotate.x < 0) rotate += new Vector3(Stats.MoveRotate, 0, 0);

        body.eulerAngles = rotate;
    }
}