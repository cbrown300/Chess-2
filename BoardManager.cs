using Godot;
using System;
using System.Collections.Generic;

public class BoardManager : Node2D
{
    public static BoardManager GlobalBoard;
    public GridContainer BoardGrid;
    private Area2D Tile; 

    public List<BoardTile> locations4PiecesOnBoard = new List<BoardTile>();

    [Export]
    public PackedScene TileScene;
    [Export]
    public PackedScene CrowPiece;
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    //public BoardTile targetTile;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //setup baord here
        //GlobalBoard = this;
        NewGame();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
    }

    public void NewGame(){
        GlobalBoard = this;
        BoardGrid = GetNode<GridContainer>("TileContainer");
        //var boardTile = GetNode<Area2D>("BoardTile");
        int num = 8;
        char letter = 'A';
        for(int i = 0; i < 64; i++){
            var boardTile = (BoardTile)TileScene.Instance();
            string name = letter + num.ToString();
            letter++;
            if(letter == 'I'){
                num--;
                letter = 'A';
            }
            boardTile.Name = name;
            BoardGrid.AddChild(boardTile);
            //setup pieces
            if(name == "A8" || name == "H8"){
                //ChessPiece piece = new Crow();
                var crow = (Crow)CrowPiece.Instance();
                //crow.updatePosition(BoardGrid.GetNode<BoardTile>(name));
                //crow.Position = boardTile.GetNode<Position2D>("Center").Position;
                crow.tileLocation = BoardGrid.GetNode<BoardTile>(name);
                GD.Print("name: " + name);
                GD.Print("Initial tile location: " + crow.tileLocation.ToString());
                //crow.updatePosition(crow.tileLocation);
                //crow.Position = boardTile.GetNode<Position2D>("Center").Position;
                GlobalBoard.AddChild(crow);
                crow.Position = boardTile.GetNode<Position2D>("Center").Position;
                //crow.updatePosition(crow.tileLocation);
                locations4PiecesOnBoard.Add(crow.tileLocation);
                crow.Connect("Dead", this, "_on_death");
                crow.color = "black";
                //piece.updatePosition(boardTile);
            }
            if(name == "A1" || name == "H1"){
                //ChessPiece piece = new Crow();
                var crow = (Crow)CrowPiece.Instance();
                //crow.updatePosition(BoardGrid.GetNode<BoardTile>(name));
                //crow.Position = boardTile.GetNode<Position2D>("Center").Position;
                crow.tileLocation = BoardGrid.GetNode<BoardTile>(name);
                GD.Print("name: " + name);
                GD.Print("Initial tile location: " + crow.tileLocation.ToString());
                //crow.updatePosition(crow.tileLocation);
                //crow.Position = boardTile.GetNode<Position2D>("Center").Position;
                GlobalBoard.AddChild(crow);
                crow.Position = boardTile.GetNode<Position2D>("Center").Position;
                //crow.updatePosition(crow.tileLocation);
                locations4PiecesOnBoard.Add(crow.tileLocation);
                //crow.Connect("Dead", GlobalBoard, "_on_death");
                crow.color = "white";
                Texture image = ResourceLoader.Load("res://Resources/White Rook (Crow).png") as Texture;
                crow.GetNode<Sprite>("Sprite").Texture = image;
                //piece.updatePosition(boardTile);
            }
        }
        //set up pieces
        //ChessPiece piece = new Crow();
        //piece.AddPiece(BoardTile);
    }

    public void _on_death(ChessPiece piece){
        int count = GlobalBoard.GetChildCount();
        for(int i = 0; i < count; i++){
            if(GlobalBoard.GetChild(i) is Crow){
                Crow crow = GlobalBoard.GetChild(i) as Crow;
                if(crow.color == piece.color){
                    crow.canKill = true;
                }
            }
        }
    }
}
