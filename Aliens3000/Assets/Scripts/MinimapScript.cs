using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
   [SerializeField] private Transform _player;
    void Start()
    {
        
    }

    void LateUpdate()
    {
        Vector3 mewPosition = _player.position;
        mewPosition.y = transform.position.y;
        transform.position = mewPosition;
    }
}


