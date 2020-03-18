using UnityEngine;
using UnityEngine.UI;

public class FontSizeSettings : MonoBehaviour
{
    [SerializeField] private Slider _fontSlider;

    public delegate void OnSizeChange();
    public OnSizeChange SizeChangeDelegate;

    public static FontSizeSettings Instance;

    private void Awake()
    {
        Instance = this;
    }

    //Inicia o Slider no último valor configurado
    private void Start()
    {
        _fontSlider.value = PlayerPrefs.GetFloat("font_size", 33.5f);
    }

    public void ChangeFontSize()
    {
        //Atualiza o PlayerPrefs com o valor atual
        PlayerPrefs.SetFloat("font_size", _fontSlider.value);

        //Executa o delegate que contém os métodos que alteram o tamanho das fontes
        SizeChangeDelegate?.Invoke();
    }
}