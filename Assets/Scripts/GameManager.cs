using System;

public class GameManager
{
	private static GameManager instance;

	public int GrassCount;

	public int PatternCount;

	public int ColorIndex;

	public int LevelCount = 1;

	public bool ChangeStatus;

	public bool PlayStatus;

	public bool reward;

	public bool rewardedFailed;

	public int levelCompCount;

	public int adcount;

	public int levelFailedCount;

	public static GameManager Instance
	{
		get
		{
			if (GameManager.instance == null)
			{
				GameManager.instance = new GameManager();
			}
			return GameManager.instance;
		}
	}
}
