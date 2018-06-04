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

namespace Wolfram.NETLink.Internal {

/// <summary>
/// The native implementation of ILoopbackLink.
/// </summary>
/// 
internal class NativeLoopbackLink : NativeLink, ILoopbackLink {

    public NativeLoopbackLink() {
    
        int err;
        lock (envLock) {
            link = api.extMLLoopbackOpen(env, out err);
        }
        if (link == IntPtr.Zero)
            throw new MathLinkException(MathLinkException.MLE_CREATION_FAILED, api.extMLErrorString(env, err));
    }
    
}

}