using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.RateLetterInterface
{
    /// <summary>
    /// Maintain offline messages for users
    /// </summary>
    public class OfflineMessageManager
    {
        private const int MaxQueueLength = 100;
        private static OfflineMessageManager _instance;
        private readonly Dictionary<string, Queue<MessageItem>> _msgDictionary; 

        private OfflineMessageManager()
        {
            _msgDictionary = new Dictionary<string, Queue<MessageItem>>();
            // Todo: Load unread message from database
        }

        public static OfflineMessageManager Instance
        {
            get
            {
                return _instance ?? (_instance = new OfflineMessageManager());
            }
        }

        public void StoreMessage(string toUser, string fromUser, string group, string messageContent)
        {
            var messageItem = new MessageItem
            {
                IsRead = false,
                MessageContent = messageContent,
                MessageGroup = group,
                MessageId = Guid.NewGuid().ToString().ToUpper(),
                FromUser = fromUser,
                TimeStamp = DateTime.Now
            };

            // Todo: Save message to database

            if (_msgDictionary.ContainsKey(toUser))
            {
                var messageQueue = _msgDictionary[toUser];
                messageQueue.Enqueue(messageItem);
                if (messageQueue.Count > MaxQueueLength)
                {
                    messageQueue.Dequeue();
                }
            }
            else
            {
                var queue = new Queue<MessageItem>(MaxQueueLength + 10);
                queue.Enqueue(messageItem);
                _msgDictionary.Add(toUser, queue);
            }

        }

        public List<MessageItem> ReadMessages(string userId)
        {
            List<MessageItem> ret;
            if (_msgDictionary.ContainsKey(userId))
            {
                if (_msgDictionary[userId].Count < MaxQueueLength)
                {
                    ret = _msgDictionary[userId].ToList();
                }
                else
                {
                    // Todo: Load offline message from database
                    ret = _msgDictionary[userId].ToList();
                }
                // Todo: Mark message IsRead in database
                _msgDictionary[userId].Clear();
            }
            else
            {
                ret = new List<MessageItem>();
            }

            return ret;
        }
    }

    /// <summary>
    /// 消息描述项
    /// </summary>
    public class MessageItem
    {
        public string MessageId;
        public string MessageContent;
        public string FromUser;
        public string MessageGroup;
        public DateTime TimeStamp;
        public bool IsRead;
    }

}