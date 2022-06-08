using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

public enum LevelType{
    money = 1,

}

[CreateAssetMenu(fileName = "LevelContainer", menuName = "ScriptableObjects/LevelContainer", order = 0)]
public class LevelContainer : ScriptableObject
{
    [Expandable]
    public List<LevelSO> levels = new List<LevelSO>();


    public int CurrentLevel{
        get{
            return PlayerPrefs.GetInt("Level");
        }
        set{
            if(value >= levels.Count ){
                value = 0;
                Loop++;
            }
            Debug.Log(value);
            PlayerPrefs.SetInt("Level", value);
        }
    }

    public int Loop
    {
        get{
            return PlayerPrefs.GetInt("Loop");
        }
        set{
            PlayerPrefs.SetInt("Loop", value);
        }
    }

    public int LevelDisplay
    {
        get{
            return (Loop)*levels.Count + (CurrentLevel+1); 
        }
    }
    public LevelSO CurrentLevelSO{
        get{
            return levels[CurrentLevel];
        }
    }

    public void StartLevel(){
        SceneManager.LoadScene((int)CurrentLevelSO.levelType);
    }

    public void EndLevel(){
    }


}
