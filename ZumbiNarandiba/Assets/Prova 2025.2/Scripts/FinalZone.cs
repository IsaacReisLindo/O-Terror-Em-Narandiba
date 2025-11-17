using UnityEngine;

public class FinalZone : MonoBehaviour
{
    public int killsNecessarios = 20; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        int kills = GameManager.instance.killCount;
        Debug.Log("Kills atuais: " + kills);

        if (kills >= killsNecessarios)
        {
            EndGame();
        }
        else
        {
            Debug.Log("Ainda faltam kills: " + kills + "/" + killsNecessarios);
        }
    }

    void EndGame()
    {
        GameManager.instance.GameWon();
        Time.timeScale = 0;
        
       
    }
}
