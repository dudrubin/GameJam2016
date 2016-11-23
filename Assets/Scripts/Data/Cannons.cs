using System;

public class Cannons
{	
	public static CannonProperties[] cannonList = new CannonProperties[]{
		new CannonProperties() { projectileType = 1, cost=0, cannonType=1, baseDamage = 15.0f, baseProjectileSpeed = 10.0f, baseTurnSpeed = 1.0f},
		new CannonProperties() { projectileType = 2, cost=100,cannonType=1, baseDamage = 22.0f, baseProjectileSpeed = 10.0f, baseTurnSpeed = 0.9f},
		new CannonProperties() { projectileType = 3, cost=200,cannonType=1, baseDamage = 30.0f, baseProjectileSpeed = 10.5f, baseTurnSpeed = 0.8f},
		new CannonProperties() { projectileType = 1, cost=350, cannonType=2, baseDamage = 22.0f, baseProjectileSpeed = 11.0f, baseTurnSpeed = 1.0f},
		new CannonProperties() { projectileType = 2, cost=500,cannonType=2, baseDamage = 25.0f, baseProjectileSpeed = 11.0f, baseTurnSpeed = 0.9f},
		new CannonProperties() { projectileType = 3, cost=750,cannonType=2, baseDamage = 28.0f, baseProjectileSpeed = 11.0f, baseTurnSpeed = 0.8f},
		new CannonProperties() { projectileType = 1, cost=1150,cannonType=3, baseDamage = 20.0f, baseProjectileSpeed = 12.0f, baseTurnSpeed = 1.0f},
		new CannonProperties() { projectileType = 2, cost=1500,cannonType=3, baseDamage = 25.0f, baseProjectileSpeed = 12.0f, baseTurnSpeed = 0.9f},
		new CannonProperties() { projectileType = 3, cost=2500,cannonType=3, baseDamage = 30.0f, baseProjectileSpeed = 12.0f, baseTurnSpeed = 0.8f}};


	public static CannonProperties getCannonProperties(int level){
		return cannonList[level - 1];
	}

	public static string getPrefabName(int cannonType){
		switch (cannonType) {
		case 1:
			return "BasicCannon";
			break;
		case 2:
			return "DoubleBarrelCannon";
			break;
		case 3:
			return "TripleBarrelCannon";
			break;
		}
		return null;
	}



}

