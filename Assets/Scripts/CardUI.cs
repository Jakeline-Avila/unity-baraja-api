using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text pokemonName;
    public TMP_Text cardID;
    public Image pokemonImage;
    public TMP_Text typeText;

    public CardDetailUI cardDetailUI;

    private int pokemonID;
    private string pokemonType;



    public void SetCard(
        int id,
        string name,
        Sprite image
    )
    {
        pokemonID = id;
        pokemonName.text = name.ToUpper();
        cardID.text = "#" + id.ToString("000");

        if (image != null)
        {
            pokemonImage.sprite = image;
        }
    }



    public void SetCard(
        int id,
        string name,
        Sprite image,
        string type
    )
    {
        pokemonID = id;
        pokemonType = type;

        pokemonName.text = name.ToUpper();
        cardID.text = "#" + id.ToString("000");

        if (image != null)
        {
            pokemonImage.sprite = image;
        }

        if (typeText != null)
        {
            typeText.text = type.ToUpper();
        }

        Image fondo = GetComponent<Image>();

        if (fondo != null)
        {
            fondo.color = GetTypeColor(type);
        }
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        MostrarDetalle();
    }


  
    public void MostrarDetalle()
    {
        if (cardDetailUI == null)
        {
            Debug.LogError(
                "CardDetailUI no está asignado en la carta."
            );

            return;
        }

        cardDetailUI.MostrarCarta(
            pokemonID,
            pokemonName.text,
            pokemonImage.sprite,
            pokemonType
        );
    }


   
    private Color GetTypeColor(string type)
    {
        switch (type)
        {
            case "fire":
                return new Color32(240, 150, 120, 255);

            case "water":
                return new Color32(130, 180, 230, 255);

            case "grass":
                return new Color32(150, 200, 140, 255);

            case "bug":
                return new Color32(180, 200, 100, 255);

            case "electric":
                return new Color32(240, 210, 90, 255);

            case "psychic":
                return new Color32(220, 150, 200, 255);

            case "poison":
                return new Color32(180, 130, 200, 255);

            case "ground":
                return new Color32(210, 180, 120, 255);

            case "rock":
                return new Color32(180, 170, 140, 255);

            case "ghost":
                return new Color32(150, 140, 190, 255);

            case "dark":
                return new Color32(100, 100, 110, 255);

            case "ice":
                return new Color32(160, 220, 230, 255);

            case "dragon":
                return new Color32(120, 120, 220, 255);

            case "fairy":
                return new Color32(240, 170, 210, 255);

            case "fighting":
                return new Color32(210, 120, 100, 255);

            case "flying":
                return new Color32(170, 190, 230, 255);

            case "steel":
                return new Color32(170, 180, 190, 255);

            case "normal":
                return new Color32(200, 200, 190, 255);

            default:
                return new Color32(220, 205, 185, 255);
        }
    }
}