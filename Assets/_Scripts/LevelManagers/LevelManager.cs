using UnityEngine;
using System.Collections;

public abstract class LevelManager : MonoBehaviour {

    public State startState;
    public float waitingTime;

    public GameObject gameOver;
    public GUIText retry;
    public GUIText levelSelect;
    



	public virtual IEnumerator Start() {
        
        GameManager.instance.SetState(startState);
        yield return new WaitForSeconds(waitingTime);
		GameManager.instance.SetState(State.Running);
	}

    public virtual void GameOver(bool livesLeft)
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);
        if (livesLeft)
        {
            //levelSelect.enabled = false;
            levelSelect.text = "Level select";
            retry.enabled = true;         
        }
        else
        {
            //levelSelect.enabled = true;
            levelSelect.text = "Continue";
            retry.enabled = false;
        }

    }


    public abstract void CompleteLevel();
}
