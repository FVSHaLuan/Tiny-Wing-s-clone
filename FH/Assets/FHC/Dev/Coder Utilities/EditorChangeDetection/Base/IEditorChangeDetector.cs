using UnityEngine;
using System.Collections;

namespace FH.DevTool
{
    public interface IEditorChangeDetector
    {
        void AddListenerWithDulicationCheck(ChangeDetectedEventHandler listener);
        /// <summary>
        /// Save current state and start detecting changes
        /// </summary>
        void Reset();
    }

    public delegate void ChangeDetectedEventHandler(object sender);
}