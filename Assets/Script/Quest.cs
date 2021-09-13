using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public TextMeshProUGUI TextContent;
    public TextMeshProUGUI TextReward;
    public TextMeshProUGUI TextAchievementRate;
    public TextMeshProUGUI BadgeClear;
    private int Achievement_Rate;
    private List<DataEnemyParam> EnemyList = new List<DataEnemyParam>();

    public void SetQuest(MasterQuestParam _param)
    {
        EnemyList = DataManager.Instance.dataenemy.list;
        DataEnemyParam param =
                DataManager.Instance.dataenemy.list.Find(p => p.Enemy_ID == _param.Target_ID);

        TextContent.text = $"{_param.Quest_Content}";

        if (_param.Reward_Type == 1)
        {
            MasterShieldParam shieldparam = 
                DataManager.Instance.mastershield.list.Find(p => p.Shield_ID == _param.Reward_ID);
            TextReward.text = $"報酬:{shieldparam.Shield_Name}のレシピ";
        }
        else
        {
            TextReward.text = "error";
        }

        if (_param.Target_ID == 100)
        {
            for (int i = 0; i < EnemyList.Count; i++)
            {
                Achievement_Rate += EnemyList[i].Kill_Count;
            }
        }
        else if (param != null) 
        {           
            Achievement_Rate = param.Kill_Count;
        }
        TextAchievementRate.text = $"{Achievement_Rate}/{_param.Goal}";

        
    }
}
