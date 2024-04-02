using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    // 0 index is for x and 1 index is for o
    public int boardID;
    public GameObject panel;
    public Button[] spacesButton;
    private int[] spacesTaken;
    private bool[] isChecked; // basically so we don't check the row or the column twice
    private bool boardFull;
    private GameController gameScript;
    [SerializeField] private GameObject[] lines;
    [SerializeField] private Sprite[] playersIcon;


    void Start()
    {
        spacesTaken = new int[9];
        isChecked = new bool[8];
        boardFull = false;
        for (int i=0; i<isChecked.Length; i++) isChecked[i] = false;
        for (int i=0; i<spacesTaken.Length; i++) spacesTaken[i] = -1;
        gameScript = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    public bool IsBoardFull(){
        for (int i=0; i<spacesTaken.Length; i++){
            if (spacesTaken[i] == -1) {
                boardFull = false;
                return boardFull;
            }
            else boardFull = true;
        }
        return boardFull;
    }

    public void OnButtonClick(int buttonID){
        spacesButton[buttonID].image.sprite = playersIcon[gameScript.currentPlayer];
        spacesButton[buttonID].image.color = gameScript.currentPlayer==0 ? new Color(183f/255f,25f/255f,42f/255f,1f) : new Color(25f/255f,144f/255f,39f/255f,1f);
        spacesButton[buttonID].interactable = false;
        spacesTaken[buttonID] = gameScript.currentPlayer;
        gameScript.SetCurrentBoard(buttonID); 
        gameScript.currentPlayer = gameScript.currentPlayer==1 ? 0 : 1;
        CheckBoard();
        gameScript.UpdateActifIcon();
        gameScript.UpdateBoards();
        gameScript.CheckEndOfTheGame();
    }



    private void CheckBoard(){
        //Column Check
        if (spacesTaken[0]==spacesTaken[3] && spacesTaken[0]==spacesTaken[6] && spacesTaken[0] != -1 && !isChecked[0]){
            lines[0].SetActive(true);
            gameScript.UpdateScore(spacesTaken[0]);
            isChecked[0] = true;
        }
        if (spacesTaken[1]==spacesTaken[4] && spacesTaken[1]==spacesTaken[7] && spacesTaken[1] != -1 && !isChecked[1]){
            lines[1].SetActive(true);
            gameScript.UpdateScore(spacesTaken[1]);
            isChecked[1] = true;
        }
        if (spacesTaken[2]==spacesTaken[5] && spacesTaken[2]==spacesTaken[8] && spacesTaken[2] != -1 && !isChecked[2]){

            lines[2].SetActive(true);
            gameScript.UpdateScore(spacesTaken[2]);
            isChecked[2] = true;
        }
        //Row Check
        if (spacesTaken[0]==spacesTaken[1] && spacesTaken[0]==spacesTaken[2] && spacesTaken[0] != -1 && !isChecked[3]){
            lines[3].SetActive(true);
            gameScript.UpdateScore(spacesTaken[0]);
            isChecked[3] = true;
        }
        if (spacesTaken[3]==spacesTaken[4] && spacesTaken[3]==spacesTaken[5] && spacesTaken[3] != -1 && !isChecked[4]){
            lines[4].SetActive(true);
            gameScript.UpdateScore(spacesTaken[3]);
            isChecked[4] = true;
        }
        if (spacesTaken[6]==spacesTaken[7] && spacesTaken[6]==spacesTaken[8] && spacesTaken[6] != -1 && !isChecked[5]){
            lines[5].SetActive(true);
            gameScript.UpdateScore(spacesTaken[6]);
            isChecked[5] = true;
        }
        //Diagonal Check
        if (spacesTaken[0]==spacesTaken[4] && spacesTaken[0]==spacesTaken[8] && spacesTaken[0] != -1 && !isChecked[6]){
            lines[6].SetActive(true);
            gameScript.UpdateScore(spacesTaken[0]);
            isChecked[6] = true;
        }
        if (spacesTaken[2]==spacesTaken[4] && spacesTaken[2]==spacesTaken[6] && spacesTaken[2] != -1 && !isChecked[7]){
            lines[7].SetActive(true);
            gameScript.UpdateScore(spacesTaken[2]);
            isChecked[7] = true;
        }
    }
}
