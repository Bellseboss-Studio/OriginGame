using System;
using System.Collections;
using SystemOfExtras;
using TMPro;
using UnityEngine;

namespace Game.VisorDeDialogosSystem
{
    public class DialogSystem : MonoBehaviour, IDialogSystem
    {
        [SerializeField] private Animator anim;
        [SerializeField] private DialogsConfiguration config;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private float secondsDelay;
        private DialogsFactory _factory;
        private Dialog _dialog;
        private IEnumerator fullTextInTextBox;
        private bool _isInUpdateFulledText;
        private bool _textIsFinishedOfShow;
        private Action<string> _ineractiveObjectAction;
        private Action<string> dialogFinish;


        private void Start()
        {
            _factory = new DialogsFactory(Instantiate(config, gameObject.transform));
        }

        public void OpenDialog()
        {
            anim.SetBool("open", true);
        }

        public void OpenDialog(string idDialog)
        {
            //Debug.Log($"is null {_dialog == null}");
            if (_textIsFinishedOfShow)
            {
                if (!_dialog.HasNextDialog)
                {
                    CloseDialog();
                    dialogFinish?.Invoke(idDialog);
                    return;
                }

                if (_dialog.HasNextDialogOption)
                {
                    SelectOption(1);
                } 
                return;
            }
            if (_dialog == null)
            {
                _dialog = _factory.Create(idDialog);   
            }
            if (_isInUpdateFulledText)
            {
                StopCoroutine(fullTextInTextBox);
                text.text = _dialog.DialogText;
                _isInUpdateFulledText = false;
                _textIsFinishedOfShow = true;
                return;
            }
            fullTextInTextBox = FullTextInTextBox(_dialog.DialogText);
            StartCoroutine(fullTextInTextBox);
            OpenDialog();
        }

        private void ApplyItemAction(string dialogId)
        {
            _ineractiveObjectAction?.Invoke(dialogId);
        }

        private IEnumerator FullTextInTextBox(string dialogDialogText)
        {
            ApplyItemAction(_dialog.Id);
            _textIsFinishedOfShow = false;
            _isInUpdateFulledText = true;
            for (int i = 0; i < dialogDialogText.Length; i++)
            {
                yield return new WaitForSeconds(secondsDelay);
                text.text = dialogDialogText.Substring(0, i + 1);
            }
            _isInUpdateFulledText = false;
            _textIsFinishedOfShow = true;
            //ApplyItemAction(_dialog.Id);
        }

        public void SelectOption(int keyPress)
        {
            if (_isInUpdateFulledText)
            {
                StopCoroutine(fullTextInTextBox);
                text.text = _dialog.DialogText;
                _isInUpdateFulledText = false;
                _textIsFinishedOfShow = true;
                ApplyItemAction(_dialog.Id);
                return;
            }
            Debug.Log($"keypress {keyPress}");
            if (!_dialog.HasNextDialog) return;
            _textIsFinishedOfShow = false;
            _isInUpdateFulledText = false;
            var nextDialog = _dialog.GetNextDialog(keyPress);
            _dialog = null;
            OpenDialog(nextDialog);
        }

        public void CloseDialog()
        {
            _dialog = null;
            _textIsFinishedOfShow = false;
            _isInUpdateFulledText = false;
            anim.SetBool("open", false);
        }

        public void OnDialogAction(Action<string> action)
        {
            _ineractiveObjectAction = action;
        }

        public void OnDialogFinish(Action<string> action)
        {
            dialogFinish = action;
        }

        public void SetDialogToNotSpaceToItems()
        {
            throw new NotImplementedException();
        }

        public Dialog GetCurrentDialog()
        {
            return _dialog;
        }
    }
}