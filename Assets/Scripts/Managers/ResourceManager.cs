using UnityEngine;

public class ResourceManager : Singelton<ResourceManager>
{
    [Header("General Prefabs")]
    public GameObject PlayerPrefab;

    [Header("Unit Prefabs")]
    public GameObject SCVPrefab;
    public GameObject MarinePrefab;

    [Header("Building Prefabs")]
    public GameObject CommandCenterPrefab;

    [Header("UI Prefabs")]
    public GameObject UIButtonPrefab;
    public GameObject SelectedIconPrefab;
}
