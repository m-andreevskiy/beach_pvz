using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    public ObjectCastleCard card;

    [SerializeField] private int castleType;
    [SerializeField] private GameObject prefab;


    public string GetBuildingConfigFile()
    {
        switch (castleType)
        {
            case 1:
                return "CastleConfigs/castle_1";

            case 2:
                return "CastleConfigs/castle_2";



            // Child
            case 80: 
                return "CastleConfigs/child_on_the_beach";

            default:
                return "CastleConfigs/castle_1";
        }
    }

    public GameObject GetPrefab()
    {
        return prefab;
    }
}
