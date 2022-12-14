using Godot;
using System;

public class BoardTile : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var sprite = GetNode<Sprite>("Sprite");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void _on_Area2D_area_entered(ChessPiece piece){
        GD.Print("Body entered: " + piece.ToString());
        if(piece.isPickedUp){
            //ChessPiece piece = obj as ChessPiece;
            piece.updatePosition(this);
            //piece.tileLocation = this;
            GD.Print("Location Set: " + this.ToString());
            //var pos = RectPosition;
            //piece.Position = pos;
        }
    }
}
