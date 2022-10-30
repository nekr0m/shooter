using UnityEngine;

/// <summary>
/// the main values are placed in a separate code as constants
/// </summary>
[CreateAssetMenu(fileName = "Stats")]
public class Stats : ScriptableObject
{
    public const int MaxHp = 100;
    public const int MaxEnergy = 100;
    public const int KillHeal = 50;
    public const int StartEnergy = 50;
    public const int EnergyBlue = 50;
    public const int EnergyRed = 15;
    public const int Demage = 15;
    public const int MpDemage = 25;
    public const int ShootTimerLimit = 2;

    public const int RedHp = 50;
    public const int BlueHp = 100;
    public const int BotShootTimerLimit = 3;
    public const float UpTimer = 1;
    public const float SleepTimer = 1;

    public const int SpawnMaxTimer = 5;
    public const int SpawnMinTimer = 2;
    public const int BossLimit = 5;
    public const float TimerDeley = 0.5f;

    public const int CritInstant = 10;

    public const float MoveStep = 0.3f;
    public const float MoveRotate = 3;
}
