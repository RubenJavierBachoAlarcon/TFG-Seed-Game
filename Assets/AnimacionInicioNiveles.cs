using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AnimacionInicioNiveles : MonoBehaviour
{
    private PlayableDirector director;
    public PlayerMovement playerMovement;
    public Animator animator;
    public PauseMenu pauseMenu;
    public static bool isPlaying = false;

    void Start()
    {
        isPlaying = true;
        pauseMenu.pauseTimer();
        playerMovement.enabled = false;
        director = GetComponent<PlayableDirector>();
        director.stopped += OnPlayableDirectorStopped;
    }


    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            isPlaying = false;
            playerMovement.enabled = true;
            animator.Play("Quieto");
            pauseMenu.resumeTimer();
        }
    }

}
