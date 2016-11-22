using System;

public class Cannons
{	
	public static CannonProperties[] cannonList = new CannonProperties[]{
	new CannonProperties() { cost=0, cannonPrefab="BasicCannon", baseDamage = 15.0f, baseProjectileSpeed = 10.0f, baseTurnSpeed = 0.2f},
	new CannonProperties() { cost=100,cannonPrefab="BasicCannon", baseDamage = 22.0f, baseProjectileSpeed = 10.0f, baseTurnSpeed = 0.2f},
	new CannonProperties() { cost=200,cannonPrefab="BasicCannon", baseDamage = 30.0f, baseProjectileSpeed = 10.5f, baseTurnSpeed = 0.2f},
	new CannonProperties() { cost=350, cannonPrefab="DoubleBarrelCannon", baseDamage = 22.0f, baseProjectileSpeed = 11.0f, baseTurnSpeed = 0.17f},
	new CannonProperties() { cost=500,cannonPrefab="DoubleBarrelCannon", baseDamage = 25.0f, baseProjectileSpeed = 11.0f, baseTurnSpeed = 0.17f},
	new CannonProperties() { cost=750,cannonPrefab="DoubleBarrelCannon", baseDamage = 28.0f, baseProjectileSpeed = 11.0f, baseTurnSpeed = 0.17f},
	new CannonProperties() { cost=1150,cannonPrefab="TripleBarrelCannon", baseDamage = 20.0f, baseProjectileSpeed = 12.0f, baseTurnSpeed = 0.15f},
	new CannonProperties() { cost=1500,cannonPrefab="TripleBarrelCannon", baseDamage = 25.0f, baseProjectileSpeed = 12.0f, baseTurnSpeed = 0.15f},
	new CannonProperties() { cost=2500,cannonPrefab="TripleBarrelCannon", baseDamage = 30.0f, baseProjectileSpeed = 12.0f, baseTurnSpeed = 0.15f}};


	public static CannonProperties getCannonProperties(int level){
		return cannonList[level];
	}
}

