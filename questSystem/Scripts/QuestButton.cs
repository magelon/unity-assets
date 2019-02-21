using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    public Button buttonComponent;
    public RawImage icon;
    public Text eventName;
    public Sprite curretImage;
    public Sprite waitingImage;
    public Sprite doneImage;
    public QuestEvent thisEvent;
    QuestEvent.EventStatus status;

    public void Setup(QuestEvent e,GameObject scrollList)
    {
        thisEvent = e;
        buttonComponent.transform.SetParent(scrollList.transform, false);
        eventName.text = "<b>" + thisEvent.name + "</b>\n" + thisEvent.description;
        status = thisEvent.status;
        icon.texture = waitingImage.texture;
        buttonComponent.interactable = false;
    }

    public void UpadteButton(QuestEvent.EventStatus s)
    {
        status = s;
        if (status == QuestEvent.EventStatus.DONE)
        {
            icon.texture = doneImage.texture;
            buttonComponent.interactable = false;
        }else if (status==QuestEvent.EventStatus.WAITING){
            icon.texture = waitingImage.texture;
            buttonComponent.interactable = false;
        }
        else if(status == QuestEvent.EventStatus.CURRENT)
        {
            icon.texture = curretImage.texture;
            buttonComponent.interactable = true;
        }
    }
}
