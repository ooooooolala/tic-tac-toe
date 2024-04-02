using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private int buttonID;
    private BoardManager boardScript;

    void Start(){
        buttonID = int.Parse(transform.name);
        boardScript = transform.parent.GetComponentInParent<BoardManager>();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick(){
        boardScript.OnButtonClick(buttonID);
    }
}
