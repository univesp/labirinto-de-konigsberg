using UnityEngine;
using TMPro;

public class FontSizeController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        //Inicializa com o último tamanho definido nas opções
        _text.fontSize = PlayerPrefs.GetFloat("font_size", 33.5f);

        //Salva o método de atualizar o tamanho no delegate
        FontSizeSettings.Instance.SizeChangeDelegate += UpdateSize;
    }

    private void UpdateSize()
    {
        _text.fontSize = PlayerPrefs.GetFloat("font_size", 33.5f);
    }

    private void OnDestroy()
    {
        //Remove o método de atualizar o tamanho no delegate
        FontSizeSettings.Instance.SizeChangeDelegate -= UpdateSize;
    }
}