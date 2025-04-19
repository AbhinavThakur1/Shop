using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductDetail : MonoBehaviour
{
    [SerializeField] TMP_InputField _nameOfProduct, _quantityOfProduct, _priceOfProduct;
    [SerializeField] TMP_Text _totalProductCost, _serialNumberText;
    [SerializeField] Button _delete;
    public string _productName;
    public int _totalProductPrice, _serialNumber, _quantity;

    void Start()
    {
        _nameOfProduct.onValueChanged.AddListener(TotalAmountProduct);
        _quantityOfProduct.onValueChanged.AddListener(TotalAmountProduct);
        _priceOfProduct.onValueChanged.AddListener(TotalAmountProduct);
        _delete.onClick.AddListener(DeleteEntry);
    }

    void NameChanged()
    {
        _productName = _nameOfProduct.text;
    }

    void TotalAmountProduct(string text)
    {
        try
        {
            _quantity = int.Parse(_quantityOfProduct.text);
            _totalProductCost.text = Mathf.Round(float.Parse(_quantityOfProduct.text) * float.Parse(_priceOfProduct.text)).ToString() + "Rs";
            _totalProductPrice = int.Parse(Mathf.Round(float.Parse(_quantityOfProduct.text) * float.Parse(_priceOfProduct.text)).ToString());
            FindFirstObjectByType<UIManager>().SumTotal();
        }
        catch
        {

        }
    }

    public void DeleteEntry()
    {
        _totalProductPrice = 0;
        FindFirstObjectByType<UIManager>().SumTotal();
        Destroy(gameObject);
    }

}
