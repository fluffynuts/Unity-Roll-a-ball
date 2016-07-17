using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
  public int Speed = 2;

  private int _xDelta;
  private int _yDelta;
  private int _zDelta;
	// Use this for initialization
	void Start () {
	  _xDelta = RandomInt(15, 20);
    _yDelta = RandomInt(30, 35);
    _zDelta = RandomInt(45, 50);
	}

  private int RandomInt(int min, int max)
  {
    var delta = max - min;
    return System.Convert.ToInt32(min + (Random.value * delta));
  }
	
	// Update is called once per frame
	void Update () 
  {
    // rotate slightly randomly between instances
    transform.Rotate(new Vector3(_xDelta, _yDelta, _zDelta) * Speed * Time.deltaTime);
	}
}
