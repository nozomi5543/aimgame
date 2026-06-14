using UnityEngine;

public class AimScoreBoardUI : MonoBehaviour
{
    [Header("Displays")]
    [SerializeField]
    private AimDigitDisplay scoreDisplay;

    [SerializeField]
    private AimDigitDisplay timeDisplay;

    [Header("Initial Values")]
    [SerializeField]
    private int initialScore = 0;

    [SerializeField]
    private int initialTime = 0;

    void Awake()
    {
        // MiniScript ‘¤‚©‚çŒÄ‚Î‚ê‚éƒCƒxƒ“ƒg“o˜^
        AimEventDispatcher.Subscribe("updateScore", OnUpdateScore);
        AimEventDispatcher.Subscribe("updateTime", OnUpdateTime);
    }

    void OnDestroy()
    {
        AimEventDispatcher.Unsubscribe("updateScore", OnUpdateScore);
        AimEventDispatcher.Unsubscribe("updateTime", OnUpdateTime);
    }

    void Start()
    {
        OnUpdateScore(new object[] { initialScore });
        OnUpdateTime(new object[] { initialTime }); 
    }

    private void OnUpdateScore(object[] args)
    {
        if (args.Length > 0 && args[0] is int score)
        {
            scoreDisplay.SetNumber(score);
        }
    }

    private void OnUpdateTime(object[] args)
    {
        if (args.Length > 0 && args[0] is int time)
        {
            timeDisplay.SetNumber(time);
        }
    }
}