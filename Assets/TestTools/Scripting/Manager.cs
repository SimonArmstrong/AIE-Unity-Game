using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager {
    public struct PlayerSettings {
        public int ID;
        public Material teamMat;
        public PlayerSettings(int id, Material mat) { this.ID = id;  this.teamMat = mat; }
    }

    // Players get added to this when players join the game
    public static List<player> players = new List<player>();
    // Create instance of PlayerSettings in menus and add it to this array
    public static List<PlayerSettings> playerSettings = new List<PlayerSettings>();
}