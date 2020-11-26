using System.Collections.Generic;

namespace Monkiato.BoardGame.Core.Component
{
    /// <summary>
    /// Used for custom game components like modular boards (where each module is a <see cref="GenericComponent"/>)
    /// or other components to interact with the modular board, as well for player components that are not
    /// strictly units (see <see cref="Monkiato.BoardGame.Core.Unit"/> namespace) or cards (see
    /// <see cref="Monkiato.BoardGame.Core.Cards"/> namespace) 
    /// </summary>
    public interface IGenericComponent
    {
        /// <summary>
        /// A Generic component is expected to contain one or more <see cref="IComponentLogic"/> associated.
        /// This getter provides the list of available component logic names.
        /// </summary>
        IEnumerable<string> AvailableLogicNames { get; }

        /// <summary>
        /// Gets a component logic by key
        /// </summary>
        /// <param name="key">component logic key name</param>
        /// <returns>the component logic if found, otherwise returns null</returns>
        IComponentLogic GetLogic(string key);
        
        /// <summary>
        /// Finds a component logic by its type and cast it to the corresponding implementation.
        /// component logic must implement <see cref="IComponentLogic"/>
        /// </summary>
        /// <typeparam name="T">type of component logic to be returned</typeparam>
        /// <returns>the component logic if found, otherwise returns null</returns>
        T GetLogic<T>() where T : IComponentLogic;
    }
}