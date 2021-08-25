using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class MasterQuestParam : CsvDataParam
{
    public int Mission_ID;
    public string Mission_Text;
    public int Goal;
    public int Reward_ID;
}

public class MasterQuest : CsvData<MasterQuestParam>
{

}
