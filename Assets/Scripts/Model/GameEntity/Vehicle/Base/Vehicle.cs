using System;
using System.Collections.Generic;

public abstract partial class Vehicle : GameEntity, ITradeable, IUpdateable
{
    private Map _map;
    public Queue<Facility> Route { get; private set; }
    private Facility Destination { get; set; }
    /// <summary>
    /// Brand new price
    /// </summary>
    public int Cost { get; }
    ///// <summary>
    ///// X coord on the map
    ///// </summary>
    //public int? X { get; private set; }
    ///// <summary>
    ///// Y coord on the map
    ///// </summary>
    //public int? Y { get; private set; }
    /// <summary>
    /// Current worth: Cost * Condition * 0.01 * 0.5.
    /// For example: When the condition is 50%, then it'll worth 25% of the brand new price.
    /// </summary>
    public double Worth => Math.Round(Cost * Condition * 0.01 * 0.5, 2);
    /// <summary>
    /// Repair cost: Cost * Condition * 0.01.
    /// </summary>
    public double RepairCost => Math.Round(Cost * Condition * 0.01, 2);
    public double Speed { get; init; }
    private double _condition;
    /// <summary>
    /// 10 seconds
    /// </summary>
    private const double _updateCondInterval = 10.0;
    /// <summary>
    /// It accumulates the seconds during every update. If >= 10, then condition updates, and -= _updateCondInterval
    /// </summary>
    private double _elapsedTimeSinceLastUpdate;
    private double _timeUntilNextStep;
    /// <summary>
    /// Seconds to wain until the next step.
    /// </summary>
    private double WaitTime => Speed / 15;
    /// <summary>
    /// Vehicles condition in % ([0-100] double value).
    /// </summary>
    public double Condition
    {
        get => _condition;
        private set
        {
            if (value < 0 || value > 100)
                throw new VehicleConditionException("Condition only can be between 0 and 100!");
            _condition = value;
        }
    }
    public Direction Direction { get; set; }
    protected Vehicle(int cost, double speed, Map map)
    {
        _map = map;
        Condition = 100;
        Cost = cost;
        Speed = speed;
        _elapsedTimeSinceLastUpdate = 0.0;
        _timeUntilNextStep = 0.0;
        Route = new();
    }
    /// <summary>
    /// Adds vehicle to the vehicles list
    /// </summary>
    /// <param name="player"></param>
    public void Purchase(Player player)
    {
        player.Money -= Cost;
        player.Vehicles.Add(this);
    }
    /// <summary>
    /// Removes vehicle from the vehicles list
    /// </summary>
    /// <param name="player"></param>
    public void Sell(Player player)
    {
        player.Money += Worth;
        player.Vehicles.Remove(this);
    }
    public void Repair(Player player)
    {
        player.Money -= RepairCost;
        Condition = 100;
    }
    /// <summary>
    /// Update runs in every _updateCondInterval (10) seconds
    /// </summary>
    /// <param name="deltaTime"></param>
    public void Update(double deltaTime)
    {
        _elapsedTimeSinceLastUpdate += deltaTime;
        _timeUntilNextStep += deltaTime;
        if (_elapsedTimeSinceLastUpdate >= _updateCondInterval)
        {
            _elapsedTimeSinceLastUpdate -= _updateCondInterval;
            Condition -= 0.01;
        }

        if (_timeUntilNextStep >= WaitTime)
        {
            _timeUntilNextStep -= WaitTime;
            Move(_map, deltaTime);
        }


    }

    
}