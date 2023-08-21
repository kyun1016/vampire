using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField id;
    public TMP_InputField password;

    public TMP_Text notify;


    // Start is called before the first frame update
    void Start()
    {
        notify.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveUserData()
    {
        // if input check have some error, then function exit
        if (!CheckInput(id.text, password.text))
        {
            return;
        }

        if (!PlayerPrefs.HasKey(id.text))
        {
            PlayerPrefs.SetString(id.text, password.text);
            notify.text = "���̵� ������ �Ϸ�ƽ��ϴ�.";
        }
        else
        {
            notify.text = "�̹� �����ϴ� ���̵� �Դϴ�.";
        }
        
    }

    public void CheckUserData()
    {
        // if input check have some error, then function exit
        if (!CheckInput(id.text, password.text))
        {
            return;
        }

        string pass = PlayerPrefs.GetString(id.text);

        if(password.text == pass)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            notify.text = "�Է��Ͻ� ���̵�� �н����尡 ��ġ���� �ʽ��ϴ�.";
        }
    }

    bool CheckInput(string id, string pwd)
    {
        if(id == "" || pwd == "")
        {
            notify.text = "���̵� �Ǵ� �н����带 �Է����ּ���.";
            return false;
        }
        else
        {
            return true;
        }
    }




}
