using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class QuestEvent 
{
   public enum EventStatus
    {
        WAITING,CURRENT,DONE
    };
    //waiting :not wake
    //current :wake
    //done
    public string name;
    public string description;
    public string id;
    public int order=-1;
    public EventStatus status;
    public QuestButton button;

    public List<QuestPath> pathlist = new List<QuestPath>();

    public QuestEvent(string n,string d)
    {
        id = Guid.NewGuid().ToString();
        name = n;
        description = d;
        status = EventStatus.WAITING;
    }

    public void UpdateQuestEvent(EventStatus es)
    {
        status = es;
        button.UpadteButton(es);
    }
    public string GetId()
    {
        return id;
    }

}
