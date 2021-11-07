using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyUIController : MonoBehaviour
{
    public TextMeshProUGUI EnemyLayerGUI;
    public Ocus OcusScript;

    // Update is called once per frame
    void Update()
    {
        EnemyLayerGUI.text = OcusScript.ThisEnemy.GetComponent<SpriteRenderer>().sortingOrder.ToString();
    }
}
