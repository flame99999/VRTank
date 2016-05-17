using UnityEngine;
using System.Collections;

public class Globals {

	public enum matID {
		CONCRETE = 0,
		BRICK,
		DIRT,
		FLESH,
		FOLIAGE,
		GLASS,
		METAL,
		MUD,
		PAPER,
		PLASTER,
		PLASTIC,
		SAND,
		STONE,
		WATER,
		WOOD
	};

	public enum damageType 
	{
		CRUSH = 0,
		BULLET,
		EXPLOSIVE,
		FIRE,
	};

	public enum healthState
	{
		ALIVE = 0,
		DEAD,
		INVINCIBLE,
	};

	public static matID GetMaterialID( PhysicMaterial physMat )
	{
		if ( physMat == null )
			return matID.CONCRETE;

		Debug.Log ( physMat.name );

		if ( physMat == null )
			return matID.CONCRETE;
		else if ( string.Equals( physMat.name, "Concrete" )	)
			return matID.CONCRETE;
		else if ( string.Equals( physMat.name, "Dirt" )	||	string.Equals( physMat.name, "Grass" )		)
			return matID.DIRT;
		else if ( string.Equals( physMat.name, "Sand" )	)
			return matID.SAND;
		else if ( string.Equals( physMat.name, "Stone" )	)
			return matID.STONE;
		else if ( string.Equals( physMat.name, "Brick" )	)
			return matID.BRICK;
		else if ( string.Equals( physMat.name, "Mud" )	)
			return matID.MUD;
		else if ( string.Equals( physMat.name, "Flesh" )	)
			return matID.FLESH;
		else if ( string.Equals( physMat.name, "Foliage" )	)
			return matID.FOLIAGE;
		else if ( string.Equals( physMat.name, "Glass" ) || string.Equals( physMat.name, "CarGlass" )	)
			return matID.GLASS;	
		else if ( string.Equals( physMat.name, "Metal" ) || string.Equals( physMat.name, "CarMaterial" )	)
			return matID.METAL;
		else if ( string.Equals( physMat.name, "Paper" )	)
			return matID.PAPER;
		else if ( string.Equals( physMat.name, "Plaster" )	)
			return matID.PLASTER;
		else if ( string.Equals( physMat.name, "Plastic" )	)
			return matID.PLASTIC;
		else if ( string.Equals( physMat.name, "Water" )	)
			return matID.WATER;
		else if ( string.Equals( physMat.name, "Wood" )	)
			return matID.WOOD;
		else
			return matID.CONCRETE;
	}
}