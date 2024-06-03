using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class codigoKonami : MonoBehaviour
{
    private List<KeyCode> codigoKonamiList = new List<KeyCode>()
    {
        KeyCode.UpArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.B,
        KeyCode.A
    };

    private List<KeyCode> entradaJugador = new List<KeyCode>();

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            KeyCode teclaPresionada = KeyCode.None;
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    teclaPresionada = vKey;
                    break;
                }
            }

            if (teclaPresionada != KeyCode.None)
            {
                entradaJugador.Add(teclaPresionada);

                // Si la entrada del jugador es más larga que el código Konami, elimina la primera entrada
                if (entradaJugador.Count > codigoKonamiList.Count)
                {
                    entradaJugador.RemoveAt(0);
                }

                // Si la entrada del jugador coincide con el código Konami, otorga vidas infinitas
                if (Enumerable.SequenceEqual(entradaJugador, codigoKonamiList))
                {
                    GameObject jugador = GameObject.Find("Jugador");
                    if (jugador != null)
                    {
                        jugador.GetComponent<ColisionEnemigo>().vida = 999;
                        jugador.GetComponent<Animator>().Play("Konami");
                    }
                }
            }
        }
    }
}
