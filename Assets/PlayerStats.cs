using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int points;

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        Debug.Log("Actual points: " + points);
    }

    public void SetPoints(int points)
    {
        this.points = points;
        Debug.Log("Points set to: " + points);
    }
    public int GetPoints()
    {
        return points;
    }
}
