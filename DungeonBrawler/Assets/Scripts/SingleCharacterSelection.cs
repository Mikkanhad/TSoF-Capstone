using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SingleCharacterSelection : MonoBehaviour {

    public Text loadingText;
    public GameObject backPrompt;
    public GameObject startImage;
    public GameObject selectCharacterPrompt;
    public GameObject cancelCharacterPrompt;
    private int hoveredCharacter;

    public string nextScene;
    public string previousScene;

    public GameObject Warrior;
    public GameObject Rogue;
    public GameObject Wizard;
    private GameObject cursor;
    public bool characterSelected;
    public int classPicked;

    public float intervalForFlash;
    public float intervalForSelect;
    public float intervalForReady;

    public float warriorPosition = -16f;
    public float roguePosition = 0f;
    public float wizardPosition = 16f;

    private ParticleSystem warriorParticle;
    private ParticleSystem rogueParticle;
    private ParticleSystem wizardParticle;
    private Light warriorLight;
    private Light rogueLight;
    private Light wizardLight;
    private Light doorLight;

    private float[] cursorPositions = new float[3];
    public GameObject[] characters = new GameObject[3];

    private void Awake()
    {
        warriorParticle = GameObject.Find("WarriorParticle").GetComponent<ParticleSystem>();
        rogueParticle = GameObject.Find("RogueParticle").GetComponent<ParticleSystem>();
        wizardParticle = GameObject.Find("WizardParticle").GetComponent<ParticleSystem>();
        warriorLight = GameObject.Find("WarriorLight").GetComponent<Light>();
        rogueLight = GameObject.Find("RogueLight").GetComponent<Light>();
        wizardLight = GameObject.Find("WizardLight").GetComponent<Light>();
        doorLight = GameObject.Find("DoorLight").GetComponent<Light>();
        SetCursorPositions();
    }
    private void Start()
    {
        hoveredCharacter = 0;
        cursor = GameObject.Find("P1");
        loadingText.GetComponent<Text>().enabled = false;
        startImage.SetActive(false);
        DisableEffects();
    }

    private void Update()
    {
        if (!characterSelected)
        {
            MoveCursor();
            SelectCharacter();
        }
        else
        {
            DeselectCharacter();
            StartGame();
        }
        StatueController();
    }

    private void SetCursorPositions()
    {
        cursorPositions[0] = warriorPosition;
        cursorPositions[1] = roguePosition;
        cursorPositions[2] = wizardPosition;
    }
    private void MoveCursor()
    {
        if(Input.GetButtonDown("MenuRight"))
        {
            hoveredCharacter += 1;
            if(hoveredCharacter >= 3)
            {
                hoveredCharacter = 0;
            }
        }
        if(Input.GetButtonDown("MenuLeft"))
        {
            hoveredCharacter -= 1;
            if(hoveredCharacter <= -1)
            {
                hoveredCharacter = 2;
            }
        }
        cursor.transform.position = new Vector3(cursorPositions[hoveredCharacter], cursor.transform.position.y);
    }
    private void SelectCharacter()
    {
        if(Input.GetButtonDown("EnterP1"))
        {
            characterSelected = true;
            GameObject tempPlayer = GameObject.Instantiate(characters[hoveredCharacter], cursor.transform);
            tempPlayer.transform.position = new Vector2(cursor.transform.position.x, -2.7f);
            tempPlayer.GetComponent<PlayerController>().playerNumber = 1;
        }
    }
    private void DeselectCharacter()
    {
        if(Input.GetButtonDown("CancelP1"))
        {
            characterSelected = false;
            for(int i = 0; i < cursor.transform.childCount; i++)
            {
                if(cursor.transform.GetChild(i).name == "Warrior(Clone)" ||
                   cursor.transform.GetChild(i).name == "Rogue(Clone)" ||
                   cursor.transform.GetChild(i).name == "Wizard(Clone)")
                {
                    Destroy(cursor.transform.GetChild(i).gameObject);
                    return;
                }
            }
        }
    }

    public void StatueController()
    {
        if (characterSelected && hoveredCharacter == 0)
        {
            warriorParticle.Play();
            warriorParticle.Emit(1);
            warriorLight.range = 5;
        }
        else if (!characterSelected)
        {
            warriorParticle.Stop();
            warriorLight.range = 0;
        }

        if (characterSelected && hoveredCharacter == 1)
        {
            rogueParticle.Play();
            rogueParticle.Emit(1);
            rogueLight.range = 5;
        }
        else if (!characterSelected)
        {
            rogueParticle.Stop();
            rogueLight.range = 0;
        }

        if (characterSelected && hoveredCharacter == 2)
        {
            wizardParticle.Play();
            wizardParticle.Emit(1);
            wizardLight.range = 5;
        }
        else if (!characterSelected)
        {
            wizardParticle.Stop();
            wizardLight.range = 0;
        }
    }
    public void DisableEffects()
    {
        warriorParticle.Pause();
        rogueParticle.Pause();
        wizardParticle.Pause();
        warriorLight.range = 0;
        rogueLight.range = 0;
        wizardLight.range = 0;
        doorLight.range = 0;
    }
    private void SetCharacter()
    {
        if(hoveredCharacter == 0)
        {
            PlayerStats.warriorNum = 1;
            PlayerStats.rogueNum = 2;
            PlayerStats.wizardNum = 3;
        }
        else if (hoveredCharacter == 1)
        {
            PlayerStats.warriorNum = 2;
            PlayerStats.rogueNum = 1;
            PlayerStats.wizardNum = 3;
        }
        else if (hoveredCharacter == 2)
        {
            PlayerStats.warriorNum = 2;
            PlayerStats.rogueNum = 3;
            PlayerStats.wizardNum = 1;
        }
    }
    public void StartGame()
    {
        if (characterSelected && Input.GetButtonDown("EnterP1"))
        {
            SetCharacter();
            StartCoroutine(LoadNewScene());
        }
    }
    IEnumerator LoadNewScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(nextScene);
        yield return null;
    }
}
