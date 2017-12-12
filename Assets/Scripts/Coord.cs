using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Coord{
    public int x;
    public int y;

    public Coord(int x, int y) {
        this.x = x;
        this.y = y; 
    }

    //Сделать конструктор без парамтеров нельзя в структуре
    //public Coord(): this(0, 0) { }
}
