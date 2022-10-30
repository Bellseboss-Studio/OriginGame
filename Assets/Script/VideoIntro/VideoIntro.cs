using System.Collections;
using SystemOfExtras;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoIntro : MonoBehaviour
{
    [SerializeField] private VideoPlayer video;

    private void Start()
    {
        video.prepareCompleted += source =>
        {
            StartCoroutine(NextScene(source));
        };
    }

    private IEnumerator NextScene(VideoPlayer videoPlayer)
    {
        yield return new WaitForSeconds((float)videoPlayer.length);
        SkipVideo();
    }

    public  void SkipVideo()
    {
        ServiceLocator.Instance.GetService<ILoadScene>().Close(() =>
        {
            SceneManager.LoadScene(1); 
        });
    }
}
