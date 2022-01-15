using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace Trello
{
	public class TrelloCard
	{
		private const string TimestampFormat = "yyyy-MM-dd_HH.mm.ss";

		private string name;
		private string desc;
		private string pos;
		private string idList;
		private string idLabels;
		private byte[] screenshot;

		public TrelloCard(string name, string desc, string pos, string idList, string idLabels, byte[] screenshot)
		{
			this.name = name;
			this.desc = desc;
			this.pos = pos;
			this.idList = idList;
			this.idLabels = idLabels;
			this.screenshot = screenshot;
		}

		public WWWForm GetPostBody()
		{
			WWWForm postBody = new WWWForm();
			postBody.AddField("name", name);
			postBody.AddField("desc", FormattedDescription());
			postBody.AddField("pos", pos);
			postBody.AddField("idList", idList);
			if (!string.IsNullOrEmpty(idLabels))
			{
				postBody.AddField("idLabels", idLabels);
			}
			if (screenshot != null)
			{
				postBody.AddBinaryData("fileSource", screenshot, "screenshot_" + DateTime.Now.ToString(TimestampFormat) + ".png");
			}
			return postBody;
		}

		private string FormattedDescription()
		{
			return "###Summary\n" + desc + "\n###Game State\n" + GetGameState() + "\n###Settings\n" + GetSettings() + "\n###System Info\n" + GetSystemInfo();
		}

		private string GetGameState()
		{
			string sceneName = SceneManager.GetActiveScene().name;
			string Username = FirebaseManager.instance.user.DisplayName;

			if(SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1)
            {
				return $"User: {Username}\nActive Scene: {sceneName}\n";
			}
            else
            {
				string PlayerPosition = GameObject.FindWithTag("Player").gameObject.transform.position.ToString();
				return $"User: {Username}\nPlayer Position: {PlayerPosition}\nActive Scene: {sceneName}\n";
			}
		}

		private string GetSettings()
		{
			return "Screen Resolution: " + Screen.currentResolution + " \nFull Screen: " + Screen.fullScreen + "\nQuality Level: " + QualitySettings.GetQualityLevel();
		}

		private string GetSystemInfo()
		{
			return "OS: " + SystemInfo.operatingSystem + "\nProcessor: " + SystemInfo.processorType + "\nMemory: " + SystemInfo.systemMemorySize + "\nGraphics API: " + SystemInfo.graphicsDeviceType + "\nGraphics Processor: " + SystemInfo.graphicsDeviceName + "\nGraphics Memory: " + SystemInfo.graphicsMemorySize + "\nGraphics Vendor: " + SystemInfo.graphicsDeviceVendor;
		}
	}
}
