using TMPro;
using UnityEngine;

public class CustomerData : MonoBehaviour
{
    [SerializeField] TMP_Text _customerName, _amountPaid, _time;
    public string _customerNameText;
    public string _sumtotalText;
    public string _dateText;

    private void Start()
    {
        _customerName.text = _customerNameText;
        _amountPaid.text = _sumtotalText;
        _time.text = _dateText;
    }

    public void DeleteEntry()
    {
        Destroy(gameObject);
    }
}
