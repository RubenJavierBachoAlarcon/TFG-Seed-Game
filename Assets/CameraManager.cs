using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras; // Asigna las cámaras que quieres desactivar aquí
    private PlayableDirector director; // Asigna el director de la timeline aquí
    public PauseMenu pauseMenu;
    public PlayerMovement playerMovement;
    public TutorialTrigger tutorialTrigger;

    private void Start()
    {
        director = GetComponent<PlayableDirector>();
        director.stopped += OnPlayableDirectorStopped;
        pauseMenu.pauseTimer();
        playerMovement.enabled = false;
        tutorialTrigger.enabled = false;
    }   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && director.state == PlayState.Playing)
        {
            director.time = director.duration;
            director.Evaluate();
            director.Stop();
        }
    }

    private void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            DisableCameras();
            pauseMenu.resumeTimer();
            playerMovement.enabled = true;
            tutorialTrigger.enabled = true;
            tutorialTrigger.startText();
        }
    }

    public void DisableCameras()
    {
        foreach (var camera in cameras)
        {
            if (camera != null)
            {
                camera.gameObject.SetActive(false);
            }
        }
    }
}
