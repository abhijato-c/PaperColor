using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenusSelection : MonoBehaviour {
    public GameObject PlayBtn;
    public GameObject OptionsBtn;
    public GameObject ExitBtn;
    public GameObject LevelMenu;
    public GameObject OptionsMenu;
    public GameObject Highlight;
    public Image TutorialFrame;
    public Sprite Lock;
    public Sprite[] LevelNums;
    public Sprite[] TutorialSlides;
    private InputAction Arrow;
    private InputAction Confirm;
    private InputAction Esc;
    private InputAction LevelNav;
    private InputAction Tuto;

    private int SelIndex = 0;
    private int LvlIndex = 0;
    private int TutorialIndex = 0;

    void Start() {
        UpdateSelection();
        if (!PlayerPrefs.HasKey("Level")) {
            PlayerPrefs.SetInt("Level", 1);
        }
    }

    void Awake() {
        Arrow = new InputAction("ArrowMv");
        Arrow.AddCompositeBinding("1DAxis").With("Positive", "<Keyboard>/upArrow").With("Negative", "<Keyboard>/downArrow");
        Arrow.performed += ArrowPressed;
        Arrow.Enable();

        Confirm = new InputAction("MenuSelect", binding: "<Keyboard>/enter");
        Confirm.performed += Selected;
        Confirm.Enable();

        Esc = new InputAction("MenuHide", binding: "<Keyboard>/escape");
        Esc.performed += Exit;
        Esc.Enable();

        LevelNav = new InputAction("LvlNav");
        LevelNav.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/rightArrow");
        LevelNav.performed += NavLevelMenu;

        Tuto = new InputAction("Tutorial");
        Tuto.AddCompositeBinding("1DAxis").With("Positive", "<Keyboard>/rightArrow").With("Negative", "<Keyboard>/leftArrow");
        Tuto.performed += TutorialSlide;
        Tuto.Enable();
    }

    void TutorialSlide(InputAction.CallbackContext context) {
        float side = context.ReadValue<float>();
        if (side < 0 && TutorialIndex > 0)
            TutorialIndex -= 1;
        else if (side > 0 && TutorialIndex < 5)
            TutorialIndex += 1;
        TutorialFrame.sprite = TutorialSlides[TutorialIndex];
    }

    void ArrowPressed(InputAction.CallbackContext context) {
        float side = context.ReadValue<float>();
        if (side > 0 && SelIndex > 0)
            SelIndex -= 1;
        else if (side < 0 && SelIndex < 2)
            SelIndex += 1;
        UpdateSelection();
    }

    void Selected(InputAction.CallbackContext context) {
        if (!LevelMenu.activeSelf && !OptionsMenu.activeSelf){
            if (SelIndex == 0) 
                OpenPlayMenu();
            else if (SelIndex == 1)
                OpenOptionsMenu();
            else if (SelIndex == 2)
                Application.Quit();
        }
        else if (LevelMenu.activeSelf) {
            if (LvlIndex < PlayerPrefs.GetInt("Level"))
                SceneManager.LoadScene("lv" + LvlIndex);
        }
    }

    void Exit(InputAction.CallbackContext context) {
        LevelMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        Arrow.Enable();
        LevelNav.Disable();
    }

    void NavLevelMenu(InputAction.CallbackContext context) {
        Vector2 dir = context.ReadValue<Vector2>();
        
        if (dir.x < 0 && LvlIndex > 0)
            LvlIndex -= 1;
        else if (dir.x > 0 && LvlIndex < 9)
            LvlIndex += 1;
        else if (dir.y > 0 && LvlIndex > 4)
            LvlIndex -= 5;
        else if (dir.y < 0 && LvlIndex < 5)
            LvlIndex +=5;
        
        Highlight.transform.position = GameObject.Find((LvlIndex+1).ToString()).transform.position;
    }

    void OpenPlayMenu() {
        LevelMenu.SetActive(true);
        LevelNav.Enable();
        Arrow.Disable();

        for (int i = 1; i <= 10; ++i) {
            GameObject LvBtn = GameObject.Find(i.ToString());
            if (i > PlayerPrefs.GetInt("Level"))
                LvBtn.GetComponent<Image>().sprite = Lock;
            else
                LvBtn.GetComponent<Image>().sprite = LevelNums[i - 1];
        }
    }

    void OpenOptionsMenu() {
        OptionsMenu.SetActive(true);
        Arrow.Disable();
    }

    void UpdateSelection() {
        DeActivate(PlayBtn);
        DeActivate(OptionsBtn);
        DeActivate(ExitBtn);
        if (SelIndex == 0)
            Activate(PlayBtn);
        else if (SelIndex == 1)
            Activate(OptionsBtn);
        else if (SelIndex == 2)
            Activate(ExitBtn);
    } 

    void Activate(GameObject Option) {
        TMP_Text tex = Option.GetComponent<TMP_Text>();
        tex.color = new Color32(230, 200, 70, 255);
        tex.fontSize = 130;
        Vector2 trans = Option.GetComponent<RectTransform>().anchoredPosition;
        trans.x = 50;
        Option.GetComponent<RectTransform>().anchoredPosition = trans;
    }

    void DeActivate(GameObject Option) {
        TMP_Text tex = Option.GetComponent<TMP_Text>();
        tex.color = new Color32(220, 220, 220, 255);
        tex.fontSize = 90;
        Vector2 trans = Option.GetComponent<RectTransform>().anchoredPosition;
        trans.x = 10;
        Option.GetComponent<RectTransform>().anchoredPosition = trans;
    }
}