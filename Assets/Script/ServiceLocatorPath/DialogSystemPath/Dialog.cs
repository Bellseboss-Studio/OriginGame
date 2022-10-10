using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Bellseboss/Dialog")]
public class Dialog : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private string option;
    [TextArea(2,5)][SerializeField] protected string dialogText;
    [SerializeField] private List<Dialog> listOfConcat;
    public string Id => id;
    public string DialogText
    {
        get
        {
            var result = dialogText;
            if (HasNextDialog)
            {
                for (int i = 0; i < listOfConcat.Count; i++)
                {
                    if (listOfConcat[i].option != "")
                    {
                        result += $"\n -{i+1} {listOfConcat[i].option}";
                    }   
                }
            }
            return result;
        }
    }

    public bool HasNextDialog => listOfConcat.Count >= 1;
    public bool HasNextDialogOption
    {
        get
        {
            if (listOfConcat.Count > 1)
            {
                return false;
            }
            else
            {
                return listOfConcat[0].option == "";
            }
            
        }
    }

    public string GetNextDialog(int keyPress)
    {
        return listOfConcat[keyPress - 1].id;
    }

    public void Concat(Dialog dialog)
    {
        listOfConcat.Add(dialog);
    }
}