﻿using System;
using System.Text;
using TrailSimulation.Core;

namespace TrailSimulation.Game
{
    /// <summary>
    ///     Attached when the player wants to continue on the trail, and doing so will force them to leave that point and be
    ///     back on the trail counting up distance traveled until they reach the next one. The purpose of this state is to
    ///     inform the player of the next points name, the distance away that it is, and that is all it will close and
    ///     simulation resume after return key is pressed.
    /// </summary>
    [ParentWindow(GameWindow.Travel)]
    public sealed class LocationDepart : InputForm<TravelInfo>
    {
        /// <summary>
        ///     This constructor will be used by the other one
        /// </summary>
        public LocationDepart(IWindow window) : base(window)
        {
        }

        /// <summary>
        ///     Fired when dialog prompt is attached to active game Windows and would like to have a string returned.
        /// </summary>
        protected override string OnDialogPrompt()
        {
            // Tell player how far it is to next location before attaching drive state.
            var prompt = new StringBuilder();
            var nextPoint = GameSimulationApp.Instance.Trail.NextLocation;
            prompt.AppendLine(
                $"{Environment.NewLine}From {GameSimulationApp.Instance.Trail.CurrentLocation.Name} it is {GameSimulationApp.Instance.Trail.DistanceToNextLocation}");
            prompt.AppendLine($"miles to {nextPoint.Name}{Environment.NewLine}");
            return prompt.ToString();
        }

        /// <summary>
        ///     Fired when the dialog receives favorable input and determines a response based on this. From this method it is
        ///     common to attach another state, or remove the current state based on the response.
        /// </summary>
        /// <param name="reponse">The response the dialog parsed from simulation input buffer.</param>
        protected override void OnDialogResponse(DialogResponse reponse)
        {
            SetForm(typeof (DriveTrail));
        }
    }
}