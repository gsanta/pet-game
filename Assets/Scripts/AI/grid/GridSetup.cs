﻿using UnityEngine;

namespace AI.grid
{
    public class GridSetup : MonoBehaviour
    {

        [SerializeField] public float cellSize;
        [SerializeField] public bool isShowDebug;
        [SerializeField] public Transform bottomRight;
        [SerializeField] public Transform topLeft;
    }
}
