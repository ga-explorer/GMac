//////////////////////////////////////////////////////////////////////////////////////
//
//   .NET/Link source code (c) 2003, Wolfram Research, Inc. All rights reserved.
//
//   Use is governed by the terms of the .NET/Link license agreement.
//
//   Author: Todd Gayley
//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Reflection;

namespace Wolfram.NETLink.Internal {

/// <summary>
/// Utility methods used by the Mathematica functions AddEventHandler and RemoveEventHandler.
/// </summary>
///
internal class EventHelper {    
    

    internal static string getDelegateTypeName(object eventsObject, string aqTypeName, string evtName) {

        EventInfo ei = getEventInfo(eventsObject, aqTypeName, evtName);
        // ei will not be null, or an exception would have been thrown.
        return ei.EventHandlerType.AssemblyQualifiedName;
    }

    internal static Delegate addHandler(object eventsObject, string aqTypeName, string evtName, Delegate dlg) {

        EventInfo ei = getEventInfo(eventsObject, aqTypeName, evtName);
        // ei will not be null, or an exception would have been thrown.
        ei.AddEventHandler(eventsObject, dlg);
        return dlg;
    }

    internal static void removeHandler(object eventsObject, string aqTypeName, string evtName, Delegate dlg) {

        EventInfo ei = getEventInfo(eventsObject, aqTypeName, evtName);
        // ei will not be null, or an exception would have been thrown.
        ei.RemoveEventHandler(eventsObject, dlg);
    }

    private static EventInfo getEventInfo(object eventsObject, string aqTypeName, string evtName) {

        EventInfo ei = null;
        // For a static event, use aqTypeName, for instance use eventsObject.
        if (eventsObject != null) {
            Type t = eventsObject.GetType();
            EventInfo[] eis = t.GetEvents(BindingFlags.Public | BindingFlags.Instance);
            foreach (EventInfo e in eis) {
                if (Utils.memberNamesMatch(e.Name, evtName)) {
                    ei = e;
                    break;
                }
            }
            if (ei == null)
                throw new ArgumentException("No public instance event named " + evtName + " exists for the given object.");
        } else {
            Type t = TypeLoader.GetType(aqTypeName, true);
            EventInfo[] eis = t.GetEvents(BindingFlags.Public | BindingFlags.Static);
            foreach (EventInfo e in eis) {
                if (Utils.memberNamesMatch(e.Name, evtName)) {
                    ei = e;
                    break;
                }
            }
            if (ei == null)
                throw new ArgumentException("No public static event named " + evtName + " exists for the type " + aqTypeName.Split(',')[0] + ".");
        }
        return ei;
    }
}

}
