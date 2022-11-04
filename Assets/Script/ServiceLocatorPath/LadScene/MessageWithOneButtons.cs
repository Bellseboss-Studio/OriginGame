using System;
using SystemOfExtras;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageWithOneButtons : MessageWithButtons
{
    [SerializeField] private TextMeshProUGUI textButtonOne;
    [SerializeField] private Button buttonOne;

    public void Show(string title, string message, string titlebuttonone, Action actionButtonOne, Action actionToCancel)
    {
        ServiceLocator.Instance.GetService<ILoadScene>().Lock();
        this.title.text = title;
        this.message.text = message;
        textButtonOne.text = titlebuttonone;
        buttonOne.onClick.AddListener(() =>
        {
            actionButtonOne?.Invoke();
            FinishedToShow();
        });
        buttonCancel.onClick.AddListener(() =>
        {
            actionToCancel?.Invoke();
            FinishedToShow();
        });
    }

    public override void FinishToShow()
    {
        buttonOne.onClick.RemoveAllListeners();
    }
}