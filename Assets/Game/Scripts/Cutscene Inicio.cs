using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneInicio : MonoBehaviour
{
    public PlayableDirector director;
    [SerializeField] public GameObject timeline;

    void Start()
    {
        director.Play();
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            timeline.SetActive(false);
        }
    }

    void OnDestroy()
    {
        // Es importante desuscribirse de los eventos cuando ya no son necesarios para evitar fugas de memoria
        director.stopped -= OnPlayableDirectorStopped;
    }
}