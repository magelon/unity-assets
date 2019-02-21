using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Quest quest = new Quest();
    public GameObject questPrintBox;
    public GameObject buttonPrefab;
    void Start()
    {
        //creat each event
        QuestEvent a = quest.AddQuestEvent("test1", "desc1");
        QuestEvent b = quest.AddQuestEvent("test2", "desc2");
        QuestEvent c = quest.AddQuestEvent("test3", "desc3");
        QuestEvent d = quest.AddQuestEvent("test4", "desc4");
        QuestEvent e = quest.AddQuestEvent("test5", "desc5");

        //define the paths between the events
        quest.AddPath(a.GetId(), b.GetId());
        quest.AddPath(b.GetId(), c.GetId());
        quest.AddPath(b.GetId(), d.GetId());
        quest.AddPath(c.GetId(), e.GetId());
        quest.AddPath(d.GetId(), e.GetId());

        quest.BFS(a.GetId());
        QuestButton button = CreatButton(a).GetComponent<QuestButton>();
        button = CreatButton(b).GetComponent<QuestButton>();
        button = CreatButton(c).GetComponent<QuestButton>();
        button = CreatButton(d).GetComponent<QuestButton>();
        button = CreatButton(e).GetComponent<QuestButton>();
        quest.PrintPath();
    }
    GameObject CreatButton(QuestEvent e)
    {
        GameObject b = Instantiate(buttonPrefab);
        b.GetComponent<QuestButton>().Setup(e, questPrintBox);
        if (e.order == 1)
        {
            b.GetComponent<QuestButton>().UpadteButton(QuestEvent.EventStatus
            .CURRENT);
            e.status = QuestEvent.EventStatus.CURRENT;
        }return b;
    }
    public void UpdateQuestsOnCompletion(QuestEvent e)
    {
        foreach(QuestEvent n in quest.questEvents)
        {
            if (n.order == (e.order + 1))
            {
                n.UpdateQuestEvent(QuestEvent.EventStatus.CURRENT);
            }
        }
    }
}
