using SystemOfExtras;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class MessageWithButtons : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI title, message;
    [SerializeField] protected Button buttonCancel;
    protected ILoadScenePrivate _service;

    public void Configure(ILoadScenePrivate service)
    {
        _service = service;
    }
 
    protected void FinishedToShow()
    {
        FinishToShow();
        buttonCancel.onClick.RemoveAllListeners();
        _service.CloseMessages();
        ServiceLocator.Instance.GetService<ILoadScene>().Unlock();
    }

    public abstract void FinishToShow();
}