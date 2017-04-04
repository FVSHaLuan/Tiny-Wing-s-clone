using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture
{
    public interface IMessage
    {
        object Sender { get; }
        string Subject { get; }
        object Content { get; }
    }

    public interface IMessageCreator : IMessage
    {
        new object Sender { get; set; }
        new string Subject { get; set; }
        new object Content { get; set; }
    }

}