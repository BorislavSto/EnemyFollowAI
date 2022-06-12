using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

    [RequireComponent(typeof(AudioSource))]
public class FirstJumpscare : MonoBehaviour
{
    public PlayableDirector timeline;
    public GameObject canvas;
    public GameObject canvas2;

    public Animator anim;

    AudioSource audioData;

    public GameObject blood;

    public GameObject enemy;

    public GameObject ThePlayer;


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            blood.SetActive(true);
            anim.Play("DeathAnim", 0, 0.0f);
            Cursor.lockState = CursorLockMode.None;
            ThePlayer.GetComponent<FirstPersonController>().enabled = false;
            StartCoroutine(ScenePlayer());
            audioData = GetComponent<AudioSource>();
            audioData.Play(0);
        }
    }
    
    IEnumerator ScenePlayer()
    {
        enemy.GetComponent<NavMeshAgent>().agentTypeID = 2;
        
        canvas.GetComponent<Canvas>().enabled = true;
        canvas2.GetComponent<Canvas>().enabled = true;
        timeline.Play();
        yield return new WaitForSeconds(1f);
    }

}
