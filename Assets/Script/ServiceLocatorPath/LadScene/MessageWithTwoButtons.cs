using System;
using SystemOfExtras;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageWithTwoButtons : MessageWithButtons
{
    [SerializeField] private TextMeshProUGUI textButtonOne, textButtonTwo;
    [SerializeField] private Button buttonOne, buttonTwo;
    public void Show(string title, string message, string titlebuttonone, Action actionButtonOne,
        string titlebuttontwo, Action actionButtonTwo, Action actionToCancel)
    {
        ServiceLocator.Instance.GetService<ILoadScene>().Lock();
        this.title.text = title;
        this.message.text = message;
        textButtonOne.text = titlebuttonone;
        textButtonTwo.text = titlebuttontwo;
        buttonOne.onClick.AddListener(() =>
        {
            actionButtonOne?.Invoke();
            FinishedToShow();
        });
        buttonTwo.onClick.AddListener(() =>
        {
            actionButtonTwo?.Invoke();
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
        buttonTwo.onClick.RemoveAllListeners();
    }
}