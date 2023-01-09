using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject InGameRoot;
    public TextMeshProUGUI LilSkeletonText;
    public TextMeshProUGUI MidSkeletonText;
    public TextMeshProUGUI HugeSkeletonText;
    public TextMeshProUGUI SoulsText;
    public TextMeshProUGUI TimerText;
    private void Update()
    {
        LilSkeletonText.text = AIManager.s_Instance.lilSkeletons.Count + "";
        MidSkeletonText.text = AIManager.s_Instance.midSkeletons.Count + "";
        HugeSkeletonText.text = AIManager.s_Instance.hugeSkeletons.Count + "";
        TimerText.text = System.TimeSpan.FromSeconds(GameManager.s_Instance.time).ToString(@"mm\:ss");
        SoulsText.text = GameManager.s_Instance.souls + "";
    }
}
