using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Core.Scripts.UI
{
    public class BlockCell : MonoBehaviour
    {
        [SerializeField] private Image blockImage;
        [SerializeField] private Image cellImage;
        [SerializeField] private TMP_Text text;
        [SerializeField] private Button button;
        [SerializeField] private Color normalColor;
        [SerializeField] private Color selectedColor;


        public void Init(Sprite sprite, string name,bool isSelected, UnityAction clickAction)
        {
            blockImage.sprite = sprite;
            text.text = name;
            button.onClick.AddListener(clickAction);

            if (isSelected)
            {
                cellImage.color = selectedColor;
                button.interactable = false;
            }
            else
            {
                cellImage.color = normalColor;
                button.interactable = true;
            }
        }
    }
}