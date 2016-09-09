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

    public static void OnStep(){
        for (int i = 0; i < players.Count; i++) {
            playerAnimators[i].SetBool("OnStep", true);
        }
    }
    
    public static void OnDirectionChange(Vector3 direction) {
    }
    public static void OnFall() {
    }
    public static void OnHop(){
    }
    public static void OnLand() {
    }
}
