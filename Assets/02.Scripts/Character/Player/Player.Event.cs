using System;

public partial class Player : Character
{
    public event Action StatusChanged;
    public event Action LevelUped;
}
