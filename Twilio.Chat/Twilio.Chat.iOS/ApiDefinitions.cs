using System;
using Foundation;
using ObjCRuntime;

namespace Twilio.Chat.iOS
{

    // @interface TCHError : NSError
    [BaseType(typeof(NSError), Name = "TCHError")]
    interface Error
    {
    }

    // @interface TCHResult : NSObject
    [BaseType(typeof(NSObject), Name = "TCHResult")]
    interface Result
    {
        // @property (readonly, nonatomic, strong) TCHError * _Nullable error;
        [NullAllowed, Export("error", ArgumentSemantic.Strong)]
        Error Error { get; }

        // @property (readonly, assign, nonatomic) NSInteger resultCode;
        [Export("resultCode")]
        nint ResultCode { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable resultText;
        [NullAllowed, Export("resultText")]
        string ResultText { get; }

        // -(BOOL)isSuccessful;
        [Export("isSuccessful")]
        bool IsSuccessful { get; }
    }

    // typedef void (^TCHCompletion)(TCHResult * _Nonnull);
    delegate void Completion(Result result);

    // typedef void (^TCHTwilioClientCompletion)(TCHResult * _Nonnull, TwilioChatClient * _Nullable);
    delegate void TwilioClientCompletion(Result result, [NullAllowed] TwilioChatClient twilioChatClient);

    // typedef void (^TCHChannelDescriptorPaginatorCompletion)(TCHResult * _Nonnull, TCHChannelDescriptorPaginator * _Nullable);
    delegate void ChannelDescriptorPaginatorCompletion(Result result, [NullAllowed] ChannelDescriptorPaginator channelDescriptorPaginator);

    // typedef void (^TCHMemberPaginatorCompletion)(TCHResult * _Nonnull, TCHMemberPaginator * _Nullable);
    delegate void MemberPaginatorCompletion(Result result, [NullAllowed] MemberPaginator memberPaginator);

    // typedef void (^TCHChannelCompletion)(TCHResult * _Nonnull, TCHChannel * _Nullable);
    delegate void ChannelCompletion(Result result, [NullAllowed] Channel channel);

    // typedef void (^TCHMessageCompletion)(TCHResult * _Nonnull, TCHMessage * _Nullable);
    delegate void MessageCompletion(Result result, [NullAllowed] Message message);

    // typedef void (^TCHMessagesCompletion)(TCHResult * _Nonnull, NSArray<TCHMessage *> * _Nullable);
    delegate void MessagesCompletion(Result result, [NullAllowed] Message[] messages);

    // typedef void (^TCHUserCompletion)(TCHResult * _Nonnull, TCHUser * _Nullable);
    delegate void UserCompletion(Result result, [NullAllowed] User user);

    // typedef void (^TCHUserDescriptorCompletion)(TCHResult * _Nonnull, TCHUserDescriptor * _Nullable);
    delegate void UserDescriptorCompletion(Result result, [NullAllowed] UserDescriptor userDescriptor);

    // typedef void (^TCHUserDescriptorPaginatorCompletion)(TCHResult * _Nonnull, TCHUserDescriptorPaginator * _Nullable);
    delegate void UserDescriptorPaginatorCompletion(Result result, [NullAllowed] UserDescriptorPaginator userDescriptorPaginator);

    // typedef void (^TCHCountCompletion)(TCHResult * _Nonnull, NSUInteger);
    delegate void CountCompletion(Result result, nuint count);

    // typedef void (^TCHMediaOnStarted)();
    delegate void MediaOnStarted();

    // typedef void (^TCHMediaOnProgress)(NSUInteger);
    delegate void MediaOnProgress(nuint bytes);

    // typedef void (^TCHMediaOnCompleted)(NSString * _Nonnull);
    delegate void MediaOnCompleted(string mediaSid);

    [Static]
    partial interface Constants
    {
        // extern NSString *const _Nonnull TCHChannelOptionFriendlyName;
        [Field("TCHChannelOptionFriendlyName", "__Internal")]
        NSString ChannelOptionFriendlyName { get; }

        // extern NSString *const _Nonnull TCHChannelOptionUniqueName;
        [Field("TCHChannelOptionUniqueName", "__Internal")]
        NSString ChannelOptionUniqueName { get; }

        // extern NSString *const _Nonnull TCHChannelOptionType;
        [Field("TCHChannelOptionType", "__Internal")]
        NSString ChannelOptionType { get; }

        // extern NSString *const _Nonnull TCHChannelOptionAttributes;
        [Field("TCHChannelOptionAttributes", "__Internal")]
        NSString ChannelOptionAttributes { get; }

        // extern NSString *const _Nonnull TCHErrorDomain;
        [Field("TCHErrorDomain", "__Internal")]
        NSString ErrorDomain { get; }

        // extern const NSInteger TCHErrorGeneric;
        [Field("TCHErrorGeneric", "__Internal")]
        nint ErrorGeneric { get; }

        // extern NSString *const _Nonnull TCHErrorMsgKey;
        [Field("TCHErrorMsgKey", "__Internal")]
        NSString ErrorMsgKey { get; }
    }

    // @interface TCHChannels : NSObject
    [BaseType(typeof(NSObject), Name = "TCHChannels")]
    interface Channels
    {
        // -(NSArray<TCHChannel *> * _Nonnull)subscribedChannels;
        [Export("subscribedChannels")]
        Channel[] SubscribedChannels { get; }

        // -(NSArray<TCHChannel *> * _Nonnull)subscribedChannelsSortedBy:(TCHChannelSortingCriteria)criteria order:(TCHChannelSortingOrder)order;
        [Export("subscribedChannelsSortedBy:order:")]
        Channel[] SubscribedChannelsSortedBy(ChannelSortingCriteria criteria, ChannelSortingOrder order);

        // -(void)userChannelDescriptorsWithCompletion:(TCHChannelDescriptorPaginatorCompletion _Nonnull)completion;
        [Export("userChannelDescriptorsWithCompletion:")]
        void UserChannelDescriptorsWithCompletion(ChannelDescriptorPaginatorCompletion completion);

        // -(void)publicChannelDescriptorsWithCompletion:(TCHChannelDescriptorPaginatorCompletion _Nonnull)completion;
        [Export("publicChannelDescriptorsWithCompletion:")]
        void PublicChannelDescriptorsWithCompletion(ChannelDescriptorPaginatorCompletion completion);

        // -(void)createChannelWithOptions:(NSDictionary<NSString *,id> * _Nullable)options completion:(TCHChannelCompletion _Nullable)completion;
        [Export("createChannelWithOptions:completion:")]
        void CreateChannelWithOptions([NullAllowed] NSDictionary options, [NullAllowed] ChannelCompletion completion);

        // -(void)channelWithSidOrUniqueName:(NSString * _Nonnull)sidOrUniqueName completion:(TCHChannelCompletion _Nonnull)completion;
        [Export("channelWithSidOrUniqueName:completion:")]
        void ChannelWithSidOrUniqueName(string sidOrUniqueName, ChannelCompletion completion);
    }

    // @interface TCHJsonAttributes : NSObject
    [BaseType(typeof(NSObject), Name = "TCHJsonAttributes")]
    interface JsonAttributes
    {
        // -(instancetype _Null_unspecified)initWithDictionary:(NSDictionary * _Nonnull)dictionary;
        [Export("initWithDictionary:")]
        JsonAttributes WithDictionary(NSDictionary value);

        // -(instancetype _Null_unspecified)initWithArray:(NSArray * _Nonnull)array;
        [Export("initWithArray:")]
        JsonAttributes WithArray(NSObject[] value);

        // -(instancetype _Null_unspecified)initWithString:(NSString * _Nonnull)string;
        [Export("initWithString:")]
        JsonAttributes WithString(string value);

        // -(instancetype _Null_unspecified)initWithNumber:(NSNumber * _Nonnull)number;
        [Export("initWithNumber:")]
        JsonAttributes WithNumber(NSNumber value);

        // @property (readonly) BOOL isDictionary;
        [Export("isDictionary")]
        bool IsDictionary { get; }

        // @property (readonly) BOOL isArray;
        [Export("isArray")]
        bool IsArray { get; }

        // @property (readonly) BOOL isString;
        [Export("isString")]
        bool IsString { get; }

        // @property (readonly) BOOL isNumber;
        [Export("isNumber")]
        bool IsNumber { get; }

        // @property (readonly) BOOL isNull;
        [Export("isNull")]
        bool IsNull { get; }

        // @property (readonly) NSDictionary * _Nullable dictionary;
        [NullAllowed, Export("dictionary")]
        NSDictionary Dictionary { get; }

        // @property (readonly) NSArray * _Nullable array;
        [NullAllowed, Export("array")]
        NSObject[] Array { get; }

        // @property (readonly) NSString * _Nullable string;
        [NullAllowed, Export("string")]
        string String { get; }

        // @property (readonly) NSNumber * _Nullable number;
        [NullAllowed, Export("number")]
        NSNumber Number { get; }
    }

    // @interface TCHMessage : NSObject
    [BaseType(typeof(NSObject), Name = "TCHMessage")]
    interface Message
    {
        // @property (readonly, copy, nonatomic) NSString * _Nullable sid;
        [NullAllowed, Export("sid")]
        string Sid { get; }

        // @property (readonly, copy, nonatomic) NSNumber * _Nullable index;
        [NullAllowed, Export("index", ArgumentSemantic.Copy)]
        NSNumber Index { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable author;
        [NullAllowed, Export("author")]
        string Author { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable body;
        [NullAllowed, Export("body")]
        string Body { get; }

        // @property (readonly, assign, nonatomic) TCHMessageType messageType;
        [Export("messageType", ArgumentSemantic.Assign)]
        MessageType Type { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable mediaSid;
        [NullAllowed, Export("mediaSid")]
        string MediaSid { get; }

        // @property (readonly, assign, nonatomic) NSUInteger mediaSize;
        [Export("mediaSize")]
        nuint MediaSize { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable mediaType;
        [NullAllowed, Export("mediaType")]
        string MediaType { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable mediaFilename;
        [NullAllowed, Export("mediaFilename")]
        string MediaFilename { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable memberSid;
        [NullAllowed, Export("memberSid")]
        string MemberSid { get; }

        // @property (readonly, copy, nonatomic) TCHMember * _Nullable member;
        [NullAllowed, Export("member", ArgumentSemantic.Copy)]
        Member Member { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable timestamp;
        [NullAllowed, Export("timestamp")]
        string Timestamp { get; }

        // @property (readonly, nonatomic, strong) NSDate * _Nullable timestampAsDate;
        [NullAllowed, Export("timestampAsDate", ArgumentSemantic.Strong)]
        NSDate TimestampAsDate { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable dateUpdated;
        [NullAllowed, Export("dateUpdated")]
        string DateUpdated { get; }

        // @property (readonly, nonatomic, strong) NSDate * _Nullable dateUpdatedAsDate;
        [NullAllowed, Export("dateUpdatedAsDate", ArgumentSemantic.Strong)]
        NSDate DateUpdatedAsDate { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable lastUpdatedBy;
        [NullAllowed, Export("lastUpdatedBy")]
        string LastUpdatedBy { get; }

        // -(void)updateBody:(NSString * _Nonnull)body completion:(TCHCompletion _Nullable)completion;
        [Export("updateBody:completion:")]
        void UpdateBody(string body, [NullAllowed] Completion completion);

        // -(TCHJsonAttributes * _Nullable)attributes;
        [NullAllowed, Export("attributes")]
        JsonAttributes Attributes { get; }

        // -(void)setAttributes:(TCHJsonAttributes * _Nullable)attributes completion:(TCHCompletion _Nullable)completion;
        [Export("setAttributes:completion:")]
        void SetAttributes([NullAllowed] JsonAttributes attributes, [NullAllowed] Completion completion);

        // -(BOOL)hasMedia;
        [Export("hasMedia")]
        bool HasMedia { get; }

        // -(void)getMediaWithOutputStream:(NSOutputStream * _Nonnull)mediaStream onStarted:(TCHMediaOnStarted _Nullable)onStarted onProgress:(TCHMediaOnProgress _Nullable)onProgress onCompleted:(TCHMediaOnCompleted _Nullable)onCompleted completion:(TCHCompletion _Nullable)completion;
        [Export("getMediaWithOutputStream:onStarted:onProgress:onCompleted:completion:")]
        void GetMediaWithOutputStream(NSOutputStream mediaStream, [NullAllowed] MediaOnStarted onStarted, [NullAllowed] MediaOnProgress onProgress, [NullAllowed] MediaOnCompleted onCompleted, [NullAllowed] Completion completion);
    }

    // @interface TCHMessageOptions : NSObject
    [BaseType(typeof(NSObject), Name = "TCHMessageOptions")]
    interface MessageOptions
    {
        // -(instancetype _Nonnull)withBody:(NSString * _Nonnull)body;
        [Export("withBody:")]
        MessageOptions WithBody(string body);

        // -(instancetype _Nonnull)withMediaStream:(NSInputStream * _Nonnull)mediaStream contentType:(NSString * _Nonnull)contentType defaultFilename:(NSString * _Nullable)defaultFilename onStarted:(TCHMediaOnStarted _Nullable)onStarted onProgress:(TCHMediaOnProgress _Nullable)onProgress onCompleted:(TCHMediaOnCompleted _Nullable)onCompleted;
        [Export("withMediaStream:contentType:defaultFilename:onStarted:onProgress:onCompleted:")]
        MessageOptions WithMediaStream(NSInputStream mediaStream, string contentType, [NullAllowed] string defaultFilename, [NullAllowed] MediaOnStarted onStarted, [NullAllowed] MediaOnProgress onProgress, [NullAllowed] MediaOnCompleted onCompleted);

        // -(instancetype _Nullable)withAttributes:(TCHJsonAttributes * _Nonnull)attributes completion:(TCHCompletion _Nullable)completion;
        [Export("withAttributes:completion:")]
        [return: NullAllowed]
        MessageOptions WithAttributes(JsonAttributes attributes, [NullAllowed] Completion completion);
    }

    // @interface TCHMessages : NSObject
    [BaseType(typeof(NSObject), Name = "TCHMessages")]
    interface Messages
    {
        // @property (readonly, copy, nonatomic) NSNumber * _Nullable lastConsumedMessageIndex;
        [NullAllowed, Export("lastConsumedMessageIndex", ArgumentSemantic.Copy)]
        NSNumber LastConsumedMessageIndex { get; }

        // -(void)sendMessageWithOptions:(TCHMessageOptions * _Nonnull)options completion:(TCHMessageCompletion _Nullable)completion;
        [Export("sendMessageWithOptions:completion:")]
        void SendMessageWithOptions(MessageOptions options, [NullAllowed] MessageCompletion completion);

        // -(void)removeMessage:(TCHMessage * _Nonnull)message completion:(TCHCompletion _Nullable)completion;
        [Export("removeMessage:completion:")]
        void RemoveMessage(Message message, [NullAllowed] Completion completion);

        // -(void)getLastMessagesWithCount:(NSUInteger)count completion:(TCHMessagesCompletion _Nonnull)completion;
        [Export("getLastMessagesWithCount:completion:")]
        void GetLastMessagesWithCount(nuint count, MessagesCompletion completion);

        // -(void)getMessagesBefore:(NSUInteger)index withCount:(NSUInteger)count completion:(TCHMessagesCompletion _Nonnull)completion;
        [Export("getMessagesBefore:withCount:completion:")]
        void GetMessagesBefore(nuint index, nuint count, MessagesCompletion completion);

        // -(void)getMessagesAfter:(NSUInteger)index withCount:(NSUInteger)count completion:(TCHMessagesCompletion _Nonnull)completion;
        [Export("getMessagesAfter:withCount:completion:")]
        void GetMessagesAfter(nuint index, nuint count, MessagesCompletion completion);

        // -(void)messageWithIndex:(NSNumber * _Nonnull)index completion:(TCHMessageCompletion _Nonnull)completion;
        [Export("messageWithIndex:completion:")]
        void MessageWithIndex(NSNumber index, MessageCompletion completion);

        // -(void)messageForConsumptionIndex:(NSNumber * _Nonnull)index completion:(TCHMessageCompletion _Nonnull)completion;
        [Export("messageForConsumptionIndex:completion:")]
        void MessageForConsumptionIndex(NSNumber index, MessageCompletion completion);

        // -(void)setLastConsumedMessageIndex:(NSNumber * _Nonnull)index completion:(TCHCountCompletion _Nullable)completion;
        [Export("setLastConsumedMessageIndex:completion:")]
        void SetLastConsumedMessageIndex(NSNumber index, [NullAllowed] CountCompletion completion);

        // -(void)advanceLastConsumedMessageIndex:(NSNumber * _Nonnull)index completion:(TCHCountCompletion _Nullable)completion;
        [Export("advanceLastConsumedMessageIndex:completion:")]
        void AdvanceLastConsumedMessageIndex(NSNumber index, [NullAllowed] CountCompletion completion);

        // -(void)setAllMessagesConsumedWithCompletion:(TCHCountCompletion _Nullable)completion;
        [Export("setAllMessagesConsumedWithCompletion:")]
        void SetAllMessagesConsumedWithCompletion([NullAllowed] CountCompletion completion);

        // -(void)setNoMessagesConsumedWithCompletion:(TCHCountCompletion _Nullable)completion;
        [Export("setNoMessagesConsumedWithCompletion:")]
        void SetNoMessagesConsumedWithCompletion([NullAllowed] CountCompletion completion);
    }

    // @interface TCHMember : NSObject
    [BaseType(typeof(NSObject), Name = "TCHMember")]
    interface Member
    {
    	// @property (readonly, copy, nonatomic) NSString * _Nullable sid;
    	[NullAllowed, Export ("sid")]
    	string Sid { get; }

        // @property (readonly, nonatomic, strong) NSString * _Nullable identity;
        [NullAllowed, Export("identity", ArgumentSemantic.Strong)]
        string Identity { get; }

        // @property (readonly, nonatomic) TCHMemberType type;
        [Export("type")]
        MemberType Type { get; }

        // @property (readonly, copy, nonatomic) NSNumber * _Nullable lastConsumedMessageIndex;
        [NullAllowed, Export("lastConsumedMessageIndex", ArgumentSemantic.Copy)]
        NSNumber LastConsumedMessageIndex { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable lastConsumptionTimestamp;
        [NullAllowed, Export("lastConsumptionTimestamp")]
        string LastConsumptionTimestamp { get; }

        // @property (readonly, nonatomic, strong) NSDate * _Nullable lastConsumptionTimestampAsDate;
        [NullAllowed, Export("lastConsumptionTimestampAsDate", ArgumentSemantic.Strong)]
        NSDate LastConsumptionTimestampAsDate { get; }

        // -(TCHJsonAttributes * _Nullable)attributes;
        [NullAllowed, Export("attributes")]
        JsonAttributes Attributes { get; }

        // -(void)setAttributes:(TCHJsonAttributes * _Nullable)attributes completion:(TCHCompletion _Nullable)completion;
        [Export("setAttributes:completion:")]
        void SetAttributes([NullAllowed] JsonAttributes attributes, [NullAllowed] Completion completion);

        // -(void)userDescriptorWithCompletion:(TCHUserDescriptorCompletion _Nonnull)completion;
        [Export("userDescriptorWithCompletion:")]
        void UserDescriptorWithCompletion(UserDescriptorCompletion completion);

        // -(void)subscribedUserWithCompletion:(TCHUserCompletion _Nonnull)completion;
        [Export("subscribedUserWithCompletion:")]
        void SubscribedUserWithCompletion(UserCompletion completion);
    }

    // @interface TCHMembers : NSObject
    [BaseType(typeof(NSObject), Name = "TCHMembers")]
    interface Members
    {
        // -(void)membersWithCompletion:(TCHMemberPaginatorCompletion _Nonnull)completion;
        [Export("membersWithCompletion:")]
        void MembersWithCompletion(MemberPaginatorCompletion completion);

        // -(void)addByIdentity:(NSString * _Nonnull)identity completion:(TCHCompletion _Nullable)completion;
        [Export("addByIdentity:completion:")]
        void AddByIdentity(string identity, [NullAllowed] Completion completion);

        // -(void)inviteByIdentity:(NSString * _Nonnull)identity completion:(TCHCompletion _Nullable)completion;
        [Export("inviteByIdentity:completion:")]
        void InviteByIdentity(string identity, [NullAllowed] Completion completion);

        // -(void)removeMember:(TCHMember * _Nonnull)member completion:(TCHCompletion _Nullable)completion;
        [Export("removeMember:completion:")]
        void RemoveMember(Member member, [NullAllowed] Completion completion);
    }

    // @interface TCHUser : NSObject
    [BaseType(typeof(NSObject), Name = "TCHUser")]
    interface User
    {
        // @property (readonly, copy, nonatomic) NSString * _Nullable identity;
        [NullAllowed, Export("identity")]
        string Identity { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable friendlyName;
        [NullAllowed, Export("friendlyName")]
        string FriendlyName { get; }

        // -(TCHJsonAttributes * _Nullable)attributes;
        [NullAllowed, Export("attributes")]
        JsonAttributes Attributes { get; }

        // -(void)setAttributes:(TCHJsonAttributes * _Nullable)attributes completion:(TCHCompletion _Nullable)completion;
        [Export("setAttributes:completion:")]
        void SetAttributes([NullAllowed] JsonAttributes attributes, [NullAllowed] Completion completion);

        // -(void)setFriendlyName:(NSString * _Nullable)friendlyName completion:(TCHCompletion _Nullable)completion;
        [Export("setFriendlyName:completion:")]
        void SetFriendlyName([NullAllowed] string friendlyName, [NullAllowed] Completion completion);

        // -(BOOL)isOnline;
        [Export("isOnline")]
        bool IsOnline { get; }

        // -(BOOL)isNotifiable;
        [Export("isNotifiable")]
        bool IsNotifiable { get; }

        // -(BOOL)isSubscribed;
        [Export("isSubscribed")]
        bool IsSubscribed { get; }

        // -(void)unsubscribe;
        [Export("unsubscribe")]
        void Unsubscribe();
    }

    // @interface TCHChannel : NSObject
    [BaseType(typeof(NSObject), Name = "TCHChannel")]
    interface Channel
    {
        [Wrap("WeakDelegate")]
        [NullAllowed]
        ChannelDelegate Delegate { get; set; }

        // @property (nonatomic, weak) id<TCHChannelDelegate> _Nullable delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable sid;
        [NullAllowed, Export("sid")]
        string Sid { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable friendlyName;
        [NullAllowed, Export("friendlyName")]
        string FriendlyName { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable uniqueName;
        [NullAllowed, Export("uniqueName")]
        string UniqueName { get; }

        // @property (readonly, nonatomic, strong) TCHMessages * _Nullable messages;
        [NullAllowed, Export("messages", ArgumentSemantic.Strong)]
        Messages Messages { get; }

        // @property (readonly, nonatomic, strong) TCHMembers * _Nullable members;
        [NullAllowed, Export("members", ArgumentSemantic.Strong)]
        Members Members { get; }

        // @property (readonly, assign, nonatomic) TCHChannelSynchronizationStatus synchronizationStatus;
        [Export("synchronizationStatus", ArgumentSemantic.Assign)]
        ChannelSynchronizationStatus SynchronizationStatus { get; }

        // @property (readonly, assign, nonatomic) TCHChannelStatus status;
        [Export("status", ArgumentSemantic.Assign)]
        ChannelStatus Status { get; }

        // @property (readonly, assign, nonatomic) TCHChannelNotificationLevel notificationLevel;
        [Export("notificationLevel", ArgumentSemantic.Assign)]
        ChannelNotificationLevel NotificationLevel { get; }

        // @property (readonly, assign, nonatomic) TCHChannelType type;
        [Export("type", ArgumentSemantic.Assign)]
        ChannelType Type { get; }

        // @property (readonly, nonatomic, strong) NSString * _Nullable dateCreated;
        [NullAllowed, Export("dateCreated", ArgumentSemantic.Strong)]
        string DateCreated { get; }

        // @property (readonly, nonatomic, strong) NSDate * _Nullable dateCreatedAsDate;
        [NullAllowed, Export("dateCreatedAsDate", ArgumentSemantic.Strong)]
        NSDate DateCreatedAsDate { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable createdBy;
        [NullAllowed, Export("createdBy")]
        string CreatedBy { get; }

        // @property (readonly, nonatomic, strong) NSString * _Nullable dateUpdated;
        [NullAllowed, Export("dateUpdated", ArgumentSemantic.Strong)]
        string DateUpdated { get; }

        // @property (readonly, nonatomic, strong) NSDate * _Nullable dateUpdatedAsDate;
        [NullAllowed, Export("dateUpdatedAsDate", ArgumentSemantic.Strong)]
        NSDate DateUpdatedAsDate { get; }

        // @property (readonly, nonatomic, strong) NSDate * _Nullable lastMessageDate;
        [NullAllowed, Export("lastMessageDate", ArgumentSemantic.Strong)]
        NSDate LastMessageDate { get; }

        // @property (readonly, nonatomic, strong) NSNumber * _Nullable lastMessageIndex;
        [NullAllowed, Export("lastMessageIndex", ArgumentSemantic.Strong)]
        NSNumber LastMessageIndex { get; }

        // -(TCHJsonAttributes * _Nullable)attributes;
        [NullAllowed, Export("attributes")]
        JsonAttributes Attributes { get; }

        // -(void)setAttributes:(TCHJsonAttributes * _Nullable)attributes completion:(TCHCompletion _Nullable)completion;
        [Export("setAttributes:completion:")]
        void SetAttributes([NullAllowed] JsonAttributes attributes, [NullAllowed] Completion completion);

        // -(void)setFriendlyName:(NSString * _Nullable)friendlyName completion:(TCHCompletion _Nullable)completion;
        [Export("setFriendlyName:completion:")]
        void SetFriendlyName([NullAllowed] string friendlyName, [NullAllowed] Completion completion);

        // -(void)setUniqueName:(NSString * _Nullable)uniqueName completion:(TCHCompletion _Nullable)completion;
        [Export("setUniqueName:completion:")]
        void SetUniqueName([NullAllowed] string uniqueName, [NullAllowed] Completion completion);

        // -(void)setNotificationLevel:(TCHChannelNotificationLevel)notificationLevel completion:(TCHCompletion _Nullable)completion;
        [Export("setNotificationLevel:completion:")]
        void SetNotificationLevel(ChannelNotificationLevel notificationLevel, [NullAllowed] Completion completion);

        // -(void)joinWithCompletion:(TCHCompletion _Nullable)completion;
        [Export("joinWithCompletion:")]
        void JoinWithCompletion([NullAllowed] Completion completion);

        // -(void)declineInvitationWithCompletion:(TCHCompletion _Nullable)completion;
        [Export("declineInvitationWithCompletion:")]
        void DeclineInvitationWithCompletion([NullAllowed] Completion completion);

        // -(void)leaveWithCompletion:(TCHCompletion _Nullable)completion;
        [Export("leaveWithCompletion:")]
        void LeaveWithCompletion([NullAllowed] Completion completion);

        // -(void)destroyWithCompletion:(TCHCompletion _Nullable)completion;
        [Export("destroyWithCompletion:")]
        void DestroyWithCompletion([NullAllowed] Completion completion);

        // -(void)typing;
        [Export("typing")]
        void Typing();

        // -(TCHMember * _Nullable)memberWithIdentity:(NSString * _Nonnull)identity;
        [Export("memberWithIdentity:")]
        [return: NullAllowed]
        Member MemberWithIdentity(string identity);

        // -(void)getUnconsumedMessagesCountWithCompletion:(TCHCountCompletion _Nonnull)completion;
        [Export("getUnconsumedMessagesCountWithCompletion:")]
        void GetUnconsumedMessagesCountWithCompletion(CountCompletion completion);

        // -(void)getMessagesCountWithCompletion:(TCHCountCompletion _Nonnull)completion;
        [Export("getMessagesCountWithCompletion:")]
        void GetMessagesCountWithCompletion(CountCompletion completion);

        // -(void)getMembersCountWithCompletion:(TCHCountCompletion _Nonnull)completion;
        [Export("getMembersCountWithCompletion:")]
        void GetMembersCountWithCompletion(CountCompletion completion);
    }

    // @protocol TCHChannelDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject), Name = "TCHChannelDelegate")]
    interface ChannelDelegate
    {
        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channel:(TCHChannel * _Nonnull)channel updated:(TCHChannelUpdate)updated;
        [Export("chatClient:channel:updated:")]
        void ChannelUpdated(TwilioChatClient client, Channel channel, ChannelUpdate updated);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channelDeleted:(TCHChannel * _Nonnull)channel;
        [Export("chatClient:channelDeleted:")]
        void ChannelDeleted(TwilioChatClient client, Channel channel);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channel:(TCHChannel * _Nonnull)channel synchronizationStatusUpdated:(TCHChannelSynchronizationStatus)status;
        [Export("chatClient:channel:synchronizationStatusUpdated:")]
        void ChannelSynchronizationStatusUpdated(TwilioChatClient client, Channel channel, ChannelSynchronizationStatus status);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channel:(TCHChannel * _Nonnull)channel memberJoined:(TCHMember * _Nonnull)member;
        [Export("chatClient:channel:memberJoined:")]
        void MemberJoined(TwilioChatClient client, Channel channel, Member member);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channel:(TCHChannel * _Nonnull)channel member:(TCHMember * _Nonnull)member updated:(TCHMemberUpdate)updated;
        [Export("chatClient:channel:member:updated:")]
        void MemberUpdated(TwilioChatClient client, Channel channel, Member member, MemberUpdate updated);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channel:(TCHChannel * _Nonnull)channel memberLeft:(TCHMember * _Nonnull)member;
        [Export("chatClient:channel:memberLeft:")]
        void MemberLeft(TwilioChatClient client, Channel channel, Member member);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channel:(TCHChannel * _Nonnull)channel messageAdded:(TCHMessage * _Nonnull)message;
        [Export("chatClient:channel:messageAdded:")]
        void MessageAdded(TwilioChatClient client, Channel channel, Message message);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channel:(TCHChannel * _Nonnull)channel message:(TCHMessage * _Nonnull)message updated:(TCHMessageUpdate)updated;
        [Export("chatClient:channel:message:updated:")]
        void MessageUpdated(TwilioChatClient client, Channel channel, Message message, MessageUpdate updated);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channel:(TCHChannel * _Nonnull)channel messageDeleted:(TCHMessage * _Nonnull)message;
        [Export("chatClient:channel:messageDeleted:")]
        void MessageDeleted(TwilioChatClient client, Channel channel, Message message);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client typingStartedOnChannel:(TCHChannel * _Nonnull)channel member:(TCHMember * _Nonnull)member;
        [Export("chatClient:typingStartedOnChannel:member:")]
        void TypingStartedOnChannel(TwilioChatClient client, Channel channel, Member member);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client typingEndedOnChannel:(TCHChannel * _Nonnull)channel member:(TCHMember * _Nonnull)member;
        [Export("chatClient:typingEndedOnChannel:member:")]
        void TypingEndedOnChannel(TwilioChatClient client, Channel channel, Member member);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channel:(TCHChannel * _Nonnull)channel member:(TCHMember * _Nonnull)member user:(TCHUser * _Nonnull)user updated:(TCHUserUpdate)updated;
        [Export("chatClient:channel:member:user:updated:")]
        void UserUpdated(TwilioChatClient client, Channel channel, Member member, User user, UserUpdate updated);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channel:(TCHChannel * _Nonnull)channel member:(TCHMember * _Nonnull)member userSubscribed:(TCHUser * _Nonnull)user;
        [Export("chatClient:channel:member:userSubscribed:")]
        void UserSubscribed(TwilioChatClient client, Channel channel, Member member, User user);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channel:(TCHChannel * _Nonnull)channel member:(TCHMember * _Nonnull)member userUnsubscribed:(TCHUser * _Nonnull)user;
        [Export("chatClient:channel:member:userUnsubscribed:")]
        void UserUnsubscribed(TwilioChatClient client, Channel channel, Member member, User user);
    }

    // @interface TCHChannelDescriptor : NSObject
    [BaseType(typeof(NSObject), Name = "TCHChannelDescriptor")]
    interface ChannelDescriptor
    {
        // @property (readonly, copy, nonatomic) NSString * _Nullable sid;
        [NullAllowed, Export("sid")]
        string Sid { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable friendlyName;
        [NullAllowed, Export("friendlyName")]
        string FriendlyName { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable uniqueName;
        [NullAllowed, Export("uniqueName")]
        string UniqueName { get; }

        // @property (readonly, nonatomic, strong) NSDate * _Nullable dateCreated;
        [NullAllowed, Export("dateCreated", ArgumentSemantic.Strong)]
        NSDate DateCreated { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable createdBy;
        [NullAllowed, Export("createdBy")]
        string CreatedBy { get; }

        // @property (readonly, nonatomic, strong) NSDate * _Nullable dateUpdated;
        [NullAllowed, Export("dateUpdated", ArgumentSemantic.Strong)]
        NSDate DateUpdated { get; }

        // -(TCHJsonAttributes * _Nullable)attributes;
        [NullAllowed, Export("attributes")]
        JsonAttributes Attributes { get; }

        // -(NSUInteger)messagesCount;
        [Export("messagesCount")]
        nuint MessagesCount { get; }

        // -(NSUInteger)membersCount;
        [Export("membersCount")]
        nuint MembersCount { get; }

        // -(void)channelWithCompletion:(TCHChannelCompletion _Nonnull)completion;
        [Export("channelWithCompletion:")]
        void ChannelWithCompletion(ChannelCompletion completion);
    }

    // @interface TCHChannelDescriptorPaginator : NSObject
    [BaseType(typeof(NSObject), Name = "TCHChannelDescriptorPaginator")]
    interface ChannelDescriptorPaginator
    {
        // -(NSArray<TCHChannelDescriptor *> * _Nonnull)items;
        [Export("items")]
        ChannelDescriptor[] Items { get; }

        // -(BOOL)hasNextPage;
        [Export("hasNextPage")]
        bool HasNextPage { get; }

        // -(void)requestNextPageWithCompletion:(TCHChannelDescriptorPaginatorCompletion _Nonnull)completion;
        [Export("requestNextPageWithCompletion:")]
        void RequestNextPageWithCompletion(ChannelDescriptorPaginatorCompletion completion);
    }

    // @interface TCHMemberPaginator : NSObject
    [BaseType(typeof(NSObject), Name = "TCHMemberPaginator")]
    interface MemberPaginator
    {
        // -(NSArray<TCHMember *> * _Nonnull)items;
        [Export("items")]
        Member[] Items { get; }

        // -(BOOL)hasNextPage;
        [Export("hasNextPage")]
        bool HasNextPage { get; }

        // -(void)requestNextPageWithCompletion:(TCHMemberPaginatorCompletion _Nonnull)completion;
        [Export("requestNextPageWithCompletion:")]
        void RequestNextPageWithCompletion(MemberPaginatorCompletion completion);
    }

    // @interface TCHUsers : NSObject
    [BaseType(typeof(NSObject), Name = "TCHUsers")]
    interface Users
    {
        // -(void)userDescriptorsForChannel:(TCHChannel * _Nonnull)channel completion:(TCHUserDescriptorPaginatorCompletion _Nonnull)completion;
        [Export("userDescriptorsForChannel:completion:")]
        void UserDescriptorsForChannel(Channel channel, UserDescriptorPaginatorCompletion completion);

        // -(void)userDescriptorWithIdentity:(NSString * _Nonnull)identity completion:(TCHUserDescriptorCompletion _Nonnull)completion;
        [Export("userDescriptorWithIdentity:completion:")]
        void UserDescriptorWithIdentity(string identity, UserDescriptorCompletion completion);

        // -(void)subscribedUserWithIdentity:(NSString * _Nonnull)identity completion:(TCHUserCompletion _Nonnull)completion;
        [Export("subscribedUserWithIdentity:completion:")]
        void SubscribedUserWithIdentity(string identity, UserCompletion completion);

        // -(NSArray<TCHUser *> * _Nonnull)subscribedUsers;
        [Export("subscribedUsers")]
        User[] SubscribedUsers { get; }
    }

    // @interface TCHUserDescriptor : NSObject
    [BaseType(typeof(NSObject), Name = "TCHUserDescriptor")]
    interface UserDescriptor
    {
        // @property (readonly, copy, nonatomic) NSString * _Nullable identity;
        [NullAllowed, Export("identity")]
        string Identity { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable friendlyName;
        [NullAllowed, Export("friendlyName")]
        string FriendlyName { get; }

        // -(TCHJsonAttributes * _Nullable)attributes;
        [NullAllowed, Export("attributes")]
        JsonAttributes Attributes { get; }

        // -(BOOL)isOnline;
        [Export("isOnline")]
        bool IsOnline { get; }

        // -(BOOL)isNotifiable;
        [Export("isNotifiable")]
        bool IsNotifiable { get; }

        // -(void)subscribeWithCompletion:(TCHUserCompletion _Nullable)completion;
        [Export("subscribeWithCompletion:")]
        void SubscribeWithCompletion([NullAllowed] UserCompletion completion);
    }

    // @interface TCHUserDescriptorPaginator : NSObject
    [BaseType(typeof(NSObject), Name = "TCHUserDescriptorPaginator")]
    interface UserDescriptorPaginator
    {
        // -(NSArray<TCHUserDescriptor *> * _Nonnull)items;
        [Export("items")]
        UserDescriptor[] Items { get; }

        // -(BOOL)hasNextPage;
        [Export("hasNextPage")]
        bool HasNextPage { get; }

        // -(void)requestNextPageWithCompletion:(TCHUserDescriptorPaginatorCompletion _Nonnull)completion;
        [Export("requestNextPageWithCompletion:")]
        void RequestNextPageWithCompletion(UserDescriptorPaginatorCompletion completion);
    }

    // @interface TwilioChatClient : NSObject
    [BaseType(typeof(NSObject))]
    interface TwilioChatClient
    {
        [Wrap("WeakDelegate")]
        [NullAllowed]
        TwilioChatClientDelegate Delegate { get; set; }

        // @property (nonatomic, weak) id<TwilioChatClientDelegate> _Nullable delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        // @property (readonly, nonatomic, strong) TCHUser * _Nullable user;
        [NullAllowed, Export("user", ArgumentSemantic.Strong)]
        User User { get; }

        // @property (readonly, assign, nonatomic) TCHClientConnectionState connectionState;
        [Export("connectionState", ArgumentSemantic.Assign)]
        ClientConnectionState ConnectionState { get; }

        // @property (readonly, assign, nonatomic) TCHClientSynchronizationStatus synchronizationStatus;
        [Export("synchronizationStatus", ArgumentSemantic.Assign)]
        ClientSynchronizationStatus SynchronizationStatus { get; }

        // +(TCHLogLevel)logLevel;
        // +(void)setLogLevel:(TCHLogLevel)logLevel;
        [Static]
        [Export("logLevel")]
        LogLevel LogLevel { get; set; }

        // +(void)chatClientWithToken:(NSString * _Nonnull)token properties:(TwilioChatClientProperties * _Nullable)properties delegate:(id<TwilioChatClientDelegate> _Nullable)delegate completion:(TCHTwilioClientCompletion _Nonnull)completion;
        [Static]
        [Export("chatClientWithToken:properties:delegate:completion:")]
        void ChatClientWithToken(string token, [NullAllowed] TwilioChatClientProperties properties, [NullAllowed] TwilioChatClientDelegate @delegate, TwilioClientCompletion completion);

        // +(NSString * _Nonnull)sdkName;
        [Static]
        [Export("sdkName")]
        string SdkName { get; }

        // +(NSString * _Nonnull)sdkVersion;
        [Static]
        [Export("sdkVersion")]
        string SdkVersion { get; }

        // -(void)updateToken:(NSString * _Nonnull)token completion:(TCHCompletion _Nullable)completion;
        [Export("updateToken:completion:")]
        void UpdateToken(string token, [NullAllowed] Completion completion);

        // -(TCHChannels * _Nullable)channelsList;
        [NullAllowed, Export("channelsList")]
        Channels ChannelsList { get; }

        // -(TCHUsers * _Nullable)users;
        [NullAllowed, Export("users")]
        Users Users { get; }

        // -(void)registerWithNotificationToken:(NSData * _Nonnull)token completion:(TCHCompletion _Nullable)completion;
        [Export("registerWithNotificationToken:completion:")]
        void RegisterWithNotificationToken(NSData token, [NullAllowed] Completion completion);

        // -(void)deregisterWithNotificationToken:(NSData * _Nonnull)token completion:(TCHCompletion _Nullable)completion;
        [Export("deregisterWithNotificationToken:completion:")]
        void DeregisterWithNotificationToken(NSData token, [NullAllowed] Completion completion);

        // -(void)handleNotification:(NSDictionary * _Nonnull)notification completion:(TCHCompletion _Nullable)completion;
        [Export("handleNotification:completion:")]
        void HandleNotification(NSDictionary notification, [NullAllowed] Completion completion);

        // -(BOOL)isReachabilityEnabled;
        [Export("isReachabilityEnabled")]
        bool IsReachabilityEnabled { get; }

        // -(void)shutdown;
        [Export("shutdown")]
        void Shutdown();
    }

    // @interface TwilioChatClientProperties : NSObject
    [BaseType(typeof(NSObject))]
    interface TwilioChatClientProperties
    {
        // @property (copy, nonatomic) NSString * _Nonnull region;
        [Export("region")]
        string Region { get; set; }

        // @property (assign, nonatomic) NSInteger commandTimeout;
        [Export("commandTimeout")]
        ulong CommandTimeout { get; set; }
    }

    // @protocol TwilioChatClientDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface TwilioChatClientDelegate
    {
        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client connectionStateUpdated:(TCHClientConnectionState)state;
        [Export("chatClient:connectionStateUpdated:")]
        void ConnectionStateUpdated(TwilioChatClient client, ClientConnectionState state);

        // @optional -(void)chatClientTokenExpired:(TwilioChatClient * _Nonnull)client;
        [Export("chatClientTokenExpired:")]
        void TokenExpired(TwilioChatClient client);

        // @optional -(void)chatClientTokenWillExpire:(TwilioChatClient * _Nonnull)client;
        [Export("chatClientTokenWillExpire:")]
        void TokenWillExpire(TwilioChatClient client);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client synchronizationStatusUpdated:(TCHClientSynchronizationStatus)status;
        [Export("chatClient:synchronizationStatusUpdated:")]
        void SynchronizationStatusUpdated(TwilioChatClient client, ClientSynchronizationStatus status);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channelAdded:(TCHChannel * _Nonnull)channel;
        [Export("chatClient:channelAdded:")]
        void ChannelAdded(TwilioChatClient client, Channel channel);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channel:(TCHChannel * _Nonnull)channel updated:(TCHChannelUpdate)updated;
        [Export("chatClient:channel:updated:")]
        void ChannelUpdated(TwilioChatClient client, Channel channel, ChannelUpdate updated);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channel:(TCHChannel * _Nonnull)channel synchronizationStatusUpdated:(TCHChannelSynchronizationStatus)status;
        [Export("chatClient:channel:synchronizationStatusUpdated:")]
        void ChannelSynchronizationStatusUpdated(TwilioChatClient client, Channel channel, ChannelSynchronizationStatus status);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channelDeleted:(TCHChannel * _Nonnull)channel;
        [Export("chatClient:channelDeleted:")]
        void ChannelDeleted(TwilioChatClient client, Channel channel);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channel:(TCHChannel * _Nonnull)channel memberJoined:(TCHMember * _Nonnull)member;
        [Export("chatClient:channel:memberJoined:")]
        void MemberJoined(TwilioChatClient client, Channel channel, Member member);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channel:(TCHChannel * _Nonnull)channel member:(TCHMember * _Nonnull)member updated:(TCHMemberUpdate)updated;
        [Export("chatClient:channel:member:updated:")]
        void MemberUpdated(TwilioChatClient client, Channel channel, Member member, MemberUpdate updated);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channel:(TCHChannel * _Nonnull)channel memberLeft:(TCHMember * _Nonnull)member;
        [Export("chatClient:channel:memberLeft:")]
        void MemberLeft(TwilioChatClient client, Channel channel, Member member);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channel:(TCHChannel * _Nonnull)channel messageAdded:(TCHMessage * _Nonnull)message;
        [Export("chatClient:channel:messageAdded:")]
        void MessageAdded(TwilioChatClient client, Channel channel, Message message);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channel:(TCHChannel * _Nonnull)channel message:(TCHMessage * _Nonnull)message updated:(TCHMessageUpdate)updated;
        [Export("chatClient:channel:message:updated:")]
        void MessageUpdated(TwilioChatClient client, Channel channel, Message message, MessageUpdate updated);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client channel:(TCHChannel * _Nonnull)channel messageDeleted:(TCHMessage * _Nonnull)message;
        [Export("chatClient:channel:messageDeleted:")]
        void MessageDeleted(TwilioChatClient client, Channel channel, Message message);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client errorReceived:(TCHError * _Nonnull)error;
        [Export("chatClient:errorReceived:")]
        void ErrorReceived(TwilioChatClient client, Error error);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client typingStartedOnChannel:(TCHChannel * _Nonnull)channel member:(TCHMember * _Nonnull)member;
        [Export("chatClient:typingStartedOnChannel:member:")]
        void TypingStartedOnChannel(TwilioChatClient client, Channel channel, Member member);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client typingEndedOnChannel:(TCHChannel * _Nonnull)channel member:(TCHMember * _Nonnull)member;
        [Export("chatClient:typingEndedOnChannel:member:")]
        void TypingEndedOnChannel(TwilioChatClient client, Channel channel, Member member);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client notificationNewMessageReceivedForChannelSid:(NSString * _Nonnull)channelSid messageIndex:(NSUInteger)messageIndex;
        [Export("chatClient:notificationNewMessageReceivedForChannelSid:messageIndex:")]
        void NotificationNewMessageReceivedForChannelSid(TwilioChatClient client, string channelSid, nuint messageIndex);

    	// @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client notificationAddedToChannelWithSid:(NSString * _Nonnull)channelSid;
    	[Export ("chatClient:notificationAddedToChannelWithSid:")]
    	void NotificationAddedToChannelWithSid (TwilioChatClient client, string channelSid);

    	// @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client notificationInvitedToChannelWithSid:(NSString * _Nonnull)channelSid;
    	[Export ("chatClient:notificationInvitedToChannelWithSid:")]
    	void NotificationInvitedToChannelWithSid (TwilioChatClient client, string channelSid);

    	// @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client notificationRemovedFromChannelWithSid:(NSString * _Nonnull)channelSid;
    	[Export ("chatClient:notificationRemovedFromChannelWithSid:")]
    	void NotificationRemovedFromChannelWithSid (TwilioChatClient client, string channelSid);
        
        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client notificationUpdatedBadgeCount:(NSUInteger)badgeCount;
        [Export("chatClient:notificationUpdatedBadgeCount:")]
        void NotificationUpdatedBadgeCount(TwilioChatClient client, nuint badgeCount);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client user:(TCHUser * _Nonnull)user updated:(TCHUserUpdate)updated;
        [Export("chatClient:user:updated:")]
        void UserUpdated(TwilioChatClient client, User user, UserUpdate updated);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client userSubscribed:(TCHUser * _Nonnull)user;
        [Export("chatClient:userSubscribed:")]
        void UserSubscribed(TwilioChatClient client, User user);

        // @optional -(void)chatClient:(TwilioChatClient * _Nonnull)client userUnsubscribed:(TCHUser * _Nonnull)user;
        [Export("chatClient:userUnsubscribed:")]
        void UserUnsubscribed(TwilioChatClient client, User user);
    }

}
