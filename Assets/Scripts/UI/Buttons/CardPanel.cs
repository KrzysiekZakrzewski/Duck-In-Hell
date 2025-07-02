using Game.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlueRacconGames.UI
{
    public class CardPanel : UIButtonBase
    {
        [SerializeField] private TextMeshProUGUI nameTxt;
        [SerializeField] private TextMeshProUGUI descriptionTxt;
        [SerializeField] private TextMeshProUGUI levelTxt;
        [SerializeField] private Image icon;

        public void SetupPanel(SelectCardManager selectCardManager, SelectCardData cardData)
        {
            ResetButton();

            var cardFactory = cardData.CardFactory;

            nameTxt.text = cardFactory.Name;
            descriptionTxt.text = cardFactory.Description;
            icon.sprite = cardFactory.Icon;
            levelTxt.text = cardData.InInventoryLvl.ToString();

            OnClickE += () => selectCardManager.OnSelectCard(cardFactory);

            SetInteractable(true);
        }
    }
}