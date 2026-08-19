using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserButtonUI : MonoBehaviour
{
    public TMP_Text userNameText;

    private int userID;
    private ApiManager apiManager;

    public void Configurar(int id, string nombre, ApiManager manager)
    {
        userID = id;
        apiManager = manager;

        userNameText.text = nombre;
    }

    public void SeleccionarUsuario()
    {
        apiManager.CargarUsuario(userID);
    }
}