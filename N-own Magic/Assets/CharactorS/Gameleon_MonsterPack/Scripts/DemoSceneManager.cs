using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoSceneManager : MonoBehaviour
{
    public List<GameObject> characters;
    public List<string> listOfTriggers = new List<string> { "Victory", "Run", "Idle", "Punch_01", "Punch_02", "Death" };
    public GameObject buttonPrefab;
    public Transform buttonContainer;

    private GameObject _currChar;
    private int _index = 0;

    private List<Button> buttons = new List<Button>();

    private void Start()
    {
        SpawnNext();
        CreateButtons();
    }

    public void SpawnNext()
    {
        if (_currChar != null) Destroy(_currChar);

        _currChar = Instantiate(characters[_index], transform);

        _index++;

        if(_index >= characters.Count)
        {
            _index = 0;
        }
    }

    private void CreateButtons()
    {
        foreach(string s in listOfTriggers)
        {
            var button = Instantiate(buttonPrefab, buttonContainer);
            button.GetComponentInChildren<Text>().text = s;
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            button.GetComponent<Button>().onClick.AddListener(() => { Play(s); });
            
        }
    }

    public void Play(string s)
    {
        if(_currChar != null)
        {
            _currChar.GetComponent<Animator>().SetTrigger(s);
        }
    }
}
