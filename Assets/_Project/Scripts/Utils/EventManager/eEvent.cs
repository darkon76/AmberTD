public enum eEvent
{
    //Scene Control
    ToMainMenu,
    LoadScene,
    
    //Level Control
    PlayerLose = 1000,
    PointsScored,
    StopLevel,

    RandomEventName,
    Decrease,


    //Level Control parameters.
    EnemyObjectCreated = 2000,
    MAX
}