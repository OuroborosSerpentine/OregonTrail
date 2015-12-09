﻿using System;
using System.Text;
using TrailSimulation.Core;

namespace TrailSimulation.Game
{
    /// <summary>
    ///     Confirm the player wishes to the destroy the current top ten list and reset it back to the hard-coded default
    ///     values.
    /// </summary>
    [ParentWindow(GameWindow.MainMenu)]
    public sealed class EraseCurrentTopTen : InputForm<NewGameInfo>
    {
        /// <summary>
        ///     This constructor will be used by the other one
        /// </summary>
        public EraseCurrentTopTen(IWindow window) : base(window)
        {
        }

        /// <summary>
        ///     Defines what type of dialog this will act like depending on this enumeration value. Up to implementation to define
        ///     desired behavior.
        /// </summary>
        protected override DialogType DialogType
        {
            get { return DialogType.YesNo; }
        }

        /// <summary>
        ///     Fired when dialog prompt is attached to active game Windows and would like to have a string returned.
        /// </summary>
        protected override string OnDialogPrompt()
        {
            var eraseTopTen = new StringBuilder();

            // Text above the table to declare what this state is.
            eraseTopTen.Append($"{Environment.NewLine}Erase Top Ten list{Environment.NewLine}{Environment.NewLine}");

            // Ask the user question if they really want to remove the top ten list.
            eraseTopTen.Append($"If you erase the current Top Ten{Environment.NewLine}");
            eraseTopTen.Append($"list, the names and scores will be{Environment.NewLine}");
            eraseTopTen.Append($"replaced by those on the original{Environment.NewLine}");
            eraseTopTen.Append($"list.{Environment.NewLine}{Environment.NewLine}");

            // Wait for use input...
            eraseTopTen.Append("Do you want to do this? Y/N");
            return eraseTopTen.ToString();
        }

        /// <summary>
        ///     Fired when the dialog receives favorable input and determines a response based on this. From this method it is
        ///     common to attach another state, or remove the current state based on the response.
        /// </summary>
        /// <param name="reponse">The response the dialog parsed from simulation input buffer.</param>
        protected override void OnDialogResponse(DialogResponse reponse)
        {
            // TODO: Actually erase current top ten list.
            SetForm(typeof (ManagementOptions));
        }
    }
}