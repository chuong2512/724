//using GameAnalyticsSDK;
//using MoreMountains.NiceVibrations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SwipeTest : MonoBehaviour
{
	private sealed class _StopParticles_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal SwipeTest _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _StopParticles_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				GameManager.Instance.levelCompCount++;
				if (GameManager.Instance.levelCompCount % 2 == 0)
				{
					GameManager.Instance.adcount++;
                        //ShowAds
                        AdsControl.Instance.showAds();
                        }
				this._current = new WaitForSeconds(0.4f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.GParticles.Stop();
				this._current = new WaitForSeconds(1f);
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			case 2u:
				this._this.FinalParticles.Play();
				//GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "game", GameManager.Instance.LevelCount);
				this._this.Victory.SetActive(true);
				this._this.Next.SetActive(true);
				this._PC = -1;
				break;
			}
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	public Swipe swipeControls;

	public GameObject Victory;

	public GameObject rateUs;

	public GameObject Next;

	public ParticleSystem GParticles;

	public ParticleSystem FinalParticles;

	private SwipeTest ST;

	private bool Status1 = true;

	private bool Status2;

	private bool Status3;

	private bool PlayStatus = true;

	private Rigidbody Rb;

	private string GrassTag = "Grass";

	private RaycastHit hit;

	private Vector3 Pos;

	private int Counter;

	private int Index;

	private void Start()
	{
		//GameAnalytics.Initialize();
		//GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "game");
		this.ST = base.GetComponent<SwipeTest>();
		this.Rb = base.GetComponent<Rigidbody>();
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == this.GrassTag)
		{
			this.Counter++;
			if (this.PlayStatus)
			{
				this.PlayStatus = false;
				this.GParticles.Play();
			}
			col.gameObject.SetActive(false);
			if (this.Counter == GameManager.Instance.GrassCount)
			{
				//base.transform.GetComponent<NiceVibrationsDemoManager>().TriggerDefault();
				base.transform.GetChild(0).gameObject.transform.GetComponent<MeshRenderer>().enabled = false;
				if (PlayerPrefs.GetInt("Rated") != 1 && GameManager.Instance.LevelCount % 3 == 0)
				{
					this.rateUs.SetActive(true);
				}
				GameManager.Instance.LevelCount++;
				col.transform.parent.parent.GetChild(1).gameObject.SetActive(true);
				this.ST.enabled = false;
				base.StartCoroutine(this.StopParticles());
			}
		}
	}

	private IEnumerator StopParticles()
	{
		SwipeTest._StopParticles_c__Iterator0 _StopParticles_c__Iterator = new SwipeTest._StopParticles_c__Iterator0();
		_StopParticles_c__Iterator._this = this;
		return _StopParticles_c__Iterator;
	}

	public void RateNow()
	{
		PlayerPrefs.SetInt("Rated", 1);
		this.rateUs.SetActive(false);
		Application.OpenURL("http://play.google.com/store/apps/details?id=" + Application.identifier);
	}

	public void Later()
	{
		this.rateUs.SetActive(false);
	}

	private void Update()
	{
		if (this.swipeControls.SwipeUp && this.Status1)
		{
			this.Status1 = false;
			this.Status2 = true;
			if (Physics.Raycast(base.transform.position, base.transform.TransformDirection(Vector3.forward), out this.hit))
			{
				this.Pos = this.hit.point;
				this.Pos.z = this.hit.point.z - 0.5f;
			}
		}
		else if (this.swipeControls.SwipeDown && this.Status1)
		{
			this.Status1 = false;
			this.Status2 = true;
			if (Physics.Raycast(base.transform.position, base.transform.TransformDirection(Vector3.back), out this.hit))
			{
				this.Pos = this.hit.point;
				this.Pos.z = this.hit.point.z + 0.5f;
			}
		}
		else if (this.swipeControls.SwipeLeft && this.Status1)
		{
			this.Status1 = false;
			this.Status3 = true;
			if (Physics.Raycast(base.transform.position, base.transform.TransformDirection(Vector3.left), out this.hit))
			{
				this.Pos = this.hit.point;
				this.Pos.x = this.hit.point.x + 0.5f;
			}
		}
		else if (this.swipeControls.SwipeRight && this.Status1)
		{
			this.Status1 = false;
			this.Status3 = true;
			if (Physics.Raycast(base.transform.position, base.transform.TransformDirection(Vector3.right), out this.hit))
			{
				this.Pos = this.hit.point;
				this.Pos.x = this.hit.point.x - 0.5f;
			}
		}
	}

	private void FixedUpdate()
	{
		if (this.Status2)
		{
			this.Rb.MovePosition(Vector3.MoveTowards(base.transform.position, this.Pos, 15f * Time.deltaTime));
			if (base.transform.position.z == this.Pos.z)
			{
				this.GParticles.Stop();
				this.Status2 = false;
				this.Status1 = true;
				this.PlayStatus = true;
			}
		}
		else if (this.Status3)
		{
			this.Rb.MovePosition(Vector3.MoveTowards(base.transform.position, this.Pos, 15f * Time.deltaTime));
			if (base.transform.position.x == this.Pos.x)
			{
				this.GParticles.Stop();
				this.Status3 = false;
				this.Status1 = true;
				this.PlayStatus = true;
			}
		}
	}
}
