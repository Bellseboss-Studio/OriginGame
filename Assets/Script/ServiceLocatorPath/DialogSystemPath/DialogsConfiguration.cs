using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bellseboss/Dialog/Configuration")]
public class DialogsConfiguration : ScriptableObject
{
    [SerializeField] private Dialog[] dialogs;
    private Dictionary<string, Dialog> idDialogs;

    private void Awake()
    {
        idDialogs = new Dictionary<string, Dialog>(dialogs.Length);
        foreach (var dialog in dialogs)
        {
            idDialogs.Add(dialog.Id, dialog);
        }
    }

    public Dialog GetDialogPrefabById(string id)
    {
        if (!idDialogs.TryGetValue(id, out var dialog))
        {
            throw new Exception($"Dialog with id {id} does not exit");
        }
        return dialog;
    }
}