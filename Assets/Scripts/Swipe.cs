using System;
using UnityEngine;

public class Swipe : MonoBehaviour
{
	private bool tap;

	private bool swipeLeft;

	private bool swipeRight;

	private bool swipeUp;

	private bool swipeDown;

	private bool isDraging;

	private Vector2 startTouch;

	private Vector2 swipeDelta;

	public Vector2 SwipeDelta
	{
		get
		{
			return this.swipeDelta;
		}
	}

	public bool SwipeLeft
	{
		get
		{
			return this.swipeLeft;
		}
	}

	public bool SwipeRight
	{
		get
		{
			return this.swipeRight;
		}
	}

	public bool SwipeDown
	{
		get
		{
			return this.swipeDown;
		}
	}

	public bool SwipeUp
	{
		get
		{
			return this.swipeUp;
		}
	}

	public bool Tap
	{
		get
		{
			return this.tap;
		}
	}

	private void Update()
	{
		this.tap = (this.swipeLeft = (this.swipeRight = (this.swipeUp = (this.swipeDown = false))));
		if (Input.GetMouseButtonDown(0))
		{
			this.tap = true;
			this.isDraging = true;
			this.startTouch = UnityEngine.Input.mousePosition;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			this.isDraging = false;
			this.Reset();
		}
		if (Input.touches.Length > 0)
		{
			if (Input.touches[0].phase == TouchPhase.Began)
			{
				this.isDraging = true;
				this.tap = true;
				this.startTouch = Input.touches[0].position;
			}
			else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
			{
				this.isDraging = false;
				this.Reset();
			}
		}
		this.swipeDelta = Vector2.zero;
		if (this.startTouch != Vector2.zero && this.isDraging)
		{
			if (Input.touches.Length > 0)
			{
				this.swipeDelta = Input.touches[0].position - this.startTouch;
			}
			else if (Input.GetMouseButton(0))
			{
				this.swipeDelta = UnityEngine.Input.mousePosition - (Vector3)this.startTouch;
			}
		}
		if (this.swipeDelta.magnitude > 50f)
		{
			float x = this.swipeDelta.x;
			float y = this.swipeDelta.y;
			if (Mathf.Abs(x) > Mathf.Abs(y))
			{
				if (x > 0f)
				{
					this.swipeRight = true;
				}
				else
				{
					this.swipeLeft = true;
				}
			}
			else if (Mathf.Abs(y) > Mathf.Abs(x))
			{
				if (y < 0f)
				{
					this.swipeDown = true;
				}
				else
				{
					this.swipeUp = true;
				}
			}
			this.Reset();
		}
	}

	private void Reset()
	{
		this.startTouch = (this.swipeDelta = Vector2.zero);
		this.isDraging = false;
	}
}
