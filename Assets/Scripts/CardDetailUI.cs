using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDetailUI : MonoBehaviour
{
    public TMP_Text detailPokemonName;
    public TMP_Text detailCardID;
    public TMP_Text detailType;
    public Image detailPokemonImage;


    public void MostrarCarta(
        int id,
        string nombre,
        Sprite imagen,
        string tipo
    )
    {
        Debug.Log(
            "MOSTRANDO DETALLE: " +
            nombre +
            " | ID: " +
            id +
            " | Tipo: " +
            tipo
        );

        gameObject.SetActive(true);

        
        transform.SetAsLastSibling();

        detailPokemonName.text = nombre.ToUpper();
        detailCardID.text = "#" + id.ToString("000");
        detailType.text = tipo.ToUpper();

        if (imagen != null)
        {
            detailPokemonImage.sprite = imagen;
        }
    }


    public void Cerrar()
    {
        gameObject.SetActive(false);
    }
}