using Godot;
using System;
using System.Collections.Generic;

/*
    Crow Characteristics
    - Can move around entire board
    - After another piece dies, it can kill in square next to it UDLR
*/
public class Crow : ChessPiece
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public List<BoardTile> legalMoves = new List<BoardTile>();
    public List<BoardTile> illegalMoves = new List<BoardTile>();

    private RayCast2D raycastLeft;
    private RayCast2D raycastRight;
    private RayCast2D raycastUp;
    private RayCast2D raycastDown;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //set color
        //BoardManager board = BoardManager.GlobalBoard;
        //tileLocation = board.GetNode<BoardTile>("TileContainer/A1");
        //tileLocation = BoardManager.GlobalBoard.GetNode<BoardTile>("TileContainer/A1");
        raycastLeft = GetNode<RayCast2D>("RayCastLeft");
        raycastRight = GetNode<RayCast2D>("RayCastRight");
        raycastUp = GetNode<RayCast2D>("RayCastUp");
        raycastDown = GetNode<RayCast2D>("RayCastDown");
        Position = tileLocation.GetNode<Position2D>("Center").GlobalPosition;
        isPickedUp = false;
        canKill = true;
        this.Connect("Dead", BoardManager.GlobalBoard, "_on_death");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        /*if(!isPickedUp){
            Position = tileLocation.GetNode<Position2D>("Center").GlobalPosition;
        }*/
        if(Input.IsActionPressed("click") && canPickup){
            Position = GetGlobalMousePosition();
            //this.canKill = true;
            isPickedUp = true;
        }else if(Input.IsActionJustReleased("click") && isPickedUp){
            isPickedUp = false;
            Position = tileLocation.GetNode<Position2D>("Center").GlobalPosition;
            legalMoves.Clear();
            //Find all next legalSpots for piece
            //crow can move over entire board
            illegalMoves = BoardManager.GlobalBoard.locations4PiecesOnBoard;
            if(canKill){
                if(raycastLeft.IsColliding()){
                    GD.Print("CollidingLeft");
                    killCollidingPiece(raycastLeft);
                }else if(raycastRight.IsColliding()){
                    GD.Print("CollidingRight");
                    killCollidingPiece(raycastRight);
                }else if(raycastUp.IsColliding()){
                    GD.Print("CollidingUp");
                    killCollidingPiece(raycastUp);
                }else if(raycastDown.IsColliding()){
                    GD.Print("CollidingDown");
                    killCollidingPiece(raycastDown);
                }else{
                    GD.Print("NoColliding");
                }
                canKill = false;
            }
            
        }else if(!isPickedUp){
            Position = tileLocation.GetNode<Position2D>("Center").GlobalPosition;
        }
    }

    public void killCollidingPiece(RayCast2D rayCast){
        ChessPiece kill = (ChessPiece)rayCast.GetCollider();
        if(kill.color != this.color){
            kill.EmitSignal("Dead", kill);
        }
        //kill.QueueFree();
        //canKill = false;
    }

    public void _on_Crow_mouse_entered(){
        canPickup = true;
    }

    public void _on_Crow_mouse_exited(){
        canPickup = false;
    }

    public void _on_Crow_Dead(){
        QueueFree();
    }

    public void _on_death(ChessPiece piece){
        int count = BoardManager.GlobalBoard.GetChildCount();
        for(int i = 0; i < count; i++){
            if(BoardManager.GlobalBoard.GetChild(i) is Crow){
                Crow crow = BoardManager.GlobalBoard.GetChild(i) as Crow;
                if(crow.color == piece.color){
                    crow.canKill = true;
                }
            }
        }
    }
}
