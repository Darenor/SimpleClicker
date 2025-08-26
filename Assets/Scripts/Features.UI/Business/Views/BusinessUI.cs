using Configs;
using Features.BuyFeature.Components;
using Features.UI.Business.Components;
using Features.UI.UIBase.Data;
using Leopotam.EcsLite;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Features.UI.Business.Views
{
    public class BusinessUI : UIElementView
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI incomeText;
        [SerializeField] private Slider progressBar;
        [SerializeField] private Button levelUpButton;
        [SerializeField] private TextMeshProUGUI levelUpCostText;
        [SerializeField] private Button upgrade1Button;
        [SerializeField] private TextMeshProUGUI upgrade1NameText;
        [SerializeField] private TextMeshProUGUI upgrade1PriceText;
        [SerializeField] private TextMeshProUGUI income1MultiplierText;
        [SerializeField] private Button upgrade2Button;
        [SerializeField] private TextMeshProUGUI upgrade2NameText;
        [SerializeField] private TextMeshProUGUI upgrade2PriceText;
        [SerializeField] private TextMeshProUGUI income2MultiplierText;

        [SerializeField] private NamesConfig namesConfig;

        public override void Init(IEcsSystems ecsSystems)
        {
            base.Init(ecsSystems);
            var businessPool = World.GetPool<BusinessViewComponent>();
            ref var businessViewComponent = ref businessPool.Add(Entity);
            businessViewComponent.BusinessUI = this;
            businessViewComponent.Id = transform.GetSiblingIndex();
            
            levelUpButton.onClick.AddListener(OnLevelUpClick);
            upgrade1Button.onClick.AddListener(() => OnUpgradeClick(1));
            upgrade2Button.onClick.AddListener(() => OnUpgradeClick(2));
        }
        private void OnLevelUpClick()
        {
            var requestEntity = World.NewEntity();
            var requestPool = World.GetPool<BuyLevelRequest>();
            ref var request = ref requestPool.Add(requestEntity);
            request.BusinessId = transform.GetSiblingIndex();
        }

        private void OnUpgradeClick(int upgradeIndex)
        {
            var requestEntity = World.NewEntity();
            var requestPool = World.GetPool<BuyUpgradeRequest>();
            ref var request = ref requestPool.Add(requestEntity);
            request.BusinessId = transform.GetSiblingIndex();
            request.UpgradeIndex = upgradeIndex;
        }

        public void UpdateUI(
            string baseNameKey,
            bool isOwned,
            int level,
            float currentIncome,
            float progress,
            float levelUpCost,
            bool upgrade1Bought,
            bool upgrade2Bought,
            string upgrade1NameKey,
            string upgrade2NameKey,
            float upgrade1Cost,
            float upgrade2Cost,
            float income1Multiplier,
            float income2Multiplier)
        {
            nameText.text = namesConfig.GetName(baseNameKey);
            levelText.text = $"Lvl: {(isOwned ? level.ToString() : "0")}";
            incomeText.text = $"Доход {currentIncome:F0}";
            progressBar.value = progress % 1f;
            levelUpCostText.text = $"Цена:{levelUpCost:F0}";
            levelUpButton.interactable = true;
            upgrade1NameText.text = namesConfig.GetName(upgrade1NameKey);
            upgrade2NameText.text = namesConfig.GetName(upgrade2NameKey);
            income1MultiplierText.text = $"{income1Multiplier} %";
            income2MultiplierText.text = $"{income2Multiplier} %";

            if (upgrade1Bought)
            {
                upgrade1PriceText.text = "Куплено";
                upgrade1Button.interactable = false;
            }
            else
            {
                upgrade1PriceText.text =  $"Цена: {upgrade1Cost:F0}";
                upgrade1Button.interactable = true;
            }

            if (upgrade2Bought)
            {
                upgrade2PriceText.text = "Куплено";
                upgrade2Button.interactable = false;
            }
            else
            {
                upgrade2PriceText.text = $"Цена: {upgrade2Cost:F0}";
                upgrade2Button.interactable = true;
            }
        }
    }
}
