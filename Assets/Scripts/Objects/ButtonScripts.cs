using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Contains all button onclick funcs
public class ButtonScripts : MonoBehaviour
{
    //start game
    public void PlayButton()
    {
        //UIManager.Inst.DisplayCanvas(1);
        //AudioManager.Inst.PlaySound(1);
    }

    //reloads scene
    public void PlayAgainButton()
    {
        //GameManager.Inst.SetCurrentState(GameManager.GameState.YetToBegin);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
