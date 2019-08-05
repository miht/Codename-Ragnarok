using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject _prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D)) {
            Vector3 spawnPos = transform.position;
            spawnPos.y = 0f;
            Instantiate(_prefab, spawnPos, Quaternion.identity);
        }
    }
}
