using UnityEngine;

public class VisualUpgradeManager : MonoBehaviour
{
    [Header("Уровни зала")]
    public GameObject[] hallLevels;

    [Header("Уровни стойки")]
    public GameObject[] deskLevels;

    [Header("Уровни склада")]
    public GameObject[] rackLevels;

    private void Start()
    {
        SetHallLevel(0);
        SetDeskLevel(0);
        SetRackLevel(0);
    }

    public void SetHallLevel(int level)
    {
        SetLevelObjects(hallLevels, level);
    }

    public void SetDeskLevel(int level)
    {
        SetLevelObjects(deskLevels, level);
    }

    public void SetRackLevel(int level)
    {
        SetLevelObjects(rackLevels, level);
    }

    private void SetLevelObjects(GameObject[] objects, int level)
    {
        if (objects == null || objects.Length == 0)
            return;

        int safeLevel = Mathf.Clamp(level, 0, objects.Length - 1);

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null)
                objects[i].SetActive(i == safeLevel);
        }
    }
}