using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaMenu : MonoBehaviour
{
    public GameObject ObjetoMenuPausa;
    public bool Pausa = false;

     void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pausa == false) 
            {
                ObjetoMenuPausa.SetActive(true);
                Pausa = true;

                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
             }
            else if(Pausa == true)
            {
                Resumir();
            }
        }
    }


    public void Resumir()
    {
        ObjetoMenuPausa.SetActive(false);
        Pausa = false;

        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void IrAlMenu(string NombreMneu)
    {
        SceneManager.LoadScene(NombreMneu);
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }
}
