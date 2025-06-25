using BlueRacconGames.Cards;
using Game.Managers;
using System;
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

        public void SetupPanel(SelectCardManager selectCardManager, CardFactorySO cardData)
        {
            ResetButton();

            nameTxt.text = cardData.Name;
            descriptionTxt.text = cardData.Description;
            icon.sprite = cardData.Icon;
            levelTxt.text = "1";

            OnClickE += () => selectCardManager.OnSelectCard(cardData);

            SetInteractable(true);
        }
    }
}