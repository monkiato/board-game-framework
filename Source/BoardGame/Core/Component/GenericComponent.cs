using System.Collections.Generic;
using Monkiato.BoardGame.Core.Exceptions;

namespace Monkiato.BoardGame.Core.Component
{
    /// <inheritdoc cref="IGenericComponent"/>
    public abstract class GenericComponent : IGenericComponent
    {
        private IReadOnlyDictionary<string, IComponentLogic> _logicMap;

        /// <summary>
        /// Initializes component, it will be ready to be used only after Init is called
        /// </summary>
        /// <param name="logicMap">read only component logic map associated to the extending component</param>
        protected void Init(IReadOnlyDictionary<string, IComponentLogic> logicMap)
        {
            _logicMap = logicMap;
        }

        /// <inheritdoc cref="IGenericComponent.AvailableLogicNames"/>
        public IEnumerable<string> AvailableLogicNames => _logicMap.Keys;

        public IComponentLogic GetLogic(string key)
        {
            return _logicMap.ContainsKey(key) ? _logicMap[key] : null;
        }
        
        public T GetLogic<T>() where T : IComponentLogic
        {
            foreach (var keyValuePair in _logicMap)
            {
                if (keyValuePair.Value is T logic)
                {
                    return logic;
                }
            }

            throw new LogicNotFoundException(GetType(), typeof(T));
        }
    }
}