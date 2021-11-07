using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorToggleScript : MonoBehaviour
{
    [Header("Set Components")]
    public GameObject OutsideToggleGrid;
    public GameObject InsideTiles;

    private bool _isAtEntrance = false;

    void Update()
    {
        if (_isAtEntrance && Input.GetKeyDown(KeyCode.E))
        {
            OutsideToggleGrid.SetActive(!OutsideToggleGrid.activeSelf);
            InsideTiles.SetActive(!InsideTiles.activeSelf);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
            _isAtEntrance = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        _isAtEntrance = false;
    }
}
