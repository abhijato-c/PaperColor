using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenusSelection : MonoBehaviour {
    public GameObject PlayBtn;
    public GameObject OptionsBtn;
    public GameObject ExitBtn;
    public GameObject LevelMenu;
    public GameObject OptionsMenu;
    public GameObject Highlight;
    private InputAction Arrow;
    private InputAction Confirm;
    private InputAction Esc;
    private InputAction LevelNav;

    private int SelIndex = 0;
    private int LvlIndex = 0;

    void Start() {
        UpdateSelection();
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
        else if (LevelMenu.activeSelf){
            SceneManager.LoadScene("lv"+LvlIndex);
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