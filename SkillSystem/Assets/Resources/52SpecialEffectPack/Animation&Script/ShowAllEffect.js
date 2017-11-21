var EffectName = ["BlueBall","DeadBall","DeadExplode","ElementalArrow","ElementalArrow2","ElementalBal","ElementalBall2","ElementalBall3","ElementalBall4","Explode","Explode2","Explode3","Explode4","Explode5","Explode6","Explode7","Explode8","Explode9","Explode10","Explode11","FireBall","FlameEmission","Hole","IceBall","IceCloud","Kunai","Kunai2","Kunai3","Kunai4","Kunai5","LaserFire","LaserFire2","LightningArrow","LightningBall","LightningBall2","LightningRotateBall","MagicCircleExplode","MagicCircleRelease","MagicCube","PosionExplode","Portal","Portal2","RainBowExplode","RainBowExplode2","RuneOfMagicCircle","StarCore","StormCloud","Strom","SummonMagicCircle","SummonMagicCircle2","SummonMagicCircle3","Swamp"];
var Effect = new Transform[53];
var Text1 : GUIText;
var i : int = 0;

function Start(){var obj = Instantiate(Effect[i], Vector3(0,0,0),Quaternion.identity);}

function Update () {

	Text1.text = i+1 + ":" +EffectName[i];
	
	if(Input.GetKeyDown(KeyCode.Z))
	{
		if(i<=0)
			i= 51;

		else
			i--;
		
		var obz = Instantiate(Effect[i], Vector3(0,0,0),Quaternion.identity);
	}
	
	if(Input.GetKeyDown(KeyCode.X))
	{
		if(i< 51)
			i++;

		else
			i=0;
		
		var obx = Instantiate(Effect[i], Vector3(0,0,0),Quaternion.identity);
	}
	
	if(Input.GetKeyDown(KeyCode.C))
		var obc = Instantiate(Effect[i], Vector3(0,0,0),Quaternion.identity);
}