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

    public static bool operator ==(Coord obj1, Coord obj2) {
        if (obj1.x == obj2.x && obj1.y == obj2.y)
            return true;
        return false;
    }
    
    public static bool operator !=(Coord obj1, Coord obj2) {
        if (obj1.x != obj2.x || obj1.y != obj2.y)
            return true;
        return false;
    }
}
