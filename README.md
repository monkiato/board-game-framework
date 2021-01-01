# Board Game Framework

Toolkit to create digital board games. The framework contains a base structure and tools  
that can be used to create many type of board games with boards, cards, custom rules, dice, etc.

### Components

- Cards and decks
- Dice
- Custom boards & Map tile support **(in-progress)**
- Units & miniatures, a representation of any kind of unit within the game 
- Players
- Game state flow (including game state controller, actions & state executor detailed below)
- Player turn controller (for turn-based games)

### Cards and decks

How to create a deck of cards using existing GenericCard class:

```
```csharp
var cards = new List<ICard>
{
    new GenericCard(new GenericCardInfo {
        Name = "Thunder",
        Type = MyGameCardType.White,
        LoreText = "Lorem ipsum...",
        AbilityText = "Lorem ipsum...",
        FrontBackgroundImage = "thunder.png",
        // there are more properties to fulfill...
    }),
    new GenericCard(new GenericCardInfo {
        Name = "Rain",
        Type = MyGameCardType.Red,
        LoreText = "Lorem ipsum...",
        AbilityText = "Lorem ipsum...",
        FrontBackgroundImage = "rain.png",
        // there are more properties to fulfill...
    }),
};

_deck = new Deck(cards);
```

Some of the main deck functionalities:

```
_deck.Shuffle()
_deck.Count()
ICard card = _deck.DrawTop() //draw top card only
List<ICard> cards = _deck.DrawTop(5) //draw 5 cards from the top
_deck.AddTop(card);
_deck.FindCard(MyGameCardType.White);
_deck.FindCard("Rain");
```

More details in [IDeck.cs](Source/BoardGame/Core/Cards/IDeck.cs)

### Dice

Pre-created D4, D6, D8, D10, D12 and D20 are available.

More details in [Dice/](Source/BoardGame/Core/Dice/) folder
  
### Game State Flow

The game state flow contains 3 main components: game state controller, actions and state executors.

#### Game State Controller

The game state controller is an abstract implementation that requires to be extended mainly to declare
the different states the game will use and executors associated to each state, as well for other custom
components required to control the game logic.

The abstract controller contains an internal player turn controller that works based on the list of player passed in the constructor. 

How to extend the game state controller:

```csharp

public class ChessGameState : GameState<ChessGameStates>
{
}

public class ChessGameStateController : GameStateController<ChessGameStates>
{
    private Board _board;
 
    public ChessGameStateController(List<IPlayer> players) : base(new ChessGameState(), players)
    {
        // Add your custom initialization here
        _board = new Board();

        InitStateExecutors(new Dictionary<Enum, IStateExecutor<ChessGameStates>>
        {
            {ChessGameStates.WaitingPlayersReady, new WaitingPlayersReadyExecutor(_board)},
            {ChessGameStates.WaitingPlayerMove, new EmptyExecutor()},
            {ChessGameStates.PlayerMoving, new PlayerMovingExecutor(_board)},
            ...
        });
    }
}
```

#### Actions

Actions represent player interactions as moving a unit, draw a card, use resources, etc.

Every action must extend [IAction interface](Source/BoardGame/Core/Actions/IAction.cs).

Some general rules to keep the flow organized:

 - Actions themselves must ensure are valid through `IAction.IsLegal(gameState)` method.
 - Actions must be executed through `IAction.Apply(gameState)` method after checking it's a valid move.
 - Actions are not intended to modify game components directly, they must validate and update the state only through `gameState.UpdateState(...)`. The state executor will perform component and internal state updates.

How tu use actions:

```csharp
IGameState<ChessGameState> gameState = ...//obtain current game state from your main game manager

var action = new MovePieceAction(Board board, Position from, To to);
if (action.IsLegal(gameState))
{
    action.Apply(gameState); // trigger game state "PlayerMoving"
}
```

#### State Executor

Every game state is associated with a state executor that will perform a specific action when that state change is triggered.
If no action is required, an empty state executor can be implemented. Also state executors can process and move to a different
state immediately.

State executor examples:

```csharp
/*
    Executor triggered when a player is moving a piece
*/
public class PlayerMovingExecutor : IStateExecutor<ChessGameStates>
{
    private Board _board;
    private PlayerTurnController _playerTurnController;

    public PlayerMovingExecutor(Board board, PlayerTurnController playerTurnController)
    {
        _board = board;
        _playerTurnController = playerTurnController;
    }

    protected override void Execute(IGameState<ChessGameStates> gameState, object sender,
        IDictionary<string, object> data)
    {
        var from = (Position) data[MovePieceAction.FromKey];
        var to = (Position) data[MovePieceAction.ToKey];

        // Run more validations if necessary

        _board.MovePiece(from, to);

        if (_board.IsCheckMate())
        {
            gameState.UpdateState(MyGameStates.EndGameConditionTriggered);
            return;
        }

        _playerTurnController.EndTurn(_playerTurnController.CurrentPlayerTurn);
        gameState.UpdateState(ChessGameStates.EndGameConditionTriggered);
    }
}
```

```csharp
/*
    Empty exectutor used for idle states where the game is waiting a specific player action updating the game state 
*/
public class EmptyExecutor : AbstractExecutor
{
    public EmptyExecutor() : base(null, null)
    {
    }

    protected override void DoExecute(IGameState<ChessGameStates> gameState, object sender,
        IDictionary<string, object> data)
    {
    }
}
```
