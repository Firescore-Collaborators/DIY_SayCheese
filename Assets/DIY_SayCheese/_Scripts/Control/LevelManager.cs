using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Tabtale.TTPlugins;

public class LevelManager : MonoBehaviour
{

    [Expandable]
    public LevelContainer levelContainer;

    private void Awake() {
        TTPCore.Setup();
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        levelContainer.StartLevel();
    }

    public void NextLevel()
    {

    }
}
