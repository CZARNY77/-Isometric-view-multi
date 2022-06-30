using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] Menu[] menus;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenMenu(string menuName)
    {
        foreach(Menu menu in menus)
        {
            if (menu.menuName == menuName) menu.Open();
            else if (menu.open) CloseMenu(menu);
        }
    }

    public void OpenMenu(Menu _menu)
    {
        foreach (Menu menu in menus)
        {
            if (menu.open) CloseMenu(menu);
        }
        _menu.Open();
    }

    public void CloseMenu(Menu _menu)
    {
        _menu.Close();
    }
}
