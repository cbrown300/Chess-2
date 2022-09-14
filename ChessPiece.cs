using Godot;
using System;

public abstract class ChessPiece : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    //public Vector2 previousMousePosition = new Vector2();
    //public bool canPickup = false;
    public string color;
    public bool canPickup;
    public bool isPickedUp;
    public BoardTile tileLocation;
    public bool canKill;
    //[Signal]
    //public delegate void Dead(ChessPiece piece);

    // Called when the node enters the scene tree for the first time.
    //public override void _Ready()
    //{
        //set color
    //}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //public override void _Process(float delta)
    //{
        
    //}

    public void updatePosition(BoardTile location){
        tileLocation = location;
        //Position = location.GetNode<Position2D>("Center").GlobalPosition;
    }

}
