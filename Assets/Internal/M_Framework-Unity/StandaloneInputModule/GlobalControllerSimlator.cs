using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalControllerSimlator : MonoBehaviour 
{
    public Font targetFont;
    public Text[] allTexts;
    public TextMesh[] textMeshes;

    [ContextMenu("GetAllTexts")]
    public void GetAllTexts()
    {
        allTexts = Resources.FindObjectsOfTypeAll<Text>();
        textMeshes = Resources.FindObjectsOfTypeAll<TextMesh>();
    }

    [ContextMenu("SetFontToAllTexts")]
    public void SetFontToAllTexts()
    {
        foreach (var item in allTexts)
        {
            item.font = targetFont;
        }

        foreach (var item in textMeshes)
        {
            item.font = targetFont;
        }
    }

    [ContextMenu("AutoSet")]
    public void AutoSet()
    {
        GetAllTexts();
        SetFontToAllTexts();

        //UnityEditor.s
    }

}
