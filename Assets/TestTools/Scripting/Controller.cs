using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Controller {
    public static int id = 0;

    public Controller() {
        Controller.id += 1;
    }

    public static Controller ControllerFromId(int id) {
        Controller controller = new Controller();

        return controller;
    }
}
