using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowQuestList : MonoBehaviour
{
    public Transform areaQuest;
    private List<MasterQuestParam> QuestList = new List<MasterQuestParam>();

    private void OnEnable()
    {
        QuestList = DataManager.Instance.masterquest.list;
        for(int i = 0; i < QuestList.Count; i++)
        {
            GameObject Quest = Instantiate(PrefabHolder.Instance.Quest) as GameObject;
            Quest.transform.SetParent(areaQuest);
            Quest.GetComponent<Quest>().SetQuest(QuestList[i]);
        }
    }
}
