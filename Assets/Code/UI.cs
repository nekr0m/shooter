using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// interface script with menu functionality and pause (time scale)
/// </summary>
public class UI : MonoBehaviour
{
    [SerializeField] GG gg;
    [SerializeField] Image GGhpImage;
    [SerializeField] Image GGmpImage;
    [SerializeField] Image ShootTimer;
    [SerializeField] Text GGhpText;
    [SerializeField] Text GGmpText;
    [SerializeField] Text ScoreText;
    [SerializeField] Text MenuScoreText;
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject CloseMenu;
    [SerializeField] GameObject Lose;
    int Score;
    /// <summary>
    /// return the time scale (unfreezes the time during restarts)
    /// </summary>
    void Awake()
    {
        Pause(false);
    }
    /// <summary>
    /// filling the shot gauge, limited by the maximum value (additional option without using methods)
    /// </summary>
    void Update()
    {
        if (gg.ShootTimer < Stats.ShootTimerLimit) ShootTimer.fillAmount = gg.ShootTimer / (float)Stats.ShootTimerLimit;
    }
    void Start()
    {
        ScoreText.text = $"{Score}";
        MenuScoreText.text = $"{Score}";
        HpMpChange();
    }
    public void Pause(bool a)
    {
        if (a) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
    /// <summary>
     /// changing points on the screen and in the pause menu
     /// </summary>
     /// <param name="a">number of people killed (shot / all from ult)</param>
    public void Hit(int a)
    {
        Score += a;
        ScoreText.text = $"{Score}";
        MenuScoreText.text = $"{Score}";
    }
    /// <summary>
    /// changing health and magic bars when calling a method
    /// </summary>
    public void HpMpChange()
    {
        GGhpText.text = $"{gg.Hp}/{Stats.MaxHp}";
        GGmpText.text = $"{gg.Energy}/{Stats.MaxEnergy}";
        GGhpImage.fillAmount = (float)gg.Hp / (float)Stats.MaxHp;
        GGmpImage.fillAmount = (float)gg.Energy / (float)Stats.MaxEnergy;
    }
    /// <summary>
    /// in case of defeat, disable the RETURN button, turn on the message about the defeat
    /// </summary>
    public void Dead()
    {
        Pause(true);
        CloseMenu.SetActive(false);
        Lose.SetActive(true);
        Menu.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
