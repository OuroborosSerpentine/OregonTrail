﻿using System;
using System.Text;
using TrailSimulation.Entity;
using TrailSimulation.Game;

namespace TrailSimulation.Event
{
    /// <summary>
    ///     Called when one of your party members dies that is not the leader of the group, the game will still be able to
    ///     continue without this person.
    /// </summary>
    [DirectorEvent(EventCategory.Person, EventExecution.ManualOnly)]
    public sealed class DeathCompanion : EventProduct
    {
        private StringBuilder _passengerDeath;

        /// <summary>
        ///     Creates a new instance of an event product with the specified event type for reference purposes.
        /// </summary>
        public DeathCompanion()
        {
            _passengerDeath = new StringBuilder();
        }

        /// <summary>
        ///     Fired when the event handler associated with this enum type triggers action on target entity. Implementation is
        ///     left completely up to handler.
        /// </summary>
        /// <param name="sourceEntity">
        ///     Entities which the event is going to directly affect. This way there is no confusion about
        ///     what entity the event is for. Will require casting to correct instance type from interface instance.
        /// </param>
        public override void Execute(IEntity sourceEntity)
        {
            // Cast the source entity as a passenger from vehicle.
            var sourcePerson = sourceEntity as Person;
            if (sourcePerson == null)
                throw new ArgumentNullException(nameof(sourceEntity),
                    "Could not cast source entity as passenger of vehicle.");

            // Check to make sure this player is not the leader (aka the player).
            if (sourcePerson.IsLeader)
                throw new ArgumentException("Cannot kill this person because it is the player!");

            _passengerDeath.AppendLine($"{sourcePerson.Name} has died.");
        }

        /// <summary>
        ///     Fired when the simulation would like to render the event, typically this is done AFTER executing it but this could
        ///     change depending on requirements of the implementation.
        /// </summary>
        /// <param name="sourceEntity"></param>
        /// <returns>Text user interface string that can be used to explain what the event did when executed.</returns>
        protected override string OnRender(IEntity sourceEntity)
        {
            return _passengerDeath.ToString();
        }
    }
}