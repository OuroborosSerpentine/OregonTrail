﻿// Created by Ron 'Maxwolf' McDowell (ron.mcdowell@gmail.com) 
// Timestamp 12/31/2015@4:38 AM

namespace TrailSimulation
{
    using System;
    using System.Text;
    using SimUnit;
    using SimUnit.Form;
    using SimUnit.Form.Input;

    /// <summary>
    ///     Prompts the player with a yes or no question regarding if they would like to use the services offered by the Indian
    ///     guide. However, he requires sets of clothing and not money like the ferry operator. If they player does not have
    ///     enough clothing in their inventory then the message will say so here since there is no opportunity to trade once
    ///     you are actually at the river crossing. The amount of clothing he asks for will also change based on the amount of
    ///     animals killed while hunting.
    /// </summary>
    [ParentWindow(typeof (Travel))]
    public sealed class IndianGuidePrompt : InputForm<TravelInfo>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="IndianGuidePrompt" /> class.
        ///     This constructor will be used by the other one
        /// </summary>
        /// <param name="window">The window.</param>
        public IndianGuidePrompt(IWindow window) : base(window)
        {
        }

        /// <summary>
        ///     Changes up the behavior of the input dialog based on if the player has enough clothes to trade the Indian guide.
        /// </summary>
        protected override DialogType DialogType
        {
            get { return HasEnoughClothingToTrade ? DialogType.YesNo : DialogType.Prompt; }
        }

        /// <summary>
        ///     Determines if the player has enough clothing to trade the Indian guide for his services in crossing the river.
        /// </summary>
        private bool HasEnoughClothingToTrade
        {
            get
            {
                return GameSimulationApp.Instance.Vehicle.Inventory[Entities.Clothes].Quantity >=
                       UserData.River.IndianCost;
            }
        }

        /// <summary>
        ///     Only allows input from the player if they have enough clothing to trade with the Indian guide, otherwise we will
        ///     treat this as a prompt only and no input.
        /// </summary>
        public override bool InputFillsBuffer
        {
            get { return HasEnoughClothingToTrade; }
        }

        /// <summary>
        ///     Fired when dialog prompt is attached to active game Windows and would like to have a string returned.
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        protected override string OnDialogPrompt()
        {
            // Builds up the first part about the Indian guide for river crossing.
            var _prompt = new StringBuilder();
            _prompt.AppendLine($"{Environment.NewLine}A Shoshoni guide says that he");
            _prompt.AppendLine("will take your wagon across");
            _prompt.AppendLine($"the river in exchange for {UserData.River.IndianCost.ToString("N0")}");
            _prompt.AppendLine($"sets of clothing.{Environment.NewLine}");

            // Change up the message based on if the player has enough clothing, they won't be able to get more if they don't here.
            if (HasEnoughClothingToTrade)
            {
                // Player has enough clothing to satisfy the Indians cost.
                _prompt.AppendLine("Will you accept this");
                _prompt.Append("offer? Y/N");
            }
            else
            {
                // Player does not have enough clothing to satisfy the Indian cost.
                _prompt.AppendLine($"You don't have {UserData.River.IndianCost.ToString("N0")} sets of");
                _prompt.AppendLine($"clothing.{Environment.NewLine}");
            }

            // Renders out the Indian guide river crossing confirmation and or denial.
            return _prompt.ToString();
        }

        /// <summary>
        ///     Fired when the dialog receives favorable input and determines a response based on this. From this method it is
        ///     common to attach another state, or remove the current state based on the response.
        /// </summary>
        /// <param name="reponse">The response the dialog parsed from simulation input buffer.</param>
        protected override void OnDialogResponse(DialogResponse reponse)
        {
            // Depending on if the player has enough clothing their response to Indian guide changes.
            if (HasEnoughClothingToTrade)
            {
                // Only care about the user response if they actually have
                switch (reponse)
                {
                    case DialogResponse.Yes:
                        UserData.River.CrossingType = RiverCrossChoice.Indian;
                        SetForm(typeof (UseIndianConfirm));
                        break;
                    case DialogResponse.No:
                    case DialogResponse.Custom:
                        CancelIndianCrossing();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(reponse), reponse, null);
                }
            }
            else
            {
                // Returns back to the river cross choice menu.
                CancelIndianCrossing();
            }
        }

        /// <summary>
        ///     Player does not have enough clothing to satisfy the Indian cost.
        /// </summary>
        private void CancelIndianCrossing()
        {
            UserData.River.CrossingType = RiverCrossChoice.None;
            SetForm(typeof (RiverCross));
        }
    }
}