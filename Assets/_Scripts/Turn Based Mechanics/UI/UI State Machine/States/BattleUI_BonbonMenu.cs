using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class BattleUIStateMachine {
    public class BattleUI_BonbonMenu : BattleUIStateMachine.BattleUIState {
        public override void Enter(BattleUIStateInput i) {
            base.Enter(i);
            RunPreAnimation();
        }
        
        public override void Update() {
            base.Update();
            int input = MySM.CheckInput();
            if (input == 0 || input == 2) {
                Debug.Log("checking input");
                Input.AnimationHandler.bonbonWindow.BonbonSelect(input != 0);
            } else if (input == 1) {
                if (!Input.AnimationHandler.bonbonWindow.bonbonOperationEnabled) {
                    Input.AnimationHandler.bonbonWindow.ToggleBonbonOperations(true);
                }
                else {
                    Input.AnimationHandler.ingredientWindow.slot = Input.AnimationHandler.bonbonWindow.mainButtonIndex;
                    Input.AnimationHandler.bonbonWindow.ToggleBonbonOperations(false);
                    MySM.Transition<BattleUI_IngredientSelect>();
                }
            } else if (input == 3) {
                if (Input.AnimationHandler.bonbonWindow.bonbonOperationEnabled)
                    Input.AnimationHandler.bonbonWindow.ToggleBonbonOperations(false);
                else MySM.Transition<InitUIState>();
            }
        }

        public override void Exit(BattleUIStateInput i) {
            base.Exit(i);
            RunPostAnimation();
        }
        
        #region Animations

        protected override void RunPreAnimation() {
            base.RunPreAnimation();
            Input.AnimationHandler.bonbonWindow.Initialize(MySM._battleStateMachine.CurrInput.ActiveActor().BonbonInventory);
        }

        protected override void RunPostAnimation() {
            base.RunPostAnimation();
            Input.AnimationHandler.bonbonWindow.ToggleMainDisplay(false);
        }

        #endregion Animations
    }
}
