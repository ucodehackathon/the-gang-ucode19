using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DataSetRow
{
    public DateTime time;
    public int tag_id;

    public float x_pos;
    public float y_pos;

    public float heading; //radians
    public float direction; //radians

    public float energy;
    public float speed;

    public float total_distance;

    public DataSetRow(DateTime date, int tag, float x_pos, float y_pos, float heading, float direction, float energy, float speed, float total_distance)
    {
        this.time = date;
        this.tag_id = tag;
        this.x_pos = x_pos;
        this.y_pos = y_pos;
        this.heading = heading;
        this.direction = direction;
        this.energy = energy;
        this.speed = speed;
        this.total_distance = total_distance;
    }

    public Vector3 GetPos()
    {
        return new Vector3(x_pos, 0, y_pos);
    }
}
