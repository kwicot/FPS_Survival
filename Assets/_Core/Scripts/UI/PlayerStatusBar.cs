using _Core.Scripts.Player;
using TMPro;
using UnityEngine;

namespace _Core.Scripts.UI
{
    public class PlayerStatusBar : MonoBehaviour
    {
        [SerializeField] private PlayerStatus playerStatus;

        [SerializeField] private TextMeshProUGUI staminaMaxText;
        [SerializeField] private TextMeshProUGUI staminaText;
        
        [SerializeField] private TextMeshProUGUI healthMaxText;
        [SerializeField] private TextMeshProUGUI healthText;


        private void Update()
        {
            staminaMaxText.text = ((int)playerStatus.MaxStamina).ToString();
            staminaText.text = ((int)playerStatus.Stamina).ToString();

            healthMaxText.text = ((int)playerStatus.MaxHealth).ToString();
            healthText.text = ((int)playerStatus.Health).ToString();
        }
    }
}