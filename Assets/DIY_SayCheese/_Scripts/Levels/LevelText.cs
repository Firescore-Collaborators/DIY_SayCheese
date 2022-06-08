using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    private void Start() {

        GetComponent<Text>().text = "Level " + (GameManager.Instance.levels.LevelDisplay);
    }
}
