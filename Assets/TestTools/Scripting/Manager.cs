using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager {
    public struct PlayerSettings {
        public int ID;
        public Material teamMat;
    }
    public static int _playerCount; 
    public static string _playerTagID = _playerCount.ToString();
    public static List<player> players = new List<player>();
    public static List<PlayerSettings> playerSettings = new List<PlayerSettings>();
}
