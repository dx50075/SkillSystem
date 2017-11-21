
public var speed : float = 0.1f;

function Update () {
	transform.Translate(Vector3.back * speed);
}