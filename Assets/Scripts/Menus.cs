using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
    [SerializeField]
    private GameObject panelRecord;
    [SerializeField]
    private TextMeshProUGUI recordText1;
    [SerializeField]
    private TextMeshProUGUI recordText2;
    [SerializeField]
    private TextMeshProUGUI recordText3;
    [SerializeField]
    private TextMeshProUGUI recordText4;
    [SerializeField]
    private TextMeshProUGUI recordText5;
    [SerializeField]
    private TextMeshProUGUI recordText6;
    [SerializeField]
    private TextMeshProUGUI recordText7;
    [SerializeField]
    private TextMeshProUGUI recordText8;
    [SerializeField]
    private TextMeshProUGUI recordText9;
    [SerializeField]
    private TextMeshProUGUI recordText10;
    [SerializeField]
    private TextMeshProUGUI recordText11;
    [SerializeField]
    private TextMeshProUGUI recordText12;

    
    public GameObject[] scripts;
    public Image [] img;
    int tope = 10;

    public void Start()
    {
        
        liberarMenus();
    }

    public void IrAOtra(string name)
    {
        SceneManager.LoadScene(name);
       
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void TablaRecord()
    {
        panelRecord.SetActive(true);
        ActualizarRecords();
    }
    public void VolverMenu()
    {
        panelRecord.SetActive(false);
       
    }

    public void ActualizarRecords()
    {
        recordText1.text = PlayerPrefs.GetInt("RecordText" + 1).ToString();
        recordText2.text = PlayerPrefs.GetInt("RecordText" + 2).ToString();
        recordText3.text = PlayerPrefs.GetInt("RecordText" + 3).ToString();
        recordText4.text = PlayerPrefs.GetInt("RecordText" + 4).ToString();
        recordText5.text = PlayerPrefs.GetInt("RecordText" + 5).ToString();
        recordText6.text = PlayerPrefs.GetInt("RecordText" + 6).ToString();
        recordText7.text = PlayerPrefs.GetInt("RecordText" + 7).ToString();
        recordText8.text = PlayerPrefs.GetInt("RecordText" + 8).ToString();
        recordText9.text = PlayerPrefs.GetInt("RecordText" + 9).ToString();
        recordText10.text = PlayerPrefs.GetInt("RecordText" + 10).ToString();
        recordText11.text = PlayerPrefs.GetInt("RecordText" + 11).ToString();
        recordText12.text = PlayerPrefs.GetInt("RecordText" + 12).ToString();
    }
    public void liberarMenus()
    {
        if (PlayerPrefs.GetInt("RecordText" + 1) > tope)
        {
            Debug.Log("ya");
        }
    }
}
