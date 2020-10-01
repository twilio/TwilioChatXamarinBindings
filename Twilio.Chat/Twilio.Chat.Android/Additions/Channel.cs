using System;

namespace Com.Twilio.Chat
{
	public partial class Channel
	{
		class Listener : Java.Lang.Object, IChannelListener
		{
			private readonly Channel parent;
			Listener(Channel parent) => this.parent = parent;

			public void OnMemberAdded(Member member) => parent?.MemberAdded(parent, new MemberAddedEventArgs(member));
			public void OnMemberDeleted(Member member) => parent?.MemberDeleted(parent, new MemberDeletedEventArgs(member));
			public void OnMemberUpdated(Member member, Member.UpdateReason reason) => parent?.MemberUpdated(parent, new MemberUpdatedEventArgs(member, reason));

			public void OnMessageAdded(Message message) => parent?.MessageAdded(parent, new MessageAddedEventArgs(message));
			public void OnMessageDeleted(Message message) => parent?.MessageDeleted(parent, new MessageDeletedEventArgs(message));
			public void OnMessageUpdated(Message message, Message.UpdateReason reason) => parent?.MessageUpdated(parent, new MessageUpdatedEventArgs(message, reason));

			public void OnSynchronizationChanged(Channel channel) => parent?.SynchronizationChanged(parent, new SynchronizationChangedEventArgs(channel));

			public void OnTypingStarted(Channel channel, Member member) => parent?.TypingStarted(parent, new TypingStartedEventArgs(channel, member));
			public void OnTypingEnded(Channel channel, Member member) => parent?.TypingEnded(parent, new TypingEndedEventArgs(channel, member));
		}

		public EventHandler<MemberAddedEventArgs> MemberAdded;
		public EventHandler<MemberDeletedEventArgs> MemberDeleted;
		public EventHandler<MemberUpdatedEventArgs> MemberUpdated;

		public EventHandler<MessageAddedEventArgs> MessageAdded;
		public EventHandler<MessageDeletedEventArgs> MessageDeleted;
		public EventHandler<MessageUpdatedEventArgs> MessageUpdated;

		public EventHandler<SynchronizationChangedEventArgs> SynchronizationChanged;

		public EventHandler<TypingStartedEventArgs> TypingStarted;
		public EventHandler<TypingEndedEventArgs> TypingEnded;
	}
}
