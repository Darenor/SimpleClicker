using Components;
using Features.Income.Components;
using Features.UI.Business.Components;
using Features.UI.UIBase.Data;
using Leopotam.EcsLite;
using TMPro;
using UnityEngine;

namespace Features.UI.Business.Views
{

    public class PlayerBalanceUI : UIElementView
    {
        [SerializeField] private TextMeshProUGUI BalanceText;

        public override void Init(IEcsSystems ecsSystems)
        {
            base.Init(ecsSystems);
            var balancePool = World.GetPool<PlayerBalanceComponent>();
            var balanceViewPool = World.GetPool<BalanceViewComponent>();
            
            var requestBusinessInfoPool = World.GetPool<GetBusinessInfoRequest>();
            requestBusinessInfoPool.Add(World.NewEntity());
            
            var requestIncomePool = World.GetPool<UpdateIncomeRequest>();
            requestIncomePool.Add(World.NewEntity());
            
            ref var balanceComponent = ref balancePool.Add(Entity);
            ref var balanceViewComponent = ref balanceViewPool.Add(Entity);
            balanceViewComponent.PlayerBalanceUI = this;
            balanceComponent.Balance = 0f;
        }

        public void UpdateBalance(float balance)
        {
            BalanceText.text = $"Баланс: {balance:F0}$";
        }
    }
}