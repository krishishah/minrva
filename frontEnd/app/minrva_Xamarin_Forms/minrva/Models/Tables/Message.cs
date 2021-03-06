﻿using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace minrva
{
	public class Message
	{
		string id;
		string createdAt;
		string chatId;
		 
		string sender;
		string receiver;

		string text;
		[JsonIgnore]
		public LayoutOptions Alignment { get; set; }
		public string MessageBoxColour { get; set; }
		public string TextColour { get; set; }

		[JsonProperty(PropertyName = "id")]
		public string Id
		{
			get { return id; }
			set { id = value; }
		}

		[JsonProperty(PropertyName = "createdAt")]
		public string CreatedAt
		{
			get { return createdAt; }
			set { createdAt = value; }
		}

		[JsonProperty(PropertyName = "chatId")]
		public string ChatId
		{
			get { return chatId; }
			set { chatId = value; }
		}

		[JsonProperty(PropertyName = "sender")]
		public string Sender
		{
			get { return sender; }
			set { sender = value; }
		}

		[JsonProperty(PropertyName = "receiver")]
		public string Receiver
		{
			get { return receiver; }
			set { receiver = value; }
		}

		[JsonProperty(PropertyName = "text")]
		public string Text
		{
			get { return text; }
			set { text = value; }
		}

		[Version]
		public string Version { get; set; }
	}
}

