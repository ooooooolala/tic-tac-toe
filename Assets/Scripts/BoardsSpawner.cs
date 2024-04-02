using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Board;
    [SerializeField] private GameObject BigBoardPrefab;
    [SerializeField] private Canvas canvas;
    private GameController gameScript;
    private string pos;

    void Awake(){
        gameScript = GetComponent<GameController>();
        InstantiateBoards();
    }

    private Vector2 GetBoardCordinates(int position){
        // x : 0, -270, -540
        // y : 0, 290, 580
        switch(position){
            case 0 :
                pos = "topLeft";
                return new Vector2(0f, 0f);
            case 1 :
                pos = "topEdge";
                return new Vector2(-270f, 0f);
            case 2 :
                pos = "topRight";
                return new Vector2(-540f, 0f);
            case 3 :
                pos = "leftEdge";
                return new Vector2(0f, 290f);
            case 4 :
                pos = "Mid";
                return new Vector2(-270f, 290f);
            case 5 :
                pos = "rightEdge";
                return new Vector2(-540f, 290f);
            case 6 :
                pos = "bottomLeft";
                return new Vector2(0f, 580f);
            case 7 :
                pos = "bottomEdge";
                return new Vector2(-270f, 580f);
            case 8 :
                pos = "bottomRight";
                return new Vector2(-540f, 580f);
            default:
                pos = "Mid";
                return new Vector2(-270f,290f);
        }
    }
    
    private Vector2 GetAnchorMin(string pos){
        switch(pos){
            case "topLeft":
                return new Vector2(0f,1f);
            case "topEdge":
                return new Vector2(0.5f,1f);
            case "topRight":
                return new Vector2(1f,1f);
            case "bottomEdge":
                return new Vector2(0.5f, 0f);
            case "bottomLeft":
                return new Vector2(0f,0f);
            case "leftEdge":
                return new Vector2(0f,0.5f);
            case "rightEdge":
                return new Vector2(1f,0.5f);
            case "bottomRight":
                return new Vector2(1f,0f);
            default:
                return new Vector2(0.5f,0.5f);
        }
    }
    
    private Vector2 GetAnchorMax(string pos){
        switch(pos){
            case "topLeft":
                return new Vector2(0f,1f);
            case "leftEdge":
                return new Vector2(0f,0.5f);
            case "topEdge":
                return new Vector2(0.5f,1f);
            case "rightEdge":
                return new Vector2(1f,0.5f);
            case "topRight":
                return new Vector2(1f,1f);
            case "bottomLeft":
                return new Vector2(0f,0f);
            case "bottomEdge":
                return new Vector2(0.5f, 0f);
            case "bottomRight":
                return new Vector2(1f,0f);
            default:
                return new Vector2(0.5f, 0.5f);
        }
    }

    private void SetAnchors(RectTransform rect, string pos){
        rect.anchorMin = GetAnchorMin(pos);
        rect.anchorMax = GetAnchorMax(pos);
    }

    void InstantiateBoards(){
        gameScript.boards = new GameObject[9];
        for(int i=0; i<9; i++){

            Vector3 position = new Vector3(GetBoardCordinates(i).x,GetBoardCordinates(i).y,0f);
            GameObject board = Instantiate(Board,position,Quaternion.identity);
            board.transform.SetParent(BigBoardPrefab.transform,false);
            RectTransform childRect = board.GetComponent<RectTransform>();
            childRect.localPosition = position;
            SetAnchors(childRect, pos);
            BoardManager boardScript = board.GetComponent<BoardManager>();
            boardScript.boardID = i;
            
            gameScript.boards[i] = board;
        }
    }


}
