using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyUIController : MonoBehaviour
{
    public TextMeshProUGUI EnemyLayerGUI;
    public SpriteRenderer ThisEnemyRenderer;

    // Update is called once per frame
    void Update()
    {
        EnemyLayerGUI.text = ThisEnemyRenderer.sortingOrder.ToString();
    }
}
