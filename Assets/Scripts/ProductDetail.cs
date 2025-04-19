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
        _nameOfProduct.onSelect.AddListener(NameChanged);
        _nameOfProduct.onDeselect.AddListener(NameChangedD);
        _quantityOfProduct.onValueChanged.AddListener(TotalAmountProduct);
        _quantityOfProduct.onSelect.AddListener(Quantity);
        _quantityOfProduct.onDeselect.AddListener(QuantityD);
        _priceOfProduct.onValueChanged.AddListener(TotalAmountProduct);
        _priceOfProduct.onSelect.AddListener(Price);
        _priceOfProduct.onDeselect.AddListener(PriceD);
        _delete.onClick.AddListener(DeleteEntry);
    }

    void NameChanged(string i)
    {
        if (i.Length < 1)
        {
            _nameOfProduct.text = " ";
        }
    }

    void Quantity(string i)
    {
        if (i.Length < 1)
        {
            _quantityOfProduct.text = " ";
        }
    }

    void Price(string i)
    {
        if (i.Length < 1)
        {
            _priceOfProduct.text = " ";
        }
    }

    void NameChangedD(string i)
    {
        if (i == " ")
        {
            _nameOfProduct.text = "";
        }
    }

    void QuantityD(string i)
    {
        if (i == " ")
        {
            _quantityOfProduct.text = "";
        }
    }

    void PriceD(string i)
    {
        if (i == " ")
        {
            _priceOfProduct.text = "";
        }
    }

    void TotalAmountProduct(string text)
    {
        try
        {
            _quantity = int.Parse(_quantityOfProduct.text);
            _totalProductCost.text = Mathf.Round(float.Parse(_quantityOfProduct.text) * float.Parse(_priceOfProduct.text)).ToString();
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
