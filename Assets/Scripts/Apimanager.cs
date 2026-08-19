using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;



public class ApiManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform cardContainer;
    public CardDetailUI cardDetailUI;
    public GameObject userButtonPrefab;
    public Transform userContainer;
    public TMPro.TMP_Text userNameText;

    private string usuarioUrl =
        "https://my-json-server.typicode.com/Jakeline-Avila/unity-baraja-api/users/1";

    void Start()
    {
        StartCoroutine(ObtenerUsuarios());
    }
    IEnumerator ObtenerUsuarios()
    {
        string url =
            "https://my-json-server.typicode.com/Jakeline-Avila/unity-baraja-api/users";

        UnityWebRequest request =
            UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;

            UserData[] usuarios =
                JsonHelper.FromJson<UserData>(json);

            Debug.Log("Usuarios encontrados: " + usuarios.Length);


            foreach (UserData usuario in usuarios)
            {
                Debug.Log(
                    "Usuario: " +
                    usuario.name +
                    " ID: " +
                    usuario.id
                );

              
                GameObject nuevoBoton =
                    Instantiate(
                        userButtonPrefab,
                        userContainer
                    );

              
                UserButtonUI userButton =
                    nuevoBoton.GetComponent<UserButtonUI>();

                userButton.Configurar(
                    usuario.id,
                    usuario.name,
                    this
                );

              
                Button button =
                    nuevoBoton.GetComponent<Button>();

                button.onClick.AddListener(
                    userButton.SeleccionarUsuario
                );
            }

            
            if (usuarios.Length > 0)
            {
                CargarUsuario(usuarios[0].id);
            }
        }
        else
        {
            Debug.LogError(
                "Error al obtener usuarios: " +
                request.error
            );
        }
    }

    public void CargarUsuario(int id)
    {
        StartCoroutine(ObtenerUsuarioPorID(id));
    }

    IEnumerator ObtenerUsuarioPorID(int id)
    {
        string url =
            "https://my-json-server.typicode.com/Jakeline-Avila/unity-baraja-api/users/" + id;

        UnityWebRequest request =
            UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;

            UserData usuario =
                JsonUtility.FromJson<UserData>(json);

            Debug.Log("Usuario seleccionado: " + usuario.name);
            userNameText.text = "Usuario: " + usuario.name;

            
           
            foreach (Transform hijo in cardContainer)
            {
                Destroy(hijo.gameObject);
            }

            
            foreach (int cartaID in usuario.cards)
            {
                StartCoroutine(ObtenerCarta(cartaID));
            }
        }
        else
        {
            Debug.LogError(
                "Error al obtener usuario: " + request.error
            );
        }
    }
    IEnumerator ObtenerUsuario()
    {
        UnityWebRequest request = UnityWebRequest.Get(usuarioUrl);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;

            Debug.Log("Usuario recibido:");
            Debug.Log(json);

            UserData usuario =
                JsonUtility.FromJson<UserData>(json);

            Debug.Log("Usuario: " + usuario.name);
            Debug.Log("ID del usuario: " + usuario.id);

            foreach (int cartaID in usuario.cards)
            {
                Debug.Log("Consultando carta ID: " + cartaID);

                StartCoroutine(ObtenerCarta(cartaID));
            }
        }
        else
        {
            Debug.LogError(
                "Error al obtener usuario: " + request.error
            );
        }
    }

    IEnumerator ObtenerCarta(int cartaID)
    {
        string pokemonUrl =
            "https://pokeapi.co/api/v2/pokemon/" + cartaID + "/";

        UnityWebRequest request =
            UnityWebRequest.Get(pokemonUrl);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;

            PokemonData pokemon =
                JsonUtility.FromJson<PokemonData>(json);

            Debug.Log(
                "Carta ID " + cartaID +
                " -> " + pokemon.name
            );

            
            string tipo = pokemon.types[0].type.name;

            Debug.Log(
                "Tipo: " + tipo
            );

          
            GameObject nuevaCarta =
                Instantiate(cardPrefab, cardContainer);

           
            CardUI cardUI =
                nuevaCarta.GetComponent<CardUI>();

            cardUI.cardDetailUI = cardDetailUI;

           
            cardUI.SetCard(
                cartaID,
                pokemon.name,
                null,
                tipo
            );

           
            StartCoroutine(
                DescargarImagen(
                    pokemon.sprites.front_default,
                    cardUI,
                    cartaID,
                    pokemon.name,
                    tipo
                )
            );
        }
        else
        {
            Debug.LogError(
                "Error al obtener carta " +
                cartaID + ": " +
                request.error
            );
        }
    }

    IEnumerator DescargarImagen(
        string url,
        CardUI cardUI,
        int id,
        string nombre,
        string tipo
    )
    {
        UnityWebRequest request =
            UnityWebRequestTexture.GetTexture(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D textura =
                DownloadHandlerTexture.GetContent(request);

            Sprite sprite = Sprite.Create(
                textura,
                new Rect(
                    0,
                    0,
                    textura.width,
                    textura.height
                ),
                new Vector2(0.5f, 0.5f)
            );

           
            cardUI.SetCard(
                id,
                nombre,
                sprite,
                tipo
            );
        }
        else
        {
            Debug.LogError(
                "Error descargando imagen: " +
                request.error
            );
        }
    }
}