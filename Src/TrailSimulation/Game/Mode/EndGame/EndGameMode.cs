﻿using System;
using TrailSimulation.Core;

namespace TrailSimulation.Game
{
    /// <summary>
    ///     Attached when the party leader dies, or the vehicle reaches the end of the trail.
    /// </summary>
    public sealed class EndGameMode : ModeProduct<EndGameCommands, EndGameInfo>
    {
        /// <summary>
        ///     Defines the current game mode the inheriting class is going to take responsibility for when attached to the
        ///     simulation.
        /// </summary>
        public override Mode Mode
        {
            get { return Mode.EndGame; }
        }

        /// <summary>
        ///     Called after the mode has been added to list of modes and made active.
        /// </summary>
        public override void OnModePostCreate()
        {
        }

        /// <summary>
        ///     Called when the mode manager in simulation makes this mode the currently active game mode. Depending on order of
        ///     modes this might not get called until the mode is actually ticked by the simulation.
        /// </summary>
        public override void OnModeActivate()
        {
        }

        /// <summary>
        ///     Fired when the simulation adds a game mode that is not this mode. Used to execute code in other modes that are not
        ///     the active mode anymore one last time.
        /// </summary>
        public override void OnModeAdded()
        {
            throw new NotImplementedException();
        }
    }
}