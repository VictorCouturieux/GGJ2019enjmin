using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    public WorldState CurrentWorldState { get; set; }
    public Camera otherCamera;
    public GameObject cylinder;

    public enum WorldState
    {
        Normal,
        Knight,
        Astronaut
    };

    [SerializeField] private GameObject stickPrefab = null;
    [SerializeField] private GameObject aquariumPrefab = null;
    [SerializeField] private GameObject normalPostProcessing = null;
    [SerializeField] private GameObject normalPostProcessing_Knight = null;

    private MusicManager musicManager = null;
    private Player player = null;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    private void Start()
    {
        CurrentWorldState = WorldState.Normal;

        musicManager = FindObjectOfType<MusicManager>();
        Assert.IsNotNull(musicManager);

        player = FindObjectOfType<Player>();
        Assert.IsNotNull(player);

        playerMovement = FindObjectOfType<PlayerMovement>();
        Assert.IsNotNull(playerMovement);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void ChangeWorldState(WorldState nextWorldState)
    {
        // TODO: Implement switching world state.
        if (nextWorldState == WorldState.Normal)
        {
            DropItem();
            otherCamera.cullingMask = (1 << LayerMask.NameToLayer("Ignore Raycast"))
                                      | (1 << LayerMask.NameToLayer("Default"));
            cylinder.SetActive(false);

            normalPostProcessing_Knight.SetActive(false);
            normalPostProcessing.SetActive(true);

            CurrentWorldState = WorldState.Normal;

//            PlayerMovement.anim.runtimeAnimatorController = player.GetComponent<PlayerMovement>().NormalAnimControl;
            
//            playerMovement.animatorOverrideController[playerMovement.idleNormal] = playerMovement.idleKnight;
//            playerMovement.animatorOverrideController[playerMovement.runNormal] = playerMovement.runKnight;
            playerMovement.animatorOverrideController[playerMovement.idleNormal] = playerMovement.idleNormal;
            playerMovement.animatorOverrideController[playerMovement.runNormal] = playerMovement.runNormal;

//            playerMovement.animatorOverrideController.
            
        }
        else if (nextWorldState == WorldState.Knight)
        {
            otherCamera.cullingMask = (1 << LayerMask.NameToLayer("Ignore Raycast"))
                                      | (1 << LayerMask.NameToLayer("KnightWorld"));
            cylinder.SetActive(true);
            normalPostProcessing_Knight.SetActive(true);
            normalPostProcessing.SetActive(false);

            CurrentWorldState = WorldState.Knight;

//            PlayerMovement.anim.runtimeAnimatorController = player.GetComponent<PlayerMovement>().KnightAnimControl;
            playerMovement.animatorOverrideController[playerMovement.idleNormal] = playerMovement.idleKnight;
            playerMovement.animatorOverrideController[playerMovement.runNormal] = playerMovement.runKnight;
            
            
            
//            playerMovement.animatorOverrideController[playerMovement.idleKnight] = playerMovement.idleNormal;
//            playerMovement.animatorOverrideController[playerMovement.runKnight] = playerMovement.runNormal;
        }

        musicManager.ChangeMusic(nextWorldState);
        //else if (nextWorldState == WorldState.Astronaut)
        //{
        //    otherCamera.cullingMask = (1 << LayerMask.NameToLayer("Ignore Raycast"))
        //                              | (1 << LayerMask.NameToLayer("KnightWorld"));
        //    cylinder.SetActive(true);
        //    CurrentWorldState = WorldState.Astronaut;

        //    PlayerMovement.anim.runtimeAnimatorController = player.GetComponent<PlayerMovement>().NormalAnimControl;
        //}

        //        Debug.Log($"Changed active WorldState to {CurrentWorldState}");
    }

    private void DropItem()
    {
        if (CurrentWorldState == WorldState.Knight)
        {
            Instantiate(stickPrefab, player.transform.position, Quaternion.identity);
        }

        if (CurrentWorldState == WorldState.Astronaut)
        {
            Instantiate(aquariumPrefab, player.transform.position, Quaternion.identity);
        }
    }
}