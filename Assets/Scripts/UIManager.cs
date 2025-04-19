using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    Dictionary<string, string>[] k;
    [SerializeField] Button _calculate, _history, _add, _submit, _back1, _back2, _clear, _exit;
    [SerializeField] GameObject _addProductObject, _calculationMenu, _mainMenu, _historyMenu, _addHistoryObject;
    [SerializeField] Transform _addProductlocation, _addHistory;
    [SerializeField] TMP_Text _sumTotal, _date;
    [SerializeField] TMP_InputField _customerName;
    int _totalAmount = 0;
    List<Dictionary<string, string>> dict;

    void Start()
    {
        _add.onClick.AddListener(AddProduct);
        _calculate.onClick.AddListener(CalculationPanel);
        _history.onClick.AddListener(HistoryPanel);
        _submit.onClick.AddListener(Save);
        _back1.onClick.AddListener(BackClicked);
        _back2.onClick.AddListener(BackClicked);
        _clear.onClick.AddListener(Clear);
        _exit.onClick.AddListener(Exit);
    }

    public void SumTotal()
    {
        _totalAmount = 0;
        foreach (ProductDetail product in FindObjectsByType<ProductDetail>(FindObjectsSortMode.None))
        {
            _totalAmount += product._totalProductPrice;
        }
        _sumTotal.text = _totalAmount.ToString() + "Rs";
    }

    void AddProduct()
    {
        Instantiate(_addProductObject, _addProductlocation);
    }

    void Exit()
    {
        Application.Quit();
    }

    void CalculationPanel()
    {
        _calculationMenu.gameObject.SetActive(true);
        _mainMenu.gameObject.SetActive(false);
        _historyMenu.gameObject.SetActive(false);
        _date.text = System.DateTime.Now.Date.ToString("dd-MM-yyyy");
    }

    void HistoryPanel()
    {
        _historyMenu.gameObject.SetActive(true);
        _calculationMenu.gameObject.SetActive(false);
        _mainMenu.gameObject.SetActive(false);
        Load();
    }


    void BackClicked()
    {
        foreach (ProductDetail product in FindObjectsByType<ProductDetail>(FindObjectsSortMode.None))
        {
            product.DeleteEntry();
        }
        foreach (CustomerData customer in FindObjectsByType<CustomerData>(FindObjectsSortMode.None))
        {
            customer.DeleteEntry();
        }
        _customerName.text = "";
        _historyMenu.gameObject.SetActive(false);
        _calculationMenu.gameObject.SetActive(false);
        _mainMenu.gameObject.SetActive(true);
    }

    void Save()
    {
        SaveSystem.DataFormat dataFormat = new SaveSystem.DataFormat();
        foreach (ProductDetail product in FindObjectsByType<ProductDetail>(FindObjectsSortMode.None))
        {
            dataFormat._customerName = _customerName.text;
            dataFormat._sumtotal = _sumTotal.text;
            dataFormat._date = System.DateTime.Now.ToString();
            product.DeleteEntry();
        }
        SaveSystem.SaveData(dataFormat);
        _customerName.text = "";
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/data";
        if (File.Exists(path))
        {
            var data = File.ReadAllText(path);
            string fixedJson = "[" + Regex.Replace(data, "}(?={)", "},") + "]";
            dict = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(fixedJson);
        }
    }

    void Load()
    {
        LoadData();
        if (dict.Count > 0)
        {
            foreach (Dictionary<string, string> data in dict)
            {
                GameObject dataObj = Instantiate(_addHistoryObject, _addHistory);
                if (data["_sumtotal"] != null)
                {
                    dataObj.GetComponent<CustomerData>()._sumtotalText = data["_sumtotal"];
                }
                if (data["_customerName"] != null)
                {
                    dataObj.GetComponent<CustomerData>()._customerNameText = data["_customerName"];
                }
                if (data["_date"] != null)
                {
                    dataObj.GetComponent<CustomerData>()._dateText = data["_date"];
                }
            }
        }
    }

    void Clear()
    {
        string path = Application.persistentDataPath + "/data";
        if (File.Exists(path))
        {
            File.WriteAllText(path, "");
        }
        foreach (CustomerData customer in FindObjectsByType<CustomerData>(FindObjectsSortMode.None))
        {
            Destroy(customer.gameObject);
        }
    }
}
