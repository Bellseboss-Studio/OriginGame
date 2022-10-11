using System.Collections;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    [SerializeField] private float m_delayTime = 0.5f;
    public void OnQuitGameClicked()
    {
        StartCoroutine(nameof(TerminateGameSequence));
    }

    IEnumerator TerminateGameSequence()
    {
        yield return new WaitForSeconds(m_delayTime);
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
