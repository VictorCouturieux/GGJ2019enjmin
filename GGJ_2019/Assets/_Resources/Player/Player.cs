using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using Random = System.Random;

public class Player : MonoBehaviour
{
    private GameManager gameManager = null;

    [SerializeField] private AudioClip[] listSwordSong;
    [SerializeField] private AudioClip[] listPickUpSong;

    private AudioSource audioSource;

    // Called before the first frame update.
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Assert.IsNotNull(gameManager);

        audioSource = GetComponent<AudioSource>();
    }

    // Called once per frame.
    private void Update()
    {
    }

    #region Public Methods
    public void Interact(Vector2 lookDirection)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDirection);

        if (hit)
        {
            GetComponent<PlayerMovement>().SetIsPickUp(hit);
        }

        if (gameManager.CurrentWorldState == GameManager.WorldState.Knight)
        {
            // Drops the stick.
            gameManager.ChangeWorldState(GameManager.WorldState.Normal);
        }

        if (gameManager.CurrentWorldState == GameManager.WorldState.Astronaut)
        {
            // Drops the stick.
            gameManager.ChangeWorldState(GameManager.WorldState.Normal);
        }
        // TODO: else drop
    }

    public void PickUp(RaycastHit2D hit)
    {
        // Picking up stick.
        if (hit.collider.gameObject.GetComponent<Stick>())
        {
            Destroy(hit.collider.gameObject);
            gameManager.ChangeWorldState(GameManager.WorldState.Knight);
        }

        // Picking up aquarium.
        if (hit.collider.gameObject.GetComponent<Aquarium>())
        {
            Destroy(hit.collider.gameObject);
            gameManager.ChangeWorldState(GameManager.WorldState.Astronaut);
        }
    }

    public void PickUpSong()
    {
        if (GetComponent<PlayerMovement>().isPickUp)
        {
            int randomSong = new Random().Next(0, listPickUpSong.Length);
            audioSource.clip = listPickUpSong[randomSong];
            audioSource.Play();
        }
    }

    public void AttackSong()
    {
        int randomSong = new Random().Next(0, listSwordSong.Length);
        audioSource.clip = listSwordSong[randomSong];
        audioSource.Play();
    }

    public void SpecialAction()
    {
        // TODO: Check for active world state.
        if (gameManager.CurrentWorldState != GameManager.WorldState.Normal)
        {
            GetComponent<PlayerMovement>().SetIsAttacking();
        }
    }
    #endregion
}