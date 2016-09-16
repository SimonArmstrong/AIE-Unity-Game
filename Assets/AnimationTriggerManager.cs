using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationTriggerManager : MonoBehaviour {
    public static List<GameObject> players = new List<GameObject>();
    public static List<Animator> playerAnimators = new List<Animator>();
    void Start() {
        players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        players.Reverse();
        for (int i = 0; i < players.Count; i++) {
            playerAnimators.Add(players[i].GetComponent<Animator>());
        }
    }
    // Called when the player moves
    public static void OnStep(){
        for (int i = 0; i < players.Count; i++) {
            playerAnimators[i].SetBool("OnStep", true);
        }
    }
    // Called when the player changes direction
    public static void OnDirectionChange(Vector3 direction) {
    }
    // Called when the player is off the ground
    public static void OnFall() {
    }
    // Called when the player performs a hop
    public static void OnHop(){
    }
    // Called when the player first hits the ground
    public static void OnLand() {
    }
}
