using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // the index 0 for x and 1 for o
    private const int x = 0;
    private const int o = 1;
    private bool gameEnded;
    private int[] scores;
    public int currentPlayer;
    public int currentBoard;
    public GameObject[] boards;
    [SerializeField] private GameObject[] turnIcons;
    [SerializeField] private Text[] scoresText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text statText; // the text for the win or the draw
    [SerializeField] private GameObject[] playersIcon;


    private void GameSetup(){
        gameEnded = false;
        currentPlayer = 0;
        currentBoard = 4;
        turnIcons[x].SetActive(true);
        turnIcons[o].SetActive(false);
        scores[x] = 0;
        scores[o] = 0;
        scoresText[x].text = "Score: 0";
        scoresText[o].text = "Score: 0";
        UpdateBoards();
        
    }

    public void SetCurrentBoard(int placement){
        if (!boards[placement].GetComponent<BoardManager>().IsBoardFull()){
            currentBoard = placement;
        }
        else{
            currentBoard = -1;
        }
    }

    public void UpdateBoards(){
        if (currentBoard == -1){
            for (int i=0; i<boards.Length; i++){
                BoardManager boardScript = boards[i].GetComponent<BoardManager>();
                if (boardScript.IsBoardFull()){
                    boardScript.panel.SetActive(true);
                    boards[i].GetComponent<Image>().color = Color.yellow;
                }
                else{
                    boardScript.panel.SetActive(false);
                    boards[i].GetComponent<Image>().color = Color.blue;
                }
            }
        }
        else{
            for (int i=0; i<boards.Length; i++){
                BoardManager boardScript = boards[i].GetComponent<BoardManager>();
                boards[i].GetComponent<Image>().color = i==currentBoard ? Color.blue : Color.yellow;
                if (currentBoard==i){
                    boardScript.panel.SetActive(false);
                }
                else {
                    boardScript.panel.SetActive(true);
                }
            }
        }
    }

    public void UpdateScore(int winner){
        scores[winner]++;
        scoresText[winner].text = $"Score: {scores[winner]}";
    }

    public void UpdateActifIcon(){
        turnIcons[currentPlayer].SetActive(true);
        int otherPlayer = currentPlayer==0 ? 1 : 0;
        turnIcons[otherPlayer].SetActive(false);
    }

    private bool IsGameEnded(){
        for (int i=0; i<boards.Length; i++){
            if (boards[i].GetComponent<BoardManager>().IsBoardFull()) gameEnded=true;
            else {
                gameEnded = false;
                return gameEnded;
            }
        }
        return gameEnded;
    }

    public void CheckEndOfTheGame(){
        if (IsGameEnded()){
            gameOverPanel.SetActive(true);
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(false);
            if (scores[0] != scores[1]){
                int winner = scores[0] > scores[1] ? 0 : 1;
                playersIcon[winner].SetActive(true);
                statText.text = "Wins";
            }
            else {
                statText.text = "Draw";
            }
        }
    }

    void Start(){
        scores = new int[2];
        GameSetup();
    }

    public void RestartButton(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenuButton(){
        SceneManager.LoadScene(0);
    }
}
