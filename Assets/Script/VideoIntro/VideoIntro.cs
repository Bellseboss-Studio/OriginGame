using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoIntro : MonoBehaviour
{
    [SerializeField] private VideoPlayer video;

    private void Start()
    {
        StartCoroutine(NextScene());
    }

    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds((float)video.clip.length);
        SkipVideo();
    }

    public  void SkipVideo()
    {
        SceneManager.LoadScene(1);
    }
}
