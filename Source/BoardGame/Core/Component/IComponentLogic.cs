namespace Monkiato.BoardGame.Core.Component
{
    /// <summary>
    /// Custom component logic that defines a specific behavior and interaction for a specific component.
    /// This component logic must be attached to a <see cref="IGenericComponent"/>
    /// logic examples: a score sheet associated to a modular board with its own validations,
    /// an availability logic that runs some internal checks (for components with a single use per turn or per game),
    /// slots available for certain actions, etc. 
    /// </summary>
    public interface IComponentLogic
    {
    }
}