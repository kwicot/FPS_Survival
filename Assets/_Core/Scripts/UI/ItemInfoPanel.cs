using _Core.Scripts.Items;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

namespace _Core.Scripts.UI
{
    public class ItemInfoPanel : MonoBehaviour
    {
        [SerializeField] private Text nameText;
        [SerializeField] private Text descriptionText;
        [SerializeField] private Text weightText;
        [SerializeField] private Text totalWeightText;
        
        [SerializeField] private Text stackableText;

        public void Init(Item item)
        {
            nameText.text = item.Name;
            descriptionText.text = "Some description";
            weightText.text = item.Weight.ToString();
            if (item.CanStack)
            {
                stackableText.text = "Stackable";
                totalWeightText.text = item.TotalWeight.ToString();
            }
            else
            {
                stackableText.text = "Non Stackable";
                totalWeightText.text = "";
            }

        }
    }
}