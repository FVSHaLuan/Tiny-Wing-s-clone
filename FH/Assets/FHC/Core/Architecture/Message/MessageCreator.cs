using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Architecture
{
    public class MessageCreator : IMessageCreator
    {
        object content;
        object sender;
        string subject;

        #region IMessageCreator
        public object Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
            }
        }

        public object Sender
        {
            get
            {
                return sender;
            }

            set
            {
                sender = value;
            }
        }

        public string Subject
        {
            get
            {
                return subject;
            }

            set
            {
                subject = value;
            }
        }
        #endregion

    }

}