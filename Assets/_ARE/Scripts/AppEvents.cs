using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppEvents : MonoBehaviour
{
    public static event EventHandler CloseBook;

    public static void CloseBookFunction()
    {
        if (CloseBook != null)
            CloseBook(new object(), new EventArgs());
    }
}
